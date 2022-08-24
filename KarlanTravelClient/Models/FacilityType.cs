namespace KarlanTravelClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacilityType")]
    public partial class FacilityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacilityType()
        {
            Facilities = new HashSet<Facility>();
        }

        [StringLength(40)]
        public string FacilityTypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string FacilityTypeName { get; set; }

        [StringLength(255)]
        public string FacilityTypeNote { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
