#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "635d33485c4b08a49260f4934e07c30d1a16a840"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_ManageUser), @"mvc.1.0.view", @"/Views/User/ManageUser.cshtml")]
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
#line 1 "G:\enableIT\SALEERP_OLD\SALEERP\Views\_ViewImports.cshtml"
using SALEERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\Views\_ViewImports.cshtml"
using SALEERP.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"635d33485c4b08a49260f4934e07c30d1a16a840", @"/Views/User/ManageUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_User_ManageUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SALEERP.ViewModel.UserLoginVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
  
    ViewBag.Title = "Add User";
    if (this.ViewContext.FormContext == null)
    {
        this.ViewContext.FormContext = new FormContext();
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Add Login User</h2>\r\n\r\n\r\n");
#nullable restore
#line 13 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
 using (Html.BeginForm("Index", "User"))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-horizontal\">\r\n        <h4>Staff</h4>\r\n        <hr />\r\n        ");
#nullable restore
#line 20 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
   Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 22 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
       Write(Html.LabelFor(model => model.UserName, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 24 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.EditorFor(model => model.UserName, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 25 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.ValidationMessageFor(model => model.UserName, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 30 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
       Write(Html.LabelFor(model => model.UserPass, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 32 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.EditorFor(model => model.UserPass, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 33 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.ValidationMessageFor(model => model.UserPass, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 38 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
       Write(Html.LabelFor(model => model.UserEmail, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 40 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.EditorFor(model => model.UserEmail, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 41 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.ValidationMessageFor(model => model.UserEmail, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 46 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
       Write(Html.LabelFor(model => model.RoleId, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 48 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.DropDownListFor(model => model.RoleId,Model.loginroles as IEnumerable<SelectListItem>, "Select"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 49 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
           Write(Html.ValidationMessageFor(model => model.RoleId, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
        </div>

        <div class=""form-group"">
            <div class=""col-md-offset-2 col-md-10"">
                <input id=""submit"" type=""submit"" value=""Create"" class=""btn btn-default"" />
            </div>
        </div>
    </div>
");
#nullable restore
#line 59 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
#nullable restore
#line 62 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\ManageUser.cshtml"
Write(Html.ActionLink("Back to List", "Index"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
</div>

<script>
    $(""#submit"").click(function () {
        var form = $(""form"")
            .removeData(""validator"") /* added by the raw jquery.validate plugin */
            .removeData(""unobtrusiveValidation"");  /* added by the jquery unobtrusive plugin*/

        $.validator.unobtrusive.parse(form);

        form.data('unobtrusiveValidation');
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SALEERP.ViewModel.UserLoginVM> Html { get; private set; }
    }
}
#pragma warning restore 1591