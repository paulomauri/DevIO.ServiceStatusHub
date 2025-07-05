namespace ServiceStatusHub.Domain.ValueObjects;

public class Attachment
{
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public long SizeInBytes { get; private set; }
    public string Url { get; private set; }

    public Attachment(string fileName, string contentType, long sizeInBytes, string url)
    {
        FileName = fileName;
        ContentType = contentType;
        SizeInBytes = sizeInBytes;
        Url = url;
    }
}
