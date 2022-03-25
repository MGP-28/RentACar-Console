using System;
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
        private int _idReserva = 0;
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
        private int IdReserva { get => _idReserva; set => _idReserva = value; }
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
            foreach (string line in file)
            {
                string name = line.Substring(line.IndexOf("«") +1, (line.IndexOf("»") - line.IndexOf("«")) -1); // posiçao de « e intervalo entre « e »
                line.Replace($"«{name}» ", "");
                string[] split = line.Split(' '); //Elimina texto da classe e separa nome do veiculo do restante
                Veiculo v = new Veiculo(GetNextIdVeiculo());
                v.Nome = name.ToString(); //nome
                v.Cor = split[2];
                v.Combustivel = split[3];
                v.Preco = double.Parse(split[4]);
                switch (split[0])
                {
                    case "Carro":
                        {
                            Carro c = new Carro(v);
                            c.NPortas = int.Parse(split[5]);
                            c.Caixa = split[6];
                            Veiculos.Add(c);
                            break;
                        }
                    case "Mota":
                        {
                            Mota c = new Mota(v);
                            c.Cilindrada = int.Parse(split[5]);
                            Veiculos.Add(c);
                            break;
                        }
                    case "Camioneta":
                        {
                            Camioneta c = new Camioneta(v);
                            c.NEixos = int.Parse(split[5]);
                            c.NPassageiros = int.Parse(split[6]);
                            Veiculos.Add(c);
                            break;
                        }
                    case "Camiao":
                        {
                            Camiao c = new Camiao(v);
                            c.PesoMax = double.Parse(split[5]);
                            Veiculos.Add(c);
                            break;
                        }
                }
            }
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
                    AdicionarReserva(inicio.AddDays(random.Next(0, 7)), inicio.AddDays(random.Next(0, 10)), avarias[random.Next(0, 2)], Clientes[0].Id, Veiculos[i].Id);
                }
                else if (rnd <= 14)
                {
                    AdicionarReserva(inicio, inicio.AddDays(random.Next(0, 2)), "Manutenção Regular", Clientes[0].Id, Veiculos[i].Id);
                }
                else if (rnd <= 49)
                {
                    AdicionarReserva(inicio, inicio.AddDays(1), "Limpeza", Clientes[0].Id, Veiculos[i].Id);
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
        public void AdicionarReserva(DateTime dataInicio, DateTime dataFim, string finalidade, int id, int idVeiculo)
        {
            IdReserva++;
            Reserva r = new Reserva(dataInicio, dataFim, finalidade, id, idVeiculo, IdReserva);
            Reservas.Add(r);
        }
        public List<Reserva> ListagemReservas()
        {
            return Reservas;
        }
    }
}
