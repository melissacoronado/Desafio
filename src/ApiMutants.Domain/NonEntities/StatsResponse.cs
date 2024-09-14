using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMutants.Domain.NonEntities
{
    public class StatsResponse
    {
        public int CountMutantDna { get; set; }
        public int CountHumanDna { get; set; }
        public double Ratio { get; set; }
    }
}
