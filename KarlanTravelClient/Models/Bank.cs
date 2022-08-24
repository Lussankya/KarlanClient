namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bank")]
    public partial class Bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        [StringLength(40)]
        public string BankId { get; set; }

        [Required]
        [StringLength(255)]
        public string BankName { get; set; }

        [Required]
        [StringLength(255)]
        public string SwiftCode { get; set; }

        [Required]
        [StringLength(40)]
        public string CountryId { get; set; }

        public bool Deleted { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
