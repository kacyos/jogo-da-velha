namespace JogoDaVelha {

    internal class Program {
        static string[,] tabuleiro = new string[3, 3];
        static char nivelDeJogo, modoDeJogo;
        static int linhaEscolhida, colunaEscolhida;
        static int numeroDeRodadas = 1;
        static string jogador;
        static bool vencedor, empate;
        static bool jogoIniciado = true;

        static void Main(string[] args) {
            Console.Title = "********* Jogo da Velha # ***************";
            IniciarJogo();
            reiniciarJogo();
        }
        public static void IniciarJogo() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t######## Jogo da velha ########");
            selecionarModoDeJogo();
            selecionarNivelDeJogo();

            while (jogoIniciado) {
                Console.Clear();
                desenharTabuleiro();
                if (jogador != "Computador") {
                    receberCoordenadas();
                    consolidarJogada();
                } else {
                    switch (nivelDeJogo) {
                        case 'f':
                            nivelFacil();
                            consolidarJogada();
                            break;
                        case 'n':
                            nivelNormal();
                            consolidarJogada();
                            break;
                        case 'd':
                            nivelDificil();
                            consolidarJogada();
                            break;
                        default:
                            break;
                    }
                }

            }

        }

        public static void selecionarModoDeJogo() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\tEscolha um modo de Jogo: \n");
            Console.Write("\n\t1 - Máquina vs Jogador\n\t2 - Jogador 01 vs Jogador 02\n");
            Console.Write("\n\tModo de jogo: ");
            modoDeJogo = char.Parse(Console.ReadLine());

            switch (modoDeJogo) {
                case '1':
                    jogador = "Computador";
                    break;
                case '2':
                    jogador = "01";
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t*** Opção inválida ***\n");
                    Console.ResetColor();
                    selecionarModoDeJogo();
                    break;
            }
            Console.Clear();
            Console.ResetColor();
        }

        public static void selecionarNivelDeJogo() {
            if (modoDeJogo == '1') {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\tDefina o nível de jogo: \n");
                Console.Write("\n\t1 - Facil\n\t2 - Normal\n\t3 - Dificil\n");
                Console.Write("\n\tNivel: ");
                nivelDeJogo = char.Parse(Console.ReadLine());

                switch (nivelDeJogo) {
                    case '1':
                        nivelDeJogo = 'f';
                        break;
                    case '2':
                        nivelDeJogo = 'n';
                        break;
                    case '3':
                        nivelDeJogo = 'd';
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\t*** Opção inválida ***\n");
                        Console.ResetColor();
                        selecionarNivelDeJogo();
                        break;
                }
                Console.Clear();
                Console.ResetColor();
            }
        }

        public static void reiniciarJogo() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\n*** Deseja continuar jogando? ***");
            Console.WriteLine("\n\ts - Sim\n\tn - Não");
            Console.Write("\n\tConstinuar: ");
            char continuar = char.Parse(Console.ReadLine().ToLower());

            switch (continuar) {
                case 's':
                    jogoIniciado = true;
                    Console.Clear();
                    IniciarJogo();
                    break;
                case 'n':
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("\t*** Jogo finalizado ***\n");
                    Console.WriteLine("\tPressione qualquer tecla para sair.");
                    jogoIniciado = false;
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t*** Opção inválida ***\n");
                    Console.ResetColor();
                    reiniciarJogo();
                    break;
            }

            Console.ResetColor();
        }

        public static void desenharTabuleiro() {


            if (!vencedor && !empate) {
                Console.ForegroundColor = jogador != "02" ? ConsoleColor.Blue : ConsoleColor.Green;
                Console.Write($"\tVez Jogador {jogador}");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"\n\t     1   2   3 \n");

            for (int linha = 0; linha < tabuleiro.GetLength(0); linha++) {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"\t {linha + 1}");
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

        public static void receberCoordenadas() {
            Console.Write("\n\tInforme a linha da Jogada: ");
            linhaEscolhida = (int.Parse(Console.ReadLine()) - 1);

            Console.Write("\tInforme a coluna da Jogada: ");
            colunaEscolhida = (int.Parse(Console.ReadLine()) - 1);

            verificarEscolha();
        }


        public static void consolidarJogada() {
            if (jogador == "01" || jogador == "Computador") {
                tabuleiro[linhaEscolhida, colunaEscolhida] = "X";
                numeroDeRodadas += 1;
                verificarVencedor();

                if (vencedor) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t*************** Vitória *******************\n");
                    Console.WriteLine($"\t\tJogador {jogador} venceu!\n");
                    Console.WriteLine("\t*******************************************");
                    Console.ResetColor();
                    desenharTabuleiro();
                    jogoIniciado = false;
                } else if (empate) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t*************** Jogo empatado *******************\n");
                    Console.WriteLine($"\t\t\tEmpate!\n");
                    Console.WriteLine("\t*************************************************");
                    Console.ResetColor();
                    desenharTabuleiro();
                    jogoIniciado = false;
                } else {
                    Console.Clear();
                    jogador = "02";
                    desenharTabuleiro();
                }

            } else {
                tabuleiro[linhaEscolhida, colunaEscolhida] = "0";
                numeroDeRodadas += 1;
                verificarVencedor();

                if (vencedor) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t*************** Vitória *******************\n");
                    Console.WriteLine($"\t\tJogador {jogador} venceu!\n");
                    Console.WriteLine("\t*******************************************");
                    Console.ResetColor();
                    desenharTabuleiro();
                    jogoIniciado = false;
                } else if (empate) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t*************** Jogo empatado *******************\n");
                    Console.WriteLine($"\t\t\tEmpate!\n");
                    Console.WriteLine("\t*************************************************");
                    Console.ResetColor();
                    desenharTabuleiro();
                    jogoIniciado = false;
                } else {
                    Console.Clear();
                    jogador = modoDeJogo == '1' ? "Computador" : "01";
                    desenharTabuleiro();
                }
            }
        }

        public static void verificarEscolha() {

            if ((tabuleiro[linhaEscolhida, colunaEscolhida] != "-") || (linhaEscolhida > 2 || linhaEscolhida < 0) || (colunaEscolhida > 2 || colunaEscolhida < 0)) {
                Console.Clear();
                desenharTabuleiro();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t**********************");
                Console.WriteLine("\tJogada não permitida.");
                Console.WriteLine("\t**********************");
                Console.ResetColor();
                receberCoordenadas();
            }
        }

        public static void verificarVencedor() {
            bool diagonalPrincipalJogador01 = tabuleiro[0, 0] == "X" && tabuleiro[1, 1] == "X" && tabuleiro[2, 2] == "X";
            bool diagonalPrincipalJogador02 = tabuleiro[0, 0] == "0" && tabuleiro[1, 1] == "0" && tabuleiro[2, 2] == "0";
            bool diagonalSecundariaJogador01 = tabuleiro[0, 2] == "X" && tabuleiro[1, 1] == "X" && tabuleiro[2, 0] == "X";
            bool diagonalSecundariaJogador02 = tabuleiro[0, 2] == "0" && tabuleiro[1, 1] == "0" && tabuleiro[2, 0] == "0";
            bool linhaJogado01, linhaJogado02, colunaJogado01, colunaJogado02;

            if (diagonalPrincipalJogador01 || diagonalPrincipalJogador02 || diagonalSecundariaJogador01 || diagonalSecundariaJogador02) {
                vencedor = true;
            }

            if (!vencedor && numeroDeRodadas >= 5) {
                for (int indice = 0; indice < tabuleiro.GetLength(0); indice++) {
                    // verifica as linhas
                    linhaJogado01 = tabuleiro[indice, 0] == "X" && tabuleiro[indice, 1] == "X" && tabuleiro[indice, 2] == "X";
                    linhaJogado02 = tabuleiro[indice, 0] == "0" && tabuleiro[indice, 1] == "0" && tabuleiro[indice, 2] == "0";

                    // verifica as colunas
                    colunaJogado01 = tabuleiro[0, indice] == "X" && tabuleiro[1, indice] == "X" && tabuleiro[2, indice] == "X";
                    colunaJogado02 = tabuleiro[0, indice] == "0" && tabuleiro[1, indice] == "0" && tabuleiro[2, indice] == "0";

                    if (linhaJogado01 || linhaJogado02 || colunaJogado01 || colunaJogado02) {
                        vencedor = true;
                        break;
                    }
                }

            }

            if (!vencedor && numeroDeRodadas > 9) {
                empate = true;
            }
        }

        public static void nivelFacil() {
            Random random = new Random();
            int linha, coluna;
            bool jogando = true;

            Console.Write("Máquina jogando:");

            while (jogando) {
                linha = random.Next(0, 3);
                coluna = random.Next(0, 3);

                if (tabuleiro[linha, coluna] == "-") {
                    linhaEscolhida = linha;
                    colunaEscolhida = coluna;
                    jogando = false;
                }
            }
        }

        public static void nivelNormal() {
            Random random = new Random();
            int numero = random.Next(0, 7);

            if (numero <= 3) {
                nivelFacil();
            } else {
                nivelDificil();
            }

        }

        public static void nivelDificil() {
            Random random = new Random();
            bool realizandoJogada = true;
            bool possibilidadeDeVitoriaDiagonal01 = (tabuleiro[0, 0] == "X" && tabuleiro[1, 1] == "X" && tabuleiro[2, 2] == "-") || (tabuleiro[1, 1] == "X" && tabuleiro[2, 2] == "X" && tabuleiro[0, 0] == "-") || (tabuleiro[0, 0] == "0" && tabuleiro[1, 1] == "0" && tabuleiro[2, 2] == "-") || (tabuleiro[1, 1] == "0" && tabuleiro[2, 2] == "0" && tabuleiro[0, 0] == "-");
            bool possibilidadeDeVitoriaDiagonal02 = (tabuleiro[0, 2] == "X" && tabuleiro[1, 1] == "X" && tabuleiro[2, 0] == "-") || (tabuleiro[1, 1] == "X" && tabuleiro[2, 0] == "X" && tabuleiro[0, 2] == "-") || (tabuleiro[0, 2] == "0" && tabuleiro[1, 1] == "0" && tabuleiro[2, 0] == "-") || (tabuleiro[1, 1] == "0" && tabuleiro[2, 0] == "0" && tabuleiro[0, 2] == "-");

            // Realiza as 3 primeiras Jogadas
            if (numeroDeRodadas <= 3) {
                while (realizandoJogada) {
                    int aleatorio = random.Next(1, 6);

                    switch (aleatorio) {
                        case 1:
                            if (tabuleiro[0, 0] == "-") {
                                linhaEscolhida = 0;
                                colunaEscolhida = 0;
                                realizandoJogada = false;
                            }
                            break;
                        case 2:
                            if (tabuleiro[0, 2] == "-") {
                                linhaEscolhida = 0;
                                colunaEscolhida = 2;
                                realizandoJogada = false;
                            };
                            break;
                        case 3:
                            if (tabuleiro[2, 0] == "-") {
                                linhaEscolhida = 2;
                                colunaEscolhida = 0;
                                realizandoJogada = false;
                            };
                            break;
                        case 4:
                            if (tabuleiro[2, 2] == "-") {
                                linhaEscolhida = 2;
                                colunaEscolhida = 2;
                                realizandoJogada = false;
                            };
                            break;
                        case 5:
                            if (tabuleiro[1, 1] == "-") {
                                linhaEscolhida = 1;
                                colunaEscolhida = 1;
                                realizandoJogada = false;
                            };
                            break;
                        default:
                            break;
                    }
                }
            }

            // Verifica possibilidade de vitória da máquina ou do jogador nas linhas
            if (realizandoJogada && !possibilidadeDeVitoriaDiagonal01 && !possibilidadeDeVitoriaDiagonal02) {
                for (int index = 0; index < tabuleiro.GetLength(0); index++) {
                    if ((tabuleiro[index, 0] == "X" && tabuleiro[index, 1] == "X" && tabuleiro[index, 2] == "-") || (tabuleiro[index, 0] == "0" && tabuleiro[index, 1] == "0" && tabuleiro[index, 2] == "-")) {
                        linhaEscolhida = index;
                        colunaEscolhida = 2;
                        realizandoJogada = false;
                        break;
                    } else if ((tabuleiro[index, 0] == "X" && tabuleiro[index, 2] == "X" && tabuleiro[index, 1] == "-") || (tabuleiro[index, 0] == "0" && tabuleiro[index, 2] == "0" && tabuleiro[index, 1] == "-")) {
                        linhaEscolhida = index;
                        colunaEscolhida = 1;
                        realizandoJogada = false;
                        break;
                    } else if ((tabuleiro[index, 1] == "X" && tabuleiro[index, 2] == "X" && tabuleiro[index, 0] == "-") || (tabuleiro[index, 1] == "0" && tabuleiro[index, 2] == "0" && tabuleiro[index, 0] == "-")) {
                        linhaEscolhida = index;
                        colunaEscolhida = 0;
                        realizandoJogada = false;
                        break;
                    }
                }
            }

            // Verifica possibilidade de vitória da máquina ou do jogador nas colunas
            if (realizandoJogada && !possibilidadeDeVitoriaDiagonal01 && !possibilidadeDeVitoriaDiagonal02) {
                for (int index = 0; index < tabuleiro.GetLength(0); index++) {
                    if ((tabuleiro[0, index] == "X" && tabuleiro[1, index] == "X" && tabuleiro[2, index] == "-") || (tabuleiro[0, index] == "0" && tabuleiro[1, index] == "0" && tabuleiro[2, index] == "-")) {
                        linhaEscolhida = 2;
                        colunaEscolhida = index;
                        realizandoJogada = false;
                        break;
                    }
                    if ((tabuleiro[0, index] == "X" && tabuleiro[2, index] == "X" && tabuleiro[1, index] == "-") || (tabuleiro[0, index] == "0" && tabuleiro[2, index] == "0" && tabuleiro[1, index] == "-")) {
                        linhaEscolhida = 1;
                        colunaEscolhida = index;
                        realizandoJogada = false;
                        break;
                    }
                    if ((tabuleiro[1, index] == "X" && tabuleiro[2, index] == "X" && tabuleiro[0, index] == "-") || (tabuleiro[1, index] == "0" && tabuleiro[2, index] == "0" && tabuleiro[0, index] == "-")) {
                        linhaEscolhida = 0;
                        colunaEscolhida = index;
                        realizandoJogada = false;
                        break;
                    }
                }
            }

            if (realizandoJogada && possibilidadeDeVitoriaDiagonal01) {
                for (int index = 0; index < tabuleiro.GetLength(0); index++) {
                    if (tabuleiro[index, index] == "-") {
                        linhaEscolhida = index;
                        colunaEscolhida = index;
                        realizandoJogada = false;
                        break;
                    }
                }
            }

            if (realizandoJogada && possibilidadeDeVitoriaDiagonal02) {
                int y = 2;
                for (int x = 0; x < tabuleiro.GetLength(0); x++) {
                    if (tabuleiro[x, y] == "-") {
                        linhaEscolhida = x;
                        colunaEscolhida = y;
                        realizandoJogada = false;
                        break;
                    }
                    y--;
                }
            }

            if (realizandoJogada) {
                nivelFacil();
            }
        }
    }
}