const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
const app=new Vue({
    el:'#app',
    data(){
        return {msg:'Welcome',isRegistered:false,users:[],
            loggedInMessages:[],curUser:{},conversations:[],
            chatMessages:[],
            curChat:null}
    },
    components:{
        register,loggedInMsg,chatMsg,addMessage,openChat
    },
    mounted(){
         connection.start().catch(err=> {
            return console.error(err.toString());
        });
    },
    methods:{
        addChatMsgs(usr,msg,convid){            
            this.chatMessages.push({user:usr,message:msg,date:new Date(),convid:convid});            
        },
        clearAndAddLoggedInMsgs(usrs,msg){    
            this.loggedInMessages.splice(0);      
            usrs.forEach(el=>{                
                this.loggedInMessages.push({user:el,message:'',date:el.date});            
            })              
        },
        clearAndAddUsers(users){
            this.users.splice(0);
            users.forEach(el=>{
                this.users.push(el);
            });
        },
        async sendMessage(msg){
               await connection.invoke("SendMessage",this.curUser,this.curChat,msg).catch(err=> {
                    return console.error(err.toString());
                }); 
        },
        async startConversation(user){             
            var arr=[this.curUser,user];            
            await connection.invoke("StartConversation",arr).catch(err=> {
                return  console.error(err.toString());
            }); 
                
        },
        chngConv(convid){
            this.curChat=this.conversations.filter(el=>el.id==convid)[0].id;
        },
       async register(user){
            this.curUser=user;            
            connection.on("UserAdded",(users)=>{        
                this.clearAndAddUsers(users);
                this.clearAndAddLoggedInMsgs(users,'');
            });
            connection.on("MessageReceived",data=>{                                
                this.addChatMsgs(data.user,data.message,data.id);
                if(this.conversations.filter(el=>el.id==data.id).length==0){
                    var conv={};
                    conv.id=data.id;
                    conv.users=[];
                    conv.users.push(data.user);                    
                    this.conversations.push(conv);                   
                }
                if(this.conversations.length==1){
                    this.curChat=this.conversations[0].id;
                }              
            });          
            connection.on("ConversationStarted",data=>{                       
                var conv={};
                conv.id=data.id;
                conv.users=[];
               data.users.filter(el=>
                el.firstName.toString().toLowerCase()!=this.curUser.firstName.toString().toLowerCase()
                && el.lastName.toString().toLowerCase()!=this.curUser.lastName.toString().toLowerCase())
                .forEach(el=>{                    
                    conv.users.push(el);                    
               });               
               this.conversations.push(conv);   
                if(this.conversations.length==1){
                    this.curChat=this.conversations[0].id;
                }              
            });  
            await connection.invoke("AddUser", user).catch(err=> {
                return  console.error(err.toString());
             });
            this.isRegistered=true;
        },
        checkCurUser(user){        
            return this.curUser.firstName==user.firstName && this.curUser.lastName==user.lastName;
        }
    },
    computed:{
        loggedInMsgs(){
            return this.loggedInMessages.filter(el=>el.message=='');
        },
        chatMsgs(){
            return this.chatMessages.filter(el=>el.message!='' && el.convid==this.curChat);
        },
        conversationStarted(){
            return this.conversations.length>0;
        },    
    }
});
