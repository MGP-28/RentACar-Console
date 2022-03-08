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
        private List<string> _combustiveis = new List<string>()
        {
            "Gasóleo","Gasolina","Híbrido","Elétrico","GPL"
        };
        private List<string> _caixas = new List<string>(){
            "Automático","Manual"
        };
        private List<Veiculo> _veiculos = new List<Veiculo>();
        internal List<string> Combustiveis { get => _combustiveis; set => _combustiveis = value; }
        internal List<string> Caixas { get => _caixas; set => _caixas = value; }
        internal List<Veiculo> Veiculos { get => _veiculos; set => _veiculos = value; }
        public Empresa()
        {
            BuildDatabase();
        }
        private void BuildDatabase()
        {
            StreamReader read = new StreamReader(@"base.txt");
            List<string> file = new List<string>();
            while (!read.EndOfStream)
            {
                file.Add(read.ReadLine());
            }
            file.Add("");
            int cnt = 0;
            string searchClass = " Carro ";
            foreach (string line in file)
            {
                if(line != "")
                {
                    Veiculo v = new Veiculo();
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
        public int SimularAvarias(DateTime inicio)
        {
            Random random = new Random();
            List<string> avarias = new List<string>
            {
                "Avaria","Inspeção chumbada","Limpeza"
            };
            int cnt = 0;
            foreach (Veiculo v in Veiculos)
            {
                if(random.Next(0,99) <= 5)
                {
                    v.AdicionarReserva(inicio, inicio.AddDays(random.Next(1, 7)), avarias[random.Next(0,2)]);
                    cnt++;
                }
            }
            return cnt;
        }
    }
}
