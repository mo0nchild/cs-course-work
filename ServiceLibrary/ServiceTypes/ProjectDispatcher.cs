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
using ServiceLibrary.DataSerializations;
using ServiceLibrary.DataTransfer;
using System.Runtime.Serialization;

using TransferDataPackage.DataSerializations;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace ServiceLibrary.ServiceTypes
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProjectDispatcher : ProfileController, ServiceContracts.IProjectDispatcher
    {
        protected System.String ProfileProjectsPath { get; set; } = default;
        protected ServiceEncoder.IServiceDataEncoder<ProjectInfo> ProjectDataEncoder { get; set; } = default;

        protected virtual System.String ProjectFileExtension { get => "graphproj"; }
        protected virtual System.String ImportFileExtension { get => "json"; }

        public ProjectDispatcher() : base()
        {
            this.ProjectDataEncoder = new ServiceEncoder.ServiceDataEncoder();
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
                if (item.GetValue(project_info) != null) continue;
                
                var exception_instance = new ProjectDispatcherException($"Пустое значение свойства: {item.Name}");
                throw new FaultException<ProjectDispatcherException>(exception_instance);
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

            var xml_formatter = new DataContractSerializer(typeof(TransferDataPackage.DataSerializations.DataSerializationBase));
            using (var writer = File.Create($"{this.ProfileProjectsPath}\\{project_info[0].FileName}.{this.ProjectFileExtension}"))
            {
                var transfer_data = new DataSerializationAdapter(new NodesCollection(nodes_field.ToList()));
                xml_formatter.WriteObject(writer, transfer_data.StateInstaller());
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
            var xml_formatter = new DataContractSerializer(typeof(TransferDataPackage.DataSerializations.DataSerializationBase));
            DataSerializations.NodesCollection nodes_collection = default;

            using (var reader = File.OpenRead(project_path))
            {
                var data_deserialized = ((DataSerializationBase)xml_formatter.ReadObject(reader))
                    .StateExtraction<DataSerializationAdapter>();

                nodes_collection = data_deserialized.NodesList;
            }
            return nodes_collection.NodesData.ToArray();
        }

        // (export_entity - название_проекта, transfer_data - {ид_профиля, эл_почта})
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
            var entity_path = $"{this.ProfileProjectsPath}\\{project_filename}.{this.ProjectFileExtension}";
            try {
                NetforkTransfer.SendData(new SmptMessageEnvelope()
                {
                    SendingEmail = profile_info.EmailName, EmailCredentials = profile_info.EmailKey,
                    ReceivingEmail = transfer_data.ToPath, AttachmentObject = entity_path
                },
                transfer_message: "Проект был экспортирован на вашу почту, используйте его в приложении");
            }
            catch (System.Exception error)
            {
                var exception_instance = new ProjectDispatcherException(error.Message);
                throw new FaultException<ProjectDispatcherException>(exception_instance);
            }
        }

        // (import_entity - название_проекта, transfer_data - { путь к файлу, название файла })
        public void ImportProject(string import_entity, TransferData transfer_data)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");

            if (!File.Exists(transfer_data.FromPath))
            { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Файл импорта не найден")); }

            DataSerializations.NodesCollection node_data = default;
            var file_info = new FileInfo(transfer_data.FromPath);

            using (var reader = file_info.OpenRead())
            {
                var data_contact = typeof(TransferDataPackage.DataSerializations.DataSerializationBase);
                XmlObjectSerializer data_serializer = default;

                if ($".{this.ImportFileExtension}" == file_info.Extension) data_serializer = new DataContractJsonSerializer(data_contact);
                else if ($".{this.ProjectFileExtension}" == file_info.Extension) data_serializer = new DataContractSerializer(data_contact);
                else {
                    var exception_instance = new ProjectDispatcherException("Расширение не поддерживается");
                    throw new FaultException<ProjectDispatcherException>(exception_instance);
                }
                var data_serialization = ((DataSerializationBase)data_serializer.ReadObject(reader))
                    .StateExtraction<DataSerializationAdapter>();

                node_data = data_serialization.NodesList;
            }
           
            this.CreateProject(new ProjectInfo() { CreateTime = DateTime.Now, FileName = transfer_data.ToPath, 
                ProjectName = import_entity });

            this.PutProjectData(import_entity, node_data.ToArray());
        }

        public IEnumerator<ProjectInfo> GetEnumerator()
        { foreach (var project in this.GetProjectsInfo()) yield return project; }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    }
}
