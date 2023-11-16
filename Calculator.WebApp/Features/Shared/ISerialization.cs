namespace Calculator.WebApp.Features.Shared;

public interface ISerialization<T> where T:class
{
    string Serialize(T value);
    T? Deserialize(string sessionValue);
}