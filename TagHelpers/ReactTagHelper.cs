using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace ReactInAspNet.TagHelpers
{
    public class ReactTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var componentName = context.AllAttributes["component"].Value;
            var props = PropsFromDataAttributes(context);
            var randomId = (new System.Random()).Next();

            var scriptElement = $@"<script>
                ReactDOM.render(React.createElement({componentName}, {props}), document.querySelector('#r-{randomId}'));
            </script>";

            var extraAttributes = GetPassedAttributes(context);
            extraAttributes.Add("id", $"r-{randomId}");

            var attributeString = string.Join(" ", extraAttributes.Select(p => $" {p.Key}=\"{p.Value}\""));
            var renderDiv = $@"<div {attributeString}></div>";
            var html = renderDiv + scriptElement;

            output.TagName = "div";
            output.Content.SetHtmlContent(
                html
            );
        }

        private Dictionary<string,string> GetPassedAttributes(TagHelperContext context)
        {
            var attributes = context.AllAttributes.Where(a => a.Name != "component" && !a.Name.StartsWith("data-"));
            return attributes.ToDictionary(a => a.Name, a => a.Value.ToString());
        }

        private string PropsFromDataAttributes(TagHelperContext context)
        {
            var dataAttributes = context.AllAttributes.Where(a => a.Name.StartsWith("data-"));
            var attributes = dataAttributes.Select(a => new { k = a.Name.Substring(5), v = a.Value });
            var dict = attributes.ToDictionary(a => a.k, a => a.v.ToString());
            return JsonConvert.SerializeObject(dict);
        }

    }
}

// example output:
// <div id="r-203943"></div>
// <script>ReactDOM.render(<JobAlertAndNewsletterSignupWidget />, document.querySelector('#r-203943'));</script>
