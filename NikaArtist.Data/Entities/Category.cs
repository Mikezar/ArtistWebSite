using LinqToDB.Mapping;

namespace NikaArtist.Data.Entities
{
    [Table(Name = "Categories")]
    public class Category
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "ParentId")]
        public int? ParentId { get; set; }

        [Column(Name = "CategoryTitleRu"), NotNull]
        public string CategoryTitleRu{ get; set; }

        [Column(Name = "CategoryTitleEn"), NotNull]
        public string CategoryTitleEn { get; set; }

		[Column(Name = "ImagePath")]
		public string ImagePath { get; set; }
	}
}
