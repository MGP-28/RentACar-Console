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
        public Camiao(Veiculo v, double pesoMax) : base(v)
        {
            PesoMax = pesoMax;
        }
        public override string ToString()
        {
            return base.ToString() + $" | {PesoMax.ToString().PadRight(9)}";
        }
        public string ToStringHTML()
        {
            return base.ToStringHTMLbase() + $"<th>{PesoMax.ToString()}</th>";
        }
    }
}
