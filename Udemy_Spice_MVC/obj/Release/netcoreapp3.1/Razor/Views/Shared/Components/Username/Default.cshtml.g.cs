#pragma checksum "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Views\Shared\Components\Username\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "126cdd23e3a7f4e8f29ddc99d2cb4b637c199cce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Username_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Username/Default.cshtml")]
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
#line 1 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Views\_ViewImports.cshtml"
using Udemy_Spice_MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Views\_ViewImports.cshtml"
using Udemy_Spice_MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"126cdd23e3a7f4e8f29ddc99d2cb4b637c199cce", @"/Views/Shared/Components/Username/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"efa37195c6acc649f9db190a1c2346429b383f60", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Username_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Udemy_Spice_MVC.Models.ApplicationUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("Hi! ");
#nullable restore
#line 2 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Views\Shared\Components\Username\Default.cshtml"
Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fas fa-user\"></i>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Udemy_Spice_MVC.Models.ApplicationUser> Html { get; private set; }
    }
}
#pragma warning restore 1591