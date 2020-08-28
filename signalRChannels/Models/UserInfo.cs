using System;
using System.Collections.Generic;

namespace signalRChannels.Models{
    public class UserCollection{
        private Dictionary<string,UserInfo> Data=new Dictionary<string, UserInfo>();

        public bool AddUser(string id){
            if(Data.ContainsKey(id))
                return false;
            
            Data.Add(id,new UserInfo(id));
            return true;            
        }
        public bool RemoveUser(string id){
            if(Data.ContainsKey(id)){
                Data.Remove(id);
                return true;
            }
            return false;
        }
        private class UserInfo
        {
            internal string Id{get;}

            internal string Name{get;private set;}
            internal UserInfo(string id){
                Id=id;
                Name="";
            } 
            internal void AddName(string name){
                Name=name;
            }
            public override int GetHashCode()=> Id.GetHashCode();

            public override string ToString()=>Id+Name;
            
        }
    }
    
}