using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheresMyMoney.ViewModels
{
    public class UserMovementsNamesViewModel
    {
        public Guid MovementId { get; set; }
        public DateTime Date { get; set; }
        public string MovementType { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }    
        public float VALUE { get; set; }
    }
}