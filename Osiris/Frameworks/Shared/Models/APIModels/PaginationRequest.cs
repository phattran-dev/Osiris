using Shared.Enums;

namespace Shared.Models.APIModels
{
    public class PaginationRequest
    {
        /// <summary>
        /// Use for full text search across multiple fields
        /// </summary>
        public string? QueryString { get; set; }
        /// <summary>
        /// Use for filtering and/or sorting on specific fields
        /// </summary>
        public List<QueryField>? QueryFields { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }

    public class QueryField
    {
        public string? FieldName { get; set; }
        public string? QueryString { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
