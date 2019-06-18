using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Services;
using NLog;
using System;
using System.Web;
using System.Web.Routing;

namespace NikaArtist.Service.Infrastructure.ImageHandling
{
	public class FileHandler : IHttpHandler
	{
		private readonly PhotoManager _photoManager;
		private readonly ILogger<Logger> _logger;

		private readonly string _photoStoragePath;
		private readonly string _thumbStoragePath;
		public bool IsReusable => true;

		protected RequestContext RequestContext { get; set; }


		public FileHandler() : base() { }

		public FileHandler(string photoStoragePath, string thumbStoragePath, RequestContext requestContext, ILogger<Logger> logger)
		{
			RequestContext = requestContext;
			_photoStoragePath = photoStoragePath;
			_thumbStoragePath = thumbStoragePath;
			_logger = logger;
			_photoManager = new PhotoManager(logger);
		}

		public void ProcessRequest(HttpContext context)
		{
			try
			{
				context.Response.ContentType = "image/jpg";
				context.Response.Cache.SetCacheability(HttpCacheability.Public);
				context.Response.BufferOutput = false;
				var nvc = HttpUtility.ParseQueryString(HttpUtility.HtmlDecode(context.Server.UrlDecode(context.Request.QueryString.ToString())));

				byte[] image = null;
				string photoPath = null;
				string photoThumbPath = null;

				if (!string.IsNullOrEmpty(context.Request.QueryString["Id"]))
				{
					var id = Convert.ToInt32(context.Request.QueryString["Id"]);
					photoPath = $"{_photoStoragePath}photo_Ph-S{id}.jpg";
					photoThumbPath = $"{_thumbStoragePath}photo_Ph-S{id}s.jpg";
					image = _photoManager.GetPhoto(id, photoPath, photoThumbPath);
				}

				if (image != null)
					context.Response.BinaryWrite(image);

				context.Response.Cache.SetETag($"{photoPath.GetHashCode()}");
				context.Response.Cache.SetCacheability(HttpCacheability.Public);
				context.Response.Cache.SetSlidingExpiration(true);
				context.Response.Cache.SetValidUntilExpires(true);

				if (context.Response.IsClientConnected)
					context.Response.Flush();

				context.Response.Close();
			}
			catch (Exception e)
			{
				if (e.Message.Contains("The remote host closed the connection."))
					return;

				_logger.LogError(e);
				context.Response.StatusCode = 404;
				context.Response.Write("File was not found");
				return;
			}

		}
	}
}
