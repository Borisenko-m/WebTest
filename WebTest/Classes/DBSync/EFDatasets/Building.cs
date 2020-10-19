using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
public class Building
{
    public ulong BuildingID { get; set; }
    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    public ulong StreetID { get; set; }
    public Street Street { get; set; }

    public ulong BuildingTypeID { get; set; }
    public BuildingType BuildingType { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","BuildingID"},
        {"name","Name"},
        {"street_id","StreetID"},
        {"building_type_id","BuildingTypeID"}
    };
}


