using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NikaArtist.Service.Models
{
    public class UploadModel
    {
        public List<PaintingUploadModel> Uploads { get; set; } = new List<PaintingUploadModel>();
    }
}
