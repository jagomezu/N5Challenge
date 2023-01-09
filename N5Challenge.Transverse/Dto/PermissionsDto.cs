using System.Runtime.Serialization;

namespace N5Challenge.Transverse.Dto
{
    [DataContract]
    public class PermissionsDto
    {
        [DataMember(Name = "UniqueIdentifier")]
        public int Id { get; set; }

        [DataMember(Name = "EmployeeForname")]
        public string EmployeeForename { get; set; }

        [DataMember(Name = "EmployeeSurname")]
        public string EmployeeSurname { get; set; }

        [DataMember(Name = "PermissionTypeId")]
        public int PermissionTypeId { get; set; }

        [DataMember(Name = "PermissionDate")]
        public DateTime PermissionDate { get; set; }
    }
}
