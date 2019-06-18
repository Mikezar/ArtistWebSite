using LinqToDB;
using LinqToDB.Data;
using NikaArtist.Data.Entities;

namespace NikaArtist.Data
{
    public class DatabaseConnection : DataConnection
    {
        public DatabaseConnection() : base("ArtistDataBase") { }

        public ITable<Painting> Paintings => GetTable<Painting>();
        public ITable<Category> Categories => GetTable<Category>();
        public ITable<PaintingCategories> PaintingCategories => GetTable<PaintingCategories>();
        public ITable<Video> Videos => GetTable<Video>();
        public ITable<Article> Articles => GetTable<Article>();
    }
}
