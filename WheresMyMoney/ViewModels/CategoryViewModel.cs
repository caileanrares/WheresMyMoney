using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheresMyMoney.ViewModels
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        
        public string MovementTypeName { get; set; }

    }
}