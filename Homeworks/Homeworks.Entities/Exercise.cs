using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Homeworks.Entities
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Problem { get; set; }
        public int Score { get; set; }
    }
}