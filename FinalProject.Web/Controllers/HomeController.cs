using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

using FinalProject.Web.ViewModels;
using FinalProject.Web.Services;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Linq;

namespace FinalProject.Web.Controllers
{
	/// <summary>
	/// home page controller
	/// </summary>
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> logger;
		private readonly IMailService mailService;
		private readonly IConfiguration config;

		/// <summary>
		/// controller for home page
		/// </summary>
		/// <param name="mailService">email service for sending mail</param>
		/// <param name="config">Configuration for the a[[;ocatopm</param>
		/// <param name="logger">Logger to log errors and such</param>
		public HomeController(IMailService mailService, 
			IConfiguration config, 
			ILogger<HomeController> logger)
		{
			this.mailService = mailService;
			this.logger = logger;
			this.config = config;
		}

		/// <summary>
		/// Main landing page -- lists all functions.  Requires at minimum tmaint role
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Index()
		{
			
			return View();
		}

		/// <summary>
		/// Privacy page end point
		/// </summary>
		/// <returns></returns>
		public IActionResult Privacy()
		{
			return View();
		}

		/// <summary>
		/// Error page to show to the user
		/// </summary>
		/// <param name="id">ID of the error</param>
		/// <returns>View telling user there has been an error.  Also emails the error out</returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(string id)
		{
			var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
			var unhandledException = feature?.Error;
			logger.LogError(unhandledException, "error {IISerror} occurred in final project", id);
			MailRequest mail = new()
			{
				ToAddress = config.GetValue<string>("ErrorEmailAddress"),
				Subject = $"Project ERROR: {id}",
				Body = $"an eror has occured {unhandledException?.Message}"
			};
			mailService.SendEmailAsync(mail);
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, HrResult = unhandledException?.HResult.ToString(), IisError = id });
		}
	}
}