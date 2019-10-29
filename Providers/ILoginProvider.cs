using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using Features.Models;

namespace Features.Providers
{
    public interface ILoginProvider
    {
        bool ValidateCredentials(string userName, string password, out string cn, out bool authorized);
        UTRGVUserProfile GetUser(string cn);
        List<User> FindUserByEmail(string email);
        User generateUser(string cn, Role role);
    }
}