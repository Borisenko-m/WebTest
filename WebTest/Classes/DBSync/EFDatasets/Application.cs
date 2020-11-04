using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

public class Application
{
    public ulong? ApplicationID { get; set; }

    public string Description { get; set; }

    public ulong? UserID { get; set; }
    public User User { get; set; }

    public ulong? AddressID { get; set; }
    public Address Address { get; set; }

    public ulong? ClassifierID { get; set; }
    public Classifier Classifier { get; set; }

    public ulong ApplicationStatusID { get; set; }
    public ApplicationStatus ApplicationStatus { get; set; }

    public ulong? CompanyID { get; set; }
    public Company Company { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","ApplicationID"},
        {"description","Description"},
        {"user_id","UserID"},
        {"address_id","AddressID"},
        {"classifier_id","ClassifierID"},
        {"application_status_id","ApplicationStatusID"},
        {"mc_id","CompanyID"},
        {"created_at","CreatedAt"},
        {"closed_date","ClosedAt"}
    };

}
