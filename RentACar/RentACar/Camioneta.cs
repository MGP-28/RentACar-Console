using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Camioneta : Veiculo
    {
        private int _nEixos;
        private int _nPassageiros;
        public int NEixos { get => _nEixos; set => _nEixos = value; }
        public int NPassageiros { get => _nPassageiros; set => _nPassageiros = value; }
        public Camioneta(Veiculo v) : base(v)
        {
        }
        public Camioneta(Veiculo v, int nEixos, int nPassageiros) : base(v)
        {
            NEixos = nEixos;
            NPassageiros = nPassageiros;
        }
        public override string ToString()
        {
            return base.ToString() + $" | {NEixos.ToString().PadRight(8)} | {NPassageiros.ToString().PadRight(14)}";
        }
        public string ToStringHTML()
        {
            return base.ToString() + $"<th>{NEixos.ToString()}</th><th>{NPassageiros.ToString()}</th>";
        }
    }
}
}
