﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:##.##}")]
        [Display(Name = "Quantity")]
        public int Available_Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:$##.##}")]
        public decimal Price { get; set; }

    }
}
