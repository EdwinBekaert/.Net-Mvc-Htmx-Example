namespace Calculator.WebApp.Features.Shared;

public interface IStateManager<T> where T:class
{
    T? GetState(string key);
    void SetState(string key, T state);
}