using NikaArtist.Data.Entities;
using NikaArtist.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace NikaArtist.Service.Services
{
    public interface IPaintingService
    {
        Painting GetPainting(int id);
        PaintingModelList GetPaintings();
		PaintingModelList GetPaintingsByCategory(int id, int take, int skip);
		PaintingUploadModel GetEditModel(int id);
        Task AddManyAsync(IEnumerable<PaintingUploadModel> models);
        Task UpdateAsync(PaintingUploadModel model);
        Task DeleteAsync(int id);
		Task ChangeOrderAsync(ImageSortModel model);
        UploadModel Upload(IEnumerable<HttpPostedFileBase> files, UploadModel listModel);
    }
}
