using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheresMyMoney.Models;


namespace WheresMyMoney.ViewModels
{
    public class CreateMovementViewModel
    {
        public MovementModel Movement { get; set; }
        public IEnumerable<CategoryModel> Category { get; set; }
        public List<MovementTypeModel>MovementType { get; set; }
    }
}