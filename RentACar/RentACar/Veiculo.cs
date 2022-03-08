﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar
{
    class Veiculo
    {
        private List<Reserva> _reserva = new List<Reserva>();
        private string _nome;
        private string _cor;
        private string _combustivel;
        private double _preco;
        public string Nome { get => _nome; set => _nome = value; }
        public string Cor { get => _cor; set => _cor = value; }
        public string Combustivel { get => _combustivel; set => _combustivel = value; }
        public double Preco { get => _preco; set => _preco = value; }
        public Veiculo()
        {
        }
        public Veiculo(string nome, string cor, string combustivel, double preco)
        {
            Nome = nome;
            Cor = cor;
            Combustivel = combustivel;
            Preco = preco;
        }
        public Veiculo(Veiculo v)
        {
            this.Nome = v.Nome;
            this.Cor = v.Cor;
            this.Combustivel = v.Combustivel;
            this.Preco = v.Preco;
        }
        public void AdicionarReserva(DateTime dataInicio, DateTime dataFim, string finalidade)
        {
            Reserva r = new Reserva(dataInicio, dataFim, finalidade);
            _reserva.Add(r);
        }
    }
}
