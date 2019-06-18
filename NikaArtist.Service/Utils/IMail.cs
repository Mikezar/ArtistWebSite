using System.Net.Mail;
using System.Threading.Tasks;

namespace NikaArtist.Service.Utils
{
	public interface IMail
	{
		MailMessage FormFeedBackMessage(string destinationEmail, string subject, string body);
		Task SendAsync(MailMessage message);
	}
}
