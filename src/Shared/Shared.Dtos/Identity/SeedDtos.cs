﻿using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Identity;

public static class SeedDtos
{
    public sealed class OperationRequest : Request
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = true;
    }

    public sealed class ScopeRequest : Request
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = true;
    }

    public sealed class PermissionRequest : Request
    {
        public string Scope { get; set; }
        public List<string> Operations { get; set; }
    }

    public sealed class RoleRequest : Request
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NormalizedName
        {
            get
            {
                return this.Name.ToUpper();
            }
        }
        public bool IsLocked { get; set; } = true;
        public int Weight { get; set; }
    }
    public sealed class SeedDataRequest : Request
    {
        public bool IsReset { get; set; } = false;
        public bool IsSeed { get; set; } = false;
    }

    public sealed class PermissionSeedRequest : Request
    {
        public string OperationCode  { get; set; }
        public string ScopeCode { get; set; }
    }
}

