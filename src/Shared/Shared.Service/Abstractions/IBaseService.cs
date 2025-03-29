using Microsoft.EntityFrameworkCore;

namespace Shared.Service.Abstractions;

public interface IBaseService<T> where T : DbContext
{
}
