// versao 5
// organizar o codigo em funcoes
// COM VALIDACAO DE DADOS DE ENTRADA PELA NEGATIVA

// VERSAO 6 - CRIAR UM ESTRUTURA DE DADOS STRUCT
// QUE CARACTERIZA O QUARTO DA PENSAO COM OS SEGUINTES CAMPOS
// - ESTADO DO QUARTO, NOME DO HOSPEDE, IDETIFICACAO DO HOSPEDE, DATA DE CHECKIN

// VERSAO 7
// NA OPERACAO DE CHECKIN CONSIDERAR A HIPOTESE DE O CLIENTE JA TER RESERVA 
// MARCADA.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_programacao_projeto1.csharp_7_funcoes
{
    public class _09_pensao7
    {
        //*************************************
        //* DEFINICAO DA ESTRUTURA DE DADOS "QUARTO"
        //*************************************
        struct Quarto
        {
            public char Estado;
            public string NomeHospede;
            public string idHospede;
            public string DataCheckin;
        }



        //*************************************
        //* FUNCAO PRINCIPAL DO PROGRAMA
        //*************************************
        public static void Main(string[] args)
        {
            // dados
            
            const int num_andares = 3, num_quartos_por_andar = 7;
            const char livre = 'L', reservado = 'R', ocupado = '0';

            char escolha = '\0';
            bool sair = false;


            Quarto[,] quartos = new Quarto[num_andares, num_quartos_por_andar];

            // algoritmo
            
            quartos = inicializaPensao(quartos, livre);

            cicloPrincipal(quartos, livre, reservado, ocupado);

            despedida();
        }


        //*************************************
        //* DEFINICAO DAS OUTRAS FUNCOES DO PROGRAMA
        //*************************************

        //*************************************
        //* FUNCAO INICIALIZA PENSAO
        //*************************************
        static Quarto[,] inicializaPensao(Quarto[,] quartos, char estadoInicial)
        {
            for (int conta_andar = 0; conta_andar < quartos.GetLength(0); conta_andar++) //ciclo mais externo que percorrerá os andares - percorrer 3x
            {
                for (int conta_quarto = 0; conta_quarto < quartos.GetLength(1); conta_quarto++) // ciclo que percorrerá os quartos - percorrer 7x
                {
                    quartos[conta_andar, conta_quarto] = inicializaQuarto(quartos[conta_andar, conta_quarto], estadoInicial);
                    
                }
            }

            return quartos;

        } // fim da funcao inicializaPensao()



        //*************************************
        //* FUNCAO INICIALIZA QUARTO
        //*************************************
        static Quarto inicializaQuarto(Quarto quarto, char estadoInicial)
        {
            quarto.Estado = estadoInicial;
            quarto.NomeHospede = "";
            quarto.idHospede = "";
            quarto.DataCheckin = "";

            return quarto;
        } // fim da funcao inicializa quarto



        //****************************************
        //* FUNCAO CICLO PRINCIPAL
        //****************************************

        static void cicloPrincipal(Quarto[,] quarto, char livre, char reservado, char ocupado)
        {
            // dados locais da funcao
            const int NUMOPCOES = 5;
            const string TITULO = "*         PENSÃO GEST         *";
            bool sair = false;
            char escolha = '0';

            string[] opcoesMenu = new string[NUMOPCOES];

            // algoritmo
            opcoesMenu = inicializaMenu(opcoesMenu);
            do
            {
                mostrar_menu(TITULO, opcoesMenu);
                escolha = obter_escolha(escolha, NUMOPCOES);
                sair = processar_escolha(escolha, quarto, livre, ocupado, reservado, NUMOPCOES, sair);


            } while (!sair);
        }



        //****************************************
        //* FUNCAO INICIALIZA MENU
        //****************************************
        static string[] inicializaMenu(string[] opcoesMenu)
        {
            opcoesMenu[0] = "*     A. Listar quartos       *";
            opcoesMenu[1] = "*     B. Reservar quarto      *";
            opcoesMenu[2] = "*     C. Check-in             *";
            opcoesMenu[3] = "*     D. Check-out            *";
            opcoesMenu[4] = "*     E. Sair do programa     *";

            return opcoesMenu;

        } // fim da funcao inicializa menu



        //***********************************
        //* FUNCAO MOSTRAR MENU
        //***********************************
        static void mostrar_menu(string titulo, string[] opcoesMenu)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("*******************************");
            Console.WriteLine("*                             *");
            Console.WriteLine(titulo);
            Console.WriteLine("*                             *");
            for (int cOpcao = 0; cOpcao < opcoesMenu.Length; cOpcao++)
            {
                Console.WriteLine(opcoesMenu[cOpcao]);
            }
            Console.WriteLine("*******************************");
            Console.WriteLine();
        } // fim da funcao mostrar_menu



        //***********************************
        //* FUNCAO PROCESSAR RESPOSTA
        //***********************************
        static char processaResposta(string resposta, char valorPreDefinido)
        {
            char saida = '\0';


            //se a rsposta for enter
            if (string.IsNullOrEmpty(resposta))
            {
                saida = valorPreDefinido;
            }
            else
            {
                saida = resposta[0];
            }
            return saida;

        } // fim da funcao processaResposta



        //****************************************
        //* FUNCAO OBTER ESCOLHA
        //****************************************
        static char obter_escolha(char escolha, int NumOpcoes)
        {
            char ultimaOpcao = (char)('A' + NumOpcoes - 1);
            Console.Write($"Qual é a sua escolha (A-{ultimaOpcao}) [A]? ");
            string resposta_escolha = Console.ReadLine();
            Console.WriteLine();
            escolha = processaResposta(resposta_escolha, 'A');

            return escolha;
        } // fim da funcao obter_escolha



        //****************************************
        //* FUNCAO PROCESSAR ESCOLHA
        //****************************************
        static bool processar_escolha(char escolha, Quarto[,] quarto, char livre, char reservado, char ocupado, int numOpcoes, bool sair)
        {
            switch (escolha)
            {
                case 'a': case 'A':
                    listarQuartos(quarto, livre, reservado); break;
                case 'b':
                case 'B':
                    reservarQuarto(quarto, reservado, ocupado); break;
                case 'c':
                case 'C':
                    checkInQuarto(quarto, reservado, ocupado); break;
                case 'd':
                case 'D':
                    checkOutQuarto(quarto, livre); break;
                case 'e':
                case 'E':
                    sair = sairPrograma(sair); break;
                default:
                    escolhaInvalida(numOpcoes); break;
            } // fim do switch


            return sair;
        } // fim da funcao processa escolha



        //****************************************
        //* FUNCAO LISTAR QUARTOS
        //****************************************
        static void listarQuartos(Quarto[,] quartos, char livre, char reservado)
        {
            Console.WriteLine("Listagem dos quartos");
            for (int conta_andar = 0; conta_andar < quartos.GetLength(0); conta_andar++)
            {
                Console.WriteLine($"-> Andar {conta_andar + 1}: ");

                for (int conta_quarto = 0; conta_quarto < quartos.GetLength(1); conta_quarto++)
                {
                    Console.Write($"  -> Quarto {conta_andar + 1}.0{conta_quarto + 1}: ");

                    if (quartos[conta_andar, conta_quarto].Estado == livre)
                        Console.WriteLine("Livre.");
                    else if (quartos[conta_andar, conta_quarto].Estado == reservado)
                        Console.WriteLine($"Reservado: Sr./Sr.ª {quartos[conta_andar, conta_quarto].NomeHospede}, Data de Reserva: {quartos[conta_andar, conta_quarto].DataCheckin}");
                    else
                        Console.WriteLine($"Ocupado: Sr./Sr.ª {quartos[conta_andar, conta_quarto].NomeHospede}, Data de Reserva: {quartos[conta_andar, conta_quarto].DataCheckin}");

                } // fim do ciclo for dos quartos num andar
                pausa();

            } // fim do ciclo for dos andares

        } // fim da fincao lista quartos



        //****************************************
        //* FUNCAO RESERVAR QUARTOS
        //****************************************
        static void reservarQuarto(Quarto[,] quartos, char reservado, char ocupado)
        {
            Console.WriteLine("Reservar quarto:");
            int numAndar = 0;
            int numQuarto = 0;
            Console.Write("Qual é o andar a selecionar (1-" + quartos.GetLength(0) + ")? ");
            string numAndarS = Console.ReadLine();
            Console.Write("Neste andar, qual o quarto a selecionar (1-" + quartos.GetLength(1) + ")? ");
            string numQuartoS = Console.ReadLine();

            // verificar se as respostas foram vazias
            if (string.IsNullOrEmpty(numAndarS) || string.IsNullOrEmpty(numQuartoS))
            {
                string mensagem = "";
                if (string.IsNullOrEmpty(numAndarS))
                    mensagem = "Não escolheu um andar.\n";
                if (string.IsNullOrEmpty(numQuartoS))
                    mensagem += "Não escolheu um quarto.";
                Console.WriteLine(mensagem);

            } // resposta nao vazia e não numérica
            else if (!int.TryParse(numAndarS, out numAndar))
            {
                Console.WriteLine("Não inseriu um valor numério de andar.");
            } // resposta nao vazia e não numérica
            else if (!int.TryParse(numQuartoS, out numQuarto))
            {
                Console.WriteLine("Não inseriu um valor numério de quarto.");
            }
            else   // valores numéricos de numAndar e numQuarto
            {
                // varificar o intervalo de valores validos
                if (numAndar < 1 || numAndar > quartos.GetLength(0))
                {
                    Console.WriteLine("Valor inválido de andar.");
                    Console.WriteLine("Os valores de andares válidos vão de 1" + quartos.GetLength(0) + ".");
                }
                else if (numQuarto < 1 || numQuarto > quartos.GetLength(1))
                {
                    Console.WriteLine("Valor inválido de quarto.");
                    Console.WriteLine("Os valores de quartos válidos vão de 1" + quartos.GetLength(1) + ".");
                }
                else // valores válidos de andar e quarto
                {
                    // varificar se o quarto não está livre
                    if (quartos[numAndar - 1, numQuarto - 1].Estado == reservado || quartos[numAndar - 1, numQuarto - 1].Estado == ocupado)
                    {
                        Console.WriteLine("O quarto selecionado não está disponível.");
                    }
                    else
                    {
                        Console.Write("Nome do hóspede: ");
                        quartos[numAndar - 1, numQuarto - 1].NomeHospede = Console.ReadLine();

                        Console.Write("Identificação do hóspede: ");
                        quartos[numAndar - 1, numQuarto - 1].idHospede = Console.ReadLine();

                        Console.Write("Data de Reserva: (dd/mm/yyyy): ");
                        quartos[numAndar - 1, numQuarto - 1].DataCheckin = Console.ReadLine();

                        quartos[numAndar - 1, numQuarto - 1].Estado = reservado;
                        Console.WriteLine("Quarto reservado com sucesso");
                    }
                }
            }
            pausa();
        } // fim da funcao reservar quarto



        //****************************************
        //* FUNCAO CHECKIN QUARTOS
        //****************************************
        static void checkInQuarto(Quarto[,] quartos, char reservado, char ocupado)
        {
            Console.WriteLine("Check-in quarto:");

            Console.Write("Já tem reserva (S/N) [N]?");
            string respostaReserva = Console.ReadLine();

            char temReserva = processaResposta(respostaReserva, 'N');
            // caso 1 hospede ja tem reserva
            if (temReserva == 's' || temReserva == 'S')
            {
                checkInQuarto_comReserva(quartos, reservado, ocupado);
            }
            else
            {
                checkInQuarto_semReserva(quartos, reservado, ocupado);
            }
        } // fim funcao checkin quartos



        //****************************************
        //* FUNCAO CHECKIN QUARTOS COM RESERVA
        //****************************************
        static void checkInQuarto_comReserva(Quarto[,] quartos, char reservado, char ocupado)
        {
            Console.WriteLine("Check-in do quarto com reserva:");
            bool reservaEncontrada = false;

            Console.Write("Qual o nome ou identificação do hóspede? ");
            string respostaApesquisar = Console.ReadLine();

            // procurar reserva no array dos quartos por nome ou identificacao
            for (int conta_andar = 0; conta_andar < quartos.GetLength(0); conta_andar++)
            {
                for(int conta_quarto = 0; conta_quarto < quartos.GetLength(1); conta_quarto++)
                {
                    // pesquisa nos quartos reservados
                    if (quartos[conta_andar, conta_quarto].Estado == reservado)
                    {
                        if (quartos[conta_andar, conta_quarto].NomeHospede.Equals(respostaApesquisar, StringComparison.OrdinalIgnoreCase)
                            || quartos[conta_andar, conta_quarto].idHospede.Equals(respostaApesquisar, StringComparison.OrdinalIgnoreCase))
                        {
                            reservaEncontrada = true;
                            Console.WriteLine();
                            Console.WriteLine("Reserva encontrada!");
                            Console.WriteLine($"Hóspede: {quartos[conta_andar, conta_quarto].NomeHospede}.");
                            Console.WriteLine($"Hóspede: {quartos[conta_andar, conta_quarto].idHospede}.");
                            Console.WriteLine($"Quarto: {conta_andar + 1}.0 {conta_quarto + 1}.");

                            // obter a data atual
                            DateTime agora = DateTime.Now;
                            quartos[conta_andar, conta_quarto].DataCheckin = agora.ToString("dd/MM/yyyy");

                            // alterar o estado do quarto
                            quartos[conta_andar, conta_quarto].Estado = ocupado;

                            Console.WriteLine("Check-in efetuado com sucesso!");
                            pausa();
                            break;
                        }

                    }

                } // fim do ciclo conta quarto

                if (reservaEncontrada)
                    break;

            } // fim do ciclo dos andares

            // se depois de procurar por todos os andares e quartos reservados, nao encontrar reserva
            if (!reservaEncontrada)
            {
                Console.WriteLine();
                Console.WriteLine("Reserva não encontrada para nome ou identoificação informada.");
            }

        } // fim da funcao checkin quarto com reserva        



            //****************************************
            //* FUNCAO CHECKIN QUARTOS SEM RESERVA
            //****************************************
            static void checkInQuarto_semReserva(Quarto[,] quartos, char reservado, char ocupado)
            {
                Console.WriteLine("Check-in do quarto sem reserva:");

                int numAndar = 0;
                int numQuarto = 0;
                Console.Write("Qual é o andar a selecionar (1-" + quartos.GetLength(0) + ")? ");
                string numAndarS = Console.ReadLine();
                Console.Write("Neste andar, qual o quarto a selecionar (1-" + quartos.GetLength(1) + "? ");
                string numQuartoS = Console.ReadLine();

            // verificar se as respostas foram vazias
            if (string.IsNullOrEmpty(numAndarS) || string.IsNullOrEmpty(numQuartoS))
            {
                string mensagem = "";
                if (string.IsNullOrEmpty(numAndarS))
                    mensagem = "Não escolheu um andar.\n";
                if (string.IsNullOrEmpty(numQuartoS))
                    mensagem += "Não escolheu um quarto.";
                Console.WriteLine(mensagem);

            } // resposta nao vazia e não numérica
            else if (!int.TryParse(numAndarS, out numAndar))
            {
                Console.WriteLine("Não inseriu um valor numério de andar.");
            } // resposta nao vazia e não numérica
            else if (!int.TryParse(numQuartoS, out numQuarto))
            {
                Console.WriteLine("Não inseriu um valor numério de quarto.");
            }
            else   // valores numéricos de numAndar e numQuarto
            {
                // varificar o intervalo de valores validos
                if (numAndar < 1 || numAndar > quartos.GetLength(0))
                {
                    Console.WriteLine("Valor inválido de andar.");
                    Console.WriteLine("Os valores de andares válidos vão de 1" + quartos.GetLength(0) + ".");
                }
                else if (numQuarto < 1 || numQuarto > quartos.GetLength(1))
                {
                    Console.WriteLine("Valor inválido de quarto.");
                    Console.WriteLine("Os valores de quartos válidos vão de 1" + quartos.GetLength(1) + ".");
                }
                else // valores válidos de andar e quarto
                {
                    // varificar se o quarto não está livre
                    if (quartos[numAndar - 1, numQuarto - 1].Estado == ocupado)
                    {
                        Console.WriteLine("O quarto selecionado já está ocupado.");
                    }
                    else if (quartos[numAndar - 1, numQuarto - 1].Estado == reservado)
                    {
                        Console.WriteLine("O quarto selecionado já está reservado.");
                    }
                    else // quarto livre 
                    {
                        // senao ha reserva previa - quarto está livre
                        Console.Write("Nome do hóspede: ");
                        quartos[numAndar - 1, numQuarto - 1].NomeHospede = Console.ReadLine();

                        Console.Write("Identificação do hóspede: ");
                        quartos[numAndar - 1, numQuarto - 1].idHospede = Console.ReadLine();
                    
                        // obter a data atual do sistema
                        DateTime agora = DateTime.Now;

                        quartos[numAndar -1, numQuarto - 1].DataCheckin = agora.ToString("dd/MM/yyyy");

                        quartos[numAndar - 1, numQuarto - 1].Estado = ocupado;
                        Console.WriteLine("Quarto ocupado com sucesso");
                    }
                }
            }
            pausa();
        } // fim da funcao checkin quarto sem reserva



            //****************************************
            //* FUNCAO CHECKOUT QUARTOS
            //****************************************
            static void checkOutQuarto(Quarto[,] quartos, char livre)
        {
            Console.WriteLine("Check-out quarto:");
            int numAndar = 0;
            int numQuarto = 0;
            Console.Write("Qual é o andar a selecionar (1-" + quartos.GetLength(0) + ")? ");
            string numAndarS = Console.ReadLine();
            Console.Write("Neste andar, qual o quarto a selecionar (1-" + quartos.GetLength(1) + ")? ");
            string numQuartoS = Console.ReadLine();

            // verificar se as respostas foram vazias
            if (string.IsNullOrEmpty(numAndarS) || string.IsNullOrEmpty(numQuartoS))
            {
                string mensagem = "";
                if (string.IsNullOrEmpty(numAndarS))
                    mensagem = "Não escolheu um andar.\n";
                if (string.IsNullOrEmpty(numQuartoS))
                    mensagem += "Não escolheu um quarto.";
                Console.WriteLine(mensagem);

            } // resposta nao vazia e não numérica
            else if (!int.TryParse(numAndarS, out numAndar))
            {
                Console.WriteLine("Não inseriu um valor numério de andar.");
            } // resposta nao vazia e não numérica
            else if (!int.TryParse(numQuartoS, out numQuarto))
            {
                Console.WriteLine("Não inseriu um valor numério de quarto.");
            }
            else   // valores numéricos de numAndar e numQuarto
            {
                // varificar o intervalo de valores validos
                if (numAndar < 1 || numAndar > quartos.GetLength(0))
                {
                    Console.WriteLine("Valor inválido de andar.");
                    Console.WriteLine("Os valores de andares válidos vão de 1" + quartos.GetLength(0) + ".");
                }
                else if (numQuarto < 1 || numQuarto > quartos.GetLength(1))
                {
                    Console.WriteLine("Valor inválido de quarto.");
                    Console.WriteLine("Os valores de quartos válidos vão de 1" + quartos.GetLength(1) + ".");
                }
                else // valores válidos de andar e quarto
                {
                    // varificar se o quarto não está livre
                    if (quartos[numAndar - 1, numQuarto - 1].Estado == livre)
                    {
                        Console.WriteLine("O quarto selecionado já está livre.");
                    }
                    else
                    {
                        quartos[numAndar - 1, numQuarto - 1] = inicializaQuarto(quartos[numAndar - 1, numQuarto - 1], livre);
                        Console.WriteLine("Quarto libertado com sucesso!");
                    }
                }
            }
            pausa();
        } // fim da funcao checkout quarto



        //****************************************
        //* FUNCAO SAIR DO PROGRAMA
        //****************************************
        static bool sairPrograma(bool sair)
        {
            Console.WriteLine("Tem certeza que quer sair do programa? (S ou N)");

            char resposta = '\0';
            string respostaS = Console.ReadLine();

            resposta = processaResposta(respostaS, 'N');

            if (resposta == 's' || resposta == 'S')
                sair = true;

            return sair;
        } // fim da funcao sair do programa



        //****************************************
        //* FUNCAO ESCOLHA INVALIDA
        //****************************************
        static void escolhaInvalida(int numOpcoes)
        {
            char ultimaOpcao = (char)('A' + numOpcoes - 1);
            Console.WriteLine("Opção inválida." + $" Indique uma opção de A a {ultimaOpcao}!");
            pausa();

        } // fim da funcao escolha invalida



        //****************************************
        // FUNCAO PAUSA
        //****************************************
        static void pausa()
        {
            Console.WriteLine();
            Console.WriteLine("Prima em <ENTER> para continuar ...");
            Console.ReadLine();

        } // fim da funcao pausa



        //****************************************
        // FUNCAO DESPEDIDA
        //****************************************
        static void despedida()
        {
            Console.WriteLine("Obrigada por utilizar os nossos serviços!!!");
        } // fim da funcao despedida
    
    } // fim da classe

} // fim do namespace
