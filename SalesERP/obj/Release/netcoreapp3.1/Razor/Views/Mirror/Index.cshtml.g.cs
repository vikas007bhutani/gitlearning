#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68d11939883458d0f5ef03a4e1c7554ebfde87bd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mirror_Index), @"mvc.1.0.view", @"/Views/Mirror/Index.cshtml")]
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
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
using PagedList.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68d11939883458d0f5ef03a4e1c7554ebfde87bd", @"/Views/Mirror/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_Mirror_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SALEERP.ViewModel.MirrorDetailsVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Pool", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline ml-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "AddMirror.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
  
    ViewBag.Title = "Agent Type";


#line default
#line hidden
#nullable disable
            WriteLiteral("<script src=\"https://code.jquery.com/jquery-1.12.4.js\"></script>\r\n<script src=\"https://code.jquery.com/ui/1.12.1/jquery-ui.js\"></script>\r\n<script>\r\n    $(document).ready(function () {\r\n        $(\'#drname\').autocomplete({\r\n            source:  \'");
#nullable restore
#line 12 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                 Write(Url.Action("Search"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\'\r\n        });\r\n    });\r\n</script>\r\n<section class=\"content\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12\">\r\n            <div class=\"card\">\r\n                <div class=\"card-header\">\r\n\r\n\r\n");
            WriteLiteral("\r\n                    <div class=\"col-md-10\">\r\n\r\n                    </div>\r\n                </div>\r\n");
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "68d11939883458d0f5ef03a4e1c7554ebfde87bd6280", async() => {
                WriteLiteral("\r\n                    <div class=\"input-group input-group-sm\">\r\n                        <input class=\"form-control form-control-navbar\" type=\"search\" name=\"searchstring\"");
                BeginWriteAttribute("value", " value=\"", 1522, "\"", 1575, 1);
#nullable restore
#line 38 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
WriteAttributeValue("", 1530, Model.Mirrors.FirstOrDefault()?.searchstring, 1530, 45, false);

#line default
#line hidden
#nullable disable
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <div class=\"card-body\">\r\n                    <div class=\"text-center w-full p-t-25 p-b-80\">\r\n                        <span class=\"text-danger\">");
#nullable restore
#line 48 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                             Write(Html.ValidationSummary(false));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        <span asp-validation-summary=\"All\" class=\"text-danger\"></span>\r\n");
            WriteLiteral(@"                    </div>

                    <div style=""height: 200px; overflow: auto;"">
                        <table id=""example2"" class=""table table-bordered table-hover"">
                            <thead >

                                <tr  style=""background-color:aquamarine;"">
                                    <th>
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "68d11939883458d0f5ef03a4e1c7554ebfde87bd9893", async() => {
                WriteLiteral(" ");
#nullable restore
#line 61 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                                                                                          Write(Html.DisplayNameFor(model => model.Mirrors.FirstOrDefault().mirrorid));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sortOrder", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 61 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
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
#line 65 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
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
                                    <th>
                                        Language
                                    </th>
");
            WriteLiteral("                                </tr>\r\n\r\n");
#nullable restore
#line 84 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                 foreach (var item in Model.Mirrors)
                                {
                                    string p_names = string.Empty, d_names = string.Empty,e_names=string.Empty,t_names=string.Empty,g_names=string.Empty ;

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 89 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.mirrorid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 92 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.mirrordate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n");
#nullable restore
#line 94 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                 foreach (var item1 in item.principle)
                                {
                                    p_names += item1.name + ",";

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    ");
#nullable restore
#line 100 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.Raw(p_names));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n");
#nullable restore
#line 102 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                 foreach (var item1 in item.driver)
                                {
                                    d_names += item1.name + ",";

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    ");
#nullable restore
#line 108 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.Raw(d_names));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n");
#nullable restore
#line 110 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                 foreach (var item1 in item.excursion)
                                {
                                    e_names += item1.name + ",";

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>\r\n                                    ");
#nullable restore
#line 116 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.Raw(e_names));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n");
            WriteLiteral("                                <td>\r\n                                    ");
#nullable restore
#line 127 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.language));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    <a");
            BeginWriteAttribute("onclick", " onclick=\"", 5979, "\"", 6016, 3);
            WriteAttributeValue("", 5989, "getEditView(", 5989, 12, true);
#nullable restore
#line 130 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
WriteAttributeValue("", 6001, item.mirrorid, 6001, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6015, ")", 6015, 1, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"javascript:void(0)\">Edit</a>\r\n");
            WriteLiteral(" |\r\n\r\n                                    ");
#nullable restore
#line 133 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                               Write(Html.ActionLink("Delete", "GetMirrorDeleted", new { id = item.mirrorid }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 136 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </table>\r\n\r\n\r\n\r\n                        <div id=\"pagingDiv\">");
#nullable restore
#line 141 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
                                       Write(Html.Raw(ViewBag.Paging));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    </div>\r\n");
            WriteLiteral("                </div>\r\n                <!-- /.card-body -->\r\n            </div>\r\n        </div>\r\n        <!-- /.col -->\r\n    </div>\r\n    <!-- /.row -->\r\n</section>\r\n<section>\r\n    <div>\r\n        <div id=\"CDiv\">\r\n\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "68d11939883458d0f5ef03a4e1c7554ebfde87bd19383", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n        </div>\r\n\r\n        <div id=\"EditDiv\" class=\"col-md-12\">\r\n\r\n        </div>\r\n    </div>\r\n</section>\r\n\r\n<script type=\"text/javascript\">\r\n   \r\n\r\n        function getEditView(id) {\r\n            $.post(\"");
#nullable restore
#line 173 "G:\enableIT\SALEERP_OLD\SALEERP\SalesERP\Views\Mirror\Index.cshtml"
               Write(Url.Action("ShowEditPartailView", "Mirror"));

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SALEERP.ViewModel.MirrorDetailsVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
