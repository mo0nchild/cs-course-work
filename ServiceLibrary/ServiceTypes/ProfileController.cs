using ServiceLibrary.ServiceContracts;
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

namespace ServiceLibrary.ServiceTypes
{
    public class ProfileController : ServiceContracts.IProfileController
    {
        protected virtual System.String FileName { get; private set; } = ".profiles";

        public ProfileController() : base() { }

        protected static XmlDocument GetProfileDocument(string filepath)
        {
            var profile_document = new XmlDocument();

            using (var file_reader = File.OpenRead(filepath))
            {
                var xml_reader = XmlReader.Create(file_reader, new XmlReaderSettings() { IgnoreWhitespace = true });
                profile_document.Load(xml_reader); 
            }
            return profile_document;
        }
        protected static void CommitProfileDocument(XmlDocument xml_document, string filepath)
        {
            using (var file_writer = File.Open(filepath, FileMode.Create))
            { xml_document.Save(XmlWriter.Create(file_writer)); }
        }

        public System.Guid? Authorization(string username, string password)
        {
            var xml_document = ProfileController.GetProfileDocument($"{Directory.GetCurrentDirectory()}\\{FileName}");

            XmlNode searching_node = default;
            foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
            {
                if (node.Attributes["name"] != null && node.Attributes["name"].Value == username
                    && node.Attributes["id"] != null) { searching_node = node; break; }
            }
            if (searching_node == null) return null;

            for(int index = 0; index < searching_node.ChildNodes.Count; index++)
            {
                var profile_data = searching_node.ChildNodes[index];

                if (profile_data.Name == "password" && profile_data.Attributes["value"] != null 
                    && profile_data.Attributes["value"].Value == password)
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
            var filesystem_entries = default(string[]);

            try { filesystem_entries = Directory.GetFileSystemEntries(profile_data.ProjectsPath); }
            catch(System.Exception error) { throw new FaultException(error.Message); }

            if (filesystem_entries.Length != 0)
            { 
                var error = new ProfileControllerException("Каталог не является пустым", ProfileControllerAction.Registration);
                throw new FaultException<ProfileControllerException>(error);
            }

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

        public void SetupProfile(System.Guid userid, ServiceContracts.ProfileData profiledata)
        {
            var xml_document = ProfileController.GetProfileDocument($"{Directory.GetCurrentDirectory()}\\{FileName}");

            XmlNode searching_node = default;
            foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
            {
                if (node.Attributes["id"] != null && node.Attributes["name"] != null 
                    && node.Attributes["id"].Value == userid.ToString()) { searching_node = node; break; }
            }
            if (searching_node == null)
            {
                var error = new ProfileControllerException("Профиль не найден", ProfileControllerAction.Setup);
                throw new FaultException<ProfileControllerException>(error);
            }

            if (profiledata.ProjectsPath != null)
            {
                var project_directory = new DirectoryInfo(this.ReadProfile(userid).ProjectsPath);

                foreach (var file in project_directory.GetFiles())
                { file.CopyTo($"{profiledata.ProjectsPath}\\{file.Name}", true); }
            }

            if (profiledata.UserName != null) searching_node.Attributes["name"].Value = profiledata.UserName;
            foreach (XmlElement property_node in searching_node.ChildNodes)
            {
                foreach (var profile_property in profiledata.GetType().GetProperties())
                {
                    var property_attribute = profile_property.GetCustomAttribute<ProfilePropertyAttribute>();
                    if (property_attribute == null || property_attribute.PropertyName != property_node.Name) continue;

                    if(profile_property.GetValue(profiledata) != null)
                    { property_node.SetAttribute("value", profile_property.GetValue(profiledata).ToString()); }
                }
            }
            ProfileController.CommitProfileDocument(xml_document, $"{Directory.GetCurrentDirectory()}\\{FileName}");
        }

        public ServiceContracts.ProfileData ReadProfile(System.Guid userid)
        {
            var xml_document = ProfileController.GetProfileDocument($"{Directory.GetCurrentDirectory()}\\{FileName}");

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

                foreach (XmlElement property_node in searching_node.ChildNodes)
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
