using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheresMyMoney.Models;

namespace WheresMyMoney.ViewModels
{
    public class CreateCategoryViewModel
    {
        public CategoryModel category { get; set; }
        public  IEnumerable<MovementTypeModel> type { get; set; }
    }
}