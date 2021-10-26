using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheresMyMoney.Models
{
    public class MovementModel
    {
        public Guid MovementId { get; set; }
        public DateTime Date { get; set; }
        public float VALUE { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Notes { get; set; }
        public Guid MovementTypeId { get; set; }


    }
}