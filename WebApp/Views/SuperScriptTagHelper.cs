using System;
using System.IO;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using Newtonsoft.Json;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace WebApp.Views
{
    [HtmlElementName("super-script")]
    [ContentBehavior(ContentBehavior.Replace)]
    public class SuperScript : TagHelper
    {
        private readonly string ScriptFormat = "<script src=\"{0}\" type=\"text/javascript\"></script>";

        [HtmlAttributeName("file-pattern")]
        public string FilePattern { get; set; }

        [Activate]
        private IHostingEnvironment HostingEnvironment { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var webRoot = HostingEnvironment.WebRoot;
            var files = Directory.EnumerateFiles(webRoot + "/lib", FilePattern, SearchOption.AllDirectories);

            var scriptTags =
                files.GroupBy(filename => filename.Replace(".min.js", ".js"))
                .Select(fileNames => fileNames.OrderByDescending(fileName => fileName.Length).First())
                .Select(fileName => string.Format(ScriptFormat, fileName.Replace(webRoot, ""))
                    .Replace('\\', '/'));

            output.TagName = null;
            output.Content = string.Join(Environment.NewLine, scriptTags);
        }
    }
}