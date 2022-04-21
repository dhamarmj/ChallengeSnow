using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Models
{
    public class ItemBase
    {
        public Guid Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:##.##}")]
        [Display(Name = "Quantity")]
        public int Available_Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:$##.##}")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}

