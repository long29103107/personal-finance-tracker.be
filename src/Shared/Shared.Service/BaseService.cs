using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Service.Abstractions;
using ILogger = Serilog.ILogger;

namespace Shared.Service;

public class BaseService : IBaseService
{
    protected readonly ILogger _logger;

    public BaseService(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }
}