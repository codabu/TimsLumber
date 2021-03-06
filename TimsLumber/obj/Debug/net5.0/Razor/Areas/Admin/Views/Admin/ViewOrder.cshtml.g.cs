#pragma checksum "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca77abaea3a840ff4ccecb045f12fe1f9cac81cd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Admin_ViewOrder), @"mvc.1.0.view", @"/Areas/Admin/Views/Admin/ViewOrder.cshtml")]
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
#line 1 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\_ViewImports.cshtml"
using TimsLumber;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\_ViewImports.cshtml"
using TimsLumber.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca77abaea3a840ff4ccecb045f12fe1f9cac81cd", @"/Areas/Admin/Views/Admin/ViewOrder.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27d583e6a54ab1d37c90bc65efd65c7b811700cc", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Admin_ViewOrder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TimsLumber.Areas.Admin.Models.AdminOrderViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Orders", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
  
    ViewData["Title"] = "Tim's Lumber - Order Summary";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-12\">\r\n        <h3>Order Id: ");
#nullable restore
#line 9 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                 Write(Model.Order.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n        <h4>Ordered by: ");
#nullable restore
#line 10 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                   Write(Model.UserNames[0]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n        <h4>Contact email address: ");
#nullable restore
#line 11 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                              Write(Model.Emails[0]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
        <table class=""table table-bordered table-striped mt-2"">
            <thead>
                <tr>
                    <th>Size</th>
                    <th>Length</th>
                    <th>Cost</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 21 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                 foreach (OrderItem OI in Model.Order.OrderItems)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 24 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                       Write(OI.LumberItem.NominalSize);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - (");
#nullable restore
#line 24 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                                                     Write(OI.LumberItem.ActualSize);

#line default
#line hidden
#nullable disable
            WriteLiteral(")</td>\r\n                        <td>");
#nullable restore
#line 25 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                       Write(OI.Length);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ft</td>\r\n                        <td>");
#nullable restore
#line 26 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                       Write(OI.Cost.ToString("C2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 28 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n        <h4 class=\"text-right\">Subtotal: ");
#nullable restore
#line 31 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                                    Write(Model.Order.Subtotal.ToString("C2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n        <h4 class=\"text-right\">Tax: ");
#nullable restore
#line 32 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                               Write(Model.Order.Tax.ToString("C2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n        <h4 class=\"text-right\">Total: ");
#nullable restore
#line 33 "C:\Users\Corry\Desktop\Spring 2021 Classes\Advanced C#\Final Project\TimsLumber\Areas\Admin\Views\Admin\ViewOrder.cshtml"
                                 Write(Model.Order.Total.ToString("C2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca77abaea3a840ff4ccecb045f12fe1f9cac81cd8240", async() => {
                WriteLiteral("\r\n            <input type=\"button\" class=\"btn btn-primary\" value=\"Back to orders\" />\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TimsLumber.Areas.Admin.Models.AdminOrderViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
