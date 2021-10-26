using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheresMyMoney.Models
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public Guid MovementTypeId { get; set; }    

    }
}