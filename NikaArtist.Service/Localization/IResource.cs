namespace NikaArtist.Service.Localization
{
    public interface IResource
    {
        string Locale { get; }

        string GetTranslation(string title);
    }
}
