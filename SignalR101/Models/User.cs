using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalR101.Models{
    public class User{
        public string FirstName { get; set; }
        public string LastName{get;set;}

        public string Id {get;set;}
    }
      
    public class UserCollection{
        private List<User> data;
        
        public UserCollection(){
            data=new List<User>();
        }
        public void Add(User user){
            data.Add(user);
        }
        public IEnumerable<dynamic> GetAllUsers(){
            return data.Select(f=>new {FirstName=f.FirstName,LastName=f.LastName,Date=DateTime.Now}).Cast<dynamic>();
        }
        public User Find(string firstName,string lastName){
            return data.Find(iel=>
                        iel.FirstName.ToUpper()==firstName.ToUpper()
                        &&
                        iel.LastName.ToUpper()==lastName.ToUpper()
                    );
        }
        public User FindAnother(string firstName,string lastName,string id){
            return data.Find(iel=>
                        iel.FirstName.ToUpper()==firstName.ToUpper()
                        &&
                        iel.LastName.ToUpper()==lastName.ToUpper()
                        && 
                        iel.Id!=id
                    );
        }
        public bool Find(User user){
            if(data.FindIndex((ur)=>{
                return ur.Id==user.Id;
            })>=0){
                return true;
            }
            return false;
        }
    }
}