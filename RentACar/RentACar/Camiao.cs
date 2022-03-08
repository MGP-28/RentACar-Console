using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Camiao : Veiculo
    {
        private double _pesoMax;
        public double PesoMax { get => _pesoMax; set => _pesoMax = value; }
        public Camiao(Veiculo v) : base(v)
        {
        }
    }
}
