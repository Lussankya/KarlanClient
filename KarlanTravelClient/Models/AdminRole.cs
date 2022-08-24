namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminRole")]
    public partial class AdminRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminRole()
        {
            Admins = new HashSet<Admin>();
        }

        [Key]
        [StringLength(40)]
        public string RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(255)]
        public string RoleNote { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
