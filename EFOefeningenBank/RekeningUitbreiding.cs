using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFOefeningenBank
{
    public partial class Rekening
    {
        public void storten(decimal bedrag)
        {
             Saldo += bedrag;
        }
    }
}
