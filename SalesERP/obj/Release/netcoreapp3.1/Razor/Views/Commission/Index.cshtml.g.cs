#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5c7c743f3b7dbf5b71dca07d46a02a8581922f8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Commission_Index), @"mvc.1.0.view", @"/Views/Commission/Index.cshtml")]
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
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
using PagedList.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5c7c743f3b7dbf5b71dca07d46a02a8581922f8", @"/Views/Commission/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_Commission_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SALEERP.ViewModel.CommissionVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:red"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Pool", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline ml-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
  
    ViewBag.Title = "Add Commission";
    if (this.ViewContext.FormContext == null)
    {
        this.ViewContext.FormContext = new FormContext();
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""content"">
    <div class=""row"">
        <div class=""col-12"">
            <div class=""card"">
                <div class=""card-header"">
                    <div class=""card card-info"">
                        <div class=""card-body"">
");
#nullable restore
#line 18 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                             using (Html.BeginForm("GetMirror", "Commission"))
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                           Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <table id=\"dataTabledriver\"");
            BeginWriteAttribute("class", " class=\"", 715, "\"", 723, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <tr>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 812, "\"", 820, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <div class=\"form-group\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f88473", async() => {
                WriteLiteral("\r\n                                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f88785", async() => {
                    WriteLiteral("--Please select--");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 26 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.unitid);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 26 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Model.unitdetails;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                            </div>\r\n                                        </td>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 1329, "\"", 1337, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <div class=\"form-group\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f812547", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 34 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.fromdate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("control-label\"", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f814342", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 35 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.fromdate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f815896", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 36 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.fromdate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </div>\r\n                                        </td>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 1861, "\"", 1869, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <div class=\"form-group\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f817893", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 41 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Todate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("control-label\"", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f819686", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 42 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Todate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f821238", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 43 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Todate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </div>\r\n                                        </td>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 2387, "\"", 2395, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            <div class=""form-group"">
                                                <span class=""input-group-append"">

                                                    <button type=""submit"" class=""btn btn-info btn-flat"" style=""background-color:mediumseagreen;"">Show</button>
                                                </span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
");
#nullable restore
#line 56 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                        </div>\r\n\r\n\r\n\r\n\r\n                    </div>\r\n\r\n");
            WriteLiteral("\r\n                    <div class=\"col-md-10\">\r\n\r\n                    </div>\r\n                </div>\r\n");
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f824175", async() => {
                WriteLiteral("\r\n                    <div class=\"input-group input-group-sm\">\r\n                        <input class=\"form-control form-control-navbar\" type=\"search\" name=\"searchstring\"");
                BeginWriteAttribute("value", " value=\"", 4014, "\"", 4022, 0);
                EndWriteAttribute();
                WriteLiteral(@" placeholder=""Search"" aria-label=""Search"">
                        <div class=""input-group-append"">
                            <button class=""btn btn-navbar"" type=""submit"">
                                <i class=""fas fa-search""></i>
                            </button>
                        </div>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <div class=\"card-body\">\r\n                    <div class=\"text-center w-full p-t-25 p-b-80\">\r\n                        <span class=\"text-danger\">");
#nullable restore
#line 92 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                             Write(Html.ValidationSummary(false));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        <span asp-validation-summary=\"All\" class=\"text-danger\"></span>\r\n");
            WriteLiteral(@"                    </div>

                    <div style=""height: 200px; overflow: auto;"">
                        <table id=""example2"" class=""table table-bordered table-hover"">
                            <thead>

                                <tr style=""background-color:aquamarine;"">
                                    <th>
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5c7c743f3b7dbf5b71dca07d46a02a8581922f827548", async() => {
                WriteLiteral(" ");
#nullable restore
#line 105 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                                                                                          Write(Html.DisplayNameFor(model => model.Mirrors.FirstOrDefault().mirrorid));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sortOrder", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 105 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                                                       WriteLiteral(ViewData["NameSortParm"]);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sortOrder"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-sortOrder", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sortOrder"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 109 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Mirrors.FirstOrDefault().mirrordate));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    </th>
                                    <th>
                                        Principle
                                    </th>
                                    <th>
                                        Driver
                                    </th>
                                    <th>
                                        Escort
                                    </th>
");
            WriteLiteral("                                </tr>\r\n\r\n");
#nullable restore
#line 128 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                 foreach (var item in Model.Mirrors)
                                {
                                    string p_names = string.Empty, d_names = string.Empty, e_names = string.Empty, t_names = string.Empty, g_names = string.Empty;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 133 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.mirrorid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 136 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.mirrordate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n");
#nullable restore
#line 138 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                         foreach (var item1 in item.principle)
                                        {
                                            p_names += item1.name + ",";

                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>\r\n                                            ");
#nullable restore
#line 144 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                       Write(Html.Raw(p_names));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n");
#nullable restore
#line 146 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                         foreach (var item1 in item.driver)
                                        {
                                            d_names += item1.name + ",";

                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>\r\n                                            ");
#nullable restore
#line 152 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                       Write(Html.Raw(d_names));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n");
#nullable restore
#line 154 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                         foreach (var item1 in item.excursion)
                                        {
                                            e_names += item1.name + ",";

                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>\r\n                                            ");
#nullable restore
#line 160 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                       Write(Html.Raw(e_names));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n");
            WriteLiteral("                                        <td>\r\n                                            <a");
            BeginWriteAttribute("onclick", " onclick=\"", 8754, "\"", 8791, 3);
            WriteAttributeValue("", 8764, "getEditView(", 8764, 12, true);
#nullable restore
#line 171 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
WriteAttributeValue("", 8776, item.mirrorid, 8776, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8790, ")", 8790, 1, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"javascript:void(0)\">Add Commission</a>\r\n");
            WriteLiteral(" |\r\n\r\n");
            WriteLiteral("                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 177 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </table>\r\n\r\n\r\n\r\n");
            WriteLiteral("                    </div>\r\n");
            WriteLiteral(@"                </div>
                <!-- /.card-body -->
            </div>
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</section>
<section>
    <div style=""background-color:mediumseagreen;"">
        <div id=""CDiv"">

        </div>

        <div id=""EditDiv"">

        </div>
    </div>
</section>

<script>
    $(function () {
        $(""#example1"").DataTable({
            ""responsive"": true,
            ""autoWidth"": false,
        });
        $('#example2').DataTable({
            ""paging"": true,
            ""lengthChange"": false,
            ""searching"": false,
            ""ordering"": true,
            ""info"": true,
            ""autoWidth"": false,
            ""responsive"": true,
        });
    });
</script>

<script type=""text/javascript"">
    function CreateView() {

        console.log($(""#AgentCode option:selected"").val());

            $.post(""");
#nullable restore
#line 231 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
               Write(Url.Action("GetMirrorDetails", "Commission"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", { mirrorid: $(""#AgentCode option:selected"").val() }, function (data) {
                if (data) {
                    $('#CDiv').html('');
                    $('#EditDiv').html('');
                    $('#CDiv').append(data);
                }
            });


        }

        function getEditView(id) {
            $.post(""");
#nullable restore
#line 243 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Commission\Index.cshtml"
               Write(Url.Action("ShowEditPartailView", "Commission"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", {id: id}, function(data) {\r\n                if (data) {\r\n                    $(\'#CDiv\').html(\'\');\r\n                    $(\'#EditDiv\').html(\'\');\r\n                    $(\'#EditDiv\').append(data);\r\n                }\r\n            });\r\n        }\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SALEERP.ViewModel.CommissionVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
