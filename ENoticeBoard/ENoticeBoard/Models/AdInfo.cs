using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ENoticeBoard.Models
{
    public class AdInfo
    {
        public string GetDisplayNameFromAd(string username)
        {
            using (System.DirectoryServices.AccountManagement.PrincipalContext ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, "Oneharvest"))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
                {
                    if (user != null)
                    {
                        if (user.DisplayName != "")
                        {
                            return user.DisplayName;
                        }
                        else
                        {
                            return user.Name + " " + user.GivenName;
                        }
                    }
                }
            }
            return "";
        }
                

        public static string GetEmpNoFromAd(string username)
        {
            using (System.DirectoryServices.AccountManagement.PrincipalContext ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, "Oneharvest"))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
                {
                    if (user != null)
                    {
                        return user.EmployeeId;
                    }
                }
            }
            return "";
        }
        public static string GetEmailFromAd(string username)
        {
            if (username == "")
            {
                return null;
            }
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "Oneharvest"))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
                {
                    if (user != null)
                    {
                        return user.EmailAddress;
                    }
                }
            }
            return null;
        }
        public static List<string> GetDepartmentFromAd(string username)
        {
            if (username == "")
            {
                return null;
            }
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "Oneharvest"))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
                {
                    if (user != null)
                    {
                        return user.GetGroups().Select(x => x.SamAccountName).ToList();
                    }
                }
            }
            return null;
        }
    }
}

