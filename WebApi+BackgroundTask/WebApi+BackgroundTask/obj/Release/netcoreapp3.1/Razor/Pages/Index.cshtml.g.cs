#pragma checksum "C:\RG\github\asp.net\WebApi+BackgroundTask\WebApi+BackgroundTask\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "37e96ee6a2296b2b367a72016e37caa5e8ccaece"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37e96ee6a2296b2b367a72016e37caa5e8ccaece", @"/Pages/Index.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\RG\github\asp.net\WebApi+BackgroundTask\WebApi+BackgroundTask\Pages\Index.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "37e96ee6a2296b2b367a72016e37caa5e8ccaece2884", async() => {
                WriteLiteral(@"
    <meta name=""viewport"" content=""width=device-width"" />
    <title>WebApiPlusWorker</title>
    <link href=""https://cdnjs.cloudflare.com/ajax/libs/tailwindcss/1.4.6/tailwind.min.css""  rel=""stylesheet""/>
    <style>
        [v-cloak]{
            display:none;
        }
        .records-move{
            transition:all 0.3s;
        }
        .records-enter-active,
        .records-leave-active{
            transition:all 0.3s;
        }
        .records-enter,
        .records-leave-to{
            opacity:0;
            transform:translateX(2rem);
        }
        section{
            height:35rem;
        }
        
    </style>

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "37e96ee6a2296b2b367a72016e37caa5e8ccaece4525", async() => {
                WriteLiteral(@"
    <div id=""app"" class=""h-screen bg-gray-100 font-sans"" v-cloak>
        <nav class=""flex flex-row m-auto items-center justify-center py-6 px-8"">
            <span class=""text-2xl text-teal-700 font-semibold"">{{header}}</span>
        </nav>
        <section class=""flex flex-row space-x-2  py-10 px-12 justify-start items-start"">
            <transition-group name=""records"" tag=""div"" class=""flex flex-col space-y-2"">
                <a v-for=""(num,ind) in numbers.slice(0,10)"" :key=""ind"" class=""text-md text-center text-white shadow py-2 px-4
                       bg-blue-700 hover:bg-blue-500 rounded-full cursor-pointer"">
                    {{num}}
                </a>
            </transition-group>
            <div v-for=""sq in 9"">
                <transition-group name=""records"" tag=""div"" class=""flex flex-col space-y-2"">
                    <a v-for=""(num,ind) in numbers.slice(10*sq,(10*sq)+10)"" :key=""ind"" class=""text-md text-center text-white shadow py-2 px-4 
                       bg-blu");
                WriteLiteral(@"e-700 hover:bg-blue-500 rounded-full cursor-pointer"">
                        {{num}}
                    </a>
                </transition-group>
            </div>
        </section>
        <div class=""flex flex-row m-auto items-center justify-start py-6 px-8"">
            <span class=""text-base text-orange-700 py-2 px-2 border-l-8 rounded border-pink-500""> Numbers get refreshed via worker service & api call every second </span>
        </div>
    </div>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/vue/2.6.11/vue.min.js"" type=""text/javascript""></script>
    <script type=""text/javascript"">
        const app = new Vue({
            el: '#app',
            data: {
                header: 'WebApiPlusWorker',
                numbers: [],
                intr: {}
            },
            created() {
                this.intr = setInterval(() => {                    
                    fetch('/values').then(response => response.json()).then(data => {                        
   ");
                WriteLiteral(@"                     this.numbers.splice(0);                        
                        data.forEach(dt => this.numbers.push(dt));
                    }).catch(err => {
                        console.log(err)
                    });
                }, 1000);
            },
            destroyed() {
                clearInterval(this.intr)
            },
            methods: {

            },
            
        })
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_Index> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_Index> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_Index>)PageContext?.ViewData;
        public Pages_Index Model => ViewData.Model;
    }
}
#pragma warning restore 1591
