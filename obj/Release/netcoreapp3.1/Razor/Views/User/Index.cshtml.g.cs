#pragma checksum "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14605eb871055f4bf48af757a067473bc6211cd0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index), @"mvc.1.0.view", @"/Views/User/Index.cshtml")]
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
#line 2 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
using PagedList.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14605eb871055f4bf48af757a067473bc6211cd0", @"/Views/User/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c6f8f5b7fd23eef9cfeadd64d65a3f306acc17", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SALEERP.ViewModel.UserLoginVM>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/PagedList.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("search"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline ml-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
  
    ViewBag.Title = "User";


#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "14605eb871055f4bf48af757a067473bc6211cd06059", async() => {
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
                        <button type=""button"" onclick=""CreateView()"" href=""javascript:void(0)"" style=""background-color:khaki;"" class=""btn btn-info btn-flat"">Create New </button>
");
            WriteLiteral("                    </div>\r\n\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14605eb871055f4bf48af757a067473bc6211cd07633", async() => {
                WriteLiteral("\r\n                        <div class=\"input-group input-group-sm\">\r\n                            <input class=\"form-control form-control-navbar\" type=\"search\" name=\"searchstring\"");
                BeginWriteAttribute("value", " value=\"", 930, "\"", 975, 1);
#nullable restore
#line 20 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
WriteAttributeValue("", 938, Model.FirstOrDefault()?.searchstring, 938, 37, false);

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
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <div class=""card-body"">
                        <table id=""example2"" class=""table table-bordered table-hover"">
                            <thead>

                                <tr style=""background-color:khaki"" >
                                    <th>
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14605eb871055f4bf48af757a067473bc6211cd010714", async() => {
                WriteLiteral(" ");
#nullable restore
#line 34 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                                                                                          Write(Html.DisplayNameFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sortOrder", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 34 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
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
#line 38 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.UserPass));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 41 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.UserEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 44 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.UserPhone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14605eb871055f4bf48af757a067473bc6211cd014514", async() => {
#nullable restore
#line 47 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                                                                                         Write(Html.DisplayNameFor(model => model.RoleName));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sortOrder", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 47 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                                                       WriteLiteral(ViewData["DateSortParm"]);

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
            WriteLiteral("\r\n                                    </th>\r\n                                    <th></th>\r\n                                </tr>\r\n\r\n");
#nullable restore
#line 52 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 56 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 59 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.UserPass));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 62 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.UserEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 65 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.UserPhone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 68 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.RoleName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            <a");
            BeginWriteAttribute("onclick", " onclick=\"", 3859, "\"", 3894, 3);
            WriteAttributeValue("", 3869, "getEditView(", 3869, 12, true);
#nullable restore
#line 71 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
WriteAttributeValue("", 3881, item.UserId, 3881, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3893, ")", 3893, 1, true);
            EndWriteAttribute();
            WriteLiteral(" href=\"javascript:void(0)\">Edit|</a>\r\n");
            WriteLiteral("\r\n                                            ");
#nullable restore
#line 75 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.ActionLink("Delete", "GetUserDeleted", new { id = item.UserId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 79 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </table>\r\n\r\n\r\n\r\n                        <div id=\"pagingDiv\">");
#nullable restore
#line 84 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                       Write(Html.Raw(ViewBag.Paging));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
            WriteLiteral("                    </div>\r\n                    <div>\r\n                        <span class=\"text-danger\">");
#nullable restore
#line 91 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
                                             Write(Html.ValidationSummary(false));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        <span asp-validation-summary=\"All\" class=\"text-danger\"></span>\r\n");
            WriteLiteral(@"                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </section>
    
    <section>
        <div  style=""background-color:khaki"">
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
            WriteLiteral("      $.post(\"");
#nullable restore
#line 137 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
               Write(Url.Action("ShowCreatePartailView", "User"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", function (data) {\r\n                if (data) {\r\n                    $(\'#CDiv\').append(data);\r\n                }\r\n            });\r\n\r\n        }\r\n\r\n        function getEditView(id) {\r\n            $.post(\"");
#nullable restore
#line 146 "G:\enableIT\SALEERP_OLD\SALEERP\Views\User\Index.cshtml"
               Write(Url.Action("ShowEditPartailView", "User"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", {id: id}, function(data) {\r\n                if (data) {\r\n                    $(\'#EditDiv\').append(data);\r\n                }\r\n            });\r\n    }\r\n");
            WriteLiteral("    </script>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SALEERP.ViewModel.UserLoginVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591