﻿using Microsoft.EntityFrameworkCore;
using Shared.Repository.Abstractions;
using Tracker.Api.Entities;

namespace Tracker.Api.Repositories.Abstractions;

public interface IGoalRepository : IRepositoryBase<Goal, DbContext>
{
}
