using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RentACar
{
    internal class Program
    {
        static void DesenharTitulo(string titulo)
        {
            DesenharDivisoria();
            Console.WriteLine($" | {titulo.PadRight(Console.WindowWidth - 6)} |");
            DesenharDivisoria();
        }
        static void DesenharLinha(string texto)
        {
            Console.WriteLine($" | {texto.PadRight(Console.WindowWidth - 6)} |");
        }
        static void DesenharLinha(string texto, int n)
        {
            Console.WriteLine($" | {n.ToString().PadLeft(3)} | {texto.PadRight(Console.WindowWidth - 12)} |");
        }
        static void DesenharDivisoria()
        {
            string empty = "";
            Console.WriteLine($" +{empty.PadRight(Console.WindowWidth - 4, '-')}+");
        }
        static void DesenharMenu()
        {
            List<string> opcoesMenu = new List<string>
            {
            "Simulador de reservas","Alterar reserva","Procurar reserva","Ver Veiculos disponíveis no momento","Ver Veiculos em manutenção",
            "Clientes","Inserir novo veículo","Consultar ganhos","Exportar informação para HTML"
            };
            Console.WriteLine("");
            DesenharTitulo("Benvindo!");
            for (int i = 0; i < opcoesMenu.Count; i++)
            {
                DesenharLinha(opcoesMenu[i],i+1);
                DesenharDivisoria();
            }
            Console.WriteLine();
            DesenharDivisoria();
            DesenharLinha("Sair", 0);
            DesenharDivisoria();
        }
        static void AlinharInput()
        {
            Console.Write(" >>> ");
        }
        static bool VerifString(string s)
        {
            if (s.Count() < 1)
                return false;
            return true;
        }
        static bool VerifNum(int n, int min)
        {
            if (n < min)
                return false;
            return true;
        }
        static bool VerifNum(int n, int min, int max)
        {
            if (n >= min && n <= max)
                return true;
            return false;
        }
        static bool VerifNum(double n)
        {
            if (n < 0)
                return false;
            return true;
        }
        static bool VerifNum(double n, double min)
        {
            if (n < min)
                return false;
            return true;
        }
        static bool VerifNum(double n, double min, double max)
        {
            if (n > min && n < max)
                return true;
            return false;
        }
        static void MensagemErro(string s)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            DesenharTitulo(s);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ReadKey(true);
        }
        static Veiculo VerificarVeiculo(List<Veiculo> veiculos, int id)
        {
            foreach (Veiculo veiculo in veiculos)
            {
                if (id == veiculo.Id) { 
                    return veiculo; }
            }
            Veiculo v = new Veiculo(-1); return v;
        }
        static Cliente VerificarCliente(List<Cliente> clientes, int id)
        {
            foreach (Cliente cliente in clientes)
            {
                if (id == cliente.Id) { return cliente; }
            }
            Cliente c = new Cliente("Erro", -1); return c;
        }
        static void MostrarDadosConfirmacaoInserirVeiculo(string nome, string cor, string combustivel,double preco)
        {
            Console.Clear();
            DesenharTitulo("Pretende confirmar os dados? (Sim / Não)");
            DesenharLinha("Nome: " + nome);
            DesenharLinha("Cor: " + cor);
            DesenharLinha("Combustível: " + combustivel);
            DesenharLinha("Preço: " + preco.ToString(".00") + " €");
        }
        static bool ConfirmacaoReserva(Veiculo viatura, DateTime inico, DateTime fim, Cliente cliente)
        {
            do
            {
                string tipo = (viatura.GetType()).ToString().Replace("RentACar.", "");
                Console.Clear();
                DesenharTitulo("Confirmação de reserva");
                DesenharLinha("Nome: " + cliente.Nome);
                DesenharLinha("ID Cliente: " + cliente.Id);
                DesenharLinha("Inicio: " + inico.ToShortDateString());
                DesenharLinha("Fim: " + fim.ToShortDateString());
                DesenharLinha("Tipo: " + tipo);
                DesenharLinha("Marca/Modelo: " + viatura.Nome);
                DesenharLinha("Cor: " + viatura.Cor);
                DesenharLinha("Combustível: " + viatura.Combustivel);
                DesenharLinha("Preço: " + viatura.Preco.ToString(".00") + " €");
                DesenharTitulo("Pretende confirmar a reserva? (Sim / Nao)");
                AlinharInput();
                string confirmacao = Console.ReadLine();
                if (confirmacao == "Nao") return false;
                else if (confirmacao != "Sim") { MensagemErro("Introduza 'Sim' ou 'Nao' !"); }
                else return true;
            } while(true);
        }
        static int InserirTipoVeiculo()
        {
            char op;
            do
            {
                Console.Clear();
                DesenharTitulo("Qual o tipo de veículo? (0 para cancelar)");
                List<string> lista = new List<string>()
                {
                    "Carro","Mota","Camioneta","Camião"
                };
                for (int i = 0; i < lista.Count; i++)
                {
                    DesenharLinha(lista[i], i + 1);
                }
                DesenharDivisoria();
                AlinharInput();
                op = Console.ReadKey(true).KeyChar;
            } while (op < '0' || op > '4');
            return int.Parse(op.ToString());
        }
        static void InserirNovoVeículo(ref Empresa empresa)
        {
            bool pass = true;
            int op = InserirTipoVeiculo();
            if (op == 0) return;
            string nome;
            do
            {
                pass = true;
                Console.Clear();
                DesenharTitulo("Insira a marca/modelo do veículo (0 para cancelar)");
                AlinharInput();
                nome = Console.ReadLine();
                if (!VerifString(nome)) { pass = false; continue; }
            } while (!pass);
            if (nome == "0") return;
            string cor;
            do
            {
                pass = true;
                Console.Clear();
                DesenharTitulo("Insira a cor do veículo (0 para cancelar)");
                AlinharInput();
                cor = Console.ReadLine();
                if (!VerifString(cor)) { pass = false; continue; }
            } while (!pass);
            if (cor == "0") return;
            char combustivelOp;
            int numFuels = empresa.Combustiveis.Count;
            do
            {
                Console.Clear();
                DesenharTitulo("Qual o tipo de veículo? (0 para cancelar)");
                for (int i = 0; i < empresa.Combustiveis.Count; i++) { DesenharLinha(empresa.Combustiveis[i], i + 1); }
                DesenharDivisoria();
                AlinharInput();
                combustivelOp = Console.ReadKey(true).KeyChar;
            } while (combustivelOp < '0' || combustivelOp > char.Parse(numFuels.ToString()));
            if (combustivelOp == '0') return;
            string combustivel = empresa.Combustiveis[int.Parse(combustivelOp.ToString())-1];
            double preco = 0;
            do
            {
                pass = true;
                Console.Clear();
                DesenharTitulo("Insira o preço diário do veículo (0 para cancelar)");
                AlinharInput();
                string s = Console.ReadLine();
                if (!VerifString(s)) { pass = false; continue; }
                else
                {
                    if (s == "0") return;
                    if(double.TryParse(s,out _))
                    {
                        if (!VerifNum(double.Parse(s), 0))
                        { MensagemErro("Inválido > Preço tem de ser maior do que 0"); pass = false; }
                        else
                        { preco = double.Parse(s); }
                    }
                    else { MensagemErro("Inválido > Apenas números são válidos"); pass = false; }
                }
            } while (!pass);
            Veiculo v = new Veiculo(empresa.GetNextIdVeiculo(),nome,cor,combustivel,preco);
            switch (op)
            {
                case 1:
                    {
                        char caixaOp = '0';
                        do
                        {
                            Console.Clear();
                            DesenharTitulo("Qual o tipo de caixa do veículo? (0 para cancelar)");
                            for (int i = 0; i < empresa.Caixas.Count; i++)
                            { DesenharLinha(empresa.Caixas[i], i + 1); }
                            DesenharDivisoria();
                            AlinharInput();
                            caixaOp = Console.ReadKey(true).KeyChar;
                        } while (caixaOp < '0' || caixaOp > '2');
                        if (caixaOp == '0') return;
                        string caixa = empresa.Caixas[int.Parse(caixaOp.ToString())-1];
                        int nPortas = 0;
                        do
                        {
                            pass = true;
                            Console.Clear();
                            DesenharTitulo("Insira o número de portas do veículo (0 para cancelar)");
                            AlinharInput();
                            string s = Console.ReadLine();
                            if (!VerifString(s))
                            { pass = false; continue; }
                            else
                            {
                                if (s == "0") return;
                                if (int.TryParse(s, out _))
                                {
                                    if (!VerifNum(int.Parse(s), 0))
                                    { MensagemErro("Inválido > Número de portas tem de ser maior do que 0"); pass = false; }
                                    else nPortas = int.Parse(s);
                                }
                                else
                                { MensagemErro("Inválido > Apenas números inteiros positivos são válidos"); pass = false; }
                            }
                        } while (!pass);
                        Carro c = new Carro(v, nPortas, caixa);
                        string confirmacao = "";
                        do
                        {
                            MostrarDadosConfirmacaoInserirVeiculo(nome, cor, combustivel, preco);
                            DesenharLinha("Nº Portas: " + nPortas);
                            DesenharLinha("Caixa: " + caixa);
                            DesenharDivisoria();
                            AlinharInput();
                            pass = true;
                            confirmacao = Console.ReadLine();
                            if (confirmacao == "Não") return;
                            else if(confirmacao != "Sim")
                            { MensagemErro("Introduza 'Sim' ou 'Não' !"); pass = false; continue; }
                        } while (!pass);
                        empresa.Veiculos.Add(c); break;
                    }
                case 2:
                    {
                        int cilindrada = 0;
                        do
                        {
                            pass = true;
                            Console.Clear();
                            DesenharTitulo("Insira a cilindrada do veículo (0 para cancelar)");
                            AlinharInput();
                            string s = Console.ReadLine();
                            if (!VerifString(s))
                            { pass = false; continue; }
                            else
                            {
                                if (s == "0") return;
                                if (int.TryParse(s, out _))
                                {
                                    if (!VerifNum(int.Parse(s), 0))
                                    { MensagemErro("Inválido > Cilindrada tem de ser maior do que 0"); pass = false; }
                                    else cilindrada = int.Parse(s);
                                }
                                else
                                { MensagemErro("Inválido > Apenas números inteiros positivos são válidos"); pass = false; }
                            }
                        } while (!pass);
                        Mota c = new Mota(v, cilindrada);
                        string confirmacao = "";
                        do
                        {
                            MostrarDadosConfirmacaoInserirVeiculo(nome, cor, combustivel, preco);
                            DesenharLinha("Cilindrada: " + cilindrada);
                            DesenharDivisoria();
                            AlinharInput();
                            pass = true;
                            confirmacao = Console.ReadLine();
                            if (confirmacao == "Não") return;
                            else if (confirmacao != "Sim")
                            { MensagemErro("Introduza 'Sim' ou 'Não' !"); pass = false; continue; }
                        } while (!pass);
                        empresa.Veiculos.Add(c); break;
                    }
                case 3:
                    {
                        int nEixos = 0;
                        do
                        {
                            pass = true;
                            Console.Clear();DesenharTitulo("Insira o número de eixos do veículo (0 para cancelar)");AlinharInput();
                            string s = Console.ReadLine();
                            if (!VerifString(s))
                            { pass = false; continue;
                            }
                            else
                            {
                                if (s == "0") return;
                                if (int.TryParse(s, out _))
                                {
                                    if (!VerifNum(int.Parse(s), 2))
                                    { MensagemErro("Inválido > Número de eixos tem de ser maior do que 2"); pass = false; }
                                    else nEixos = int.Parse(s);
                                }
                                else { MensagemErro("Inválido > Apenas números inteiros positivos são válidos"); pass = false; }
                            }
                        } while (!pass);
                        int nPassageiros = 0;
                        do
                        {
                            pass = true;
                            Console.Clear();DesenharTitulo("Insira o número máximo de passageiros do veículo (0 para cancelar)");AlinharInput();
                            string s = Console.ReadLine();
                            if (!VerifString(s))
                            { pass = false; continue; }
                            else
                            {
                                if (s == "0") return;
                                if (int.TryParse(s, out _))
                                {
                                    if (!VerifNum(int.Parse(s), 0))
                                    { MensagemErro("Inválido > Número de passageiros tem de ser maior do que 0"); pass = false; }
                                    else
                                        nPassageiros = int.Parse(s);
                                }
                                else
                                { MensagemErro("Inválido > Apenas números inteiros positivos são válidos"); pass = false; }
                            }
                        } while (!pass);
                        Camioneta c = new Camioneta(v, nEixos, nPassageiros);
                        string confirmacao = "";
                        do
                        {
                            MostrarDadosConfirmacaoInserirVeiculo(nome, cor, combustivel, preco);DesenharLinha("Nº Eixos: " + nEixos);DesenharLinha("Nº Passageiros: " + nPassageiros);DesenharDivisoria(); AlinharInput();
                            pass = true;
                            confirmacao = Console.ReadLine();
                            if (confirmacao == "Não")
                                return;
                            else if (confirmacao != "Sim")
                            { MensagemErro("Introduza 'Sim' ou 'Não' !"); pass = false; continue; }
                        } while (!pass);
                        empresa.Veiculos.Add(c); break;
                    }
                case 4:
                    {
                        double pesoMax = 0;
                        do
                        {
                            pass = true;
                            Console.Clear();DesenharTitulo("Insira o peso máximo do veículo (0 para cancelar)");AlinharInput();
                            string s = Console.ReadLine();
                            if (!VerifString(s))
                            { pass = false; continue; }
                            else
                            {
                                if (s == "0") return;
                                if (double.TryParse(s, out _))
                                {
                                    if (!VerifNum(double.Parse(s), 0))
                                    { MensagemErro("Inválido > Preço tem de ser maior do que 0"); pass = false; }
                                    else
                                    { pesoMax = double.Parse(s); }
                                }
                                else
                                { MensagemErro("Inválido > Apenas números são válidos"); pass = false; }
                            }
                        } while (!pass);
                        Camiao c = new Camiao(v, pesoMax);
                        string confirmacao = "";
                        do
                        {
                            MostrarDadosConfirmacaoInserirVeiculo(nome, cor, combustivel, preco); DesenharLinha("Peso máx.: " + pesoMax); DesenharDivisoria(); AlinharInput();
                            pass = true;
                            confirmacao = Console.ReadLine();
                            if (confirmacao == "Não") return;
                            else if (confirmacao != "Sim")
                            { MensagemErro("Introduza 'Sim' ou 'Não' !"); pass = false; continue; }
                        } while (!pass);
                        empresa.Veiculos.Add(c); break;
                    }
            }
            DesenharTitulo("Veículo inserido com sucesso!");
            Console.ReadKey(true);
        }
        static List<Veiculo> VeiculosPorClasse(List<Veiculo> veiculos, int n)
        {
            List<Veiculo> lista = new List<Veiculo>();
            switch (n)
            {
                case 1:
                    {
                        foreach (Veiculo veiculo in veiculos)
                        {
                            if (veiculo.GetType() == typeof(Carro))
                                lista.Add(veiculo);
                        } break;
                    }
                case 2:
                    {
                        foreach (Veiculo veiculo in veiculos)
                        {
                            if (veiculo.GetType() == typeof(Mota))
                                lista.Add(veiculo);
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (Veiculo veiculo in veiculos)
                        {
                            if (veiculo.GetType() == typeof(Camioneta))
                                lista.Add(veiculo);
                        }
                        break;
                    }
                case 4:
                    {
                        foreach (Veiculo veiculo in veiculos)
                        {
                            if (veiculo.GetType() == typeof(Camiao))
                                lista.Add(veiculo);
                        }
                        break;
                    }
            }
            return lista;
        }
        static List<Veiculo> VerificarDisponiveisData(List<Veiculo> veiculos, List<Reserva> reservas, DateTime data)
        {
            foreach (Reserva reserva in reservas)
            {
                if (data.DayOfYear >= reserva.DataInicio.DayOfYear && data.DayOfYear <= reserva.DataFim.DayOfYear)
                {
                    foreach (Veiculo veiculo in veiculos)
                    {
                        if (reserva.IdVeiculo == veiculo.Id) { veiculos.Remove(veiculo); break; }
                    }
                }
            }
            return veiculos;
        }
        static List<Veiculo> VerificarDisponiveis(DateTime dataInicio, DateTime dataFim, List<Veiculo> veiculos, List<Reserva> reservas)
        {
            List<Veiculo> veiculosDisponiveis = new List<Veiculo>(veiculos);
            foreach (Reserva reserva in reservas)
            {
                if (dataInicio.CompareTo(reserva.DataInicio) < 0 && dataFim.CompareTo(reserva.DataInicio) >= 0 && dataFim.CompareTo(reserva.DataFim) <= 0 //Final na reserva
                    || dataFim.CompareTo(reserva.DataFim) > 0 && dataInicio.CompareTo(reserva.DataInicio) >= 0 && dataInicio.CompareTo(reserva.DataFim) <= 0 //Inicio na reserva
                    || dataInicio.CompareTo(reserva.DataInicio) < 0 && dataFim.CompareTo(reserva.DataFim) > 0 //Reserva a meio do intervalo
                    || dataInicio.CompareTo(reserva.DataInicio) >= 0 && dataFim.CompareTo(reserva.DataFim) <= 0) //Reserva engloba o intervalo
                { veiculosDisponiveis.Remove(VerificarVeiculo(veiculos, reserva.IdVeiculo)); }
            }
            return veiculosDisponiveis;
        }
        static List<Reserva> VerificarDisponiveis(DateTime dataInicio, DateTime dataFim, List<Reserva> reservas)
        {
            List <Reserva> encontrados = new List<Reserva>(reservas);
            foreach (Reserva reserva in reservas)
            {
                if (dataInicio.CompareTo(reserva.DataInicio) < 0 && dataFim.CompareTo(reserva.DataInicio) >= 0 && dataFim.CompareTo(reserva.DataFim) <= 0 //Final na reserva
                    || dataFim.CompareTo(reserva.DataFim) > 0 && dataInicio.CompareTo(reserva.DataInicio) >= 0 && dataInicio.CompareTo(reserva.DataFim) <= 0 //Inicio na reserva
                    || dataInicio.CompareTo(reserva.DataInicio) < 0 && dataFim.CompareTo(reserva.DataFim) > 0 //Reserva a meio do intervalo
                    || dataInicio.CompareTo(reserva.DataInicio) >= 0 && dataFim.CompareTo(reserva.DataFim) <= 0) //Reserva engloba o intervalo
                { encontrados.Remove(reserva); }
            }
            return encontrados;
        }
        static void VerVeiculosDisponiveisAgora(List<Veiculo> veiculos, int op, List<Reserva> reservas)
        {
            switch (op)
            {
                case 1:
                    { veiculos = VeiculosPorClasse(veiculos, 1); break; }
                case 2:
                    { veiculos = VeiculosPorClasse(veiculos, 2); break; }
                case 3:
                    { veiculos = VeiculosPorClasse(veiculos, 3); break; }
                case 4:
                    { veiculos = VeiculosPorClasse(veiculos, 4); break; }
            }
            veiculos = VerificarDisponiveisData(veiculos, reservas, DateTime.Now);
            VerVeiculosDisponiveis(veiculos, op, true);
        }
        static void VerVeiculosDisponiveis(List<Veiculo> veiculos, int op, bool isHoje)
        {
            string titulos = $"{"ID".PadLeft(3)} | {"Nome".PadRight(14)} | {"Cor".PadRight(8)} | { "Combustivel".PadRight(8)} | { "Preco €"}";
            string apendice = "";
            if(isHoje) apendice = " disponíveis no presente dia";
            switch (op)
            {
                case 1:
                    {
                        DesenharTitulo("Carros" + apendice);
                        DesenharLinha(titulos + $"{" | Nº Portas".ToString()} | {"Caixa".PadRight(10)}");
                        break;
                    }
                case 2:
                    {
                        DesenharTitulo("Motas" + apendice);
                        DesenharLinha(titulos + " | Cilindrada".PadRight(10));
                        break;
                    }
                case 3:
                    {
                        DesenharTitulo("Camionetas" + apendice);
                        DesenharLinha(titulos + $" | {"Nº Eixos".ToString()} | {"Nº Passageiros".ToString()}");
                        break;
                    }
                case 4:
                    {
                        DesenharTitulo("Camiões" + apendice);
                        DesenharLinha(titulos + " | Peso Max.");
                        break;
                    }
            }
            DesenharDivisoria();
            if (veiculos.Count == 0)
                DesenharLinha("Não existem Veiculos disponíveis!");
            foreach (Veiculo veiculo in veiculos)
            {
                DesenharLinha(veiculo.ToString(), veiculo.Id);
            }
            DesenharDivisoria();
        }
        static void VerTodosVeiculos(List<Veiculo> todosVeiculos)
        {
            for (int i = 0; i < 4; i++) {
                List<Veiculo> veiculosClasse = VeiculosPorClasse(todosVeiculos, i + 1);
                VerVeiculosDisponiveis(veiculosClasse, i + 1, false); 
            }
        }
        static List<Veiculo> VerificarManutencao(DateTime data, List<Veiculo> veiculos, List<Reserva> reservas, ref List<string> motivos, ref List<DateTime> fimManutencao)
        {
            List<Veiculo> manutencao = new List<Veiculo>();
            foreach (Reserva reserva in reservas)
            {
                if (data.DayOfYear >= reserva.DataInicio.DayOfYear && data.DayOfYear <= reserva.DataFim.DayOfYear && reserva.IdCliente == 1337)
                { 
                    manutencao.Add(VerificarVeiculo(veiculos,reserva.IdVeiculo)); 
                    motivos.Add(reserva.Finalidade);
                    fimManutencao.Add(reserva.DataFim);
                }
            }
            return manutencao;
        }
        static void VerVeiculosManuntenção(Empresa empresa)
        {
            List<string> motivos = new List<string>(); List<DateTime> fimManutencao = new List<DateTime>();
            List<Veiculo> veiculos = VerificarManutencao(DateTime.Now, empresa.Veiculos, empresa.Reservas, ref motivos, ref fimManutencao);
            Console.Clear();
            DesenharTitulo("Veiculos em manutenção");
            DesenharLinha($"{"Tipo".PadRight(9)} | {"ID".PadLeft(4)} | { "Nome".PadRight(14)} | {"Cor".PadRight(8)} | { "Combustivel".PadRight(8)} | { "Preco €"} | Data final | Motivo");
            DesenharDivisoria();
            for (int i = 0; i < veiculos.Count; i++)
            {
                string tipo = veiculos[i].GetType().ToString().Replace("RentACar.", "");
                string info = veiculos[i].ToString().Remove(49);
                DesenharLinha($"{tipo.PadRight(9)} | {veiculos[i].Id.ToString().PadLeft(4)} | {info} | {fimManutencao[i].ToShortDateString()} | {motivos[i]}");
            }
            DesenharDivisoria();
            Console.ReadKey();
        }
        static DateTime VerifData(string input)
        {
            DateTime data = new DateTime();
            if(DateTime.TryParse(input, out _))
            {
                data = DateTime.Parse(input);
            }
            return data;
        }
        static int CriarCliente(ref Empresa empresa)
        {
            string s;
            do
            {
                Console.Clear(); DesenharTitulo("Introduza o nome do novo cliente:"); AlinharInput();
                s = Console.ReadLine();
                if (VerifString(s)) break;
            } while (true);
            empresa.AddCliente(s);
            return empresa.Clientes.Last().Id;
        }
        static void ListarClientes(List<Cliente> clientes, bool porNome)
        {
            string s = "nome";
            if (porNome) { clientes = clientes.OrderBy(cliente => cliente.Nome).ToList(); s = "ID"; }
            DesenharTitulo($"Listagem de Clientes (* para ordenar por {s})");
            DesenharLinha($"{"ID".PadLeft(4)} | Nome");
            DesenharDivisoria();
            if (clientes.Count == 0)
                DesenharLinha("Não existem clientes registados!");
            for (int i = 0; i < clientes.Count; i++)
            {
                DesenharLinha(clientes[i].ToString());
            }
            DesenharDivisoria();
        }
        static void SimularReserva(ref Empresa empresa)
        {
            DateTime dataInicio = new DateTime();
            do
            {
                Console.Clear(); DesenharTitulo("Insira data de inicio (+ para dia de hoje, 0 para cancelar)"); AlinharInput();
                string s; dataInicio = DateTime.Now;
                do
                {
                    s = Console.ReadLine();
                } while (!VerifString(s));
                if (s == "0")
                    return;
                else if (s != "+")
                    dataInicio = VerifData(s);
                if (dataInicio.CompareTo(DateTime.MinValue) != 0 && dataInicio.Year >= DateTime.Now.Year && dataInicio.DayOfYear >= DateTime.Now.DayOfYear) break;
                else MensagemErro("Introduza uma data válida");
            } while (true);
            DateTime dataFim = new DateTime();
            do
            {
                Console.Clear(); DesenharTitulo("Insira data de fim (+ para a mesma data de início, 0 para cancelar)"); AlinharInput();
                string s; dataFim = dataInicio;
                do
                {
                    s = Console.ReadLine();
                } while (!VerifString(s));
                if (s == "0")
                    return;
                else if (s != "+")
                    dataFim = VerifData(s);
                if (dataFim.CompareTo(DateTime.MinValue) != 0 && dataFim.Year >= dataInicio.Year && dataFim.DayOfYear >= dataInicio.DayOfYear) break;
                else MensagemErro("Introduza uma data válida");
            } while (true);
            List<Veiculo> veiculos = new List<Veiculo>(); List<Veiculo> disponiveis = new List<Veiculo>(); int tipoVeiculo;
            do
            {
                tipoVeiculo = InserirTipoVeiculo();
                if (tipoVeiculo == 0) return;
                veiculos = VeiculosPorClasse(empresa.Veiculos, tipoVeiculo);
                if (veiculos.Count == 0) MensagemErro("Não existem viaturas disponíveis");
            } while (veiculos.Count == 0);            
            do
            {
                int idVeiculo; int cliente = -1; string s;
                do
                {
                    do
                    {
                        Console.Clear(); DesenharTitulo($"Inicio: {dataInicio.ToShortDateString()} | Fim: {dataFim.ToShortDateString()}");
                        disponiveis = VerificarDisponiveis(dataInicio, dataFim, veiculos, empresa.Reservas);
                        VerVeiculosDisponiveis(disponiveis, tipoVeiculo, false); DesenharLinha("Introduza o número correspondente à viatura para continuar (N/B para avançar/recuar um dia, 0 para cancelar)"); DesenharDivisoria(); AlinharInput(); 
                        s = Console.ReadLine();
                        if (s == "N" || s == "n") { dataInicio = dataInicio.AddDays(1); dataFim = dataFim.AddDays(1); }
                        else if (s == "B" || s == "b") {
                            if (dataInicio.CompareTo(DateTime.Now) > 0) { dataInicio = dataInicio.AddDays(-1); dataFim = dataFim.AddDays(-1); }
                            else MensagemErro("Data de início de reserva já corresponde ao presente dia!"); 
                        }
                    } while (!VerifString(s) || !int.TryParse(s,out _));
                    if (s == "0")
                        return;
                    idVeiculo = int.Parse(s);
                    if (VerificarVeiculo(disponiveis, int.Parse(s)).Id != -1) break;
                    else MensagemErro("Inválido!");
                } while (true);
                do
                {
                    Console.Clear(); DesenharTitulo("Introduza o cliente (* para listagem, + para novo cliente, - para referência interna, 0 para cancelar)"); AlinharInput();
                    s = Console.ReadLine();
                    switch (s)
                    {
                        case "*":
                            {
                                { ConsultarClientes(empresa.Clientes); break; }
                            }
                        case "+": { cliente = CriarCliente(ref empresa); break; }
                        case "-": { cliente = 1337; break; }
                        case "0": return;
                        default: { int.TryParse(s, out cliente); break; }
                    }
                    if (cliente >= 0 && cliente <= empresa.Clientes.Last().Id) break;
                    else if(s != "*") MensagemErro("Inválido");
                } while (true);
                if (ConfirmacaoReserva(VerificarVeiculo(disponiveis, idVeiculo),dataInicio,dataFim,VerificarCliente(empresa.Clientes,cliente)))
                {
                    Console.Clear(); DesenharTitulo("Anotaçao para a reserva (0 para cancelar)"); AlinharInput();
                    s = Console.ReadLine();
                    string anotacao = "";
                    if (s == "0") return;
                    else if (s != "" && cliente != 0) anotacao = $"Reserva: {s}";
                    else anotacao = s;
                    empresa.AdicionarReserva(dataInicio, dataFim, anotacao, cliente, idVeiculo);
                    break;
                }
            } while (true);
        }
        static List<Reserva> ProcurarReservas(List<Reserva> reservas, int idCliente, int idVeiculo, DateTime dataInicio, DateTime dataFim)
        {
            List<Reserva> encontrados = new List<Reserva>();
            if(idCliente != -1 && idVeiculo != -1) //iCliente + idVeiculo
            {
                foreach (Reserva reserva in reservas)
                {
                    if (reserva.IdCliente == idCliente && reserva.IdVeiculo == idVeiculo) { encontrados.Add(reserva); }
                }
            }
            if (idCliente == -1 && idVeiculo != -1) //idVeiculo
            {
                foreach (Reserva reserva in reservas)
                {
                    if (reserva.IdVeiculo == idVeiculo) { encontrados.Add(reserva); }
                }
            }
            else if(idCliente != -1 && idVeiculo == -1) //idCliente
            {
                foreach (Reserva reserva in reservas)
                {
                    if (reserva.IdCliente == idCliente) { encontrados.Add(reserva); }
                }
            } //nenhum dos dois: não executa, passa para verificar datas
            if(dataInicio.CompareTo(DateTime.MinValue) != 0)
            {
                encontrados = VerificarDisponiveis(dataInicio, dataFim, encontrados);
            }
            return encontrados;
        }
        static void VerReservas(List<Reserva> reservas, bool nums)
        {
            if (reservas.Count == 0) { DesenharTitulo("Não existem resultados!"); return; }
            DesenharTitulo("Reservas encontradas"); DesenharLinha($"{"Nº".PadLeft(3)} | {"Data Inicio".PadRight(11)} | {"Data Fim".PadRight(11)} | {"ID Cliente".ToString().PadRight(10)} | {"ID Veículo".ToString().PadRight(10)} | Anotação"); DesenharDivisoria();
            for (int i = 0; i < reservas.Count; i++)
            {
                if (nums) { DesenharLinha(reservas[i].ToString(), i + 1); }
                else { DesenharLinha(reservas[i].ToString()); }
            }
            DesenharDivisoria();
        }
        static void VerInformacaoInserida(List<string> s)
        {
            DesenharTitulo("Informação inserida");
            foreach (string line in s)
            {
                DesenharLinha(line);
            }
            DesenharDivisoria();
        }
        static void ReservaProcura(ref Empresa empresa)
        {
            int idCliente = -1, idVeiculo = -1; bool hasParameters = false, loop = true;
            DateTime dataInicio = new DateTime(), dataFim = new DateTime();
            do
            {
                Console.Clear(); List<string> s = new List<string>();
                if (hasParameters)
                {
                    if (idCliente != -1) s.Add($"Cliente nº: {idCliente}");
                    if (idVeiculo != -1) s.Add($"Veículo nº: {idVeiculo}");
                    if (dataInicio.CompareTo(DateTime.MinValue) != 0) s.Add($"Data Inicio {dataInicio.ToShortDateString()}");
                    if (dataFim.CompareTo(DateTime.MinValue) != 0) s.Add($"Data Fim {dataFim.ToShortDateString()}");
                    VerInformacaoInserida(s);
                }
                DesenharTitulo("Escolha uma opção");
                DesenharLinha("ID cliente", 1);
                DesenharLinha("ID veículo", 2);
                DesenharLinha("Data Início", 3);
                DesenharLinha("Data Fim", 4);
                DesenharDivisoria();
                DesenharLinha("Avançar com pesquisa",5);
                DesenharDivisoria();
                DesenharLinha("Sair", 0);
                DesenharDivisoria();
                char key;
                do
                {
                    key = Console.ReadKey().KeyChar;
                } while (key < '0' || key > '5' && key != '9');
                switch (key)
                {
                    case '0': return;
                    case '1':
                        {
                            do
                            {
                                bool flag = true;
                                Console.Clear(); DesenharTitulo("Introduza o ID do cliente (* para listagem, 0 para cancelar)"); AlinharInput();
                                string read = Console.ReadLine();
                                if (read == "*")
                                {
                                    ConsultarClientes(empresa.Clientes);
                                }
                                if (read == "0") break;
                                else if (int.TryParse(read, out idCliente))
                                {
                                    if (VerificarCliente(empresa.Clientes, idCliente).Id == -1) { MensagemErro("Cliente inexistente!"); }
                                    else { hasParameters = true; break; }
                                }
                                else if (!flag) { continue; }
                                else MensagemErro("Inválido!");
                            } while (true);
                            break;
                        }
                    case '2':
                        {
                            do
                            {
                                bool flag = true;
                                Console.Clear(); DesenharTitulo("Introduza o ID do veiculo (- para listagem, 0 para cancelar)"); AlinharInput();
                                string read = Console.ReadLine();
                                if (read == "-")
                                {
                                    bool order = false;
                                    do
                                    {
                                        Console.Clear();
                                        VerTodosVeiculos(empresa.Veiculos);
                                        do
                                        {
                                            char key1 = Console.ReadKey().KeyChar;
                                            if (key1 == '*') { order = !order; break; }
                                            else if (key1 == 13) { flag = false; break; }
                                        } while (true);
                                    } while (flag);
                                }
                                if (read == "0") break;
                                else if (int.TryParse(read, out idVeiculo))
                                {
                                    if (VerificarVeiculo(empresa.Veiculos, idVeiculo).Id == -1) { MensagemErro("Veículo inexistente!"); }
                                    else { hasParameters = true; break; }
                                }
                                else if (!flag) { continue; }
                                else MensagemErro("Inválido!");
                            } while (true);
                            break;
                        }
                    case '3':
                        {
                            do
                            {
                                Console.Clear(); DesenharTitulo("Introduza a data inicial (0 para cancelar)"); AlinharInput();
                                string read = Console.ReadLine();
                                if (read == "0") break;
                                else if (DateTime.TryParse(read, out dataInicio)) { hasParameters = true; break; }
                                else MensagemErro("Inválido");
                            } while (true);
                            break;
                        }
                    case '4':
                        {
                            do
                            {
                                Console.Clear(); DesenharTitulo("Introduza a data de fim (0 para cancelar)"); AlinharInput();
                                string read = Console.ReadLine();
                                if (read == "0") break;
                                else if (DateTime.TryParse(read, out dataFim)) { 
                                    if(dataInicio.CompareTo(DateTime.MinValue) > 0 && dataFim.CompareTo(dataInicio) >= 0)
                                    { hasParameters = true; break; }
                                    else { MensagemErro("Data final não pode ser menor à inicial"); }
                                }
                                else MensagemErro("Inválido");
                            } while (true);
                            break;
                        }
                    case '5':
                        {
                            bool isGeral = false;
                            Console.Clear();
                            if (dataInicio.CompareTo(DateTime.MinValue) == 0 && dataFim.CompareTo(DateTime.MinValue) == 0 && idCliente == -1 && idVeiculo == -1) {
                                MensagemErro("ATENÇÃO: Vai iniciar uma procura total de reservas"); 
                                Console.Clear(); DesenharTitulo("Continuar? ('Sim' para confirmar)"); AlinharInput(); string readConfirmacaoPesquisa = Console.ReadLine();
                                if (readConfirmacaoPesquisa == "Sim") { isGeral = true; }
                                else { break; }
                            }
                            do
                            {
                                if (isGeral) { Console.Clear(); DesenharTitulo("Procura total"); VerReservas(empresa.Reservas, true); }
                                else { VerInformacaoInserida(s); List<Reserva> encontrados = ProcurarReservas(empresa.Reservas, idCliente, idVeiculo, dataInicio, dataFim); VerReservas(encontrados, true); }
                                DesenharTitulo("* para listagem de clientes, - para listagem de veículos, 0 para sair");
                                char keyVerReservas = Console.ReadKey(true).KeyChar;
                                switch (keyVerReservas)
                                {
                                    case '*':
                                        {
                                            { ConsultarClientes(empresa.Clientes); break; }
                                        }
                                    case '-':
                                        { Console.Clear(); VerTodosVeiculos(empresa.Veiculos); Console.ReadKey(); break; }
                                    case '0': { loop = false; break; }
                                }
                            } while (loop);
                            break;
                        }
                }
            } while (loop);
        }
        static void ExportarHTML(List<Veiculo> veiculos)
        {
            StreamWriter write = new StreamWriter("veiculos.html");
            for (int i = 1; i <= 4; i++)
            {
                write.WriteLine("<table>");
                List<Veiculo> veiculosClasse = VeiculosPorClasse(veiculos, i);
                string classe = (veiculosClasse[0].GetType().ToString().Split('.'))[1];
                write.WriteLine($"<caption>{classe}</caption>");
                write.Write("<tr><th>ID</th><th>Marca/Modelo</th><th>Combustível</th><th>Preço</th>");
                switch (i)
                {
                    case 1:
                        { write.Write("<th>Nº Portas</th><th>Caixa</th>"); break; }
                    case 2:
                        { write.Write("<th>Cilindrada</th>"); break; }
                    case 3:
                        { write.Write("<th>Nº Eixos</th><th>Nº Passageiros</th>"); break; }
                    case 4:
                        { write.Write("<th>Peso Máx.</th>"); break; }
                }
                write.Write("</tr>\n");
                foreach (Veiculo v in veiculosClasse)
                {
                    switch (i)
                    {
                        case 1:
                            {   Carro c = (Carro)v;
                                write.Write($"<tr>{c.ToStringHTML()}</tr>"); break; }
                        case 2:
                            {   Mota c = (Mota)v;
                                write.Write($"<tr>{c.ToStringHTML()}</tr>"); break;
                            }
                        case 3:
                            {   Camioneta c = (Camioneta)v;
                                write.Write($"<tr>{c.ToStringHTML()}</tr>"); break;}
                        case 4:
                            {
                                Camiao c = (Camiao)v;
                                write.Write($"<tr>{c.ToStringHTML()}</tr>"); break;}
                    }
                }
                write.WriteLine("</table>");
            }
            write.Close();
            Console.Clear(); DesenharTitulo("Exportação para HTML concluida com sucesso!"); Console.ReadKey();
        }
        static void ConsultarGanhos(ref Empresa empresa)
        {
            string read;
            DateTime dataInicio = new DateTime(), dataFim = new DateTime();
            do {
                Console.Clear(); DesenharTitulo("Consultar Ganhos");
                DesenharLinha("Introduza a data inicial (0 para cancelar)"); DesenharDivisoria(); AlinharInput();
                read = Console.ReadLine();
                if (read == "0") return;
                else if (DateTime.TryParse(read, out dataInicio)) { break; }
                else MensagemErro("Inválido");
            } while (true);
            do
            {
                Console.Clear(); DesenharTitulo("Consultar Ganhos"); DesenharLinha("Data incial: " + dataInicio.ToShortDateString());
                DesenharLinha("Introduza a data final (0 para cancelar)"); DesenharDivisoria(); AlinharInput();
                read = Console.ReadLine();
                if (read == "0") return;
                else if (DateTime.TryParse(read, out dataFim))
                {
                    if (dataInicio.CompareTo(DateTime.MinValue) > 0 && dataFim.CompareTo(dataInicio) >= 0)
                    { break; }
                    else { MensagemErro("Data final não pode ser menor à inicial"); }
                }
                else MensagemErro("Inválido");
            } while (true);
            double soma = 0;
            foreach (Reserva reserva in empresa.Reservas)
            {
                if(reserva.IdCliente != 1337 && dataInicio.CompareTo(reserva.DataFim) <= 0 && reserva.DataInicio.CompareTo(dataFim) <= 0)
                {
                    int cnt = 0; 
                    DateTime dataComp = new DateTime(); dataComp = DateTime.Parse(dataInicio.ToShortDateString());
                    while(dataComp.Date.CompareTo(reserva.DataInicio.Date) < 0) { dataComp = dataComp.AddDays(1); }
                    while(dataComp.Date.CompareTo(dataFim.Date) <= 0 && dataComp.Date.CompareTo(reserva.DataFim.Date) <= 0) { cnt++; dataComp = dataComp.AddDays(1); }
                    soma += cnt * (empresa.Veiculos.Where(veiculo => veiculo.Id == reserva.IdVeiculo).First().Preco);
                }
            }
            Console.Clear(); DesenharTitulo("Ganhos Totais"); DesenharLinha("Data inicial: " + dataInicio.ToShortDateString()); DesenharLinha("Data final: " + dataFim.ToShortDateString());
            if (soma > 0) { DesenharTitulo($"{soma.ToString(".00")} €"); }
            else { DesenharTitulo("Sem reservas no periodo de tempo selecionado!"); }
            Console.ReadKey();
        }
        static void ConsultarClientes(List<Cliente> clientes)
        {
            bool order = false, loop = true;
            do
            {
                Console.Clear(); ListarClientes(clientes, order);
                do
                {
                    char keyCliente = Console.ReadKey().KeyChar;
                    if (keyCliente == '*') { order = !order; break; }
                    else if (keyCliente == 13) { loop = false; break; }
                } while (true);
            } while (loop);
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Empresa empresa = new Empresa();

            empresa.AdicionarReserva(DateTime.Now.AddDays(10), DateTime.Now.AddDays(12), "TESTE RESERVAS", 1337, 1);
            empresa.AdicionarReserva(DateTime.Now.AddDays(9), DateTime.Now.AddDays(12), "TESTE RESERVAS", 1337, 1);
            empresa.AdicionarReserva(DateTime.Now.AddDays(10), DateTime.Now.AddDays(13), "TESTE RESERVAS", 1337, 1);
            empresa.AdicionarReserva(DateTime.Now.AddDays(9), DateTime.Now.AddDays(13), "TESTE RESERVAS", 1337, 1);
            empresa.AdicionarReserva(DateTime.Now.AddDays(11), DateTime.Now.AddDays(11), "TESTE RESERVAS", 1337, 1);
            empresa.AdicionarReserva(DateTime.Now.AddDays(11), DateTime.Now.AddDays(15), "TESTE RESERVAS", 1337, 1);
            empresa.AdicionarReserva(DateTime.Now.AddDays(13), DateTime.Now.AddDays(15), "TESTE RESERVAS", 1337, 1);

            bool loop = true;
            do
            {
                Console.Clear();
                DesenharMenu();
                char op; 
                do
                {
                    op = Console.ReadKey(true).KeyChar;
                } while (op < '0' || op > '9');
                switch (op)
                {
                    case '0':
                        loop = false; break;
                    case '1':
                        { SimularReserva(ref empresa); break; }
                    case '2':
                        { /*AlterarReserva(ref empresa);*/ break; }
                    case '3':
                        { ReservaProcura(ref empresa); break; }
                    case '4': //Ver veículos disponíveis no momento
                        {
                            int classeVeiculo = InserirTipoVeiculo(); if (classeVeiculo == 0) return;
                            Console.Clear(); VerVeiculosDisponiveisAgora(empresa.Veiculos, classeVeiculo, empresa.Reservas); Console.ReadKey(); break;
                        }
                    case '5':
                        { VerVeiculosManuntenção(empresa); break; }
                    case '6':
                        { ConsultarClientes(empresa.Clientes); break; }
                    case '7':
                        { InserirNovoVeículo(ref empresa); break; }
                    case '8':
                        { ConsultarGanhos(ref empresa); break; }
                    case '9':
                        { ExportarHTML(empresa.Veiculos); break; }
                }
            } while (loop);
        }
    }
}
