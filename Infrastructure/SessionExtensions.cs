using System.Text.Json;

namespace Backend4.Infrastructure;

public static class SessionExtensions
{
    public static void SetJson<T>(this ISession session, string key, T value)
        => session.SetString(key, JsonSerializer.Serialize(value));

    public static T? GetJson<T>(this ISession session, string key)
    {
        var json = session.GetString(key);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public static void RemoveMany(this ISession session, params string[] keys)
    {
        foreach (var k in keys)
            session.Remove(k);
    }
}
