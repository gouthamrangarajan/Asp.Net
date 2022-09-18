using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AzureAD_GmailAuth.Models
{
    public class UserStore : IUserStore<IdentityUser>
    {
        public  Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {                        
            throw new NotImplementedException();
        }

        public  Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {            
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public  Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public  Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {         
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {         
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }
    }

    public class RoleStore : IRoleStore<IdentityRole>
    {
        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {         
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {            
            throw new NotImplementedException();
        }
    }
}