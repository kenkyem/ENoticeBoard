﻿using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ENoticeBoard.Models
{
    public class AdInfo
    {
         public static string GetDisplayNameFromAd(string username)
        {
            using (PrincipalContext ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, "Oneharvest"))
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
           
        public static List<string> GetDepartmentFromAd(string username)
        {
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


        public static string GetEmpNoFromAd(string username)
        {
            using (PrincipalContext ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, "Oneharvest"))
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
            using (PrincipalContext ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, "Oneharvest"))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
                {
                    if (user != null)
                    {
                        return user.EmailAddress;
                    }
                }
            }
            return "";
        }
    }
}