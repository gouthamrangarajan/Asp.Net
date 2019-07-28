using System;
using System.Collections.Generic;

namespace SignalR101.Models{

  public class ActiveChat{      
        private UserCollection _users;
        public ActiveChat(UserCollection users){
            _users=users;
        }

        //RG, active chats, list of user and guid for each chat
        private Dictionary<Guid,List<User>> chats=new Dictionary<Guid, List<User>>();

        private void AttachUserId(List<User> users){
            users.ForEach(usr=>{
                if(string.IsNullOrWhiteSpace(usr.Id)){
                    var found=_users.Find(usr.FirstName,usr.LastName);
                    usr.Id=found.Id;
                }
            });
        }
        //RG, if chat is already available bring the guid 
        //else start one
        public Guid StartChat(List<User> users,out bool exists){        
            AttachUserId(users);    
            exists=false;
            var guid=Guid.NewGuid();
            List<bool> found=new List<bool>();  
            foreach(var dicItem in chats){  
                var item=dicItem.Value;
                found.Clear();                            
                users.ForEach(pel=>{                    
                   if(item.FindIndex(iel=>
                        iel.Id==pel.Id
                    )>-1){
                        found.Add(true);
                    }
                });
                if(found.Count==users.Count){
                    exists=true;
                    guid=dicItem.Key;
                    break;
                }
            }
            if(found.Count!=users.Count){
                chats.Add(guid,users);
            }
            return guid;
        }

        public List<Guid> FindExistingChats(User user){            
            List<Guid> guids=new List<Guid>();
            foreach(var kvp in chats){
                kvp.Value.ForEach(elUser=>{
                    if(elUser.FirstName.ToUpper()==user.FirstName.ToUpper()
                        &&
                        elUser.LastName.ToUpper()==user.LastName.ToUpper()
                     )
                    {
                        guids.Add(kvp.Key);
                    }
                });
            }
            return guids;
        }
    }
}