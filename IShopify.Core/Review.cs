using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core
{
    public class Review
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
