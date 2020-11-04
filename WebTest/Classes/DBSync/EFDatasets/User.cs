using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class User
{
    public ulong UserID { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string FirstName { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string MiddleName { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string LastName { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; }

    public string FullName { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","UserID"},
        {"first_name","FirstName"},
        {"mid_name","MiddleName"},
        {"last_name","LastName"},
        {"phone","PhoneNumber"}
    };

}