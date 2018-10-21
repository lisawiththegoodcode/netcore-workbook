using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ToDo.Tests
{
    class CardGroupViewComponentTests
    {
        [Fact]
        public void Compoent_ShouldGet40OldestItemsWhichAreTheOldest()
        {
            //ASSEMBLE
            var component = new CardGroupViewComponentTests();

            //ACT
            component.Invoke();
        }
    }
}
