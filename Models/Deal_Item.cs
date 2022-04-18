﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Models
{
    public class Deal_Item : Item
    {
        [DisplayFormat(DataFormatString = "{0:##.##}%")]
        public decimal Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        [Display(Name = "End Date")]
        public DateTime End_Date { get; set; }
    }
}