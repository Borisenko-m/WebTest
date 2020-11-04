using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Company
{
    public ulong CompanyID { get; set; }

    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }


    [Required, Column(TypeName = "varchar(200)")]
    public string Address { get; set; }


    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","CompanyID"},
        {"name","Name"},
        {"address","Address"}
    };
}
