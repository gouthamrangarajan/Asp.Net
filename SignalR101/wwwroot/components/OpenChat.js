var openChat={
    template:`
     <aside class="menu">
        <transition-group name="fade" tag="ul" class="menu-list">
         <li v-for="conv in convs" v-bind:key="conv.id">
           <a @click.stop="clicked(conv.id)" v-bind:class="{'is-active':cur==conv.id}">
                <span v-for="(usr,ind) in conv.users" v-bind:key="ind">
                    {{usr.firstName}} {{usr.lastName}}
                </span>
           </a>
         </li>
        </transition-group>
     </aside>
    `,
    props:{
        convs:{
            required:true,
            type:Array
        },
        cur:{
            required:true,
            type:String
        }
    },
    mounted(){        
    },
    methods:{
        clicked(convid){
            this.$emit('clicked',convid);
        }
    }
}