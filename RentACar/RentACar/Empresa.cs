﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RentACar
{
    class Empresa
    {
        private int _idVeiculo = 0;
        private int _idCliente = 0;
        private List<Cliente> _Clientes = new List<Cliente>();
        private List<string> _combustiveis = new List<string>()
        {
            "Gasóleo","Gasolina","Híbrido","Elétrico","GPL"
        };
        private List<string> _caixas = new List<string>(){
            "Automático","Manual"
        };
        private List<Veiculo> _veiculos = new List<Veiculo>();
        private List<Reserva> _reservas = new List<Reserva>();
        internal List<string> Combustiveis { get => _combustiveis; set => _combustiveis = value; }
        internal List<string> Caixas { get => _caixas; set => _caixas = value; }
        internal List<Veiculo> Veiculos { get => _veiculos; set => _veiculos = value; }
        private int IdVeiculo { get => _idVeiculo; set => _idVeiculo = value; }
        private int IdCliente { get => _idCliente; set => _idCliente = value; }
        internal List<Cliente> Clientes { get => _Clientes; set => _Clientes = value; }
        internal List<Reserva> Reservas { get => _reservas; set => _reservas = value; }
        public Empresa()
        {
            BuildDatabase();
        }
        public int GetNextIdVeiculo()
        {
            IdVeiculo++;
            return IdVeiculo;
        }
        private int GetNextClientId()
        {
            IdCliente++;
            return IdCliente;
        }
        private void BuildDatabase()
        {
            BuildClientes();
            BuildCarros();
            SimularAvarias(DateTime.Now);
        }
        public void BuildCarros()
        {
            StreamReader read = new StreamReader(@"base.txt");
            List<string> file = new List<string>();
            while (!read.EndOfStream)
            {
                file.Add(read.ReadLine());
            }
            file.Add("");
            read.Close();
            int cnt = 0;
            string searchClass = " Carro ";
            foreach (string line in file)
            {
                if (line != "")
                {
                    Veiculo v = new Veiculo(GetNextIdVeiculo());
                    string line1 = line.Replace(searchClass, "!");
                    string[] split = line1.Split('!');
                    v.Nome = split[0].ToString();
                    string[] data = split[1].Split(' ');
                    v.Cor = data[0];
                    v.Combustivel = data[1];
                    data[2] = data[2].Replace("€", "");
                    v.Preco = double.Parse(data[2]);
                    switch (cnt)
                    {
                        case 0:
                            {
                                Carro c = new Carro(v);
                                c.NPortas = int.Parse(data[3]);
                                c.Caixa = data[4];
                                Veiculos.Add(c);
                                break;
                            }
                        case 1:
                            {
                                Mota c = new Mota(v);
                                data[3] = data[3].Replace("cc", "");
                                c.Cilindrada = int.Parse(data[3]);
                                Veiculos.Add(c);
                                break;
                            }
                        case 2:
                            {
                                Camioneta c = new Camioneta(v);
                                c.NEixos = int.Parse(data[3]);
                                c.NPassageiros = int.Parse(data[4]);
                                Veiculos.Add(c);
                                break;
                            }
                        case 3:
                            {
                                Camiao c = new Camiao(v);
                                data[3] = data[3].Replace("kg", "");
                                c.PesoMax = double.Parse(data[3]);
                                Veiculos.Add(c);
                                break;
                            }
                    }
                }
                else
                {
                    cnt++;
                    switch (cnt)
                    {
                        case 1: searchClass = " Mota "; break;
                        case 2: searchClass = " Camioneta "; break;
                        case 3: searchClass = " Camião "; break;
                    }
                }
            }
        }
        public void AdicionarReserva(DateTime dataInicio, DateTime dataFim, string finalidade, int id, int idVeiculo)
        {
            Reserva r = new Reserva(dataInicio, dataFim, finalidade, id, idVeiculo);
            Reservas.Add(r);
        }
        public List<Reserva> ListagemReservas()
        {
            return Reservas;
        }
        public void SimularAvarias(DateTime inicio)
        {
            Random random = new Random();
            List<string> avarias = new List<string>
            {
                "Avaria","Inspeção chumbada"
            };
            for (int i = 0; i < Veiculos.Count; i++)
            {
                int rnd = random.Next(0,128);
                if(rnd <= 4)
                {
                    AdicionarReserva(inicio, inicio.AddDays(random.Next(2, 7)), avarias[random.Next(0, 2)], 1337, Veiculos[i].Id);
                }
                else if (rnd <= 14)
                {
                    AdicionarReserva(inicio, inicio.AddDays(1), "Manutenção Regular", 1337, Veiculos[i].Id);
                }
                else if (rnd <= 49)
                {
                    AdicionarReserva(inicio, inicio.AddDays(1), "Limpeza", 1337, Veiculos[i].Id);
                }
            }
        }
        public void BuildClientes()
        {
            Cliente c = new Cliente("Referencia_Interna", 1337);
            Clientes.Add(c);
            StreamReader read = new StreamReader(@"nomes.txt");
            List<string> nomes = new List<string>();
            List<string> apelidos = new List<string>();
            while (true)
            {
                nomes.Add(read.ReadLine());
                if (nomes.Last() == "") { nomes.RemoveAt(nomes.Count - 1); break; }
            }
            while (!read.EndOfStream)
            {
                apelidos.Add(read.ReadLine());
            }
            read.Close();
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                string nome = $"{nomes[rnd.Next(0, 26)]} {apelidos[rnd.Next(0, 26)]}";
                AddCliente(nome);
            }
        }
        public void AddCliente(string nome)
        {
            Cliente c = new Cliente(nome, GetNextClientId());
            Clientes.Add(c);
        }
    }
}
