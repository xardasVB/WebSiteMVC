using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMVC.Helpers
{
    public static class PagingHelper
    {
        public static string PageLinks(this HtmlHelper html,
            int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= totalPages; i++)
            {
                TagBuilder tagLi = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == currentPage)
                    tagLi.AddCssClass("active");
                tagLi.InnerHtml = tag.ToString();
                result.AppendLine(tagLi.ToString());
            }
            TagBuilder tagDiv = new TagBuilder("div");
            TagBuilder tagUl = new TagBuilder("ul");

            tagUl.InnerHtml = result.ToString();
            tagUl.AddCssClass("pagination");
            tagDiv.InnerHtml = tagUl.ToString();

            return tagDiv.ToString();
        }
    }
}