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
        private string _finalidade;
        private bool _isManutencao;
        public DateTime DataInicio { 
            get => _dataInicio;
            set => _dataInicio = value;
        }
        public DateTime DataFim { 
            get => _dataFim;
            set => _dataFim = value;
        }
        public string Finalidade { get => _finalidade; set => _finalidade = value; }
        public bool IsManutencao { get => _isManutencao; set => _isManutencao = value; }

        public Reserva(DateTime dataInicio, DateTime dataFim, string finalidade)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Finalidade = finalidade;
            IsManutencao = false;
        }
        public Reserva(DateTime dataInicio, DateTime dataFim, string finalidade, bool isManutencao)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Finalidade = finalidade;
            IsManutencao = isManutencao;
        }
    }
}
