using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Services
{
	/// <summary>
	/// Implmentation of a mail service to send emails.  Uses MailKit as recommended by Microsoft to send emails
	/// </summary>
	public class MailService : IMailService
	{
		private readonly MailSettings mailSettings;
		private readonly ILogger<MailService> logger;

		/// <summary>
		/// create an instance of the mail service
		/// </summary>
		/// <param name="mailSettings">A mailsettings object that contains the settings from the mailsettings section of appsettings.json</param>
		/// <param name="logger"></param>
		public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
		{
			this.mailSettings = mailSettings.Value;
			this.logger = logger;
		}

		/// <summary>
		/// send a mail 
		/// </summary>
		/// <param name="mailRequest">A mail request object encapsulating the email</param>
		/// <exception cref="ArgumentNullException"></exception>
		public void SendEmailAsync(MailRequest mailRequest)
		{
			if (mailRequest == null)
			{
				throw new ArgumentNullException(nameof(mailRequest));
			}

			MimeMessage message = new();
			message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.From));
			message.To.Add(MailboxAddress.Parse(mailRequest.ToAddress));
			message.Subject = mailRequest.Subject;
			var builder = new BodyBuilder();
			if ((mailRequest.Attachments != null) && mailRequest.Attachments.Any())
			{
				byte[] fileBytes;
				foreach (var file in mailRequest.Attachments)
				{
					if (file.Length > 0)
					{
						using MemoryStream ms = new();
						file.CopyTo(ms);
						fileBytes = ms.ToArray();
						builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
					}
				}
			}

			builder.HtmlBody = mailRequest.Body;

			message.Body = builder.ToMessageBody();
			logger.LogDebug("sending message");
			using SmtpClient client = new();
			logger.LogDebug("Connecting to SMTP server");
			client.Connect(mailSettings.SmtpServer, mailSettings.SmtpPort, false);
			logger.LogDebug("sending");
			try
			{
				client.Send(message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to send message to {mailRequest.ToAddress}", mailRequest);
			}
			finally
			{
				client.Disconnect(true);
			}
		}
	}
}
