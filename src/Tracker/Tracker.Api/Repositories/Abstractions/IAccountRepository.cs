﻿using Microsoft.EntityFrameworkCore;
using Shared.Repository.Abstractions;
using Tracker.Api.Entities;

namespace Tracker.Api.Repositories.Abstractions;

public interface IAccountRepository : IRepositoryBase<Account, DbContext>
{
}
