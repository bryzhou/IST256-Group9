using Microsoft.Identity.Web.UI;
using FinalProject.Web.ViewModels;
using FinalProject.Web.Services;

try
{

	var builder = WebApplication.CreateBuilder(args);

	builder.Services.AddHttpLogging(logging =>
	{
		logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode;
	});

	builder.Services.Configure<CookiePolicyOptions>(options =>
	{
		options.Secure = CookieSecurePolicy.Always;
	});
	
	builder.Host.ConfigureServices(services =>
	{

		services.Configure<SessionOptions>(sopts =>
		{
			sopts.Cookie.MaxAge = TimeSpan.FromMinutes(30);
			sopts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		});

		services.AddAuthorization(options =>
		{
			options.FallbackPolicy = options.DefaultPolicy;
		});

		services.AddControllersWithViews();
		services
			.AddRazorPages()
			.AddMicrosoftIdentityUI();

		services.AddAutoMapper(typeof(Program).Assembly);
	});

	builder.Logging.ClearProviders();
	builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	builder.Logging.AddConsole();

	InjectDependencies(builder);

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Home/Error");
		// make HSTS last for 90 days, allow psu.edu
		app.UseHsts(opt =>
		{
			opt.MaxAge(days: 90);
			opt.IncludeSubdomains();
		});
		// no sniff
		app.UseXContentTypeOptions();
		// cross site blocked
		app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
		// secure cookie flag
		app.UseCookiePolicy(new CookiePolicyOptions
		{
			HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
			Secure = CookieSecurePolicy.Always
		});
		// x-frame-options
		app.UseXfo(opt => opt.SameOrigin());

		// allow google fonts
		app.UseCsp(opt => opt
			.DefaultSources(s => s.Self()
				.CustomSources("data:")
				.CustomSources("https:"))
			.StyleSources(s => s.Self()
				.CustomSources("fonts.googleapis.com", "code.jquery.com", "www.w3.org")
				.UnsafeInline()
			)
			.ScriptSources(s => s.Self()
				.CustomSources("cse.google.com", "www.w3.org")
				.UnsafeInline()
				.UnsafeEval()
			)
			.ImageSources(s => s.Self()
				.CustomSources("data:", "www.w3.org", "static.apps.psu.edu")
			)
		);

		app.UseHttpsRedirection();

	}
	else
	{
		app.UseDeveloperExceptionPage();
	}

	app.UseHttpLogging();

	app.UseStaticFiles();

	app.UseCookiePolicy(new CookiePolicyOptions()
	{
		MinimumSameSitePolicy = SameSiteMode.None
	});

	app.UseRouting();


	app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

	app.UseEndpoints(endpoints =>
	{
		endpoints.MapRazorPages();
		endpoints.MapControllers();
		endpoints.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
	});

	app.Run();
}
catch (Exception exception)
{
	Console.Write(exception.ToString());
	throw;

}
finally
{
	// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
}

void InjectDependencies(WebApplicationBuilder builder)
{
	IServiceCollection services = builder.Services;
	// --------------------
	// other services
	// --------------------
	services.AddTransient<IMailService, MailService>();
}
