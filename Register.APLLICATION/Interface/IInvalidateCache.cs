namespace Register.APPLICATION.Interface
{
    public interface IInvalidateCache
    {
        string[] InvalidateKeys { get; }
        string[] InvalidateVersions { get; }
    }
}
