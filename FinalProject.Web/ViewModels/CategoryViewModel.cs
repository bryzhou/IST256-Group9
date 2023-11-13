using System.ComponentModel.DataAnnotations;

namespace FinalProject.Web.ViewModels
{
	public class CategoryViewModel
	{
		private string[] statuses = { "Error", "Success", "SomeOther Status" };
		[Required,Display(Name ="Category Id"),Range(1,1500,ErrorMessage ="ID must be a number between 1 and 1500")]
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public int Status { get; set; }
		public string StatusMessage()
		{ 
			return statuses[this.Status];
		}
	}
	




}
