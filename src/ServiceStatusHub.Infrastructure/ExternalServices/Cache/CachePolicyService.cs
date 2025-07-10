
using Microsoft.Extensions.Options;
using ServiceStatusHub.Application.Interfaces;
using ServiceStatusHub.Application.Settings;

namespace ServiceStatusHub.Infrastructure.ExternalServices.Cache;

public class CachePolicyService : ICachePolicyService
{
    private readonly Dictionary<string, int> _ttlConfig;

    public CachePolicyService(IOptions<CacheTtlOptions> options)
    {
        _ttlConfig = options.Value.Expirations;
    }

    public TimeSpan GetExpirationFor(string key)
    {
        // Match exato
        if (_ttlConfig.TryGetValue(key, out int minutes))
            return TimeSpan.FromMinutes(minutes);

        // Match parcial (ex: "pedidos_" → "pedidos_todos")
        var fallback = _ttlConfig.Keys
            .FirstOrDefault(k => key.StartsWith(k, StringComparison.OrdinalIgnoreCase));

        if (fallback != null)
            return TimeSpan.FromMinutes(_ttlConfig[fallback]);

        // Default
        return _ttlConfig.TryGetValue("default", out int defaultMinutes)
            ? TimeSpan.FromMinutes(defaultMinutes)
            : TimeSpan.FromMinutes(5);
    }
}
