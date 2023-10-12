namespace FinalProject.Web.ViewModels
{
	/// <summary>
	/// Email request to be used to send email messages
	/// </summary>
	public class MailRequest
	{
		/// <summary>
		/// for whom the message is for.  
		/// </summary>
		public string? ToAddress { get; set; }
		/// <summary>
		/// subject of the message
		/// </summary>
		public string? Subject { get; set; }
		/// <summary>
		/// the body in HTML or plain text
		/// </summary>
		public string? Body { get; set; }
		/// <summary cref="IFormFile">
		/// Attachements in the form of IFormFile
		/// </summary>
		public List<IFormFile>? Attachments { get; set; }
	}
}
