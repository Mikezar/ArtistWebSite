using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NikaArtist.Service.Utils
{
	public class Mail : IMail
	{
		private readonly string _email;
		private readonly string _password;

		public Mail(string email, string password)
		{
			_email = email;
			_password = password;
		}

		public MailMessage FormFeedBackMessage(string destinationEmail, string subject, string body)
		{
			var from = new MailAddress(_email, "Nika Artist");
			var to = new MailAddress(_email);

			return  new MailMessage(from, to)
			{
				Subject = subject,
				Body = $"<p><b>Email:</b>{destinationEmail}</p></br><p>{body}</p>",
				IsBodyHtml = true
			};

		}

		public async Task SendAsync(MailMessage message)
		{
			using (var smtpClient = new SmtpClient("smtp.gmail.com", 587) {

				EnableSsl = true,
				Credentials = new NetworkCredential(_email, _password)
			})
			{
				await smtpClient.SendMailAsync(message);
			};
		}
	}
}
