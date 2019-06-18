using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NikaArtist.Service.Models
{
    public class ArticleEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
		[Required(ErrorMessage = "Укажите название статьи")]
		[MaxLength(100, ErrorMessage = "Длина названия не может превышать 100 символов")]
		public string ArticleTitleRu { get; set; }

        [Display(Name = "Title")]
		[Required(ErrorMessage = "Укажите название статьи")]
		[MaxLength(100, ErrorMessage = "Длина названия не может превышать 100 символов")]
		public string ArticleTitleEn { get; set; }

		[Display(Name = "Описание")]
		[MaxLength(150, ErrorMessage = "Длина описания не может превышать 150 символов")]
		public string DescriptionRu { get; set; }

		[Display(Name = "Description")]
		[MaxLength(150, ErrorMessage = "Длина описания не может превышать 150 символов")]
		public string DescriptionEn { get; set; }

		[Display(Name = "RawHtmlRu")]
		[AllowHtml]
		[Required(ErrorMessage = "Укажите тело статьи")]
		[MaxLength(8000, ErrorMessage = "Длина разметки не может превышать 8000 символов")]
        public string RawHtmlRu { get; set; }

        [Display(Name = "RawHtmlEn")]
		[Required(ErrorMessage = "Укажите тело статьи")]
		[AllowHtml]
		[MaxLength(8000, ErrorMessage = "Длина разметки не может превышать 8000 символов")]
        public string RawHtmlEn { get; set; }

        [Required]
        public DateTimeOffset CreationDate { get; set; }

		[Display(Name = "Обложка альбома")]
		public int? CoverId { get; set; }
    }
}
