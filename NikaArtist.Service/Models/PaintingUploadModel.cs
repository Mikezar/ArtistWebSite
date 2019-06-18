﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NikaArtist.Service.Models
{
    public class PaintingUploadModel
    {
        public int Id { get; set; }

		public int Order { get; set; }

        public string PhotoPath { get; set; }

        public string ThumbnailPath { get; set; }

        public string FileName { get; set; }

        [Display(Name = "Название")]
        [MaxLength(100, ErrorMessage = "Длина названия фотографии не может превышать 100 символов")]
        public string TitleRu { get; set; }

        [Display(Name = "Title")]
        [MaxLength(100, ErrorMessage = "Длина названия фотографии не может превышать 100 символов")]
        public string TitleEng { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(500, ErrorMessage = "Длина описания фотографии не может превышать 500 символов")]
        public string DescriptionRu { get; set; }

        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "Длина описания фотографии не может превышать 500 символов")]
        public string DescriptionEng { get; set; }

        [Required]
        public DateTimeOffset CreationDate { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Категории")]
        public IEnumerable<SelectListItem> Categories { get; set; }

        public string Action { get; set; }
    }
}
