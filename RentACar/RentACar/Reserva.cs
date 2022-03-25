using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Reserva
    {
        private int _idReserva;
        private DateTime _dataInicio;
        private DateTime _dataFim;
        private int _idCliente;
        private int _idVeiculo;
        private string _finalidade;
        public DateTime DataInicio { get => _dataInicio; set => _dataInicio = value; }
        public DateTime DataFim { get => _dataFim; set => _dataFim = value; }
        public string Finalidade { get => _finalidade; set => _finalidade = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public int IdVeiculo { get => _idVeiculo; set => _idVeiculo = value; }
        public int IdReserva { get => _idReserva; set => _idReserva = value; }

        public Reserva()
        {
            DataInicio = DateTime.MinValue;
            DataFim = DateTime.MinValue;
            Finalidade = "";
            IdCliente = -1;
            IdVeiculo = -1;
            Random random = new Random();
            IdReserva = random.Next(0, 100000000);
        }
        public Reserva(Reserva reserva)
        {
            this.DataInicio = reserva.DataInicio;
            this.DataFim = reserva.DataFim;
            this.Finalidade = reserva.Finalidade;
            this.IdCliente = reserva.IdCliente;
            this.IdVeiculo = reserva.IdVeiculo;
            this.IdReserva = reserva.IdReserva;
        }
        public Reserva(DateTime dataInicio, DateTime dataFim, string finalidade, int idCliente, int idVeiculo, int idReserva)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Finalidade = finalidade;
            IdCliente = idCliente;
            IdVeiculo = idVeiculo;
            IdReserva = idReserva;
        }
        public override string ToString()
        {
            string finalidade = Finalidade;
            if (Finalidade.Length > Console.WindowWidth - 64) { finalidade = Finalidade.Remove(0, Console.WindowWidth - 64); }
            return $"{DataInicio.ToShortDateString().PadRight(11)} | {DataFim.ToShortDateString().PadRight(11)} | {IdCliente.ToString().PadRight(10)} | {IdVeiculo.ToString().PadRight(10)} | {finalidade}";
        }
        public void ToStringDoc()
        {

        }
    }
}
