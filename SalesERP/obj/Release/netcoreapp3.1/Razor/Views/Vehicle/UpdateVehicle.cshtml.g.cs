#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5c08aafc8ee030c18ed61da9646242f5dc1340a5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vehicle_UpdateVehicle), @"mvc.1.0.view", @"/Views/Vehicle/UpdateVehicle.cshtml")]
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
#line 1 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\_ViewImports.cshtml"
using SALEERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\_ViewImports.cshtml"
using SALEERP.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c08aafc8ee030c18ed61da9646242f5dc1340a5", @"/Views/Vehicle/UpdateVehicle.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_Vehicle_UpdateVehicle : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SALEERP.ViewModel.VehicleVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
  
    ViewBag.Title = "Update Vehicle";
    if (this.ViewContext.FormContext == null)
    {
        this.ViewContext.FormContext = new FormContext();
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 10 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
 using (Html.BeginForm("Update", "Vehicle"))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-horizontal\">\r\n        <h4>Vehicle</h4>\r\n        <hr />\r\n        ");
#nullable restore
#line 17 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
   Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 19 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
       Write(Html.LabelFor(model => model.VehicleType, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 21 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
           Write(Html.EditorFor(model => model.VehicleType, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 22 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
           Write(Html.ValidationMessageFor(model => model.VehicleType, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n       \r\n\r\n        <div class=\"form-group\">\r\n            <div class=\"col-md-offset-2 col-md-10\">\r\n                ");
#nullable restore
#line 30 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
           Write(Html.HiddenFor(model => model.VehicleId, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <input id=\"submit\" type=\"submit\" value=\"Update\" class=\"btn btn-default\" />\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 35 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
#nullable restore
#line 38 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Vehicle\UpdateVehicle.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SALEERP.ViewModel.VehicleVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
