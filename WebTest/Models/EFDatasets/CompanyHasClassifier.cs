using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class CompanyHasClassifier
{

    public ulong CompanyID { get; set; }
    public Company Company { get; set; }

    public ulong ClassifierID { get; set; }
    public Classifier Classifier { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"mc_id","CompanyID"},
        {"classifier_id","ClassifierID"}
    };
    
}
