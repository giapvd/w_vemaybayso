using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("Airport")]
    public class Airport
    {
        [Key]
        public int Id { get; set; }

        [StringLength(5)]
        public string AirportCode { get; set; }

        [StringLength(5)]
        public string CityCode { get; set; }

        [StringLength(350)]
        public string CityName { get; set; }

        [StringLength(5)]
        public string CountryCode { get; set; }

        [StringLength(350)]
        public string CountryName { get; set; }

        public int? CountryPriority { get; set; }

        [StringLength(5)]
        public string StateCode { get; set; }

        [StringLength(250)]
        public string AirportNameEN { get; set; }

        [StringLength(250)]
        public string AirportNameVN { get; set; }

        [StringLength(350)]
        public string SuggestNormal { get; set; }

        [StringLength(350)]
        public string SuggestNormalVN { get; set; }

        [StringLength(350)]
        public string SuggestMobile { get; set; }

        public bool? IsAutoSuggest { get; set; }
        public bool? IsMainAirport { get; set; }
        public bool? Active { get; set; }
        public int? OrderDisplay { get; set; }
    }
}
