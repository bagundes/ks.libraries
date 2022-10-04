namespace KS.Core.Base;

public interface IModel<T>
{
    string Serialize();

    T? Deserialize(string data);
}