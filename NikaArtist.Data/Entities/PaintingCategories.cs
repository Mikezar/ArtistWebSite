using LinqToDB.Mapping;

namespace NikaArtist.Data.Entities
{
    [Table(Name = "PaintingCategories")]
    public class PaintingCategories
    {
        [Column(Name = "PaintingId"), NotNull]
        public int PaintingId { get; set; }

        [Column(Name = "CategoryId"), NotNull]
        public int CategoryId { get; set; }
    }
}
