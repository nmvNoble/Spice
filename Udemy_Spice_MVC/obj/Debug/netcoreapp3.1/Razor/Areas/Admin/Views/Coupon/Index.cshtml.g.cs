#pragma checksum "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a792de79aef4fd25baee1d8ae026084b51821d4e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Coupon_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Coupon/Index.cshtml")]
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
#line 1 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\_ViewImports.cshtml"
using Udemy_Spice_MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\_ViewImports.cshtml"
using Udemy_Spice_MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a792de79aef4fd25baee1d8ae026084b51821d4e", @"/Areas/Admin/Views/Coupon/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"efa37195c6acc649f9db190a1c2346429b383f60", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Coupon_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Coupon>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_CreateButtonPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_TableButtonsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
  
    ViewData["Title"] = "Coupon Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n<br />\r\n<div class=\"border backgroundWhite\">\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n            <h2 class=\"text-info\"> Coupon List </h2>\r\n        </div>\r\n        <div class=\"col-6 text-right\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a792de79aef4fd25baee1d8ae026084b51821d4e4296", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <br />\r\n    <div>\r\n");
#nullable restore
#line 19 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
         if (Model.Count() > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <table class=\"table table-striped border\">\r\n                <tr class=\"table-secondary\">\r\n                    <th>\r\n                        ");
#nullable restore
#line 24 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 27 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 30 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.minimum_amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 33 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.is_active));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                    <th></th>\r\n                </tr>\r\n");
#nullable restore
#line 38 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 42 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                       Write(Html.DisplayFor(m => item.name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 45 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                       Write(Html.DisplayFor(m => item.discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 48 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                       Write(Html.DisplayFor(m => item.minimum_amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 51 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                       Write(Html.DisplayFor(m => item.is_active));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a792de79aef4fd25baee1d8ae026084b51821d4e9215", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 54 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = item.id;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 57 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n");
#nullable restore
#line 59 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p>No coupons exists...</p>\r\n");
#nullable restore
#line 63 "D:\Udemy\ASP.NET Core 3.1\Udemy_Spice_MVC\Udemy_Spice_MVC\Areas\Admin\Views\Coupon\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Coupon>> Html { get; private set; }
    }
}
#pragma warning restore 1591
