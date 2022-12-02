using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikroszimulacio_MTA4NG.Entities
{
    public class DeathProbability
    {

        public Gender gender { get; set; }
        public int Age { get; set; }
        
        public double ProbOfDeath { get; set; }
    }
}
