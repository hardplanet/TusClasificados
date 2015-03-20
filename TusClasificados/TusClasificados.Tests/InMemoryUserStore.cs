using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusClasificados.Site.Models;

namespace TusClasificados.Tests
{
    public class InMemoryUserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        public Dictionary<string, ApplicationUser> _users = new Dictionary<string, ApplicationUser>();

        public IQueryable<ApplicationUser> Users
        {
            get { return _users.Values.AsQueryable(); }
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return
                 Task.FromResult(
                     Users.FirstOrDefault(u => u.Email == email));
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationUser> IUserStore<ApplicationUser, string>.FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationUser> IUserStore<ApplicationUser, string>.FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
