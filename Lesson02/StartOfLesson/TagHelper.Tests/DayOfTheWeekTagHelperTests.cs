using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelper.Tests.Infrastructure;
using TagHelpers;
using Xunit;

namespace TagHelper.Tests
{
    public class DayOfTheWeekTagHelperTests : BaseTagHelperTest
    {
        [Fact]
        public void TagHelper_ShouldDisplayMMddyyyyForDates7DaysorFurtherInTheFuture()
        {
            //Assemble
            //what should break it? 8 days
            var value = DateTime.Now.AddDays(8);
            var tagHelper = new DayOfTheWeekTagHelper();
            tagHelper.For = GetModelExpression(value);

            //pass in attributes we'd expect to find when this runs
            TagHelperAttributeList attributes = new TagHelperAttributeList
            {
                new TagHelperAttribute("asp-for", value),
                new TagHelperAttribute("day-of-week")
            };

            TagHelperContext context = null;
            TagHelperOutput output = new TagHelperOutput("span", attributes, BlankContent);



            //Act
            //run the test
            tagHelper.Process(context, output);

            //Assert
            //
            Assert.Equal(value.ToString("MM/dd/yyy"), output.Content.GetContent());
        }
    }
}
