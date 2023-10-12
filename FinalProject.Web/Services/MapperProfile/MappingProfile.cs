using AutoMapper;
using FinalProject.DAL.Models;
using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Services.MapperProfile
{
	/// <summary>
	/// automapper mapping profile to be used to map viewmodels => models and models => view models
	/// this eliminates boring code like
	///			x.Fruit = y.Fruit
	///	note, name and signatuyre similarities are required.  
	///	therefore int Cash and string Cash will not map but
	///	int cash and int Cash will 
	/// </summary>
	/// <remarks>All new view models and models need mapping here</remarks>
	public class MappingProfile : Profile
	{
		/// <summary>
		/// an instance of a mapping profile
		/// </summary>
		public MappingProfile()
		{
		}

	}
}
