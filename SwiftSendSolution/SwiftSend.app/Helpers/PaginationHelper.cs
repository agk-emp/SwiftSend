using SwiftSend.app.Common;

namespace SwiftSend.app.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResult<T> ToPaginatedResult<T>(this IEnumerable<T> list,
            int pageNumber, int pageSize)
        {
            if (pageNumber < 0)
            {
                pageNumber = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return new PagedResult<T>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize),
                TotalItems = list.Count(),

            };
        }
    }
}
