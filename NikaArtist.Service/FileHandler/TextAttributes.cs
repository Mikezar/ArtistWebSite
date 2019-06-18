namespace NikaArtist.Service.FileHandler
{
    public class TextAttributes
    {
        public int Id { get; set; }
        public string WatermarkFont { get; set; }
        public int WatermarkFontSize { get; set; }
        public string WatermarkText { get; set; }
        public int Alpha { get; set; }

        public TextAttributes Default => new TextAttributes()
        {
            WatermarkFont = "Bell MT",
            WatermarkFontSize = 60,
            WatermarkText = "nikaphoenix.artist@gmail.com",
            Alpha = 80
        };
    }
}
