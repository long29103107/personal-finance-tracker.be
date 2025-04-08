namespace Shared.Domain;

public interface IScopedCache
{
    ScopedContext ScopedContext { get; set; }
    bool StartTransaction(string tags);
    bool IsInTransaction { get; }
    string TransactionTags { get; }
    Dictionary<string, object> Values { get; }
}

public class ScopedCache : IScopedCache
{
    private bool _isTransaction = false;
    private List<string> _transactionTags = null;
    public ScopedContext ScopedContext { get; set; }
    public Dictionary<string, object> Values { get; } = new Dictionary<string, object>();
    public bool StartTransaction(string tags)
    {
        if (ScopedContext.IsTransactionOrdinary)
        {
            _isTransaction = true;
        }
        if (_transactionTags == null)
        {
            _transactionTags = new List<string>();
        }
        _transactionTags.AddRange(tags.Split(','));
        return _isTransaction;
    }

    public bool IsInTransaction
    {
        get
        {
            return _isTransaction;
        }
    }
    public string TransactionTags
    {
        get
        {
            if (_transactionTags == null) return null;
            return string.Join(',', _transactionTags.GroupBy(x => x)
                                    .Select(x => x.First()));
        }
    }
}

