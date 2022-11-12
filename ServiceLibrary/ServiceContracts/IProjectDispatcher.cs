﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceLibrary.ServiceContracts
{
    [ServiceContractAttribute(Name = "ProjectTransfer")]
    public interface IProjectTransfer
    {
        [OperationContractAttribute, FaultContractAttribute(typeof(ProjectDispatcherException))]
        void ExportProject(System.String project_name);

        [OperationContractAttribute, FaultContractAttribute(typeof(ProjectDispatcherException))]
        void ImportProject(System.String project_name);
    }

    [ServiceContractAttribute(Name = "ProjectDispatcher")]
    public interface IProjectDispatcher : IEnumerable<ServiceContracts.ProjectInfo>, IProjectTransfer
    {
        [FaultContractAttribute(typeof(ServiceContracts.ProjectDispatcherException))]
        [OperationContractAttribute]
        ServiceContracts.ProjectInfo[] GetProjectsInfo();

        [OperationContractAttribute]
        System.Boolean SetProjectsDirectory(System.String directory_path);

        [FaultContractAttribute(typeof(ServiceContracts.ProjectDispatcherException))]
        [OperationContractAttribute]
        ServiceContracts.NodeData[] TakeProjectData(System.String project_name);

        [FaultContractAttribute(typeof(ServiceContracts.ProjectDispatcherException))]
        [OperationContractAttribute]
        void PutProjectData(System.String project_name, ServiceContracts.NodeData[] nodes_field);

        [FaultContractAttribute(typeof(ServiceContracts.ProjectDispatcherException))]
        [OperationContractAttribute]
        void CreateProject(ServiceContracts.ProjectInfo project_info);

        [FaultContractAttribute(typeof(ServiceContracts.ProjectDispatcherException))]
        [OperationContractAttribute]
        void DeleteProject(System.String project_name);
    }

    [DataContractAttribute, SerializableAttribute]
    public sealed class ProjectDispatcherException : System.Object
    {
        [DataMemberAttribute]
        public System.String Message { get; private set; } = default(string);

        [DataMemberAttribute]
        public System.String ProjectName { get; private set; } = default(string);

        public ProjectDispatcherException(string message) : base() => this.Message = message;
    }

    [DataContractAttribute, SerializableAttribute]
    public sealed class ProjectInfo : System.Object
    {
        [DataMemberAttribute]
        public System.String ProjectName { get; set; } = default;

        [DataMemberAttribute]
        public System.String FileName { get; set; } = default;

        [DataMemberAttribute]
        public System.DateTime CreateTime { get; set; } = default;

        public override System.Boolean Equals(System.Object other_object)
        {
            if(other_object is ServiceContracts.ProjectInfo other_info)
            {
                return this.ProjectName == other_info.ProjectName 
                    && this.FileName == other_info.FileName;
            }
            return false;
        }
        public override System.Int32 GetHashCode() => base.GetHashCode();
    }
}
