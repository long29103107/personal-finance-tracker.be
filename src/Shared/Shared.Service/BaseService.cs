using Microsoft.EntityFrameworkCore;
using Shared.Service.Abstractions;

namespace Shared.Service;

public class BaseService<T> : IBaseService<T> where T : DbContext
{
    protected readonly T _context;

    public BaseService(T context)
    {
        _context = context;
    }
}