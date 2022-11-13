using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ServiceLibrary.ServiceContracts;
using System.ServiceModel.Channels;
using System.Xml.Serialization;
using ServiceLibrary.DataSerialization;
using ServiceLibrary.DataTransfer;

namespace ServiceLibrary.ServiceTypes
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProjectDispatcher : ProfileController, ServiceContracts.IProjectDispatcher
    {
        protected System.String ProfileProjectsPath { get; set; } = default;
        protected ServiceEncoder.IServiceDataEncoder<ProjectInfo> ProjectDataEncoder { get; set; } = default;
        protected ServiceContracts.IProjectTransfer ProjectTransmitter { get; set; } = default;

        protected virtual System.String ProjectFileExtension { get => "graphproj"; }

        public ProjectDispatcher() : base()
        {
            this.ProjectDataEncoder = new ServiceEncoder.ServiceDataEncoder();
            this.ProjectTransmitter = new DataTransfer.DataTransferController(
                new SmtpTransfer());
        }

        public bool SetProjectsDirectory(System.Guid project_id)
        {
            var directory_path = this.ReadProfile(project_id)?.ProjectsPath;
            var project_path = $"{directory_path}\\{ProfileController.ProjectFilename}";

            if (!File.Exists(project_path)) { File.Create(project_path).Close(); return false; }
            this.ProfileProjectsPath = directory_path; return true;
        }

        private void ThrowExceptionIfPathNull(string message)
        {
            if(this.ProfileProjectsPath == null)
            { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException(message)); }
        }

        private void ThrowExceptionIfProjectInfoNull(ServiceContracts.ProjectInfo project_info)
        {
            foreach (var item in project_info.GetType().GetProperties())
            {
                if (item.GetValue(project_info) == null)
                {
                    var exception_instance = new ProjectDispatcherException($"Пустое значение свойства: {item.Name}");
                    throw new FaultException<ProjectDispatcherException>(exception_instance);
                }
            }
        }

        public void CreateProject(ServiceContracts.ProjectInfo project_info)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            this.ThrowExceptionIfProjectInfoNull(project_info);

            foreach (ServiceContracts.ProjectInfo project_data in this.GetProjectsInfo())
            {
                if (project_data.Equals(project_info))
                { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Проект уже создан")); }
            }
            using (var writer = File.AppendText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectFilename}"))
            { writer.WriteLine(this.ProjectDataEncoder.EncodeData(project_info)); }
        }

        public void UpdateProject(string project_name, ServiceContracts.ProjectInfo project_info)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            this.ThrowExceptionIfProjectInfoNull(project_info);

            ServiceContracts.ProjectInfo found_project = default;
            foreach (ServiceContracts.ProjectInfo project_data in this.GetProjectsInfo())
            {
                if (project_data.ProjectName == project_name) { found_project = project_data; }
            }
            if (found_project == null)
            { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Проект не найден")); }

            if (File.Exists($"{this.ProfileProjectsPath}\\{found_project.FileName}.{this.ProjectFileExtension}"))
            {
                string GetFullFilename(string filename)
                    => $"{this.ProfileProjectsPath}\\{filename}.{this.ProjectFileExtension}";

                File.Move(GetFullFilename(found_project.FileName), GetFullFilename(project_info.FileName));
            }
            this.DeleteProject(found_project.ProjectName, found_project.FileName != project_info.FileName);

            using (var writer = File.AppendText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectFilename}"))
            { writer.WriteLine(this.ProjectDataEncoder.EncodeData(project_info)); }
        }

        public void DeleteProject(string project_name, bool delete_file = true)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            var projects_list = this.GetProjectsInfo();

            using (var writer = File.CreateText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectFilename}"))
            {
                foreach (ServiceContracts.ProjectInfo data in projects_list)
                {
                    if (data.ProjectName != project_name) writer.WriteLine(this.ProjectDataEncoder.EncodeData(data));
                    else if (delete_file != default(bool)) 
                    { File.Delete($"{this.ProfileProjectsPath}\\{data.FileName}.{this.ProjectFileExtension}"); }
                }
            }
        }

        public ServiceContracts.ProjectInfo[] GetProjectsInfo()
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");

            var projects_list = new List<ServiceContracts.ProjectInfo>();
            using (var reader = File.OpenText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectFilename}"))
            {
                var reader_line = string.Empty;
                while ((reader_line = reader.ReadLine()) != null)
                {
                    projects_list.Add(this.ProjectDataEncoder.DecodeData(reader_line));
                }
            }
            return projects_list.ToArray();
        }

        public void PutProjectData(string project_name, NodeData[] nodes_field)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            var project_info = this.GetProjectsInfo().Where((ProjectInfo info) => info.ProjectName == project_name).ToArray();

            if(project_info.Length <= 0) 
            { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Проект не найден")); }

            var xml_formatter = new XmlSerializer(typeof(DataSerialization.NodesCollection));
            using (var writer = File.Create($"{this.ProfileProjectsPath}\\{project_info[0].FileName}.{this.ProjectFileExtension}"))
            {
                xml_formatter.Serialize(writer, new NodesCollection(nodes_field.ToList()));
            }
        }

        public NodeData[] TakeProjectData(string project_name)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            var project_info = this.GetProjectsInfo().Where((ProjectInfo info) => info.ProjectName == project_name).ToArray();

            if (project_info.Length <= 0) return null;
            var project_path = $"{this.ProfileProjectsPath}\\{project_info[0].FileName}.{this.ProjectFileExtension}";

            if (!File.Exists(project_path))
            {
                this.DeleteProject(project_name);
                throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Файл проекта не найден"));
            }

            var xml_formatter = new XmlSerializer(typeof(DataSerialization.NodesCollection));
            DataSerialization.NodesCollection nodes_collection = default;

            using (var reader = File.OpenRead(project_path))
            { 
                nodes_collection = xml_formatter.Deserialize(reader) as NodesCollection; 
            }
            return nodes_collection.NodesData.ToArray();
        }
        // (export_entity - название_проекта, transfer_data - {ид_профиля + пароль, эл_почта})
        public void ExportProject(string export_entity, TransferData transfer_data)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");

            var project_filename = default(string);
            foreach (var project in this.GetProjectsInfo())
            {
                if (project.ProjectName == export_entity) project_filename = project.FileName;
            }
            if (project_filename == null) 
            { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Проект не найден")); }

            var profile_info = this.ReadProfile(Guid.Parse(transfer_data.FromPath));
            var transfer_updated = new TransferData() 
            { 
                FromPath = $"{profile_info.EmailName};{profile_info.EmailKey}", ToPath = transfer_data.ToPath
            };
            var entity_path = $"{this.ProfileProjectsPath}\\{project_filename}.{this.ProjectFileExtension}";

            // (export_entity - путь к файлу, transfer_data - {эл_почта откуда, эл_почта куда})
            try { this.ProjectTransmitter.ExportProject(entity_path, transfer_updated); }
            catch (System.Exception error)
            {
                var exception_instance = new ProjectDispatcherException(error.Message);
                throw new FaultException<ProjectDispatcherException>(exception_instance);
            }
        }

        public void ImportProject(string export_entity, TransferData transfer_data)
        {
            
        }
        public bool PathVerification(TransferData transfer_data)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ProjectInfo> GetEnumerator()
        { foreach (var project in this.GetProjectsInfo()) yield return project; }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    }
}
