namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankAccount")]
    public partial class BankAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankAccount()
        {
            Customers = new HashSet<Customer>();
        }

        public int BankAccountId { get; set; }

        [Required]
        [StringLength(255)]
        public string AccountName { get; set; }

        [Required]
        [StringLength(40)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(40)]
        public string BankId { get; set; }

        public bool Deleted { get; set; }

        public virtual Bank Bank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
