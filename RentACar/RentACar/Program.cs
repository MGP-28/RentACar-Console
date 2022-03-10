using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            "Inserir novo veículo","Ver veículos disponíveis no momento",
            "Ver veículos em manutenção","Simulador de reservas","Alterar reserva","Exportar informação para HTML",
            "Consultar ganhos"
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
        static bool VerifNum(int n)
        {
            if (n < 0)
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
        static void MostrarDadosConfirmacaoInserirVeiculo(string nome, string cor, string combustivel,double preco)
        {
            Console.Clear();
            DesenharTitulo("Pretende confirmar os dados? (Sim / Não)");
            DesenharLinha("Nome: " + nome);
            DesenharLinha("Cor: " + cor);
            DesenharLinha("Combustível: " + combustivel);
            DesenharLinha("Preço: " + preco.ToString(".00") + " €");
        }
        static bool ConfirmacaoReserva(Veiculo viatura, DateTime inico, DateTime fim)
        {
            do
            {
                string tipo = (viatura.GetType()).ToString().Replace("RentACar.", "");
                Console.Clear();
                DesenharTitulo("Pretende confirmar a reserva? (Sim / Não)");
                DesenharLinha("Inicio: " + inico.ToShortDateString());
                DesenharLinha("Inicio: " + fim.ToShortDateString());
                DesenharLinha("Tipo: " + tipo);
                DesenharLinha("Nome: " + viatura.Nome);
                DesenharLinha("Cor: " + viatura.Cor);
                DesenharLinha("Combustível: " + viatura.Combustivel);
                DesenharLinha("Preço: " + viatura.Preco.ToString(".00") + " €");
                DesenharDivisoria(); AlinharInput();
                string confirmacao = Console.ReadLine();
                if (confirmacao == "Não") return false;
                else if (confirmacao != "Sim") { MensagemErro("Introduza 'Sim' ou 'Não' !"); }
                else return true;
            }while(true);
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
            Veiculo v = new Veiculo(empresa.GetNextId(),nome,cor,combustivel,preco);
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
        static List<Veiculo> VerificarDisponiveis(DateTime data, List<Veiculo> veiculos)
        {
            List<Veiculo> disponiveis = new List<Veiculo>();
            for (int i = 0; i < veiculos.Count; i++)
            {
                bool isAvailable = true;
                List<Reserva> reservas = veiculos[i].ListagemReservas();
                foreach (Reserva reserva in reservas)
                {
                    if (data.DayOfYear >= reserva.DataInicio.DayOfYear && data.DayOfYear <= reserva.DataFim.DayOfYear)
                    { isAvailable = false; break; }
                }
                if(isAvailable) disponiveis.Add(veiculos[i]);
            }
            return disponiveis;
        }
        static List<Veiculo> VerificarDisponiveis(DateTime dataInicio, DateTime dataFim, List<Veiculo> veiculos)
        {
            List<Veiculo> disponiveis = new List<Veiculo>();
            for (int i = 0; i < veiculos.Count; i++)
            {
                bool isAvailable = true;
                List<Reserva> reservas = veiculos[i].ListagemReservas();
                foreach (Reserva reserva in reservas)
                {
                    if (dataInicio.CompareTo(reserva.DataInicio) < 0 && dataFim.CompareTo(reserva.DataInicio) >= 0 && dataFim.CompareTo(reserva.DataFim) <= 0 //Final na reserva
                        || dataFim.CompareTo(reserva.DataFim) > 0 && dataInicio.CompareTo(reserva.DataInicio) >= 0 && dataInicio.CompareTo(reserva.DataFim) <= 0 //Inicio na reserva
                        || dataInicio.CompareTo(reserva.DataInicio) < 0 && dataFim.CompareTo(reserva.DataFim) > 0 //Reserva a meio do intervalo
                        || dataInicio.CompareTo(reserva.DataInicio) >= 0 && dataFim.CompareTo(reserva.DataFim) <= 0) //Reserva engloba o intervalo
                    { isAvailable = false; break; }
                }
                if (isAvailable) disponiveis.Add(veiculos[i]);
            }
            return disponiveis;
        }
        static void VerVeiculosDisponiveis(List<Veiculo> baseVeiculos, int op, bool verification)
        {
            List<Veiculo> veiculos = new List<Veiculo>();
            string titulos = $"{"".PadRight(3)} | {"Nome".PadRight(14)} | {"Cor".PadRight(8)} | { "Combustivel".PadRight(8)} | { "Preco €"}";
            switch (op)
            {
                case 1:
                    {
                        veiculos = VeiculosPorClasse(baseVeiculos, 1);
                        DesenharTitulo("Carros disponíveis no presente dia");
                        DesenharLinha(titulos + $"{" | Nº Portas".ToString()} | {"Caixa".PadRight(10)}");
                        break;
                    }
                case 2:
                    {
                        veiculos = VeiculosPorClasse(baseVeiculos, 2);
                        DesenharTitulo("Motas disponíveis no presente dia");
                        DesenharLinha(titulos + " | Cilindrada".PadRight(10));
                        break;
                    }
                case 3:
                    {
                        veiculos = VeiculosPorClasse(baseVeiculos, 3);
                        DesenharTitulo("Camionetas disponíveis no presente dia");
                        DesenharLinha(titulos + $" | {"Nº Eixos".ToString()} | {"Nº Passageiros".ToString()}");
                        break;
                    }
                case 4:
                    {
                        veiculos = VeiculosPorClasse(baseVeiculos, 4);
                        DesenharTitulo("Camiões disponíveis no presente dia");
                        DesenharLinha(titulos + " | Peso Max.");
                        break;
                    }
            }
            if(verification)
                veiculos = VerificarDisponiveis(DateTime.Now, veiculos);
            DesenharDivisoria();
            if (veiculos.Count == 0)
                DesenharLinha("Não existem veículos disponíveis!");
            for (int i = 0; i < veiculos.Count; i++)
            {
                DesenharLinha(veiculos[i].ToString(),i+1);
            }
            DesenharDivisoria();
        }
        static List<Veiculo> VerificarManutencao(DateTime data, List<Veiculo> veiculos, ref List<string> motivos)
        {
            List<Veiculo> manutencao = new List<Veiculo>();
            for (int i = 0; i < veiculos.Count; i++)
            {
                List<Reserva> reservas = veiculos[i].ListagemReservas();
                foreach (Reserva reserva in reservas)
                {
                    if (data.DayOfYear >= reserva.DataInicio.DayOfYear && data.DayOfYear <= reserva.DataFim.DayOfYear && reserva.IsManutencao == true)
                    { manutencao.Add(veiculos[i]); motivos.Add(reserva.Finalidade); break; }
                }
            }
            return manutencao;
        }
        static void VerVeículosManuntenção(List<Veiculo> baseVeiculos)
        {
            List<string> motivos = new List<string>();
            List<Veiculo> veiculos = VerificarManutencao(DateTime.Now, baseVeiculos, ref motivos);
            Console.Clear();
            DesenharTitulo("Veículos em manutenção");
            DesenharLinha($"{"Tipo".PadRight(9)} | { "Nome".PadRight(14)} | {"Cor".PadRight(8)} | { "Combustivel".PadRight(8)} | { "Preco €"} | Motivo");
            DesenharDivisoria();
            for (int i = 0; i < veiculos.Count; i++)
            {
                string tipo = veiculos[i].GetType().ToString().Replace("RentACar.", "");
                string info = veiculos[i].ToString().Remove(49);
                DesenharLinha($"{tipo.PadRight(9)} | {info} | {motivos[i]}");
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
            int tipoVeiculo = InserirTipoVeiculo();
            if (tipoVeiculo == 0) return;
            List<Veiculo> veiculos = VeiculosPorClasse(empresa.Veiculos, tipoVeiculo);
            veiculos = VerificarDisponiveis(dataInicio, dataFim, veiculos);
            do
            {
                int id;
                do
                {
                    string s;
                    do
                    {
                        Console.Clear(); DesenharTitulo($"Inicio: {dataInicio.ToShortDateString()} | Fim: {dataInicio.ToShortDateString()}"); VerVeiculosDisponiveis(veiculos, tipoVeiculo, false); DesenharLinha("Introduza o número correspondente à viatura para continuar (0 para cancelar)"); DesenharDivisoria(); AlinharInput(); 
                        s = Console.ReadLine();
                    } while (!VerifString(s) || !int.TryParse(s,out _));
                    if (s == "0")
                        return;
                    id = int.Parse(s) - 1;
                    if (VerifNum(id, 0, veiculos.Count)) break;
                    else MensagemErro("Inválido!");
                } while (true);
                if (ConfirmacaoReserva(veiculos[id],dataInicio,dataFim))
                {
                    Console.Clear();DesenharTitulo("Pretende deixar anotação na reserva? ('Não' para avançar)");AlinharInput();
                    string s = Console.ReadLine();
                    empresa.Veiculos[veiculos[id].Id].AdicionarReserva(dataInicio,dataFim,"Reserva: " + s);
                    break;
                }
            } while (true);

        }
        static void AlterarReserva(ref Empresa empresa)
        {
            
        }
        static void ExportarHTML(ref Empresa empresa)
        {

        }
        static void ConsultarGanhos(ref Empresa empresa)
        {

        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Empresa empresa = new Empresa();
            bool loop = true;
            do
            {
                Console.Clear();
                DesenharMenu();
                char op; 
                do
                {
                    op = Console.ReadKey(true).KeyChar;
                } while (op < '0' || op > '7');
                switch (op)
                {
                    case '0':
                        loop = false; break;
                    case '1':
                        InserirNovoVeículo(ref empresa); break;
                    case '2':
                        {
                            int classeVeiculo = InserirTipoVeiculo(); if (classeVeiculo == 0) return;
                            Console.Clear(); VerVeiculosDisponiveis(empresa.Veiculos, classeVeiculo, true); Console.ReadKey(); break;
                        }
                    case '3':
                        VerVeículosManuntenção(empresa.Veiculos); break;
                    case '4':
                        SimularReserva(ref empresa); break;
                    case '5':
                        AlterarReserva(ref empresa); break;
                    case '6':
                        ExportarHTML(ref empresa); break;
                    case '7':
                        ConsultarGanhos(ref empresa); break;
                }
            } while (loop);
        }
    }
}
