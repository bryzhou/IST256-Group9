using FinalProject.DAL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FinalProject.DAL.Repositories
{
	/// <summary>
	/// Needs Microsoft.extensions.Http
	/// </summary>
	public class AdventureWorksRepository
	{
		private readonly IHttpClientFactory clientFactory;
		ILogger<AdventureWorksRepository> logger;
		AdventureWorksSettings settings;

		public AdventureWorksRepository(IOptions<AdventureWorksSettings> settings, IHttpClientFactory clientFactory, ILogger<AdventureWorksRepository> logger)
		{
			this.clientFactory = clientFactory;
			this.logger = logger;
			this.settings = settings.Value;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			string url = settings.categoryUri;
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(url))
			{
				Headers =
				{
					{ "Accept", "application/json" },
					{ "User-Agent", "Group-99" }
				}
			};

			var httpclient = clientFactory.CreateClient();

			var httpResponseMessage = httpclient.SendAsync(httpRequestMessage).Result;
			
			httpResponseMessage.EnsureSuccessStatusCode();

			logger.LogDebug("Got categories");

			var result = httpResponseMessage.Content.ReadAsStringAsync().Result;

			IEnumerable<Category>  data = JsonConvert.DeserializeObject<IEnumerable<Category>>(result) ?? new List<Category>();

			return data;

		}



	}
}
