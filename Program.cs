using System; // padrao
using System.Collections.Generic; // para usar listas
using System.IO; // para carregar arquivos do meu pc (textos)
using System.Text.RegularExpressions; // (para mexer com regex)
using System.Threading; // (para mexer com threads)

namespace megaSorteador
{
    public class MegaSenaJogo
    {
        public int[] Dezenas = new int[6];
    }
    class Program
    {
       
        static bool hasNumber(string __)
        {
            bool c = false;
            string numbers = "0123456789";
            int pointer = 0;
            for (int x = 0; x < __.Length; x++)
            {
            restart:
                if (pointer != 9)
                    if (__[x] == numbers[pointer])
                    {
                        c = true;
                        return true;
                    }
                    else
                    {
                        pointer++;
                        goto restart;
                    }
                else
                    pointer = 0;
            }
            return c;
        }

        static bool isNumberMega(string __)
        {
            if (__.Length == 1)
            {
                return false;
            }
            else
                return true;
        }



        public static List<MegaSenaJogo> Jogos = new List<MegaSenaJogo>();

        static void readData(string path)
        {

            string[] Data = File.ReadAllLines(path);
            int Counter = 0;

            string JogoMega = String.Empty;

            for (int i = 0; i < Data.Length; i++)
            {
                if (hasNumber(Data[i]))
                {

                    //   var JogoCheck = Regex.Match(Data[i], @"\d+");
                    var JogoCheck = Regex.Match(Data[i], "[^<td rowspawn = \"1\">/].").Groups[0].Value;
                    if (!String.IsNullOrWhiteSpace(JogoCheck))
                    {
                        JogoCheck = Regex.Match(JogoCheck, @"\d+").Value;
                        if (hasNumber(JogoCheck))
                            if (isNumberMega(JogoCheck))
                            {
                                if (Counter < 7)
                                {
                                    JogoMega = JogoMega + $"{JogoCheck} ";
                                    Counter = Counter + 1;
                                }
                                else
                                {
                                    string[] l = JogoMega.Split(' ');
                                    MegaSenaJogo jogo = new MegaSenaJogo();

                                    for (int A = 0; A < 6; A++)
                                    {
                                        jogo.Dezenas[A] = int.Parse(l[A]);
                                    }
                                    for (int x = 0; x < jogo.Dezenas.Length; x++) {
                                        if (jogo.Dezenas[x] > 60)
                                        {
                                            jogo.Dezenas[x] = new Random().Next(0, 60);
                                        }
                                            }
                                    Jogos.Add(jogo);                              
                                    JogoMega = String.Empty;
                                    Counter = 0;
                                }
                            }
                    }

                }
            }
        }

        static void Main(string[] args)
        {



            List<int> numb = new List<int>();

            readData("JOGO.HTM");
            Console.WriteLine($"{Jogos.Count} Jogos Carregados");
            Console.Beep();
            Console.WriteLine($"Pressione [Enter] para gerar um jogo.");
        // Console.WriteLine(isNumberMega("60"));

        //Analizer

        GENERATE:

            Console.WriteLine("");

            for (int i = 0; i < 6; i++)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[");
                Console.ForegroundColor = ConsoleColor.Green;
                 Console.Write($"{Jogos[new Random().Next(0, Jogos.Count)].Dezenas[new Random().Next(0, 6)] }");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"]");
                Thread.Sleep(500);
            }

            Console.ReadKey();
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                goto GENERATE;
            }
        }
    }
}

