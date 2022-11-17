using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ServiceLibrary.DataTransfer;
using ServiceLibrary.ServiceContracts;

namespace ServiceLibrary.ServiceTypes
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProfileController : ServiceContracts.IProfileController
    {
        public static System.String ProjectFilename { get => ".projects"; }

        protected INetworkTransfer<SmptMessageEnvelope> NetforkTransfer { get; private set; } = default;
        protected virtual System.String FileName { get => ".profiles"; }
        
        public ProfileController() : base() { this.NetforkTransfer = new SmtpTransfer(); }

        private void SetProfileDocumentIfNotFound(string directory)
        {
            if (!File.Exists($"{directory}\\{this.FileName}"))
            {
                using (var writer = new StreamWriter(File.Create($"{directory}\\{this.FileName}")))
                { writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><profiles></profiles>"); }
            }
        }

        protected static XmlDocument GetProfileDocument(string filepath)
        {
            var exception_instance = new ProfileControllerException("Файл профилей не найден");
            if (!File.Exists(filepath))
            {
                throw new FaultException<ProfileControllerException>(exception_instance);
            }
            var profile_document = new XmlDocument();
            using (var file_reader = File.OpenRead(filepath))
            {
                var xml_reader = XmlReader.Create(file_reader, new XmlReaderSettings() { IgnoreWhitespace = true });

                try { profile_document.Load(xml_reader); } 
                catch { new FaultException<ProfileControllerException>(exception_instance); } 
            }
            return profile_document;
        }

        protected static void CommitProfileDocument(XmlDocument xml_document, string filepath)
        {
            using (var writer = File.Open(filepath, FileMode.Create)) { xml_document.Save(XmlWriter.Create(writer)); }
        }

        private void ThrowExceptionIfPathNotEmpty(System.String profile_path)
        {
            var filesystem_entries = default(string[]);

            try { filesystem_entries = Directory.GetFileSystemEntries(profile_path); }
            catch (System.Exception error) { throw new FaultException(error.Message); }

            if (filesystem_entries.Length != 0)
            {
                var error = new ProfileControllerException("Каталог не является пустым", ProfileControllerAction.Registration);
                throw new FaultException<ProfileControllerException>(error);
            }
        }

        private void ThrowExceptionIfEmailBad(string email_name, string email_key)
        {
            var emailcheck_message = new DataTransfer.SmptMessageEnvelope()
            {
                SendingEmail = email_name, EmailCredentials = email_key
            };
            if (!this.NetforkTransfer.CheckChannel(emailcheck_message))
            {
                var exception_instance = new ProfileControllerException("Email не найден");
                throw new FaultException<ProfileControllerException>(exception_instance);
            }
        }

        public string[] GetProfilesName()
        {
            var profile_path = $"{Directory.GetCurrentDirectory()}\\{this.FileName}";
            var result_list = new List<string>();

            XmlDocument xml_document = default;
            try { xml_document = ProfileController.GetProfileDocument(profile_path); } catch { return new string[] { }; }

            foreach (XmlNode profile_node in xml_document.DocumentElement.ChildNodes)
            {
                if(profile_node.Attributes["name"] != null) result_list.Add(profile_node.Attributes["name"].Value);
            }
            return result_list.ToArray();
        }

        public System.Guid? Authorization(string username, string password)
        {
            var profile_path = $"{Directory.GetCurrentDirectory()}\\{this.FileName}";

            XmlDocument xml_document = default;
            try { xml_document = ProfileController.GetProfileDocument(profile_path); } catch { return null; }

            XmlNode searching_node = default;
            foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
            {
                if (node.Attributes["name"] != null && node.Attributes["id"] != null 
                    && node.Attributes["name"].Value == username) { searching_node = node; break; }
            }
            if (searching_node == null) return null;

            for(int index = 0; index < searching_node.ChildNodes.Count; index++)
            {
                var profile_data = searching_node.ChildNodes[index];

                if (profile_data.Name == "password" && profile_data.Attributes["value"].Value == password 
                    && profile_data.Attributes["value"] != null)
                {
                    if (Guid.TryParse(searching_node.Attributes["id"].Value, out var guid_result))
                    { return guid_result; } else break;
                }
            }
            return null;
        }

        public System.Guid Registration(ServiceContracts.ProfileData profile_data)
        {
            foreach (var property in profile_data.GetType().GetProperties())
            {
                if (property.GetValue(profile_data) == null)
                { throw new FaultException<ProfileControllerException>(new ProfileControllerException("Данные пустые")); }
            }
            this.ThrowExceptionIfPathNotEmpty(profile_data.ProjectsPath);
            this.ThrowExceptionIfEmailBad(profile_data.EmailName, profile_data.EmailKey);

            this.SetProfileDocumentIfNotFound(Directory.GetCurrentDirectory());

            var xml_document = ProfileController.GetProfileDocument($"{Directory.GetCurrentDirectory()}\\{FileName}");
            foreach (XmlNode profile in xml_document.DocumentElement.ChildNodes)
            {
                if (profile.Attributes["name"] != null && profile.Attributes["name"].Value == profile_data.UserName)
                { throw new FaultException<ProfileControllerException>(new ProfileControllerException("Профиль уже создан")); }
            }
            var profile_id = Guid.NewGuid();
            var xml_profile = xml_document.CreateElement("profile");
            
            xml_profile.SetAttribute("name", profile_data.UserName);
            xml_profile.SetAttribute("id", profile_id.ToString());

            foreach (var property in profile_data.GetType().GetProperties())
            {
                var property_attribute = property.GetCustomAttribute<ProfilePropertyAttribute>();
                if (property_attribute != null)
                {
                    var profile_property = xml_document.CreateElement(property_attribute.PropertyName);
                    profile_property.SetAttribute("value", property.GetValue(profile_data).ToString());
                    xml_profile.AppendChild(profile_property);
                }
            }
            xml_document.DocumentElement.AppendChild(xml_profile);
            using (var projects_file = File.Create($"{profile_data.ProjectsPath}\\{ProjectFilename}")) { }

            ProfileController.CommitProfileDocument(xml_document, $"{Directory.GetCurrentDirectory()}\\{FileName}");
            return profile_id;
        }

        public void DeleteProfile(System.Guid userid)
        {
            var xml_document = ProfileController.GetProfileDocument($"{Directory.GetCurrentDirectory()}\\{FileName}");

            XmlNode searching_node = default;
            foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
            {
                if (node.Attributes["id"] != null && node.Attributes["id"].Value == userid.ToString())
                { searching_node = node; break; }
            }
            if(searching_node != null)
            {
                xml_document.DocumentElement.RemoveChild(searching_node);
                ProfileController.CommitProfileDocument(xml_document, $"{Directory.GetCurrentDirectory()}\\{FileName}");
            }
        }

        public void SetupProfile(System.Guid userid, ServiceContracts.ProfileData profile_data)
        {
            var exception_obj = new ProfileControllerException("Профиль не найден", ProfileControllerAction.Setup);

            var previous_profiledata = this.ReadProfile(userid);
            if (previous_profiledata == null) { throw new FaultException<ProfileControllerException>(exception_obj); }

            var xml_document = ProfileController.GetProfileDocument($"{Directory.GetCurrentDirectory()}\\{FileName}");
            XmlNode searching_node = default;

            foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
            {
                if (node.Attributes["id"] != null && node.Attributes["name"] != null 
                    && node.Attributes["id"].Value == userid.ToString()) { searching_node = node; break; }
            }
            if (searching_node == null) throw new FaultException<ProfileControllerException>(exception_obj);

            if (profile_data.ProjectsPath != null && profile_data.ProjectsPath != previous_profiledata.ProjectsPath)
            {
                this.ThrowExceptionIfPathNotEmpty(profile_data.ProjectsPath);
                var project_dir = new DirectoryInfo(previous_profiledata.ProjectsPath);

                foreach (var file in project_dir.GetFiles()) { file.CopyTo($"{profile_data.ProjectsPath}\\{file.Name}", true); }
            }

            if (profile_data.UserName != null) searching_node.Attributes["name"].Value = profile_data.UserName;
            foreach (var profile_property in profile_data.GetType().GetProperties())
            {
                var property_attribute = profile_property.GetCustomAttribute<ProfilePropertyAttribute>();
                if (property_attribute == null) continue;

                var founded_node = searching_node.ChildNodes.OfType<XmlElement>()
                    .Where((XmlElement element) => property_attribute.PropertyName == element.Name).ToArray()[0];

                var property_instance = profile_property.GetValue(profile_data);
                if (founded_node == null)
                {
                    founded_node = xml_document.CreateElement(property_attribute.PropertyName);
                    founded_node.SetAttribute("value", property_instance.ToString());

                    searching_node.AppendChild(founded_node);
                }
                founded_node.SetAttribute("value", property_instance.ToString());
            }
            ProfileController.CommitProfileDocument(xml_document, $"{Directory.GetCurrentDirectory()}\\{FileName}");
        }

        public ServiceContracts.ProfileData ReadProfile(System.Guid userid)
        {
            var profile_path = $"{Directory.GetCurrentDirectory()}\\{this.FileName}";

            XmlDocument xml_document = default;
            try { xml_document = ProfileController.GetProfileDocument(profile_path); } catch { return null; }

            XmlNode searching_node = default;
            foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
            {
                if (node.Attributes["id"] != null && node.Attributes["name"] != null 
                    && node.Attributes["id"].Value == userid.ToString()) { searching_node = node; break; }
            }
            if (searching_node == null) return null;

            var result = new ServiceContracts.ProfileData() { UserName = searching_node.Attributes["name"].Value };
            foreach (var profile_property in result.GetType().GetProperties())
            {
                var property_attribute = profile_property.GetCustomAttribute<ProfilePropertyAttribute>();
                if (property_attribute == null) continue;

                foreach (XmlNode property_node in searching_node.ChildNodes)
                {
                    if (property_node.Name != property_attribute.PropertyName 
                        || property_node.Attributes["value"] == null) continue;

                    profile_property.SetValue(result, property_node.Attributes["value"].Value);
                }
            }
            return result;
        }
    }
}
