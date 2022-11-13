using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceLibrary.ServiceContracts
{
    [ServiceContractAttribute(Name = "ProfileController")]
    public interface IProfileController
    {
        [FaultContractAttribute(typeof(ProfileControllerException))]
        [OperationContractAttribute]
        System.Guid Registration(ServiceContracts.ProfileData profiledata);
        
        [OperationContractAttribute]
        System.Guid? Authorization(System.String username, System.String password);

        [OperationContractAttribute]
        System.String[] GetProfilesName();

        [FaultContractAttribute(typeof(ProfileControllerException))]
        [OperationContractAttribute]
        void SetupProfile(System.Guid userid, ServiceContracts.ProfileData profile_data);

        [OperationContractAttribute]
        ServiceContracts.ProfileData ReadProfile(System.Guid userid);

        [FaultContractAttribute(typeof(ProfileControllerException))]
        [OperationContractAttribute]
        void DeleteProfile(System.Guid userid);
    }

    [DataContractAttribute]
    public enum ProfileControllerAction : System.SByte 
    { 
        [EnumMemberAttribute] None, [EnumMemberAttribute] Authorization, [EnumMemberAttribute] Registration, 
        [EnumMemberAttribute] Setup,  [EnumMemberAttribute] Delete 
    }

    [DataContractAttribute, SerializableAttribute]
    public sealed class ProfileControllerException : System.Object
    {
        [DataMemberAttribute]
        public ServiceContracts.ProfileControllerAction Action { get; private set; } = default;

        [DataMemberAttribute]
        public System.String Message { get; private set; } = default;

        public ProfileControllerException(string message, ProfileControllerAction action): base() 
            => (this.Message, this.Action) = (message, action);

        public ProfileControllerException(string message) : this(message, ProfileControllerAction.None) { }
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class ProfilePropertyAttribute : System.Attribute
    {
        public System.String PropertyName { get; private set; } = default;
        public ProfilePropertyAttribute(string name) : base() => this.PropertyName = name;
    }

    [DataContractAttribute, SerializableAttribute]
    public sealed class ProfileData : System.Object
    {
        [DataMemberAttribute]
        public System.String UserName { get; set; } = default(string);

        [DataMemberAttribute, ProfilePropertyAttribute("email-name")]
        public System.String EmailName { get; set; } = default(string);

        [DataMemberAttribute, ProfilePropertyAttribute("email-key")]
        public System.String EmailKey { get; set; } = default(string);

        [DataMemberAttribute, ProfilePropertyAttribute("project-path")]
        public System.String ProjectsPath { get; set; } = default(string);

        [DataMemberAttribute, ProfilePropertyAttribute("password")]
        public System.String Password { get; set; } = default(string);

    }
}
