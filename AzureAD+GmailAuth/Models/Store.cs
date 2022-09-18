using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AzureAD_GmailAuth.Models
{
    //RG should actually write to a db, temporarily writing to file for POC purpose    
    public class UserStore : IUserStore<IdentityUser>
    {
        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {                        
            if(user==null)
                throw new ArgumentNullException(nameof(user));
            
            if(File.Exists("users.txt"))
            {
                var lines=await File.ReadAllLinesAsync("users.txt");            
                foreach(var line in lines){
                    var userObj=JsonSerializer.Deserialize<IdentityUser>(line);
                    if(userObj.Id==user.Id)
                        return IdentityResult.Success;
                }
            }
            
            await File.AppendAllLinesAsync("users.txt",new List<string>{JsonSerializer.Serialize(user)});
            return IdentityResult.Success;
        }

        public  async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {            
            var lines=await File.ReadAllLinesAsync("users.txt");
            var userList=new List<IdentityUser>();
            foreach(var line in lines){
                var userObj=JsonSerializer.Deserialize<IdentityUser>(line);
                if(userObj.Id!=user.Id)
                    userList.Add(userObj);
            }
            await File.WriteAllTextAsync("users.txt","");
            foreach(var userObj in userList){
                await File.AppendAllLinesAsync("users.txt",new List<string>{JsonSerializer.Serialize(userObj)});
            }
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            
        }

        //there is a scenario where this method is called by Auth Code to figure out User from UserStore 
        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {            
            var lines=await File.ReadAllLinesAsync("users.txt");            
            foreach(var line in lines){
                var userObj=JsonSerializer.Deserialize<IdentityUser>(line);
                if(userObj.Id==userId)
                    return userObj;
            }
            throw new Exception("User Not Found ");
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