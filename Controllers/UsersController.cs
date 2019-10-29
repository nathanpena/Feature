using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Features.Models;
using Features.Providers;
using Features.ViewModels;
using AutoMapper;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;
using Features.Common;
using Microsoft.Ajax.Utilities;

namespace Features.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly ILoginProvider _loginProvider;
        //private readonly IUtils _utils;
        private UTRGVAppContext db = new UTRGVAppContext();


        public UsersController(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
        }

        [Authorize]
        [HttpGet, Route("me")]
        public async Task<IHttpActionResult> me()
        {
            UTRGVUserProfile user = _loginProvider.GetUser(User.Identity.Name);
            var dbUser = await db.Users.Where(u => u.Cn == user.Cn).FirstOrDefaultAsync();

            if (dbUser != null)
                user.Role = dbUser.Role.Name;
            else
                user.Role = "Faculty";


            return Ok(user);
        }



        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = Mapper.Map<UserViewModel, User>(user);

            db.Users.Add(userModel);
            await db.SaveChangesAsync();

            return Ok(db.Users.ProjectTo<UserViewModel>(user));

        }

        [Authorize]
        [HttpGet, Route("Find/{email}")]
        public IHttpActionResult Find(string email)
        {
            var users = _loginProvider.FindUserByEmail(email);
            var res = Mapper.Map<List<UserViewModel>>(users);
            return Ok(res);
        }
    }
}
