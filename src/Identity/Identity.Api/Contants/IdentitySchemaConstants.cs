namespace Identity.Api.Contants;


public static class IdentitySchemaConstants
{
    //public const string IdentitySchema = "Identity";
    public class Table
    {
        public const string Users = "Users";
        public const string Roles = "Roles";
        public const string UserRoles = "UserRoles";
        public const string UserLogins = "UserLogins";
        public const string UserTokens = "UserTokens";
        public const string UserClaims = "UserClaims";
        public const string RoleClaims = "RoleClaims";
    }

    public class RoleCode
    {
        public const string SuperAdmin = "super-admin";
        public const string Admin = "admin";
    }

    public class Role
    {
        public const string SuperAdmin = "Super Admin";
        public const string Admin = "Admin";
    }
}