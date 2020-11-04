using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class MicroDistrict
{
    public ulong MicroDistrictID { get; set; }
    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    public ulong CityID { get; set; }
    public City City { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","MicroDistrictID"},
        {"name","Name"},
        {"city_id","CityID"}
    };

}