using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ServiceLibrary.ServiceContracts;
using ServiceLibrary.ServiceEncoder;
using System.ServiceModel.Channels;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using ServiceLibrary.DataSerialization;

namespace ServiceLibrary.ServiceTypes
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProjectDispatcher : ProfileController, ServiceContracts.IProjectDispatcher
    {
        private System.String ProfileProjectsPath { get; set; } = default;
        private IServiceDataEncoder<ProjectInfo> ProjectDataEncoder { get; set; } = default;

        private const System.String ProjectFileExtension = "graphproj";

        public ProjectDispatcher() : base()
        {
            this.ProjectDataEncoder = new ServiceDataEncoder();
        }

        public bool SetProjectsDirectory(string directory_path)
        {
            if (!File.Exists($"{directory_path}\\{ProfileController.ProjectExtention}")) return false;
            this.ProfileProjectsPath = directory_path; return true;
        }

        private void ThrowExceptionIfPathNull(string message)
        {
            if(this.ProfileProjectsPath == null)
            {
                var exception_instance = new ProjectDispatcherException(message);
                throw new FaultException<ProjectDispatcherException>(exception_instance); 
            }
        }

        public void CreateProject(ProjectInfo project_info)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");

            foreach (ServiceContracts.ProjectInfo project_data in this.GetProjectsInfo())
            {
                if (project_data.Equals(project_info))
                { throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Проект уже создан")); }
            }

            using (var writer = File.AppendText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectExtention}"))
            { writer.WriteLine(this.ProjectDataEncoder.EncodeData(project_info)); }
        }

        public void DeleteProject(string project_name)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            var projects_list = this.GetProjectsInfo();

            using (var writer = File.CreateText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectExtention}"))
            {
                foreach (var data in projects_list)
                {
                    if (data.ProjectName != project_name) writer.WriteLine(this.ProjectDataEncoder.EncodeData(data));
                    else File.Delete($"{this.ProfileProjectsPath}\\{data.FileName}.{ProjectDispatcher.ProjectFileExtension}");
                }
            }
        }

        public ServiceContracts.ProjectInfo[] GetProjectsInfo()
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");

            var projects_list = new List<ServiceContracts.ProjectInfo>();
            using (var reader = File.OpenText($"{this.ProfileProjectsPath}\\{ProfileController.ProjectExtention}"))
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
            using (var writer = File.OpenWrite($"{this.ProfileProjectsPath}\\{project_info[0].FileName}.{ProjectFileExtension}"))
            {
                xml_formatter.Serialize(writer, new NodesCollection(nodes_field.ToList()));
            }
        }

        public NodeData[] TakeProjectData(string project_name)
        {
            this.ThrowExceptionIfPathNull("Путь к каталогу с проектами не установлен");
            var project_info = this.GetProjectsInfo().Where((ProjectInfo info) => info.ProjectName == project_name).ToArray();

            if (project_info.Length <= 0) return null;
            var project_path = $"{this.ProfileProjectsPath}\\{project_info[0].FileName}.{ProjectFileExtension}";

            if (!File.Exists(project_path))
            { 
                throw new FaultException<ProjectDispatcherException>(new ProjectDispatcherException("Проект не найден")); 
            }

            var xml_formatter = new XmlSerializer(typeof(DataSerialization.NodesCollection));
            DataSerialization.NodesCollection nodes_collection = default;

            using (var reader = File.OpenRead(project_path))
            { 
                nodes_collection = xml_formatter.Deserialize(reader) as NodesCollection; 
            }
            return nodes_collection.NodesData.ToArray();
        }

        public void ExportProject(string project_name)
        {
            throw new NotImplementedException();
        }

        public void ImportProject(string project_name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ProjectInfo> GetEnumerator()
        { foreach (var project in this.GetProjectsInfo()) yield return project; }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        
    }
}
