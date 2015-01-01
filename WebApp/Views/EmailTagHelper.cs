using System;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;

namespace WebApp.Views
{
    [ContentBehavior(ContentBehavior.Modify)]
    public class EmailTagHelper : TagHelper
    {
        public static string EmailDomain = "mycompany.com";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Content = output.Content + "@" + EmailDomain;
            output.Attributes["href"] = "mailto:" + output.Content;
        }
    }
}