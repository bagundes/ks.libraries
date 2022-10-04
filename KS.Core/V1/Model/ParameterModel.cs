using System.ComponentModel.DataAnnotations;

namespace KS.Core.V1.Model;

public class ParameterModel : Base.Model<ParameterModel>
{

    [Key]
    public string Id
    {
        get => KS.Core.V1.Security.Hash.MD5($"{Base}_{Key}");
        set => Id = value;
    }

    /// <summary>
    /// Project/Library reference
    /// </summary>
    public string Base { get; set; }
    /// <summary>
    /// Key name
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; set; }
    
    [Required]
    public DateTime UpdateTime { get; set; }
}