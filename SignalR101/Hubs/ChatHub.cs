using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Internal;
using SignalR101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalR101.Hubs
{
    public class ChatHub:Hub
    {
        private UserCollection _users;
        private ActiveChat _activeChats;
        public ChatHub(UserCollection users,ActiveChat activeChats)
        {
            _users=users;
            _activeChats=activeChats;
        }

        public async Task AddUser(User user)
        {            
            user.Id=Context.ConnectionId;
            if(!_users.Find(user))
            {
                _users.Add(user);
            }                                   
            _activeChats.FindExistingChats(user).ForEach(async grp=>{
                await Groups.AddToGroupAsync(user.Id,grp.ToString());
            });
            await Clients.All.SendAsync("UserAdded",_users.GetAllUsers());     
        }

        public async Task StartConversation(List<User> users){                        
            var exists=false;
            var guid=_activeChats.StartChat(users,out exists);
             if(!exists){                
              users.ForEach(async usr=>{                    
                    await Groups.AddToGroupAsync(usr.Id,guid.ToString());
                });                
             }            
            await Clients.Group(guid.ToString()).SendAsync("ConversationStarted",new {id=guid.ToString(),
                users=users.Select(el=>new {FirstName=el.FirstName,LastName=el.LastName})});
        }
        public async Task SendMessage(User user,string guid,string message)
        {        
           await Clients.Group(guid).SendAsync("MessageReceived",new {message=message,id=guid,user=user});
        }
    }

}