#pragma checksum "C:\Users\gfernandes\source\repos\PontoColeta\PontoColeta\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c183478412e2ff012a68d1ea8aaa0c296378f085"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
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
#nullable restore
#line 1 "C:\Users\gfernandes\source\repos\PontoColeta\PontoColeta\Views\_ViewImports.cshtml"
using PontoColeta;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\gfernandes\source\repos\PontoColeta\PontoColeta\Views\_ViewImports.cshtml"
using PontoColeta.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c183478412e2ff012a68d1ea8aaa0c296378f085", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aefe57c3f6da09edfa5a319a6c510b04e6cdb356", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\gfernandes\source\repos\PontoColeta\PontoColeta\Views\Home\About.cshtml"
  
    ViewData["Title"] = "Sobre o projeto";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 4 "C:\Users\gfernandes\source\repos\PontoColeta\PontoColeta\Views\Home\About.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<p>
    O nosso principal objetivo aqui é criar uma aplicação com o intuito de ajudar na distribuição de informações [locais] onde possam ser realizado descartes da forma correta.  O Ponto Coleta faz o meio de campo entre o consumidor e os estabelecimentos que recebem algum tipo de material/produto que precise de um descarte mais adequado que o convencional.
</p>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
