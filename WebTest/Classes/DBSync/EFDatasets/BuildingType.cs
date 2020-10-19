using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class BuildingType
{
    public ulong BuildingTypeID { get; set; }
    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","BuildingTypeID"},
        {"name","Name"},
    };

}

