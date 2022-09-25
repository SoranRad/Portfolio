using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Domain.SharedKernel.Extention
{
    public static class HtmlExtention
    {
        public static bool IsHtmlEmpty(this string HtmlString)
        {
             
            if (string.IsNullOrWhiteSpace(HtmlString))
                return true;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(HtmlString);

            var htmlBody = htmlDoc.DocumentNode;
            var childNodes = htmlBody.ChildNodes;


            if (childNodes
                .Any(node =>
                    !string.IsNullOrWhiteSpace(node.GetDirectInnerText())
                    || !string.IsNullOrWhiteSpace(node.InnerText)
                )
               )
                return false;

            return true;
        }
    }
}
