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
        public Mota(Veiculo v, int cilindrada) : base(v)
        {
            Cilindrada = cilindrada;
        }
        public override string ToString()
        {
            return base.ToString() + $" | {Cilindrada.ToString().PadRight(10)}";
        }
        public string ToStringHTML()
        {
            return base.ToStringHTMLbase() + $"<th>{Cilindrada.ToString()}</th>";
        }
        public override string ToStringToFile()
        {
            return $"Mota {base.ToStringToFile()} {Cilindrada}";
        }
    }
}
