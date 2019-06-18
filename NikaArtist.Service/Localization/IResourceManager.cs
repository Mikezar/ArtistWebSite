namespace NikaArtist.Service.Localization
{
    public interface IResourceManager
    {
        IResource GetResource(string resourceName, string locale);
    }
}
