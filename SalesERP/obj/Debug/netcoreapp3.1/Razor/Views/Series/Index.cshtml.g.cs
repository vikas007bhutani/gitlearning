#pragma checksum "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9c2f64412fdfb30ef4fa068f5e1bde1bdfa16bc1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Series_Index), @"mvc.1.0.view", @"/Views/Series/Index.cshtml")]
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
#line 1 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\_ViewImports.cshtml"
using SALEERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\_ViewImports.cshtml"
using SALEERP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
using PagedList.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9c2f64412fdfb30ef4fa068f5e1bde1bdfa16bc1", @"/Views/Series/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_Series_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SALEERP.ViewModel.SeriesVM>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/PagedList.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Series", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
      
    ViewBag.Title = "Agent Type";

    

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9c2f64412fdfb30ef4fa068f5e1bde1bdfa16bc15840", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    <section class=""content"">
        <div class=""row"">
            <div class=""col-12"">
                <div class=""card"">
                    <div class=""card-header"">
                        <a onclick=""CreateView()"" href=""javascript:void(0)"">Create New</a>
");
            WriteLiteral("                    </div>\r\n\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9c2f64412fdfb30ef4fa068f5e1bde1bdfa16bc17319", async() => {
                WriteLiteral("\r\n                        <div class=\"input-group input-group-sm\">\r\n                            <input class=\"form-control form-control-navbar\" type=\"search\" name=\"searchstring\"");
                BeginWriteAttribute("value", " value=\"", 849, "\"", 894, 1);
#nullable restore
#line 20 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
WriteAttributeValue("", 857, Model.FirstOrDefault()?.searchstring, 857, 37, false);

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
            WriteLiteral("\r\n                    <div class=\"card-body\">\r\n                        <div class=\"text-center w-full p-t-25 p-b-80\">\r\n                            <span class=\"text-danger\">");
#nullable restore
#line 30 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                                 Write(Html.ValidationSummary(false));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            <span asp-validation-summary=\"All\" class=\"text-danger\"></span>\r\n");
            WriteLiteral(@"                        </div>
                        <table id=""example2"" class=""table table-bordered table-hover"">
                            <thead>

                                <tr style=""background-color:bisque"" >
                                    <th>
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9c2f64412fdfb30ef4fa068f5e1bde1bdfa16bc110896", async() => {
                WriteLiteral(" ");
#nullable restore
#line 41 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                                                                                          Write(Html.DisplayNameFor(model => model.SeriesName));

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
#line 41 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
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
#line 45 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.SeriesCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th></th>\r\n                                </tr>\r\n\r\n");
#nullable restore
#line 50 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                 foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 54 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.SeriesName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 57 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.SeriesCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n\r\n                                    <td>\r\n                                        <a");
            BeginWriteAttribute("onclick", " onclick=\"", 3057, "\"", 3094, 3);
            WriteAttributeValue("", 3067, "getEditView(", 3067, 12, true);
#nullable restore
#line 61 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
WriteAttributeValue("", 3079, item.SeriesId, 3079, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3093, ")", 3093, 1, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"javascript:void(0)\">Edit</a>\r\n");
            WriteLiteral(" |\r\n\r\n                                        ");
#nullable restore
#line 64 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                   Write(Html.ActionLink("Delete", "GetSeriesDeleted", new { id = item.SeriesId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 67 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </table>\r\n\r\n\r\n\r\n                        <div id=\"pagingDiv\">");
#nullable restore
#line 72 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
                                       Write(Html.Raw(ViewBag.Paging));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
            WriteLiteral(@"                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </section>
    <section>
        <div style=""background-color:bisque"">
            <div id=""CDiv"" class=""col-md-6"">

            </div>

            <div id=""EditDiv"" class=""col-md-6"">

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
            ");
            WriteLiteral("$.post(\"");
#nullable restore
#line 117 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
               Write(Url.Action("ShowCreatePartailView", "Series"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", function (data) {\r\n                if (data) {\r\n                    $(\'#CDiv\').append(data);\r\n                }\r\n            });\r\n\r\n        }\r\n\r\n        function getEditView(id) {\r\n            $.post(\"");
#nullable restore
#line 126 "F:\Kishor\Projects\AkbarERP\SalesERP\Views\Series\Index.cshtml"
               Write(Url.Action("ShowEditPartailView", "Series"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", {id: id}, function(data) {\r\n                if (data) {\r\n                    $(\'#EditDiv\').append(data);\r\n                }\r\n            });\r\n        }\r\n    </script>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SALEERP.ViewModel.SeriesVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591