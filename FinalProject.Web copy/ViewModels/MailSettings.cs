namespace FinalProject.Web.ViewModels
{
	/// <summary>
	/// Encapsulates the MailSettings section of Appsettings.json
	/// </summary>
	public class MailSettings
	{
		/// <summary>
		/// From whom the message is sent -- this is the address e.g. rambutan@fruit.com
		/// </summary>
		public string? From { get; set; }
		/// <summary>
		/// the display name to be displayed for the given address e.g. Rambutan Fruit
		/// </summary>
		public string? DisplayName { get; set; }
		/// <summary>
		/// the SMTP server to use to send the message e.g. smtpisawesome.psu.edu
		/// </summary>
		public string? SmtpServer { get; set; }
		/// <summary>
		/// the port to be used for SMTP e.g. 25,
		/// </summary>
		public int SmtpPort { get; set; }
	}
}
