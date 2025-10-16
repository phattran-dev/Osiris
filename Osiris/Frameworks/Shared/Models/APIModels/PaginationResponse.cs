namespace Shared.Models.APIModels
{
    public class PaginationResponse<TResult>
    {
        public IEnumerable<TResult>? Items { get; set; }
        public int TotalCount { get; set; } = 0;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
