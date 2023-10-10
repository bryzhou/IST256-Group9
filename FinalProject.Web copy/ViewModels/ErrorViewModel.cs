namespace FinalProject.Web.ViewModels
{
	/// <summary>
	/// Error View Model for displaying an error to an end ser
	/// </summary>
    public class ErrorViewModel
    {
		/// <summary>
		/// request ID from the WebHost
		/// </summary>
        public string? RequestId { get; set; }
		/// <summary>
		/// HR Result - Status of the executed code -- error number to show to user to be used in identifying the error in splunk logs
		/// </summary>
        public string? HrResult { get; set; }
		/// <summary>
		/// IIS Error number
		/// </summary>
        public string IisError { get; set; } = string.Empty;
		/// <summary>
		/// boolean if the request ID should be shown to the user
		/// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
		/// <summary>
		/// boolean if the hresult should be shown to the user
		/// </summary>
        public bool ShowHrResult => !string.IsNullOrWhiteSpace(HrResult);

    }
}