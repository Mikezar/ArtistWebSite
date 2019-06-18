using NikaArtist.Service.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NikaArtist.Service.Models
{
	public class PaintingFilterModel
	{
		public PaintingModelList PaintingModel { get; set; }

		public int CategoryId { get; set; }

		[Display(Name = "Категории")]
		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}