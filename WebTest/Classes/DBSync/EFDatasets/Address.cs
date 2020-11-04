using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Address
{
    public ulong AddressID { get; set; }
    public int? ResidenceNumber { get; set; }
    public int? FloorsNumber { get; set; }
    public int? ApartmentsNumber { get; set; }
    public int? EntrancesNumber { get; set; }
    public ulong RegionID { get; set; }
    public Region Region { get; set; }

    public ulong DistrictID { get; set; }
    public District District { get; set; }

    public ulong CityID { get; set; }
    public City City { get; set; }

    public ulong? MicroDistrictID { get; set; }
    public MicroDistrict MicroDistrict { get; set; }

    public ulong StreetID { get; set; }
    public Street Street { get; set; }

    public ulong BuildingID { get; set; }
    public Building Building { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","AddressID"},
        {"number_residence","ResidenceNumber"},
        {"number_floors","FloorsNumber"},
        {"number_apartments","ApartmentsNumber"},
        {"number_entrances","EntrancesNumber"},
        {"region_id","RegionID"},
        {"district_id","DistrictID"},
        {"city_id","CityID"},
        {"microdistrict_id","MicroDistrictID"},
        {"street_id","StreetID"},
        {"building_id","BuildingID"}
    };

}