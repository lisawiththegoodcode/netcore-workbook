using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.ViewComponents
{
    public class CardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CardViewModel model)
        {
            return View(model);
        }
    }
}
