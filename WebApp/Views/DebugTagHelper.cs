using System;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using Newtonsoft.Json;

namespace WebApp.Views
{
    [ContentBehavior(ContentBehavior.Replace)]
    public class DebugTagHelper : TagHelper
    {
        public bool? Condition { get; set; }

        public string Header { get; set; }

        public dynamic Data { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(Condition.HasValue && false == Condition.Value)
            {
                output.TagName = null;
                return;
            }

            output.TagName = "div";
            output.Content = "<h2>" + Header + "</h2><p>" + JsonConvert.SerializeObject(Data) + "</p>";
            output.Attributes["class"] = "jumbotron";
            output.Attributes["style"] = "background-color:pink";
        }
    }
}