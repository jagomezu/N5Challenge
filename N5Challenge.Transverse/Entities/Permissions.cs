using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5Challenge.Transverse.Entities
{
    [Table("Permissions")]
    public class Permissions
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("EmployeeForename")]
        public string EmployeeForename { get; set; }

        [Column("EmployeeSurname")]
        public string EmployeeSurname { get; set; }

        [Column("PermissionType")]
        public int PermissionTypeId { get; set; }

        [Column("PermissionDate")]
        public DateTime PermissionDate { get; set; }
    }
}
