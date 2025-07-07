
namespace ServiceStatusHub.Application.DTOs;

public record ServiceDto(Guid serviceId, string name, string url, int type, string environment);
