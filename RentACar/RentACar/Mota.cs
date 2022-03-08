using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Mota : Veiculo
    {
        private int _cilindrada;
        public int Cilindrada { get => _cilindrada; set => _cilindrada = value; }
        public Mota(Veiculo v) : base(v)
        {
        }
    }
}
