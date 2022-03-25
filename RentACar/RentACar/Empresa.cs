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
            BuildReservas();
            SimularAvarias(DateTime.Now);
        }
        private void BuildCarros()
        {
            StreamReader read = new StreamReader(@"ficheiros/veiculos.txt");
            List<string> file = new List<string>();
            while (!read.EndOfStream)
            {
                file.Add(read.ReadLine());
            }
            read.Close();
            foreach (string line in file)
            {
                string name = line.Substring(line.IndexOf("«") +1, (line.IndexOf("»") - line.IndexOf("«")) -1); // posiçao de « e intervalo entre « e »
                string[] split = line.Replace($"«{name}» ", "").Split(' '); //Elimina texto da classe e separa nome do veiculo do restante
                Veiculo v = new Veiculo(GetNextIdVeiculo());
                v.Nome = name.ToString(); //nome
                v.Id = int.Parse(split[1]);
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
            foreach (Reserva reserva in Reservas)
            {
                if (reserva.DataInicio.Date == DateTime.Now.Date && reserva.Finalidade == "Limpeza") { return; }
            }
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
        private void BuildClientes()
        {
            StreamReader read = new StreamReader(@"ficheiros/clientes.txt");
            List<string> file = new List<string>();
            Cliente c = new Cliente("Referencia_Interna", 1337);
            Clientes.Add(c);
            while (!read.EndOfStream)
            {
                file.Add(read.ReadLine());
            }
            read.Close();
            file.RemoveAt(0);
            foreach (string line in file)
            {
                string[] split = line.Split('#');
                c = new Cliente(split[1], int.Parse(split[0]));
                Clientes.Add(c); IdCliente = c.Id;
            }
            read = new StreamReader(@"ficheiros/nomes.txt"); //20 novos clientes aleatorios
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
        private void BuildReservas()
        {
            StreamReader read = new StreamReader(@"ficheiros/reservas.txt");
            List<string> file = new List<string>();
            while (!read.EndOfStream)
            {
                file.Add(read.ReadLine());
            }
            read.Close();
            foreach (string line in file)
            {
                DateTime inicio = new DateTime(); DateTime fim = new DateTime();
                string finalidade = line.Substring(line.IndexOf("«") + 1, (line.IndexOf("»") - line.IndexOf("«")) - 1);
                string[] split = line.Replace($"«{finalidade}»", "").Split(' ');
                if (!DateTime.TryParse(split[3], out inicio)) { //data inicio
                    string[] date = split[3].Split('/'); string hold = date[0]; date[0] = date[1]; date[1] = hold; //switch day/month
                    DateTime.TryParse($"{int.Parse(date[0])}/{int.Parse(date[1])}/{int.Parse(date[2])}", out inicio); } //try again
                if (!DateTime.TryParse(split[4], out fim)) { //data fim
                    string[] date = split[4].Split('/'); string hold = date[0]; date[0] = date[1]; date[1] = hold; //switch day/month
                    DateTime.TryParse($"{int.Parse(date[0])}/{int.Parse(date[1])}/{int.Parse(date[2])}", out fim);
                }
                Reserva r = new Reserva(inicio, fim, finalidade, int.Parse(split[2]), int.Parse(split[1]), int.Parse(split[0]));
                Reservas.Add(r);
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
        public void DataToFiles()
        {
            StreamWriter write = new StreamWriter(@"ficheiros/veiculos.txt",false);
            foreach (Veiculo veiculo in Veiculos)
            {
                write.WriteLine(veiculo.ToStringToFile());
            }
            write.Close();
            write = new StreamWriter(@"ficheiros/reservas.txt", false);
            foreach (Reserva reserva in Reservas)
            {
                write.WriteLine(reserva.ToStringToFile());
            }
            write.Close();
            write = new StreamWriter(@"ficheiros/clientes.txt", false);
            foreach (Cliente cliente in Clientes)
            {
                write.WriteLine(cliente.ToStringToFile());
            }
            write.Close();
        }
    }
}
