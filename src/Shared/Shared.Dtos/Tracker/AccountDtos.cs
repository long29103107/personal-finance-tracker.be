using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Tracker;

public static class AccountDtos
{

    private const string _concurrencyVND = "VND";
    //Request
    public sealed class AccountCreateRequest : Request
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public string Currency { get; set; } = _concurrencyVND;
    }

    public sealed class AccountUpdateRequest : Request
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public string Currency { get; set; } = _concurrencyVND;
    }
    public sealed class AccountSummaryRequest : Request
    {

    }

    //Response

    public sealed class AccountListResponse : Response
    {
        public int? UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public string Currency { get; set; } = _concurrencyVND;
    }

    public sealed class AccountResponse : Response
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public string Currency { get; set; } = _concurrencyVND;
    }

    public class AccountSummaryResponse
    {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
    }


}
