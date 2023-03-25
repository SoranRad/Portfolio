using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelper
{
    public class SpinnerTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        [HtmlAttributeName("asp-id")]
        public string id { get; set; }

        [HtmlAttributeName("asp-class")]
        public string SpinnerClass { get; set; } = "spinner-border spinner-border-sm";

        [HtmlAttributeName("asp-AppendClass")]
        public string AdditionClass { get; set; }

        [HtmlAttributeName("asp-style")]
        public string Style { get; set; } = "display: none;";

        [HtmlAttributeName("asp-AddToInner")]
        public bool AddClassToInnerTag { get; set; } = false;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            if (!string.IsNullOrWhiteSpace(id))
                output.Attributes.SetAttribute("id", id);

            if (!string.IsNullOrWhiteSpace(Style))
                output.Attributes.SetAttribute("style", Style);

            output.Attributes.SetAttribute("role", "status");


            if (!string.IsNullOrWhiteSpace(SpinnerClass))
            {
                if (!AddClassToInnerTag)
                    output.Attributes.SetAttribute("class", SpinnerClass);
                else
                    output.Content.AppendHtml("<i class='" + SpinnerClass + "'>" + "</i>");
            }

            if (!string.IsNullOrWhiteSpace(AdditionClass))
                output.AddClass(AdditionClass, HtmlEncoder.Default);

            output.Content.AppendHtml("</span>");

            return Task.CompletedTask;
        }
    }
}
