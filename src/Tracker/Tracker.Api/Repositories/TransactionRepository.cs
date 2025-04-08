using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class TransactionRepository : RepositoryBase<Transaction, FinancialDbContext>, ITransactionRepository
{
    public TransactionRepository(FinancialDbContext context) : base(context) { }
}