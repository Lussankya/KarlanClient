namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("TouristSpot")]
    public partial class TouristSpot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TouristSpot()
        {
            TourDetails = new HashSet<TourDetail>();
        }

        [StringLength(40)]
        public string TouristSpotId { get; set; }

        [Required]
        [StringLength(255)]
        public string TouristSpotName { get; set; }

        [Required]
        [StringLength(40)]
        public string CityId { get; set; }

        [Required]
        [StringLength(40)]
        public string SubCategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string TouristSpotLocation { get; set; }

        public double TouristSpotRating { get; set; }

        public long OpenHour { get; set; }

        public long ClosingHour { get; set; }

        public bool TouristSpotAvailability { get; set; }

        [StringLength(255)]
        public string TouristSpotImage { get; set; }
        
        public decimal? Cord_Lat { get; set; }

        
        public decimal? Cord_Long { get; set; }


        

        [StringLength(255)]
        public string TouristSpotNote { get; set; }

        public bool Deleted { get; set; }

        [JsonIgnore]
        public virtual City City { get; set; }

        [JsonIgnore]
        public virtual SubCategory SubCategory { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TourDetail> TourDetails { get; set; }
    }
}
