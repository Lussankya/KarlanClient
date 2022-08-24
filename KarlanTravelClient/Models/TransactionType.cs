namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionType")]
    public partial class TransactionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionType()
        {
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        [StringLength(40)]
        public string TransactionTypeId { get; set; }

        [Required]
        [StringLength(40)]
        public string TransactionTypeName { get; set; }

        [StringLength(255)]
        public string TransactionTypeNote { get; set; }

        public double TransactionPriceRate { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
