using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarOMeterWebApiCore.Model
{
    public class Bar
    {
        public string BarName { get; set; }

        private int _Rating;
        public int Rating
        {
            get { return _Rating; }
            set
            {
                if (value >= 1 && value <= 5)
                    _Rating = value;
            }
        }
    }
}
