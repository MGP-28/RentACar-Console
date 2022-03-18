using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Reserva
    {
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

        public Reserva(DateTime dataInicio, DateTime dataFim, string finalidade, int id, int idVeiculo)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Finalidade = finalidade;
            IdCliente = id;
            IdVeiculo = idVeiculo;
        }
        public override string ToString()
        {
            string finalidade = Finalidade;
            if (Finalidade.Length > Console.WindowWidth - 64) { finalidade = Finalidade.Remove(0, Console.WindowWidth - 64); }
            return $"{DataInicio.ToShortDateString().PadRight(11)} | {DataFim.ToShortDateString().PadRight(11)} | {IdCliente.ToString().PadRight(10)} | {IdVeiculo.ToString().PadRight(10)} | {finalidade}";
        }
    }
}
