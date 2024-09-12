import{_ as c,f as g,o as a,c as i,a as o,z as f,w as d,h as p,F as b,g as x,t as v}from"./index-e5788531.js";import{T as m}from"./tagify-d17b84b2.js";const _={data(){return{user:null,departments:null,groups:null,token:null,groupTags:null,updateUserCheck:!1}},watch:{addUserCheck(s,e){s==!0&&this.$nextTick(()=>{this.loadGroups()})},updateUserCheck(s,e){s==!0&&this.$nextTick(()=>{this.loadUpdateGroups()})}},created(){this.token=this.authStore.getToken;var s=this.mainStore.getAdmins,e=this.$route.params.id;this.user=s.filter(n=>n._id==e)[0],console.log(`this is the user found: ${this.user.empName}`);var t=this.mainStore.getDepartments;console.log(`departments from the store: ${t}`),this.departments=t.filter(n=>n.email==null),this.groups=this.mainStore.getGroups,console.log(`departments from this instance: ${this.departments}`),this.groups=this.mainStore.getGroups,console.log(`this is from the groups inside update ${this.groups}`),this.updateUserCheck=!0,this.token=this.authStore.getToken,console.log(`this is the token from update users: ${this.token}`)},methods:{updateUser:function(){var s=this;this.user.groups=this.groupTags.value.map(function(t){return t.value});var e={user:this.user,token:this.token};g.post(s.globalUrl+"updateUser",e).then(t=>{console.log(t),this.$router.push("/admin/users")},t=>{console.log(t)})},loadGroups(){var s=[];console.log("from tagify function"),console.log(this.groups.length);for(let t of this.groups)s.push(t.name),console.log("inside for loop"),console.log(t.name);var e=document.querySelector("#groups");this.groupTags=new m(e,{whitelist:s,maxTags:10,enforceWhitelist:!0,dropdown:{maxItems:20,classname:"tags-look",enabled:0,closeOnSelect:!1}})},loadUpdateGroups(){var s=[];console.log("from tagify function"),console.log(this.groups.length);for(let t of this.groups)s.push(t.name),console.log("inside for loop"),console.log(t.name);var e=document.querySelector("#updateGroups");this.groupTags=new m(e,{whitelist:s,maxTags:10,enforceWhitelist:!0,dropdown:{maxItems:20,classname:"tags-look",enabled:0,closeOnSelect:!1}})}}},k={class:"flex justify-center items-center"},w={class:"shadow-2xl hover:shadow-blue-600 w-1/2 h-auto flex flex-col justify-center items-center bg-white"},y=o("div",{class:"flex flex-row justify-center items-start w-full mb-10 bg-blue-500 text-white p-3"},[o("span",{class:"text-4xl font-bold"},"Update User")],-1),U=o("label",{class:"mb-20 font-bold"},"Email Address",-1),T=o("label",{class:"mb-20 font-bold mt-10"},"Department",-1),S={id:"departments"},G=["value"],$=o("label",{class:"mb-20 font-bold mt-10"},"Groups",-1),C=["value"],V=o("br",{class:"border border-solid border-black"},null,-1),A=o("button",{type:"submit",class:"w-full mt-8 h-11 bg-blue-500 rounded-md text-2xl text-white font-bold mb-2"},"Update",-1);function D(s,e,t,n,l,u){return a(),i("div",k,[o("div",w,[y,o("form",{onSubmit:e[3]||(e[3]=f((...r)=>u.updateUser&&u.updateUser(...r),["prevent"])),class:"px-4"},[U,d(o("input",{"onUpdate:modelValue":e[0]||(e[0]=r=>l.user.mailAddress=r),required:"",class:"w-full h-11 mb-4 border border-solid border-gray-200"},null,512),[[p,l.user.mailAddress]]),T,d(o("input",{list:"departments",autocomplete:"off",name:"department",id:"department","onUpdate:modelValue":e[1]||(e[1]=r=>l.user.department=r),type:"text",class:"mb-4 h-11 w-full border r-solid border-gray-200"},null,512),[[p,l.user.department]]),o("datalist",S,[(a(!0),i(b,null,x(l.departments,(r,h)=>(a(),i("option",{key:h,value:r.name},v(r.name),9,G))),128))]),$,o("input",{type:"text",name:"groups",id:"updateGroups",value:l.user.groups,class:"mb-4 h-11 w-full border border-solid border-gray-200"},null,8,C),V,A,o("button",{onClick:e[2]||(e[2]=r=>this.$router.back()),class:"w-full h-11 bg-gray-300 rounded-md text-2xl text-gray-500 font-bold mb-10"},"Return")],32)])])}const B=c(_,[["render",D]]);export{B as default};