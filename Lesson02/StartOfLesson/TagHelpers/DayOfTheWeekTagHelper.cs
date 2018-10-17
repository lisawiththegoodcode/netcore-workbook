using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;

namespace TagHelpers
{
    //which attributes to I look for
    [HtmlTargetElement("*", Attributes = "day-of-week")]

    public class DayOfTheWeekTagHelper : TagHelper
    {
        //what attributes do I read data out of
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var value = For.Model as DateTime?;
            var now = DateTime.Now;

            if (value.HasValue)
            {
                if (value > now && value < now.AddDays(7))
                {
                    var ariaAttribute = new TagHelperAttribute("aria-valuetext", value.Value.ToString("D"));
                    output.Attributes.Add(ariaAttribute);
                    output.Content.SetContent(value.Value.DayOfWeek.ToString());
                } else
                {
                    output.Content.SetContent(value.Value.ToString("MM/dd/yyy"));
                }
                var dayOfWeekAttribute = output.Attributes.FirstOrDefault(x => x.Name == "day-of-week");
                output.Attributes.Remove(dayOfWeekAttribute);
            }
        }
    }
}
