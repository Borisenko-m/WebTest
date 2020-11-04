using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

public class Application
{
    public ulong ApplicationID { get; set; } = default;

    public string Description { get; set; } = default;

    public ulong UserID { get; set; } = default;
    public User User { get; set; } = default;

    public ulong AddressID { get; set; } = default;
    public Address Address { get; set; } = default;


    public ulong ClassifierID { get; set; } = default;
    public Classifier Classifier { get; set; } = default;


    public ulong ApplicationStatusID { get; set; } = default;
    public ApplicationStatus ApplicationStatus { get; set; } = default;


    public ulong CompanyID { get; set; } = default;
    public Company Company { get; set; } = default;

    public DateTime CreatedAt { get; set; } = default;
    public DateTime? ClosedAt { get; set; } = default;

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
