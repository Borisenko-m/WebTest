using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Classifier
{
    public ulong ClassifierID { get; set; }

    [Required, Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    [Required, Column(TypeName = "varchar(45)")]
    public string AppAcceptanceTime { get; set; }

    [Required, Column(TypeName = "varchar(45)")]
    public string AppDecisionTime { get; set; }

    [Timestamp]
    public byte[] CreatedAt { get; set; }

    [NotMapped]
    public Dictionary<string, string> KeyValuePairs { get; set; } = new Dictionary<string, string>()
    {
        {"id","ClassifierID"},
        {"name","Name"},
        {"application_acceptance_time","AppAcceptanceTime"},
        {"application_decision_time","AppDecisionTime"}
    };

}
