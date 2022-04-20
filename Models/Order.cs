using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSnow.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        [Display(Name = "Created")]
        public DateTime Date_Created { get; set; }

        public Item Item_Number { get; set; }
        public Guid Item_NumberId { get; set; }

        public int Quantity { get; set; }
    }
}
