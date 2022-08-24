namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        public int AdminId { get; set; }

        [Required]
        [StringLength(30)]
        public string AdminName { get; set; }

        [Required]
        [StringLength(255)]
        public string AdminPassword { get; set; }

        [Required]
        [StringLength(40)]
        public string RoleId { get; set; }

        [StringLength(255)]
        public string AdminNote { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }

        public virtual AdminRole AdminRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
