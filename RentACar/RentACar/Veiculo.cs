using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Veiculo
    {
        private int _id;
        private List<Reserva> _reservas = new List<Reserva>();
        private string _nome;
        private string _cor;
        private string _combustivel;
        private double _preco;
        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Cor { get => _cor; set => _cor = value; }
        public string Combustivel { get => _combustivel; set => _combustivel = value; }
        public double Preco { get => _preco; set => _preco = value; }
        internal List<Reserva> Reservas { get => _reservas; set => _reservas = value; }
        
        public Veiculo(int id)
        {
            Id = id;
        }
        public Veiculo(int id, string nome, string cor, string combustivel, double preco)
        {
            Nome = nome;
            Cor = cor;
            Combustivel = combustivel;
            Preco = preco;
            Id = id;
        }
        public Veiculo(Veiculo v)
        {
            this.Nome = v.Nome;
            this.Cor = v.Cor;
            this.Combustivel = v.Combustivel;
            this.Preco = v.Preco;
            this.Id = v.Id;
        }
        public void AdicionarReserva(DateTime dataInicio, DateTime dataFim, string finalidade, int id)
        {
            Reserva r = new Reserva(dataInicio, dataFim, finalidade, id);
            Reservas.Add(r);
        }
        public List<Reserva> ListagemReservas()
        {
            return Reservas;
        }
        public override string ToString()
        {
            return $"{Nome.PadRight(14)} | {Cor.PadRight(8)} | {Combustivel.PadRight(11)} | {Preco.ToString(".00").PadLeft(7)}";
        }
        public string ToStringHTML()
        {
            return $"<th>{Nome}</th><th>{Cor}</th><th>{Combustivel}</th><th>{Preco.ToString(".00")}</th>";
        }
    }
}