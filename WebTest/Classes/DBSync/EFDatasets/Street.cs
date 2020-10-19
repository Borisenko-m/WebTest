using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Street
{
    public ulong StreetID { get; set; }
    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    public ulong CityID { get; set; }
    public City City { get; set; }

    public ulong? MicroDistrictID { get; set; }
    public MicroDistrict MicroDistrict { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","StreetID"},
        {"name","Name"},
        {"city_id","CityID"},
        {"microdistrict_id","MicroDistrictID"}
    };
}
