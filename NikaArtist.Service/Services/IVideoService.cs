using NikaArtist.Data.Entities;
using NikaArtist.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NikaArtist.Service.Services
{
    public interface IVideoService
    {
        Video GetVideo(int id);
        VideoModelList GetVideos();
        VideoEditModel GetEditModel(int id);
        Task AddAsync(VideoEditModel model);
        Task UpdateAsync(VideoEditModel model);
        Task DeleteAsync(int id);
    }
}
