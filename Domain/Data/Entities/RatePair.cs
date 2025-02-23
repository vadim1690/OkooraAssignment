using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class RatePair
    {
        public int Id { get; set; }
        public string PairName { get; set; } = string.Empty;
        public  decimal Rate { get; set; }
        public  DateTime LastUpdate { get; set; }
    }
}
