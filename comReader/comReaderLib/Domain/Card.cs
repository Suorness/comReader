using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comReaderLib.Domain
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public virtual Person Person { get; set; }
    }
}
