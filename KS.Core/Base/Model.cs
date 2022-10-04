using Newtonsoft.Json;

namespace KS.Core.Base;


/// <summary>
/// Base model
/// </summary>
/// <typeparam name="T">Instance from model</typeparam>
public abstract  class Model<T> : IModel<T>
{

    public string Serialize()
    {
#if DEBUG
        var options = Formatting.Indented;
#else
        var options = Newtonsoft.Json.Formatting.None;
#endif
        return JsonConvert.SerializeObject(this, options);
    }

    public virtual T? Deserialize(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
}