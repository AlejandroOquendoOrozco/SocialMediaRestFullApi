namespace SocialMedia.Core.CustomEntitys
{
    public class PageList<T>:List<T>
    {

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool HasPreviusPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public int? NextPageNumber => HasNextPage ?CurrentPage+1:(int?)null;

        public int? PreviusPageNumber=> HasPreviusPage ? CurrentPage - 1 : (int?)null;
        public PageList(List<T> items,int count,int pageNumber,int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
                    
        }
        public static PageList<T> Create(IEnumerable<T> source,int pageNumber, int pageSize) {

           var Count= source.Count();
           var Items = source.Skip((pageNumber - 1 * pageSize)).Take(pageSize).ToList();
           
           return new PageList<T>(Items, Count, pageNumber, pageSize);
        }
    }
}
