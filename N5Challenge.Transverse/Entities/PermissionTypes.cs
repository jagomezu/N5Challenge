using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5Challenge.Transverse.Entities
{
    [Table("PermissionTypes")]
    public class PermissionTypes
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}
