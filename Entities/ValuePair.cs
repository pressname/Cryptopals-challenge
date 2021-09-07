using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptopals.Entities
{
    public class ValuePair
    {
        public int EditDistance { get; set; }
        public int Keysize { get; set; }

        public ValuePair(int keysize, int distance)
        {
            Keysize = keysize;
            EditDistance = distance;
        }
        
    }
}
