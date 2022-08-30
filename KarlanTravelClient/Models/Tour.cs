namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tour")]
    public partial class Tour
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            TourDetails = new HashSet<TourDetail>();
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        [StringLength(40)]
        public string TourId { get; set; }

        [Required]
        [StringLength(255)]
        public string TourName { get; set; }

        public bool TourAvailability { get; set; }

        
        public DateTime TourStart { get; set; }
        

        public DateTime TourEnd { get; set; }

        [Column(TypeName = "money")]
        public decimal TourPrice { get; set; }

        [Required]
        [StringLength(40)]
        public string CategoryId1 { get; set; }

        [StringLength(40)]
        public string CategoryId2 { get; set; }

        public int MaxBooking { get; set; }

        public int BookTimeLimit { get; set; }

        public double TourRating { get; set; }

        [StringLength(255)]
        public string TourImage { get; set; }

        [StringLength(255)]
        public string TourNote { get; set; }

        public bool Deleted { get; set; }

        public virtual Category Category { get; set; }

        public virtual Category Category1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TourDetail> TourDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}
