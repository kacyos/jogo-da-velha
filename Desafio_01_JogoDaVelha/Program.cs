
namespace JogoDaVelha {

    internal class Program {

        static void Main(string[] args) {
            Console.Title = "********* Jogo da Velha # ***************";
            iniciarJogo();
        }

        static string[,] tabuleiro = new string[3, 3];
        static int linha, coluna;
        static int numeroJogadas = 1;
        static int jogador = 1;
        static bool vencedor = false;        

        private static void iniciarJogo() {
            Console.Clear();
            resetarTabuleiro();            

            while (!vencedor) {
                exibirVez();
                desenharTabuleiro();
                Console.WriteLine();
                receberCoordenadas();
                verificarJogada();
                realizarJogada();
                Console.WriteLine();
            }

            fimDeJogo();
        }

        private static void fimDeJogo() {
            Console.WriteLine("R - Reiniciar");
            Console.WriteLine("S - Sair");

            string entrada = Console.ReadLine().ToLower();

            switch (entrada) {
                case "r":
                    iniciarJogo();
                    break;
                case "s":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida");
                    Console.ResetColor();
                    fimDeJogo();
                    break;
            }
        }

        private static void verificarVencedor() {
            bool linhaJogado01;
            bool linhaJogado02;
            bool colunaJogado01;
            bool colunaJogado02;
            bool diagonalJogado01 = tabuleiro[0, 0] == "X" && tabuleiro[1, 1] == "X" && tabuleiro[2, 2] == "X";
            bool diagonalJogado02 = tabuleiro[0, 0] == "0" && tabuleiro[1, 1] == "0" && tabuleiro[2, 2] == "0";

            // Verifica as linhas e colunas
            for (int indice = 0; indice < tabuleiro.GetLength(1); indice++) {
                // verifica as linhas
                linhaJogado01 = tabuleiro[indice, 0] == "X" && tabuleiro[indice, 1] == "X" && tabuleiro[indice, 2] == "X";
                linhaJogado02 = tabuleiro[indice, 0] == "0" && tabuleiro[indice, 1] == "0" && tabuleiro[indice, 2] == "0";
                // verifica as colunas
                colunaJogado01 = tabuleiro[0, indice] == "X" && tabuleiro[1, indice] == "X" && tabuleiro[2, indice] == "X";
                colunaJogado02 = tabuleiro[0, indice] == "0" && tabuleiro[1, indice] == "0" && tabuleiro[2, indice] == "0";

                if (linhaJogado01 || linhaJogado02 || colunaJogado01 || colunaJogado02) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("***********************************");
                    Console.WriteLine($"\tVencedor Jogador {jogador}!");
                    Console.WriteLine("***********************************");
                    Console.ResetColor();
                    vencedor = true;
                    break;
                }
            }

            

            diagonalJogado01 = tabuleiro[0, 2] == "X" && tabuleiro[1, 1] == "X" && tabuleiro[2, 0] == "X";
            diagonalJogado02 = tabuleiro[0, 2] == "0" && tabuleiro[1, 1] == "0" && tabuleiro[2, 0] == "0";


            if (diagonalJogado01 || diagonalJogado02) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***********************************");
                Console.WriteLine($"\tVencedor Jogador {jogador}!");
                Console.WriteLine("***********************************");
                Console.ResetColor();
                vencedor = true;
            }

            if (!vencedor && numeroJogadas == 9) {
                vencedor = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*****************************");
                Console.WriteLine($"\tJogo empatado!");
                Console.WriteLine("*****************************");
                Console.ResetColor();
            }
        }

        private static void receberCoordenadas() {
            Console.ForegroundColor = jogador == 1 ? ConsoleColor.Blue : ConsoleColor.Green;
            Console.Write("Informe a linha: ");
            linha = int.Parse(Console.ReadLine());

            Console.ForegroundColor = jogador == 1 ? ConsoleColor.Blue : ConsoleColor.Green;
            Console.Write("Informe a Coluna: ");
            coluna = int.Parse(Console.ReadLine());

            verificarJogada();
        }

        private static void realizarJogada() {

            if (jogador == 1) {
                tabuleiro[linha - 1, coluna - 1] = "X";

            } else {
                tabuleiro[linha - 1, coluna - 1] = "0";
            }

            Console.Clear();
            verificarVencedor();
            jogador = jogador > 1 ? 1 : 2;
            numeroJogadas += 1;
        }

        private static void exibirVez() {
            Console.ForegroundColor = jogador == 1 ? ConsoleColor.Blue : ConsoleColor.Green;
            Console.WriteLine();
            Console.Write("\t─▄▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▄\r\n\t█░░░█░░░░░░░░░░▄▄░██░█\r\n\t█░▀▀█▀▀░▄▀░▄▀░░▀▀░▄▄░█\r\n\t█░░░▀░░░▄▄▄▄▄░░██░▀▀░█\r\n\t─▀▄▄▄▄▄▀─────▀▄▄▄▄▄▄▀");
            Console.WriteLine();
            Console.WriteLine("\n********** Jogo da Velha **********");
            Console.WriteLine($"Player {jogador} jogando...");
        }

        private static void resetarTabuleiro() {
            numeroJogadas = 1;
            jogador = 1;
            vencedor = false;

            for (int linha= 0; linha < tabuleiro.GetLength(0); linha++) {
                for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++) {
                    tabuleiro[linha, coluna] = "-";
                }
            }
        }

        private static void desenharTabuleiro() {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"     1   2   3 ");
            Console.ResetColor();

            Console.WriteLine();


            for (int linha = 0; linha < tabuleiro.GetLength(0); linha++) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($" {linha + 1}");
                Console.ResetColor();
               
                // Preenche o tabuleiro com as informações
                for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++) {
                    Console.Write(" | ");
                    if ((tabuleiro[linha, coluna] != "X") && (tabuleiro[linha, coluna] != "0")) {
                        tabuleiro[linha, coluna] = "-";                        
                        Console.Write($"{tabuleiro[linha, coluna]}");

                    } else if (tabuleiro[linha, coluna] == "X") {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{tabuleiro[linha, coluna]}");
                        Console.ResetColor();
                    } else {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{tabuleiro[linha, coluna]}");
                        Console.ResetColor();
                    }
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }

        private static void verificarJogada() {
            if (linha < 1 || coluna < 1) {
                Console.Clear();
                exibirVez();
                desenharTabuleiro();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jogada inválida");
                receberCoordenadas();
            } else if (linha > 3 || coluna > 3) {
                Console.Clear();
                exibirVez();
                desenharTabuleiro();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jogada inválida");
                receberCoordenadas();
            } else if (tabuleiro[linha - 1, coluna - 1] != "-") {
                Console.Clear();
                exibirVez();
                desenharTabuleiro();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jogada inválida");
                receberCoordenadas();
            }

        }

    }
}