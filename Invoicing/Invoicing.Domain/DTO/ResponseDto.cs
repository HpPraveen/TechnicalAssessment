namespace Invoicing.Domain.DTO
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string>? ErrorMessages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int FirstPageNumber { get; set; }
        public int LastPageNumber { get; set; }
        public int NextPageNumber { get; set; }
        public int PreviousPageNumber { get; set; }
        public Uri? FirstPageUri { get; set; }
        public Uri? LastPageUri { get; set; }
        public Uri? NextPageUri { get; set; }
        public Uri? PreviousPageUri { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
