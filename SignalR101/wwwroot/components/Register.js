var register={
    template:`
 <div>
    <h3 class="has-text-centered has-text-info is-size-3">Hi...</h3>    
    <div class="box">
        <div class="field">
            <label class="label">First Name</label>
            <div class="control">
             <input :class="{'input':true,'is-danger':firstName.isError}" type="text" 
                 v-model.lazy.trim="firstName.value" @keyup.enter="submit">
            </div>
            <transition name="fade">
                <p class="help is-danger" v-if="firstName.isError">Please enter valid first name </p>
            </transition>
        </div>
        <div class="field">
            <label class="label">Last Name</label>
            <div class="control">
                <input :class="{'input':true,'is-danger':lastName.isError}" type="text" 
                v-model.lazy.trim="lastName.value" @keyup.enter="submit">
            </div>
            <transition name="fade">
                <p class="help is-danger" v-if="lastName.isError">Please enter valid last name </p>
            </transition>
        </div>
        <a class="button is-fullwidth is-primary is-medium" @click.stop="submit">Join</a>
    </div>    
  </div>
    `,
    data(){
        return {
            firstName:{value:'',isError:false},
            lastName:{value:'',isError:false}
        }
    },
    methods:{
        submit(){            
            if(this.firstName.value!='' && this.lastName.value!=''){
                this.firstName.isError=false;
                this.lastName.isError=false;
                this.$emit('submit',{firstName:this.firstName.value,lastName:this.lastName.value});
            } 
            else{
              if(this.firstName.value==''){
                  this.firstName.isError=true;
              }   
              else{
                this.firstName.isError=false;
              }
              if(this.lastName.value==''){
                  this.lastName.isError=true;
              }
              else{
                this.lastName.isError=false;
              }
            }
        }
    },
    watch:{
        firstName(newVal,oldVal){
            if( newVal.value!=oldVal.value && newVal.value!=''){
                newVal.isError=false;
            }
        },
        lastName(newVal,oldVal){
            if( newVal.value!=oldVal.value && newVal.value!=''){
                newVal.isError=false;
            }
        }
    }
}