using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Data.Seed
{
    public static class RoleDefaults
    {
        [Display(Name = "Administrator")]
        public const string Admin = "Admin";
        [Display(Name = "User of the system")]
        public const string User = "User";
    }
}