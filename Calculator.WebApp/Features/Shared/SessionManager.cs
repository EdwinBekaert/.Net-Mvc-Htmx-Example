using System.Text;
using Newtonsoft.Json;

namespace Calculator.WebApp.Features.Shared;

public class SessionManager<T> : IStateManager<T>, ISerialization<T> where T : class
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionManager(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor 
                                  ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    public T? GetState(string key)
    {
        var sessionValue = _httpContextAccessor.HttpContext?.Session.Get(key);
        return sessionValue != null 
            ? Deserialize(Encoding.UTF8.GetString(sessionValue)) 
            : default;
    }

    public void SetState(string key, T state) 
        => _httpContextAccessor.HttpContext?.Session.Set(key, Encoding.UTF8.GetBytes(Serialize(state)));

    public T? Deserialize(string sessionValue) 
        => JsonConvert.DeserializeObject<T>(sessionValue);

    public string Serialize(T value)
        => JsonConvert.SerializeObject(value);
}