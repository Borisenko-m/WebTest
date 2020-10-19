using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class District
{
    public ulong DistrictID { get; set; }
    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    public ulong RegionID { get; set; }
    public Region Region { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","DistrictID"},
        {"name","Name"},
        {"region_id","RegionID"}
    };
}