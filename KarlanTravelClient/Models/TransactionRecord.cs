namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;

    [Table("TransactionRecord")]
    public partial class TransactionRecord
    {
        public int TransactionRecordId { get; set; }

        [Required]
        [StringLength(40)]
        public string TransactionTypeId { get; set; }

        [Required]
        [StringLength(40)]
        public string TourId { get; set; }

        public int CustomerID { get; set; }

        [Column(TypeName = "money")]
        public decimal TransactionFee { get; set; }

        public bool Paid { get; set; }

        public DateTime RecordedTime { get; set; }

        public DateTime DueDate { get; set; }

        public bool Canceled { get; set; }

        public int? AdminID { get; set; }

        [StringLength(255)]
        public string TransactionNote { get; set; }

        public bool Deleted { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Tour Tour { get; set; }

        public virtual TransactionType TransactionType { get; set; }
    }
}
