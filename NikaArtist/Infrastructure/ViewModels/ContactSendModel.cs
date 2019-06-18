using NikaArtist.SysData.Resources;
using System.ComponentModel.DataAnnotations;

namespace NikaArtist.Infrastructure.ViewModels
{
	public class ContactSendModel
	{
		[Required(ErrorMessageResourceName = "fillEmail", ErrorMessageResourceType = typeof(Static))]
		[MaxLength(25, ErrorMessageResourceName = "count", ErrorMessageResourceType = typeof(Static))]
		public string Email { get; set; }

		[Required(ErrorMessageResourceName = "fillSubject", ErrorMessageResourceType = typeof(Static))]
		[MaxLength(100, ErrorMessageResourceName = "count", ErrorMessageResourceType = typeof(Static))]
		public string Subject { get; set; }

		[Required(ErrorMessageResourceName = "fillBody", ErrorMessageResourceType = typeof(Static))]
		[MaxLength(500, ErrorMessageResourceName = "count", ErrorMessageResourceType = typeof(Static))]
		public string Message { get; set; }
	}
}