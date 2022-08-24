namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubCategory")]
    public partial class SubCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubCategory()
        {
            TouristSpots = new HashSet<TouristSpot>();
        }

        [StringLength(40)]
        public string SubCategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string SubCategoryName { get; set; }

        [StringLength(255)]
        public string SubCategoryNote { get; set; }

        [Required]
        [StringLength(40)]
        public string CategoryId { get; set; }

        public bool Deleted { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TouristSpot> TouristSpots { get; set; }
    }
}
