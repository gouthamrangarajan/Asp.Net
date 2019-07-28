var chatMsg={
    template:`
    <transition name="fade" appear>
        <article class="media">
             <p class="media-left"  v-if="!isCurUser">
               <small>{{date | formatDate(padZero)}} </small>
               <br/>
               <span class="tag is-medium is-info">{{user|dsp}}</span>
            </p>
            <div class="media-content">
                <div class="content">
                <p class="control padding-1">
                    <textarea class="textarea has-fixed-size" readonly v-bind:value="msg">                    
                    </textarea>
                </p>
                </div>
            </div>
            <div class="media-right" v-if="isCurUser">
                <small>{{date | formatDate(padZero)}} </small>
                <br />
                <span class="tag is-medium is-info">You</span>
            </div>
        </article>
    </transition>
    `,
    props:{
        user:{
            type:Object,
            required:true
        },
        msg:{
            type:String,
            required:true
        },
        date:{
            type:Date,
            required:true
        },
        isCurUser:{
            type:Boolean,
            required:true
        }
    },
    mounted(){
        console.log(this.user);
        console.log(this.msg);
    },
    filters:{
        dsp(value){
            return value.firstName.substring(0,1)+value.lastName.substring(0,1);
        },
        formatDate(value,padZero){
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
            }
    }
}