namespace ServiceStatusHub.WebApi.HealthCheck.MemoryHealthCheck;

public class MemoryCheckOptions
{
    public string Memorystatus { get; set; }
    //public int Threshold { get; set; }
    // Failure threshold (in bytes)
    public long Threshold { get; set; } = 1024L * 1024L * 1024L;
}
