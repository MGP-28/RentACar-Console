using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Carro : Veiculo
    {
        private int _nPortas;
        private string _caixa;
        public int NPortas { get => _nPortas; set => _nPortas = value; }
        public string Caixa { get => _caixa; set => _caixa = value; }

        public Carro(Veiculo v) : base(v)
        {
        }
        public Carro(Veiculo v, int nPortas, string caixa) : base(v)
        {
            NPortas = nPortas;
            Caixa = caixa;
        }
        public override string ToString()
        {
            return base.ToString() + $" | {NPortas.ToString().PadRight(9)} | {Caixa.PadRight(10)}";
        }
        public string ToStringHTML()
        {
            return base.ToStringHTMLbase() + $"<th>{NPortas}</th><th>{Caixa}</th>";
        }
        public override string ToStringToFile()
        {
            return $"Carro {base.ToStringToFile()} {NPortas} {Caixa}";
        }
    }
}
