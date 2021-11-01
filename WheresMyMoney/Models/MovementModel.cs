using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }


        public string Notes { get; set; }


        [Display(Name = "Movement Type")]
        public Guid MovementTypeId { get; set; }


    }
}