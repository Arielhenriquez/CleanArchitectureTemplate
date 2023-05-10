namespace CleanArchitectureTemplate.Application.Common.PaginationResponse
{
    public class Paged<T> where T : class
    {
        public List<T> Items { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }
        private Paged(List<T> items, int totalRecords, int currentPage, int pageSize)
        {
            Items = items;
            PageNumber = currentPage;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = Convert.ToInt32(Math.Ceiling(totalRecords / (double)pageSize));
        }
        public static Paged<T> Create(List<T> items, int totalRecords, int currentPage, int pageSize)
        {
            return new Paged<T>(items, totalRecords, currentPage, pageSize);
        }
    }
}
