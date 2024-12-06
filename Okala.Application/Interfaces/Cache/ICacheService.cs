namespace Okala.Application.Interfaces.Cache;

public interface ICacheService
{
    bool TryGetValue<T>(string key,out T? value);
    void Set<T>(string key, T content,TimeSpan? absoluteExpiration = null);
}