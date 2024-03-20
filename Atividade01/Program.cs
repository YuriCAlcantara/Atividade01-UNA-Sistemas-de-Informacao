using System;
using System.Linq;
using System.Collections.Generic;
using Atividade01.Objetos;

namespace Atividade01
{
    class Program //4º 
    {
        private static Usuario? usuarioLogado;
        private static string senha;

        static void Main(string[] args) //1ª
        {
            // - Inicia com menu
            //      - listagem de eventos
            //      - cadastro
            //      - login
            // - listar eventos
            // - cadastrar usuario
            // - login
            // - cadastrar eventos
            // - participar do evento
            // - cancelar participacao
            // - listar os eventos que o usuario vai participar

            MenuPrincipal();
        }

        static void MenuPrincipal()//3º
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("=                             Menu Principal                                =");
            Console.WriteLine("=============================================================================");

            Console.WriteLine("Escolha sua opção");
            Console.WriteLine("1 - Lista de Eventos");
            Console.WriteLine("2 - Cadastro de Eventos");
            Console.WriteLine("3 - Cadastro de Usuario");
            Console.WriteLine("4 - Login");
            Console.WriteLine("7 - Sair");



            if (usuarioLogado != null)
            {
                Console.WriteLine("5 - Participar de evento");
                Console.WriteLine("6 - Cancelar participação");
                Console.WriteLine("7 - Sair");

            }

            short opcao = short.Parse(Console.ReadLine().Trim());

            switch (opcao)
            {
                case 1: ListarEventos(); break;
                case 2: CadastroDeEvento(); break;
                case 3: CadastroDeUsuario(); break;
                case 4: RealizarLogin(senha); break;
                case 5: ParticiparDeEvento(); break;
                case 6: CancelarParticipacao(); break;
                case 7: DesejaSair(); break;

                default:
                    MenuPrincipal();
                    break;
            }
        }

        static void DesejaSair()
        {
            string? desejaSair = "";//2º

            while (desejaSair != "S")
            {
                
                Console.Write("Deseja sair (s/n)? ");

                desejaSair = Console.ReadLine().ToUpper();
             
            }

            Console.WriteLine("Obrigado por usar nossa plataforma. Volte sempre!!!");
            MenuPrincipal();

        }
        static void ListarEventos()
        {
            Console.WriteLine("Listagem de Eventos:");

            BancoDeDados.CarregarDados();

            List<Evento> eventosPassados = new List<Evento>();
            List<Evento> eventosPresentes = new List<Evento>();
            List<Evento> proximosEventos = new List<Evento>();

            BancoDeDados.FiltraEventos(ref eventosPassados, ref eventosPresentes, ref proximosEventos);


            if (eventosPassados.Count > 0) //contagem de eventos no BD
            {
                Console.WriteLine("Eventos Passados");

                foreach (var evento in eventosPassados) //não utilizaremos um "For" pois não vamos precisar de um numero ou id de identificação
                {
                    Console.WriteLine($"{evento.DataInicial.ToString("dd/MM/yy HH:mm")} - {evento.Nome}");
                    Console.WriteLine("               " + evento.Descricao);
                    Console.WriteLine("");
                }
            }

            if (eventosPresentes.Count > 0) //contagem de eventos no BD
            {
                Console.WriteLine("Eventos Ocorrendo");

                foreach (var evento in eventosPresentes) //não utilizaremos um "For" pois não vamos precisar de um numero ou id de identificação
                {
                    Console.WriteLine($"{evento.DataInicial.ToString("dd/MM/yy HH:mm")} - {evento.Nome}");
                    Console.WriteLine("               " + evento.Descricao);
                    Console.WriteLine("");
                }
            }

            if (proximosEventos.Count > 0) //contagem de eventos no BD
            {
                Console.WriteLine("Eventos Futuros");

                foreach (var evento in proximosEventos) //não utilizaremos um "For" pois não vamos precisar de um numero ou id de identificação
                {
                    Console.WriteLine($"{evento.DataInicial.ToString("dd/MM/yy HH:mm")} - {evento.Nome}");
                    Console.WriteLine("               " + evento.Descricao);
                    Console.WriteLine("");
                }
            }

            if (eventosPassados.Count == 0
            &&  eventosPresentes.Count == 0
            &&  proximosEventos.Count == 0)
            {
                Console.WriteLine("Nenhum evento cadastrado.");
            }
                



            MenuPrincipal();
        }


        static void CadastroDeEvento()
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("=                             Cadastro de Eventos                           =");
            Console.WriteLine("=============================================================================");

            var novoEvento = new Evento();

            Console.Write("Nome do Evento: ");

            novoEvento.Nome = Console.ReadLine();

            Console.Write("Descrição do Evento: ");

            novoEvento.Descricao = Console.ReadLine();

            DateTime dataDigitada = DateTime.MinValue;

            while (dataDigitada == DateTime.MinValue)

            {
                dataDigitada = new DateTime(DateTime.Now.Year,
                                            DateTime.Now.Month,
                                            DateTime.Now.Day,
                                            DateTime.Now.Hour, 0, 0);
                Console.Write("Data Inicial: " + dataDigitada.ToString("dd/MM/yyyy HH:mm "));

                string? valorDigitado = Console.ReadLine();

                if (valorDigitado != "")
                {

                    try //é usado por programadores de C# para particionar o código que pode ser afetado por uma exceção
                    {
                        dataDigitada = DateTime.Parse(valorDigitado);
                        novoEvento.DataInicial = dataDigitada;
                    }
                    catch //associados(try) são usados para tratar qualquer exceção resultante
                    {

                        Console.WriteLine("Data inválida, tente novamente!!");
                        dataDigitada = DateTime.MinValue;
                    }
                }
                else
                {
                    novoEvento.DataInicial = dataDigitada;
                }

            }


            dataDigitada = DateTime.MinValue; // Essa regra deve ser repetida para entrar no próximo loop


            while (dataDigitada == DateTime.MinValue)
            {
                dataDigitada = new DateTime(DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    DateTime.Now.Hour + 3, 0, 0);
                Console.Write("Data Final: " + dataDigitada.ToString("dd/MM/yyyy HH:mm"));
                {
                    string? valorDigitado = Console.ReadLine();

                    if (valorDigitado != "")
                    {
                        try
                        {
                            dataDigitada = DateTime.Parse(valorDigitado);
                            novoEvento.DataFinal = dataDigitada;
                        }
                        catch
                        {
                            Console.WriteLine("Data inválida.");
                            dataDigitada = DateTime.MinValue;

                        }
                    }
                    else
                    {
                        novoEvento.DataFinal = dataDigitada;
                    }
                }

            }

            Console.Write("Endereço do Evento: ");

            novoEvento.Endereco = Console.ReadLine();

            while (novoEvento.Endereco.Length < 5) //Existem ruas com nomes curtos
            {
                Console.Write("Verificar e inserir endereço corretamente: ");

                novoEvento.Endereco = Console.ReadLine();
            }

            Console.WriteLine("Selecione a Categoria: "); //Foi criado uma classe Enum com as opções de categoria e utilizado um "GetValues

            List<CategoriaDeEvento> categorias = new List<CategoriaDeEvento>();

            var valores = Enum.GetValues(typeof(CategoriaDeEvento)).Cast<CategoriaDeEvento>();

            categorias.AddRange(valores);


            for (int i = 0; i < categorias.Count; i++)
            {
                Console.WriteLine((i + 1) + ")" + categorias[i]);
            }

            string opcaoDigitada = Console.ReadLine();

            int opcao = -1;

            while (opcao == -1)
            {
                try
                {
                    opcao = Convert.ToInt32(opcaoDigitada);
                    if (opcao > 0 && opcao <= categorias.Count)
                    {
                        //ok
                        novoEvento.Categoria = categorias[opcao - 1];
                    }
                    else
                    {
                        Console.WriteLine($"Opção inválida, digite de 1 a {categorias.Count}");

                        opcaoDigitada = Console.ReadLine();

                        opcao = -1;
                    }
                }
                catch
                {
                    Console.WriteLine($"Opção inválida, digite de 1 a {categorias.Count}");

                    opcaoDigitada = Console.ReadLine();
                }
            }

            BancoDeDados.SalvarEvento(novoEvento);

            Console.WriteLine("Evento salvado com sucesso.");

            MenuPrincipal();

        }

        static void CadastroDeUsuario()
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("=                             Cadastro de Usuários                           =");
            Console.WriteLine("==============================================================================");
            Console.WriteLine("");

            Usuario novoUsuario = new Usuario();

            Console.WriteLine("Nome Completo:");

            novoUsuario.NomeCompleto = Console.ReadLine();

            while (novoUsuario.NomeCompleto == "")
            {
                Console.WriteLine("Digite um nome válido:");

                novoUsuario.NomeCompleto = Console.ReadLine();
            }

            Console.WriteLine("Email: ");

            novoUsuario.Email = Console.ReadLine();

            while (novoUsuario.Email == "" || !novoUsuario.Email.Contains('@'))
            {
                Console.WriteLine("Digite um email válido:");

                novoUsuario.Email = Console.ReadLine();
            }

            Console.WriteLine("Nome de Usuário: ");

            novoUsuario.NomeDeUsuario = Console.ReadLine();

            while (novoUsuario.NomeDeUsuario == "")
            {
                Console.WriteLine("Digite um nome de Usuário válido:");

                novoUsuario.Email = Console.ReadLine();
            }


            Console.WriteLine("Senha:");

            novoUsuario.Senha = Console.ReadLine();

            BancoDeDados.SalvarUsuario(novoUsuario);

            Console.WriteLine("Usuario salvado com sucesso.");

            Console.WriteLine("");

            MenuPrincipal();


        }

        static void RealizarLogin(string senha)
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("=                                    Login                                  =");
            Console.WriteLine("=============================================================================");
            Console.WriteLine("");

            string? nome;

            Console.Write("Nome de Usuário: ");

            nome = Console.ReadLine();

            while (nome == "")
            {
                Console.Write("Nome de Usuário: ");

                nome = Console.ReadLine();

            }
            Console.Write("Senha: ");

            senha = Console.ReadLine();

            while (senha == "")
            {
                Console.Write("Senha: ");

                senha = Console.ReadLine();
            }

            //banco faz login

            usuarioLogado = BancoDeDados.RealizarLogin(nome, senha: senha);

            if (usuarioLogado == null)
            {
                Console.WriteLine("Login inválido");
            }
            else
            {
                Console.WriteLine("Login realizado com sucesso! Bem-Vindo." + usuarioLogado.NomeCompleto);
            }

            Console.WriteLine("");

            MenuPrincipal();
        }

        static void ParticiparDeEvento()
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("=                           Participar de Evento                            =");
            Console.WriteLine("=============================================================================");
            Console.WriteLine("");

            Console.WriteLine("Selecione um evento" + Environment.NewLine);

            for (int i = 0; i < BancoDeDados.Eventos.Count; i++)
            {
                var evento = BancoDeDados.Eventos[i];

                Console.WriteLine($"\r\n{(i + 1)}) {evento.Nome} - de {evento.DataInicial:dd/MM/yy HH:mm} até {evento.DataFinal:dd/MM/yy HH:mm}");
            }

            Console.WriteLine("");

            int opcao = -1;

            var entrada = Console.ReadLine();

            while (opcao < 0 || opcao > BancoDeDados.Eventos.Count)
            {
                try
                {
                    opcao = Convert.ToInt32(entrada);

                    if (opcao < 0 ||  opcao >= BancoDeDados.Eventos.Count)
                    {
                        Console.WriteLine($"Opção inválida, escolha de 1 a {BancoDeDados.Eventos.Count}");
                        opcao = -1;
                    }
                }
                catch 
                {
                    Console.WriteLine($"Opção inválida, escolha de 1 a { BancoDeDados.Eventos.Count}");                    
                }

                entrada = Console.ReadLine();

            }

            var eventoEscolhido = BancoDeDados.Eventos[opcao - 1];

            try
            {
                BancoDeDados.SalvarUsuarioXEvento(usuarioLogado, eventoEscolhido);
                Console.WriteLine("Participação Confirmada.");
            }
            catch (Exception x)
            {

                if (x.Message == "Participação existente!!!")
                {
                    Console.WriteLine("Você já está cadastrado para este evento");
                }
                else
                    throw;
            }

            

            MenuPrincipal();

        }

        static void CancelarParticipacao()
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("=                         Cancelar Participação                             =");
            Console.WriteLine("=============================================================================");
            Console.WriteLine("");

            var eventos = BancoDeDados.EventosConfirmados(usuarioLogado);


            if (eventos.Count != 0)
            {
                Console.WriteLine("Eventos que você está participando:");


                for (int i = 0; i < eventos.Count; i++)
                {
                    var ev = eventos[i];

                    Console.WriteLine($"{ (i + 1) }) {ev.Nome} - de {ev.DataInicial:dd/MM/yy HH:mm} até {ev.DataFinal:dd/MM/yy HH:mm} ");
                }

                Console.WriteLine("Qual deseja cancelar? ");
                int opcao = -1;

                var entrada = Console.ReadLine();

                while (opcao < 0 || opcao > eventos.Count)
                {
                    try
                    {
                        opcao = Convert.ToInt32(entrada);

                        if (opcao < 0 || opcao >= eventos.Count)
                        {
                            Console.WriteLine($"Opção inválida, escolha de 1 a {eventos.Count}");
                            opcao = -1;
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Opção inválida, escolha de 1 a {eventos.Count}");
                    }

                    entrada = Console.ReadLine();

                }

                var eventoEscolhido = eventos[opcao - 1];

                BancoDeDados.ExcluirUsuarioXEvento(usuarioLogado, eventoEscolhido);

                Console.WriteLine("Participação Cancelada.");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Usuário não encontrado na(s) lista(s) de presença");
            }
            
            

            MenuPrincipal();

        }
    }
}