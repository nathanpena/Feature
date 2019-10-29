using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Configuration;
using Features.Models;

namespace Features.Providers
{
    public class LDAPLoginProvider : ILoginProvider
    {
        private string _srvAcc = WebConfigurationManager.AppSettings["UTRGV_SRV_ACCOUNT"];
        private string _srvPassword = WebConfigurationManager.AppSettings["UTRGV_SRV_PASSWORD"];
        private string _domain = WebConfigurationManager.AppSettings["UTRGV_AD_DOMAIN"];
        private string _groups = WebConfigurationManager.AppSettings["UTRGV_ALLOWED_GROUPS"];

        public bool ValidateCredentials(string userName, string password, out string cn, out bool authorized)
        {

            using (var pc = new PrincipalContext(ContextType.Domain, _domain, userName, password))
            {
                UserPrincipal user = null;
                cn = "";

                authorized = false;
                bool isValid = pc.ValidateCredentials(userName, password);
                if (isValid)
                {
                    user = UserPrincipal.FindByIdentity(pc, IdentityType.UserPrincipalName, userName);
                    foreach (var s in _groups.Split(';')) {
                        var group = GroupPrincipal.FindByIdentity(pc, s);
                        if (group == null)
                            continue;
                        if(user.IsMemberOf(group))
                        {
                            authorized = true;
                            break;
                        }
                    }
                    cn = user.Name;
                }


                return isValid;
            }
        }

        public List<User> FindUserByEmail(string email)
        {
            DirectoryEntry rootDSE = rootDSE = new DirectoryEntry("LDAP://" + _domain, _srvAcc, _srvPassword);
            DirectorySearcher search = new DirectorySearcher(rootDSE);
            search.Asynchronous = true;
            search.PageSize = 1001;// To Pull up more than 100 records.

            search.Filter = "(&(&(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(userPrincipalName=" + email + "*))(|(memberOf=CN=utrgv-staff,OU=Groups,DC=ad,DC=utrgv,DC=edu)(memberOf=CN=utrgv-faculty,OU=Groups,DC=ad,DC=utrgv,DC=edu)))";//UserAccountControl will only Include Non-Disabled Users.
            SearchResultCollection result = search.FindAll();
            List<User> users = new List<User>();
            foreach (SearchResult item in result)
            {
                var user = new User();
                if (item.Properties["displayName"].Count > 0)
                    user.Name = item.Properties["displayName"][0].ToString();
                if (item.Properties["cn"].Count > 0)
                    user.Cn = item.Properties["cn"][0].ToString();
                if (item.Properties["mail"].Count > 0)
                    user.Email = item.Properties["mail"][0].ToString();

                users.Add(user);
            }

            return users;

        }


        public User generateUser(string cn, Role role)
        {
            //if the user does not exist, then load it from AD
            var userProfile = GetUser(cn);

            //if user does not exist in AD then clearly they f'ed up
            if (userProfile == null)
                return null;

            var user = new User()
            {
                Active = true,
                Cn = cn,
                Email = userProfile.Email,
                Name = userProfile.Name,
                Role = role,
                RoleId = role.Id
            };

            return user;
        }

        public UTRGVUserProfile GetUser(string cn)
        {
            UTRGVUserProfile user = new UTRGVUserProfile();

            DirectoryEntry rootDSE = rootDSE = new DirectoryEntry("LDAP://" + _domain, _srvAcc, _srvPassword);
            DirectorySearcher search = new DirectorySearcher(rootDSE);
            search.Asynchronous = true;
            search.PageSize = 1001;// To Pull up more than 100 records.

            search.Filter = "(&(&(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2)(cn=" + cn + "*))(|(memberOf=CN=utrgv-staff,OU=Groups,DC=ad,DC=utrgv,DC=edu)(memberOf=CN=utrgv-faculty,OU=Groups,DC=ad,DC=utrgv,DC=edu)memberOf=CN=utrgv-students,OU=Groups,DC=ad,DC=utrgv,DC=edu))";//UserAccountControl will only Include Non-Disabled Users.
            SearchResultCollection result = search.FindAll();


            foreach (SearchResult item in result)
            {
                if (item.Properties["displayName"].Count > 0)
                    user.Name = item.Properties["displayName"][0].ToString();
                if (item.Properties["cn"].Count > 0)
                    user.Cn = item.Properties["cn"][0].ToString();
                if (item.Properties["mail"].Count > 0)
                    user.Email = item.Properties["mail"][0].ToString();
                
            }

            return user;
        }
    }
}