using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Services
{
	/// <summary>
	/// Generic interface for wrapping an email service
	/// </summary>
	public interface IMailService
	{
		/// <summary>
		/// Send an email 
		/// </summary>
		/// <param name="mailRequest">email message to be sent</param>
		void SendEmailAsync(MailRequest mailRequest);
	}
}
