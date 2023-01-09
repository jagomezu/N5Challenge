using System.Runtime.Serialization;

namespace N5Challenge.Transverse.Dto
{
    [DataContract]
    public class PermissionTypesDto
    {
        [DataMember(Name = "UniqueIdentifier")]
        public int Id { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }
    }
}
