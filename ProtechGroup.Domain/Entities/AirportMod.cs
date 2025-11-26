using System;

namespace ProtechGroup.Domain.Entities
{
    public class AirportView
    {
        public string AirportCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string AirportName { get; set; }
      
    }
    public class AirportMod
    {

        public int Id { get; set; }
        public string AirportCode { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int CountryPriority { get; set; }
        public string StateCode { get; set; }
        public string AirportNameEN { get; set; }
        public string AirportNameVN { get; set; }
        public string SuggestNormal { get; set; }
        public string SuggestNormalVN { get; set; }
        public string SuggestMobile { get; set; }
        public bool IsAutoSuggest { get; set; }
        public bool IsMainAirport { get; set; }
        public bool Active { get; set; }
        public int OrderDisplay { get; set; }

    }
}
