#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2f308fb03e19220b8f85fc78282ade43dfaa2c6f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pool_ManagePool), @"mvc.1.0.view", @"/Views/Pool/ManagePool.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f308fb03e19220b8f85fc78282ade43dfaa2c6f", @"/Views/Pool/ManagePool.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_Pool_ManagePool : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SALEERP.ViewModel.PoolVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
  
    ViewBag.Title = "Add Pool";
    if (this.ViewContext.FormContext == null)
    {
        this.ViewContext.FormContext = new FormContext();
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 11 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
 using (Html.BeginForm("Index", "Pool"))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-horizontal\">\r\n        <h4>Staff</h4>\r\n        <hr />\r\n        ");
#nullable restore
#line 18 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
   Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 20 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
       Write(Html.LabelFor(model => model.PoolName, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 22 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.EditorFor(model => model.PoolName, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 23 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.ValidationMessageFor(model => model.PoolName, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 28 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
       Write(Html.LabelFor(model => model.PoolDesc, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 30 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.EditorFor(model => model.PoolDesc, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 31 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.ValidationMessageFor(model => model.PoolDesc, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 36 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
       Write(Html.LabelFor(model => model.PoolStartDate, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 38 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.EditorFor(model => model.PoolStartDate, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 39 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.ValidationMessageFor(model => model.PoolStartDate, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 44 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
       Write(Html.LabelFor(model => model.PoolEndDate, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 46 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.EditorFor(model => model.PoolEndDate, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 47 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
           Write(Html.ValidationMessageFor(model => model.PoolEndDate, "", new { @class = "text-danger" }));

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
#line 57 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
#nullable restore
#line 60 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Pool\ManagePool.cshtml"
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

    //Datemask dd/mm/yyyy
    $('#datemask').inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' })
    //Datemask2 mm/dd/yyyy
    $('#datemask2').inputmask('mm/dd/yyyy', { 'placeholder': 'mm/dd/yyyy' })
    //Money Euro
    $('[data-mask]').inputmask()
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SALEERP.ViewModel.PoolVM> Html { get; private set; }
    }
}
#pragma warning restore 1591