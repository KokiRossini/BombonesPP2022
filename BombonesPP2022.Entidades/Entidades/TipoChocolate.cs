using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombonesPP2022.Entidades.Entidades
{
    public class TipoChocolate
    {
        public int TipoChocolateId { get; set; }
        public string Chocolate { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
