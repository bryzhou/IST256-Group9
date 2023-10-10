namespace FinalProject.Web.Utilities
{
	/// <summary>
	/// A class for making paginated lists of type T where T is an object
	/// </summary>
	/// <typeparam name="T">A view model object</typeparam>
    public class PaginatedList<T> : List<T>
    {
		/// <summary>
		/// The current page being viewed
		/// </summary>
		public int PageIndex { get; private set; }
		/// <summary>
		/// total number of pages available
		/// </summary>
		public int TotalPages { get; private set; }
		/// <summary>
		/// the size of the pages in quantity 
		/// </summary>
		public int PageSize { get; private set; }

		/// <summary>
		/// create a paginated list of stuff
		/// </summary>
		/// <param name="items">the class IEnumerable of items to paginate</param>
		/// <param name="count">number of items we got</param>
		/// <param name="pageIndex">the page to display</param>
		/// <param name="pageSize">number of items to show on the page</param>
		public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			PageSize = pageSize;

			AddRange(items);
		}

		/// <summary>
		/// do we have a previous page?  e.g. is the current index 1
		/// this is here for a convenience to not have to have logic with display
		/// </summary>
		public bool HasPreviousPage => PageIndex > 1;

		/// <summary>
		/// do we have a next page?  e.g. is the current less than the totla number of pages
		/// this is here for a convenience to not have to have logic with display
		/// </summary>
		public bool HasNextPage => PageIndex < TotalPages;

		/// <summary>
		/// Create a paginated list
		/// </summary>
		/// <param name="source">the list of stuff to turn into a paginated list of stuff</param>
		/// <param name="pageIndex">page to segment to be displayed</param>
		/// <param name="pageSize">items to show on the page</param>
		/// <returns></returns>
		public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
		{
			var count = source.Count();
			// skip and take is great -- take from the page index -1 times the page size till we reach the page size
			var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
			return new PaginatedList<T>(items, count, pageIndex, pageSize);
		}
	}
}
