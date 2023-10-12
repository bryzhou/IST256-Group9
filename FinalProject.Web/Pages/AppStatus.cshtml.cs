using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinalProject.Web.ViewModels;
namespace FinalProject.Web.Pages
{
	/// <summary>
	/// Page for taking an application offline or bringing it back online
	/// for this to work, application paths must be listed in the configuration database
	/// </summary>
	public class AppStatusModel : PageModel
	{
		/// <summary>
		/// Binding class for form fields
		/// </summary>
//		[BindProperty]
//		public SomeClass ViewModel { get; set; }

		/// <summary>
		/// Create an Instance of the App Status Page
		/// </summary>
		public AppStatusModel()
		{
		}

		/// <summary>
		/// Event fired on HTTP get of this page
		/// </summary>
		public void OnGet()
		{

		}

		/// <summary>
		/// Event fired on HTTP Post to this page
		/// </summary>
		/// <returns>An action result -- page or HTTP Status code</returns>
		public IActionResult OnPost()
		{
			return Page();
		}
	}
}
