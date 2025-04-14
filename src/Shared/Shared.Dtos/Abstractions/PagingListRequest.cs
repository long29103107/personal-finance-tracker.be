namespace Shared.Dtos.Abstractions;

public abstract class PagingListRequest : ListRequest
{
    private int? _page;
    public int Page
    {
        get { return _page ?? 1; }
        set { _page = value; }
    }
    private int? _pageSize;
    public int PageSize
    {
        get { return _pageSize ?? 50; }
        set { _pageSize = value; }
    }

    public new PagingListResponse<T> GetListResponse<T>(List<T> Results) where T : class
    {
        PagingListResponse<T> listResponse = new PagingListResponse<T>
        {
            Results = Results ?? new List<T>(),
            Count = Count,
            Page = Page,
            PageSize = PageSize
        };

        //if(this.ScopedContext == null) this.ScopedContext = new ScopedContext();

        //if (this.ScopedContext.RequestQueryString == null)
        //{
        //    this.ScopedContext.RequestQueryString = HttpUtility.ParseQueryString("page=1");
        //}

        //if (listResponse.Results.Count >= PageSize)
        //{
        //    this.ScopedContext.RequestQueryString["page"] = (this.Page + 1).ToString();
        //    listResponse.NextUrl = this.ScopedContext.RequestPath + "?" + this.ScopedContext.RequestQueryString.ToString();
        //}

        //if (this.Page > 1)
        //{
        //    this.ScopedContext.RequestQueryString["page"] = (Math.Max(1, this.Page - 1)).ToString();
        //    listResponse.PrevUrl = this.ScopedContext.RequestPath + "?" + this.ScopedContext.RequestQueryString.ToString();
        //}

        if (listResponse.Count <= 0)
        {
            listResponse.Count = listResponse.Results.Count;
        }

        return listResponse;
    }
}
