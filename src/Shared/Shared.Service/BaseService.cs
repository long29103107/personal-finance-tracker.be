using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Service.Abstractions;
using ILogger = Serilog.ILogger;

namespace Shared.Service;

public class BaseService<TRepoManager> : IBaseService<TRepoManager>
    where TRepoManager : class
{
    protected readonly ILogger _logger;
    protected readonly TRepoManager _repoManager;

    public BaseService(ILogger logger, TRepoManager repoManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        _repoManager = repoManager;
    }
}