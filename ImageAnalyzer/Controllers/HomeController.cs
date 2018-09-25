using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageAnalyzer.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Bug()
		{
			throw new NotSupportedException();
		}
		public async Task<ActionResult> Image(string url)
		{
			var key = "2c52934a04364f5f842c2c2b7ef2b24b";
			var credentials = new ApiKeyServiceClientCredentials(key);
			ComputerVisionClient client = new ComputerVisionClient(credentials);
			client.Endpoint = "https://southcentralus.api.cognitive.microsoft.com";
			var result = await client.AnalyzeImageAsync(url,
				new List<VisualFeatureTypes>
				{
					VisualFeatureTypes.Adult,
					VisualFeatureTypes.Categories,
					VisualFeatureTypes.Color,
					VisualFeatureTypes.Description,
					VisualFeatureTypes.Faces,
					VisualFeatureTypes.ImageType,
					VisualFeatureTypes.Tags,
				}
				);
			return Content("result " + 
				string.Join(",", result.Description.Captions.Select(x => x.Text))
				+ Environment.NewLine + 
				string.Join(",", result.Tags.Select(x => x.Name)));
		}
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}