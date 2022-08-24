namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TourDetail")]
    public partial class TourDetail
    {
        public int TourDetailId { get; set; }

        [Required]
        [StringLength(255)]
        public string TourDetailName { get; set; }

        [Required]
        [StringLength(255)]
        public string Activity { get; set; }

        public DateTime ActivityTimeStart { get; set; }

        public DateTime ActivityTimeEnd { get; set; }

        [Required]
        [StringLength(40)]
        public string TouristSpotId { get; set; }

        [Required]
        [StringLength(40)]
        public string FacilityId { get; set; }

        [StringLength(255)]
        public string ActivityNote { get; set; }

        [Required]
        [StringLength(40)]
        public string TourId { get; set; }

        public bool Deleted { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual Tour Tour { get; set; }

        public virtual TouristSpot TouristSpot { get; set; }
    }
}
