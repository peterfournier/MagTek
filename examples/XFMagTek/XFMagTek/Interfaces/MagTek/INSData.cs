namespace XFMagTek.Interfaces.MagTek
{
    public interface INSData
    {
        long Length { get; }
        byte[] Bytes { get; }
    }
}
