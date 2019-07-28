var loggedInMsg={
    template:`
    <transition name="fade" appear>
        <article class="message is-success is-pointer">           
            <div class="message-header">
              {{date | formatDate(padZero)}} 
            </div>
            <div class="message-body">       
              <span v-if="!isCurUser">     
              {{user.firstName}} {{user.lastName}}
              </span>
              <span v-else>
                You
              </span>
               logged in<br/>       
              <div class="button"  @click.stop="userClicked" v-if="!isCurUser">
                Start Conversation
              </div>                  
          </div>            
        </article>
      </transition>
    `,
    mounted(){
      
    },
    props:{       
        user:{
          required:true,
          type:Object
        },
        date:{
          required:true,
          type:Date
        },
        isCurUser:{
          type:Boolean,
          required:true
        }
    },
    filters:{
      formatDate(value,padZero){
        value=new Date(value);
         var mnth=value.getMonth();
         mnth++;
         var mnthStr=padZero(mnth);                  
         var dayStr=padZero(value.getDate());         
         var yrStr=value.getFullYear();
         var hrs=padZero(value.getHours());
         var mins=padZero(value.getMinutes());
         var secs=padZero(value.getSeconds());

         return mnthStr+'/'+dayStr+'/'+yrStr+' '+hrs+':'+mins+':'+secs;
      }
    },
    methods:{
      padZero(num){
        if(num<10){
          return '0'+num;
        }
        return num;
      },
      userClicked(){
        if(!this.isCurUser){
          this.$emit('clicked',this.user);
        }
      }
    }
}
