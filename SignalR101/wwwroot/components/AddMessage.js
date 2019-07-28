var addMessage={
    template:`
    <form>
        <div class="field">
            <p class="control">
                <textarea class="textarea" placeholder="Enter a message..."                 
                v-model.trim="msg" @keyup.enter.exact="send"></textarea>
            </p>
        </div>
        <nav class="level">
            <div class="level-left">
            </div>
            <div class="level-right">
                <div class="level-item">
                    <a class="button is-link" @click.stop="send">Send</a>
                </div>
            </div>
        </nav>
    </form>
    `,
    data(){
        return {msg:''}
    },
    methods:{
        send(){            
            if(this.msg!=""){
                this.$emit('send',this.msg);                
                this.msg='';
            }
        }
    }
}