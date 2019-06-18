using NikaArtist.Service.FileHandler;
using NikaArtist.Service.LoggerService;
using NLog;
using System;

namespace NikaArtist.Service.Services
{
	public class PhotoManager
	{
		private readonly ILogger<Logger> _logger;

		public PhotoManager(ILogger<Logger> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Получение фотографии
		/// </summary>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <returns></returns>
		public byte[] GetPhoto(int photoId, string photoPath, string thumbPath)
		{
			try
			{
				return new ImageProcessor(photoPath, thumbPath, new TextAttributes().Default).Watermark(
					new ImageAttributes() {
						IsWatermarkApplied = true,
						IsWatermarkBlack = true
					}
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex);
				return null;
			}
		}
	}
}
