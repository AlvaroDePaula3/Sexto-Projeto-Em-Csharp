using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Sexto_Projeto_Em_C_.Program;

namespace Sexto_Projeto_Em_C_
{
    internal class Program
    {

        static string marcaDoInicio;
        static string marcaDoFim;
        static string Nome;
        static string nomeDeUsuario;
        static string dataDeNascimento;
        static string Idade;
        public struct cadastroDeUsuario
        {
            public string nome;
            public string nomeDeUsuario;
            public UInt32 idade;
            public DateTime dataDeNascimento;
        }

        public enum saida
        {
            Sucesso = 0,
            Sair = 1,
            Excecao = 2
        }

        public static void Exibirmensagem(string Exibirmensagem)
        {
            Console.Write(Exibirmensagem);
            Console.WriteLine("Aperte qualquer coisa pra prosseguir");
            Console.ReadKey(true);
            Console.Clear();
        }

        public static saida digitoUsuario(ref string digito, string mensagem)
        {
            saida retornar;
            Console.WriteLine(mensagem);
            string variavelTemporaria = Console.ReadLine();
            if (variavelTemporaria == "d" || variavelTemporaria == "D")
                retornar = saida.Sair;
            else
            {
                digito = variavelTemporaria;
                retornar = saida.Sucesso;
            }
            Console.Clear();
            return retornar;
        }

        //método pra data
        public static saida dataUsuario(ref DateTime data, string mensagem)
        {
            saida retornar;
            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string variavelTemporaria = Console.ReadLine();
                    if (variavelTemporaria == "d" || variavelTemporaria == "D")
                        retornar = saida.Sair;
                    else
                    {
                        data = Convert.ToDateTime(variavelTemporaria);
                        retornar = saida.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Aperte qualquer tecla pra prosseguir");
                    Console.ReadKey();
                    Console.Clear();
                    retornar = saida.Excecao;
                }
            } while (retornar == saida.Excecao);
            Console.Clear();
            return retornar;
        }

        //método pra Uint32
        public static saida idadeUsuario(ref UInt32 idade, string mensagem)
        {
            saida retornar;
            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string variavelTemporaria = Console.ReadLine();
                    if (variavelTemporaria == "d" || variavelTemporaria == "D")
                        retornar = saida.Sair;
                    else
                    {
                        idade = Convert.ToUInt32(variavelTemporaria);
                        retornar = saida.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Aperte qualquer tecla pra prosseguir");
                    Console.ReadKey();
                    Console.Clear();
                    retornar = saida.Excecao;
                }
            } while (retornar == saida.Excecao);
            Console.Clear();
            return retornar;
        }

        public static saida RegistraUsuario(ref List<cadastroDeUsuario> registrarUsuario)
        {
            cadastroDeUsuario CadastrarUsuario;
            CadastrarUsuario.nome = "";
            CadastrarUsuario.nomeDeUsuario = "";
            CadastrarUsuario.idade = 0;
            CadastrarUsuario.dataDeNascimento = new DateTime();
            if (digitoUsuario(ref CadastrarUsuario.nome, "Digite seu nome completo ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (digitoUsuario(ref CadastrarUsuario.nomeDeUsuario, "Digite seu login ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (idadeUsuario(ref CadastrarUsuario.idade, "Digite sua idade ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (dataUsuario(ref CadastrarUsuario.dataDeNascimento, "Digite sua data de nascimento no formato (DD/MM/AAAA) ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            registrarUsuario.Add(CadastrarUsuario);
            return saida.Sucesso;
        }

        public static void SalvaDados (string gravar, List<cadastroDeUsuario> registrarUsuario)
        {
            try
            {

            string armazenar = "";
            foreach(cadastroDeUsuario armazenarDados in registrarUsuario)
            {
                armazenar += marcaDoInicio + "\r\n";
                armazenar += Nome + armazenarDados.nome + "\r\n";
                armazenar += nomeDeUsuario + armazenarDados.nomeDeUsuario + "\r\n";
                armazenar += Idade + armazenarDados.idade + "\r\n";
                armazenar += dataDeNascimento + armazenarDados.dataDeNascimento.ToString("dd/MM/yyyy") + "\r\n";
                armazenar += marcaDoFim + "\r\n";
            }
            File.WriteAllText(gravar, armazenar);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void carregar(string dadosUsuario, ref List<cadastroDeUsuario> registrarUsuario)
        {
            try
            {
                if(File.Exists(dadosUsuario))
                {
                    string[] dados = File.ReadAllLines(dadosUsuario);
                    cadastroDeUsuario cadastrar;
                    cadastrar.nome = "";
                    cadastrar.nomeDeUsuario = "";
                    cadastrar.idade = 0;
                    cadastrar.dataDeNascimento = new DateTime();

                    foreach (string cadastro in dados)
                    {
                        if (cadastro.Contains(marcaDoInicio))
                            continue;
                        if (cadastro.Contains(marcaDoFim))
                            registrarUsuario.Add(cadastrar);
                        if (cadastro.Contains(Nome))
                            cadastrar.nome = cadastro.Replace(Nome, "");
                        if (cadastro.Contains(nomeDeUsuario))
                            cadastrar.nomeDeUsuario = cadastro.Replace(nomeDeUsuario, "");
                        if (cadastro.Contains(Idade))
                            cadastrar.idade = Convert.ToUInt32(cadastro.Replace(Idade, ""));
                        if (cadastro.Contains(dataDeNascimento))
                            cadastrar.dataDeNascimento = Convert.ToDateTime(cadastro.Replace(dataDeNascimento, ""));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        static void Main(string[] args)
        {
            List<cadastroDeUsuario> registrarUsuario = new List<cadastroDeUsuario>();
            string botao = "";
            marcaDoInicio = "##### Inicio #####";
            marcaDoFim = "##### Fim #####";
            Nome = "Nome: ";
            nomeDeUsuario = "Nome de usuario = ";
            Idade = "Idade: ";
            dataDeNascimento = "Data de nascimento: ";
            string enderecoDoArquivo = @"DadosDosUsuarios.txt";

            carregar(enderecoDoArquivo, ref registrarUsuario);

            do
            {
                Console.WriteLine("Pressione R para registrar usuário ou pressione D para sair da aplicação");
                botao = Console.ReadKey(true).KeyChar.ToString().ToLower();

                if (botao == "r")
                {
                    //cadastrar um usuário novo
                    if (RegistraUsuario(ref registrarUsuario) == saida.Sucesso)
                        SalvaDados(enderecoDoArquivo, registrarUsuario);

                }
                else if (botao == "d")
                {
                    Exibirmensagem("Obrigado e volte sempre, o programa será encerrado em breve");

                }
                else
                {
                    Exibirmensagem("Opção desconhecida, por favor, selecione uma das opções conhecidas pelo nosso sistema");
                }

            } while (botao != "d");
        }
    }
}
