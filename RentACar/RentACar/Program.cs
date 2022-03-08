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
            Console.WriteLine($" | {n} | {texto.PadRight(Console.WindowWidth - 10)} |");
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
            "Inserir novo veículo","Alterar estado de um veículo","Ver veículos disponíveis para aluguer",
            "Ver veículos em manutenção","Simulador de reservas","Exportar informação para HTML",
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
            if (n > min && n < max)
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
        static void InserirNovoVeículo(ref Empresa empresa)
        {
            char op; bool pass = true;
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
            if (op == '0')
                return;
            string nome;
            do
            {
                pass = true;
                Console.Clear();
                DesenharTitulo("Insira a marca/modelo do veículo (0 para cancelar)");
                AlinharInput();
                nome = Console.ReadLine();
                if (!VerifString(nome))
                {
                    pass = false;
                    continue;
                }
            } while (!pass);
            if (nome == "0")
                return;
            string cor;
            do
            {
                pass = true;
                Console.Clear();
                DesenharTitulo("Insira a cor do veículo (0 para cancelar)");
                AlinharInput();
                cor = Console.ReadLine();
                if (!VerifString(cor))
                {
                    pass = false;
                    continue;
                }
            } while (!pass);
            if (cor == "0")
                return;
            char combustivel;
            int numFuels = empresa.Combustiveis.Count;
            do
            {
                Console.Clear();
                DesenharTitulo("Qual o tipo de veículo? (0 para cancelar)");
                for (int i = 0; i < empresa.Combustiveis.Count; i++)
                {
                    DesenharLinha(empresa.Combustiveis[i], i + 1);
                }
                DesenharDivisoria();
                AlinharInput();
                combustivel = Console.ReadKey(true).KeyChar;
            } while (combustivel < '0' || combustivel > char.Parse(numFuels.ToString()));
            if (combustivel == '0')
                return;
            double preco = 0;
            do
            {
                pass = true;
                Console.Clear();
                DesenharTitulo("Insira o preço diário do veículo (0 para cancelar)");
                AlinharInput();
                string s = Console.ReadLine();
                if (!VerifString(s))
                {
                    pass = false;
                    continue;
                }
                else
                {
                    if (s == "0") return;
                    if(double.TryParse(s,out _))
                    {
                        if (!VerifNum(double.Parse(s), 0))
                        {
                            MensagemErro("Inválido > Preço tem de ser maior do que 0");
                            pass = false;
                        }
                        else
                        {
                            preco = double.Parse(s);
                        }
                    }
                    else
                    {
                        MensagemErro("Inválido > Apenas números são válidos");
                        pass = false;
                    }
                }
            } while (!pass);
            Veiculo v = new Veiculo(nome,cor, empresa.Combustiveis[int.Parse(combustivel.ToString())],preco);
            switch (op)
            {
                case '1':
                    {
                        char caixa = '0';
                        do
                        {
                            Console.Clear();
                            DesenharTitulo("Qual o tipo de caixa do veículo? (0 para cancelar)");
                            for (int i = 0; i < empresa.Caixas.Count; i++)
                            {
                                DesenharLinha(empresa.Caixas[i], i + 1);
                            }
                            DesenharDivisoria();
                            AlinharInput();
                            caixa = Console.ReadKey(true).KeyChar;
                        } while (combustivel < '0' || combustivel > '2');
                        if (combustivel == '0')
                            return;
                        int nPortas = 0;
                        do
                        {
                            pass = true;
                            Console.Clear();
                            DesenharTitulo("Insira o número de portas do veículo (0 para cancelar)");
                            AlinharInput();
                            string s = Console.ReadLine();
                            if (!VerifString(s))
                            {
                                pass = false;
                                continue;
                            }
                            else
                            {
                                if (s == "0") return;
                                if (int.TryParse(s, out _))
                                {
                                    if (!VerifNum(int.Parse(s), 0))
                                    {
                                        MensagemErro("Inválido > Número de portas tem de ser maior do que 0");
                                        pass = false;
                                    }
                                    else
                                    {
                                        nPortas = int.Parse(s);
                                    }
                                }
                                else
                                {
                                    MensagemErro("Inválido > Apenas números inteiros positivos são válidos");
                                    pass = false;
                                }
                            }
                        } while (!pass);
                        Carro c = new Carro(v, nPortas, empresa.Caixas[int.Parse(caixa.ToString())]);
                        DesenharTitulo("Pretende confirmar os dados? (Sim / Não)");
                        DesenharLinha("Nome: " + nome);
                        DesenharLinha("Cor:" + cor);
                        DesenharLinha("Combustível" + );
                        DesenharLinha("Preço: " + );
                        DesenharLinha("Preço: " + );
                        DesenharLinha("Preço: " + );
                        break;
                    }
            }
        }
        static void AlterarEstadoVeículo(ref Empresa empresa)
        {
            
        }
        static void VerVeículosDisponíveis(ref Empresa empresa)
        {

        }
        static void VerVeículosManuntenção(ref Empresa empresa)
        {

        }
        static void SimularReserva(ref Empresa empresa)
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
            empresa.SimularAvarias(DateTime.Now);
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
                        AlterarEstadoVeículo(ref empresa); break;
                    case '3':
                        VerVeículosDisponíveis(ref empresa); break;
                    case '4':
                        VerVeículosManuntenção(ref empresa); break;
                    case '5':
                        SimularReserva(ref empresa); break;
                    case '6':
                        ExportarHTML(ref empresa); break;
                    case '7':
                        ConsultarGanhos(ref empresa); break;
                }
            } while (loop);
        }
    }
}
