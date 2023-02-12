using Invoicing.Domain.DTO;

namespace Invoicing.API
{
    public static class PaginationHelper
    {
        public static ResponseDto CreatePageResponse<T>(List<T> data, int pageNumber, int pageSize, int totalRecords, string? endpointUri)
        {
            ResponseDto response = new()
            {
                Result = data,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var totalPages = ((double)totalRecords / (double)pageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.NextPageNumber = pageNumber >= 1 && pageNumber < roundedTotalPages ? pageNumber + 1 : pageNumber;
            response.PreviousPageNumber = pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages ? pageNumber - 1 : pageNumber;
            response.FirstPageNumber = 1;
            response.LastPageNumber = roundedTotalPages;
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;


            if (endpointUri == null) return response;

            response.NextPageUri = pageNumber >= 1 && pageNumber < roundedTotalPages
                ? GetPageUri(pageNumber + 1, pageSize, endpointUri) : null;

            response.PreviousPageUri = pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages
                ? GetPageUri(pageNumber - 1, pageSize, endpointUri) : null;

            response.FirstPageUri = GetPageUri(1, pageSize, endpointUri);

            response.LastPageUri = GetPageUri(roundedTotalPages, pageSize, endpointUri);

            return response;
        }

        public static Uri GetPageUri(int pageNumber, int pageSize, string? endpointUri)
        {
            var modifiedUri = endpointUri + "/" + pageNumber + "/" + pageSize;
            return new Uri(modifiedUri);
        }
    }
}
