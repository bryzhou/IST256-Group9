using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FinalProject.Web.TagHelpers
{
	/// <summary>
	/// Razor Tag Helper for displaying sort order on table headings for sortable tables
	/// </summary>
    public class SortDisplayTagHelper : TagHelper
    {
		/// <summary>
		/// Encapsulated info related to this view
		/// </summary>
        [ViewContext]
        public ViewContext? ViewContext { get; set; }
		/// <summary>
		/// Name of the property in the underlying model class
		/// </summary>
        public string PropertyName { get; set; } = string.Empty;
		/// <summary>
		/// current sort order of the list to be able to properly create the link
		/// </summary>
        public string CurrentSortOrder { get; set; } = string.Empty;
        /// <summary>
		/// The object type of the model
		/// </summary>
		public Type? ModelType { get; set; }

		/// <summary>
		/// Process the tag helper on render of the page
		/// </summary>
		/// <param name="context">Context of the tag helper</param>
		/// <param name="output">output to render</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";			// generate a HTML span

            var propInfo = ModelType?.GetProperty(PropertyName);
            string? nameToShow = propInfo?.GetCustomAttribute<DisplayAttribute>()?.Name;		// is there a display name = x attribte on the property
            if (string.IsNullOrEmpty(nameToShow))
            {
                nameToShow = PropertyName;
            }

            output.Content.AppendHtml(nameToShow);

			// are we sorting on this field?  IF so, show the proper sort
            if (CurrentSortOrder.Equals($"{PropertyName}_desc", StringComparison.OrdinalIgnoreCase))
            {
                output.Content.AppendHtml(" <i class=\"fa fa-arrow-down-z-a\" aria-hidden=\"true\"></i><span class=\"sr-only\">Sorted descending</span>");
            }
            else if (CurrentSortOrder.Equals($"{PropertyName}", StringComparison.OrdinalIgnoreCase))
            {
                output.Content.AppendHtml(" <i class=\"fa fa-arrow-up-a-z\" aria-hidden=\"true\"></i><span class=\"sr-only\">Sorted ascending</span>");
            }
			// later team decision may be a 3rd option to show up down arrow on unsorted fields to match with existing sort 

        }
    }
}
