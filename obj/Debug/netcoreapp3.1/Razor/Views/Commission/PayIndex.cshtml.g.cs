#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a092ac6d9a466809acc7f2607b5d940177e30390"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Commission_PayIndex), @"mvc.1.0.view", @"/Views/Commission/PayIndex.cshtml")]
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
#nullable restore
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
using PagedList.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a092ac6d9a466809acc7f2607b5d940177e30390", @"/Views/Commission/PayIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_Commission_PayIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SALEERP.ViewModel.PayCommissionVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:red"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Pool", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline ml-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
  
    ViewBag.Title = "Pay Commission";


#line default
#line hidden
#nullable disable
            WriteLiteral("<script src=\"https://code.jquery.com/jquery-1.12.4.js\"></script>\r\n<script src=\"https://code.jquery.com/ui/1.12.1/jquery-ui.js\"></script>\r\n<script>\r\n");
            WriteLiteral("    $(document).ready(function () {\r\n\r\n\r\n        $(\'body\').on(\'focus\', \'input[id^=\"Name\"] \', function () {\r\n\r\n            $(this).autocomplete({\r\n                source: \'");
#nullable restore
#line 32 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                    Write(Url.Action("Demo_Search","Commission"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"?agentcode=' + $(""#AgentCode option:selected"").val() +'',
                select: function (event, ui) {

                //set tagids to save
                    $(this).val(ui.item.value);
                    $(this).parent().find('input[type=hidden]').val(ui.item.id);
                    //Tags for display
                    // this.value = ui.value;
                    return false;
                }
                });
        });

    });
</script>
<section class=""content"">
    <div class=""row"">
        <div class=""col-12"">
            <div class=""card"">
                <div class=""card-header"">
                    <div class=""card card-info"">
                        <div class=""card-body"">
");
#nullable restore
#line 54 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                             using (Html.BeginForm("GetCommission", "Commission"))
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                           Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <table id=\"dataTabledriver\"");
            BeginWriteAttribute("class", " class=\"", 1970, "\"", 1978, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <tr>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 2067, "\"", 2075, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <div class=\"form-group\">\r\n                                                ");
#nullable restore
#line 62 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                           Write(Html.DropDownListFor(model => model.AgentCode, Model._agentuser as IEnumerable<SelectListItem>, "Select"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                ");
#nullable restore
#line 63 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                           Write(Html.ValidationMessageFor(model => model.AgentCode, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                                            </div>\r\n                                        </td>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 2587, "\"", 2595, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n                                            <div class=\"form-group\">\r\n");
            WriteLiteral("                                                ");
#nullable restore
#line 71 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                           Write(Html.HiddenFor(model => model.AgentId, new { htmlAttributes = new { @class = "form-control", required = "required", placeholder = "Enter ..." } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n");
            WriteLiteral("                                                ");
#nullable restore
#line 78 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                           Write(Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control", placeholder = "Enter ..." } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                ");
#nullable restore
#line 79 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                           Write(Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                                            </div>\r\n                                        </td>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 3642, "\"", 3650, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <div class=\"form-group\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a092ac6d9a466809acc7f2607b5d940177e3039011299", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 85 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a092ac6d9a466809acc7f2607b5d940177e3039013088", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 86 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.fromdate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a092ac6d9a466809acc7f2607b5d940177e3039014636", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 87 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.fromdate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </div>\r\n                                        </td>\r\n                                        <td");
            BeginWriteAttribute("class", " class=\"", 4174, "\"", 4182, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <div class=\"form-group\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a092ac6d9a466809acc7f2607b5d940177e3039016627", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 92 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a092ac6d9a466809acc7f2607b5d940177e3039018414", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 93 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Todate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a092ac6d9a466809acc7f2607b5d940177e3039019960", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 94 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Todate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </div>\r\n                                        </td>\r\n");
            WriteLiteral("                                        <td");
            BeginWriteAttribute("class", " class=\"", 5244, "\"", 5252, 0);
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
#line 114 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                        </div>\r\n\r\n\r\n\r\n\r\n                    </div>\r\n\r\n");
            WriteLiteral("\r\n                    <div class=\"col-md-10\">\r\n\r\n                    </div>\r\n                </div>\r\n");
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a092ac6d9a466809acc7f2607b5d940177e3039022917", async() => {
                WriteLiteral("\r\n                    <div class=\"input-group input-group-sm\">\r\n                        <input class=\"form-control form-control-navbar\" type=\"search\" name=\"searchstring\"");
                BeginWriteAttribute("value", " value=\"", 6871, "\"", 6879, 0);
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <div class=\"card-body\">\r\n                    <div class=\"text-center w-full p-t-25 p-b-80\">\r\n                        <span class=\"text-danger\">");
#nullable restore
#line 150 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                             Write(Html.ValidationSummary(false));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        <span asp-validation-summary=\"All\" class=\"text-danger\"></span>\r\n");
            WriteLiteral(@"                    </div>

                    <div style=""height: 200px; overflow: auto;"">
                        <table id=""example3"" class=""table table-bordered table-hover"">
                            <thead>

                                <tr style=""background-color:aquamarine;"">
                                    <th>
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a092ac6d9a466809acc7f2607b5d940177e3039026285", async() => {
                WriteLiteral(" ");
#nullable restore
#line 163 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                                                                                          Write(Html.DisplayNameFor(model => model.pay_list[0].MirrorId));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sortOrder", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 163 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
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
#line 167 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].MirrorDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 170 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 173 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].AgentType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 176 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].commission_amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 179 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].tds));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 182 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].paid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 185 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                   Write(Html.DisplayNameFor(model => model.pay_list[0].balance));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n");
            WriteLiteral("                                </tr>   \r\n\r\n");
#nullable restore
#line 195 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                 foreach (var item in Model.pay_list)
                                {
                                    string p_names = string.Empty, d_names = string.Empty, e_names = string.Empty, t_names = string.Empty, g_names = string.Empty;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 200 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.MirrorId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 203 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.MirrorDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 207 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 210 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.AgentType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 213 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.commission_amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 216 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.tds));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 219 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.paid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 222 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.balance));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n");
            WriteLiteral("                                        <td>\r\n                                            <a");
            BeginWriteAttribute("onclick", " onclick=\"", 12189, "\"", 12225, 3);
            WriteAttributeValue("", 12199, "getEditView(", 12199, 12, true);
#nullable restore
#line 233 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
WriteAttributeValue("", 12211, item.AgentId, 12211, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 12224, ")", 12224, 1, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"javascript:void(0)\">Pay Commission</a>\r\n");
            WriteLiteral(" |\r\n\r\n\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 239 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
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

<script>
    $(""#submit"").click(function () {
        var form = $(""form"")
            .removeData(""validator"") /* added by the raw jquery.validate plugin */
            .removeData(""unobtrusiveValidation"");  /* added by the jquery uno");
            WriteLiteral("btrusive plugin*/\r\n\r\n        $.validator.unobtrusive.parse(form);\r\n\r\n        form.data(\'unobtrusiveValidation\');\r\n    });\r\n\r\n\r\n</script>\r\n<script type=\"text/javascript\">\r\n    function CreateView() {\r\n\r\n\r\n\r\n            $.post(\"");
#nullable restore
#line 306 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
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
#line 318 "G:\enableIT\SALEERP_OLD\SALEERP\Views\Commission\PayIndex.cshtml"
               Write(Url.Action("ShowEditPartailPayView", "Commission"));

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SALEERP.ViewModel.PayCommissionVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
