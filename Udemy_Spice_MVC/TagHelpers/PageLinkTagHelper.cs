using Microsoft.AspNetCore.Mvc;//added
using Microsoft.AspNetCore.Mvc.Rendering;//added
using Microsoft.AspNetCore.Mvc.Routing;//added
using Microsoft.AspNetCore.Mvc.ViewFeatures;//added
using Microsoft.AspNetCore.Razor.TagHelpers;//added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy_Spice_MVC.Models;

namespace Udemy_Spice_MVC.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext viewContext { get; set; }

        public PagingInfo pageModel { get; set; }
        public string pageAction { get; set; }
        public bool pageClassesEnabled { get; set; }
        public string pageClass { get; set; }
        public string pageClassNormal { get; set; }
        public string pageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(viewContext);
            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= pageModel.totalPage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                string url = pageModel.urlParam.Replace(":", i.ToString());
                tag.Attributes["href"] = url;
                if (pageClassesEnabled)
                {
                    tag.AddCssClass(pageClass);
                    tag.AddCssClass(i == pageModel.currentPage ? pageClassSelected : pageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);

        }

    }
}
