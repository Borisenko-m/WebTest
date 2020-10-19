using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


public class CompanyHasAddress
{

    public ulong CompanyID { get; set; }
    public Company Company { get; set; }

    public ulong AddressID { get; set; }
    public Address Address { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"mc_id","CompanyID"},
        {"address_id","AddressID"}
    };

}
