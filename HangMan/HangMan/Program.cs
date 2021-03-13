using System;
using System.Collections.Generic;

namespace HangMan
{
    public class SETTINGS 
    {
        public bool flag, flag2, gameOverPlayer2;
        public char guessChar;
        public static string secondPlayerWord;
        public static bool[] stringGuessIndex = new bool[30]; 
        public static int choice, gamePlayMode,  settingMode, life, counter, iterateString;
        public string[] singlePlayerWords = { 
            "INDIA", "CHINA", "RUSSIA", "BRAZIL", "ARGENTINA", "BANGLADESH", "BHUTAN", "CHAD",
            "FIJI", "GHANA", "JAPAN", "MOROCCO", "TUNISIA", "ZAMBIA", "SWEDEN", "MEXICO"
        };
        public string[] options = { "  @ Start Game  ", "  @ Settings  ", "  @ Quit  " }; 
        public string[] player_options = { "  @ Play with computer  ", "  @ Two player mode  " };
        public void clear_screen() { Console.Clear(); }
        public void human_color() { Console.ForegroundColor = ConsoleColor.Yellow; }
        public void stand_color() { Console.ForegroundColor = ConsoleColor.DarkYellow; }
        public void clearStringGuessIndex() { for(int i = 0; i < 30 && stringGuessIndex[i]; i++) { stringGuessIndex[i] = false; } }
        public void reset_game()
        { 
            choice = 0; gamePlayMode = 0; settingMode = 0; iterateString = 0; life = 5; 
            flag = false; counter = 0; clearStringGuessIndex(); Console.ResetColor(); flag2 = false;
        }
        public SETTINGS() 
        { 
            choice = 0; gamePlayMode = 0; settingMode = 0; iterateString = 0; life = 5; flag = false; 
            counter = 0; gameOverPlayer2 = false; flag2 = false; 
        }
    }

    public class MOVEMENT : SETTINGS
    {
        public void menu_select_movement()
        {
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    choice -= (choice != 0) ? 1 : 0; break;
                case ConsoleKey.DownArrow:
                    choice += (settingMode == 0 && choice != 2 || settingMode == 2 && choice != 1) ? 1 : 0; break;
                case ConsoleKey.Enter:
                    if(settingMode == 0)
                    {
                        if(choice == 0) { settingMode = 1; }
                        else if(choice == 1) {
                            choice = 0;
                            settingMode = 2; 
                        }
                        else if(choice == 2) { settingMode = 3; }
                    }
                    else if(settingMode == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        if(choice == 0) 
                        {
                            gamePlayMode = 0;
                            Console.WriteLine("\n\t\t\t\t(Single Player Mode Selected)");
                        } 
                        else 
                        { 
                            gamePlayMode = 1;
                            Console.WriteLine("\n\t\t\t\t(Two Player Mode Selected)");
                        }
                        settingMode = 0;
                        choice = 0;
                        System.Threading.Thread.Sleep(1000);
                        Console.ResetColor();
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public class DISPLAY : SETTINGS
    {
        public int game_menu()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("\n\n\t\t\t\t                \n");
            Console.Write($"\t\t\t\t  Hangman Game  \n");
            Console.Write("\t\t\t\t                \n\n\n");
            Console.ResetColor();
            if(settingMode == 0) 
            {
                for (int i = 0; i < 3; i++)
                {
                    if (choice == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine("\t\t\t\t" + options[i] + "\n");
                }
                Console.ResetColor();
                return 1;
            }
            else if(settingMode == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\t\t\t\tGameplay Mode:\n\n");
                for (int i = 0; i < 2; i++)
                {
                    if (choice == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine("\t\t\t\t" + player_options[i] + "\n");
                }
                Console.ResetColor();
                return 1;
            }
            else if(settingMode == 3) { return 2; }
            return 0;
        }
    }

    public class GAMEPLAY : SETTINGS
    {
        public void hangman_display()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if(gamePlayMode == 0) { Console.Write($"\n\t\tHangman Game - Single Player\tWord-type: Country\tLife: {life}\t\tLevel: {iterateString+1}\n\n\t"); }
            else { Console.Write($"\n\t\tHangman Game - Two Player\t\t\t\tLife: {life}\n\n\t"); }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for(int i = 0; i < 10; i++) { Console.Write("-"); }
            Console.ResetColor();
            Console.WriteLine("O");
            stand_color();
            for(int j = 0; j < 12; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0)
                    {
                        Console.Write("\t|");
                    }
                    else if (i == 8 && j < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("|");
                    }
                    else if (j == 2 && i == 7 && life < 5) { human_color(); Console.Write("("); }
                    else if (j == 2 && i == 9 && life < 5) { human_color(); Console.Write(")"); }
                    else if(j == 3 && i == 8 && life < 4) { human_color(); Console.Write("v"); }
                    else if (j == 4 && i == 7 && life < 3) { human_color(); Console.Write("/|\\"); }
                    else if (j == 5 && i == 8 && life < 2) { human_color(); Console.Write("|"); }
                    else if (j == 6 && i == 8 && life < 1) { human_color(); Console.Write("^"); }
                    else if (j == 7 && i == 7 && life < 1) { human_color(); Console.Write("/ \\"); }
                    else
                    {
                        Console.Write(" ");
                    }
                    stand_color();
                }
                Console.WriteLine();
            }
            Console.Write("      ");
            for (int i = 0; i < 5; i++) { Console.Write("-"); }
            Console.ResetColor();
            if(life == 0)
            {
                Console.WriteLine("\n\n\n\t\t\tGame Over!\n\n\n");
                System.Threading.Thread.Sleep(2000);
                clear_screen();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("\n\n\n\t\t\t\t\t  G  A  M  E    O  V  E  R  \n\n\n");
                System.Threading.Thread.Sleep(2000);
                reset_game();
            }
        }
        public void start_playing()
        {
            if(gamePlayMode == 0 && iterateString != 16)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\n      Word:\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                int lengthStr = singlePlayerWords[iterateString].Length;
                for (int i = 0; i < lengthStr; i++)
                {
                    if(stringGuessIndex[i]) 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{singlePlayerWords[iterateString][i]}  ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else { Console.Write("_  "); }
                }
                Console.Write("\n\n      Guess: "); 
                guessChar = char.ToUpper(Console.ReadKey().KeyChar);
                for (int i = 0; i < lengthStr; i++)
                {
                    if (singlePlayerWords[iterateString][i] == guessChar && !stringGuessIndex[i])
                    {
                        stringGuessIndex[i] = true;
                        flag = true;
                        counter++;
                        if(lengthStr == counter) { goto block; }
                        break;
                    }
                }
                if (flag) { flag = false; } 
                else 
                { 
                    life--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t(Wrong)");
                    System.Threading.Thread.Sleep(1000);
                    Console.ResetColor();
                }
                if(lengthStr == counter)
                {
                    counter = 0;
                    clearStringGuessIndex();
                    iterateString++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\n\t\t(Correct)");
                    System.Threading.Thread.Sleep(1000);
                    Console.ResetColor();
                }
            block:;
            }
            else if(gamePlayMode == 1 && !gameOverPlayer2)
            {
                if(!flag)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\n\n      Give a word to your frind to guess: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    secondPlayerWord =  Console.ReadLine().ToUpper();
                    Console.ResetColor();
                    flag = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n\n      Word:\t");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    int lengthStr = secondPlayerWord.Length;
                    for (int i = 0; i < lengthStr; i++)
                    {
                        if (stringGuessIndex[i])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{secondPlayerWord[i]}  ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else { Console.Write("_  "); }
                    }
                    Console.Write("\n\n      Guess: ");
                    guessChar = char.ToUpper(Console.ReadKey().KeyChar);
                    for (int i = 0; i < lengthStr; i++)
                    {
                        if (secondPlayerWord[i] == guessChar && !stringGuessIndex[i])
                        {
                            stringGuessIndex[i] = true;
                            flag2 = true;
                            counter++;
                            if (lengthStr == counter) { goto block2; }
                            break;
                        }
                    }
                    if (flag2) { flag2 = false; }
                    else
                    {
                        life--;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\n\t\t(Wrong)");
                        System.Threading.Thread.Sleep(1000);
                        Console.ResetColor();
                    }
                    if (lengthStr == counter)
                    {
                        counter = 0;
                        clearStringGuessIndex();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\n\t\t(Correct)");
                        System.Threading.Thread.Sleep(1000);
                        Console.ResetColor();
                        gameOverPlayer2 = true;
                    }
                block2:;
                }
            }
            else
            {
                clear_screen();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n\n\n\t\t\t\t\t\tCongratulations!");
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\n\n\t\t\t\t\t\tYou win the game!");
                System.Threading.Thread.Sleep(1500);
                Console.ResetColor();
                reset_game();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GAMEPLAY gameplay = new GAMEPLAY();
            SETTINGS settings = new SETTINGS();
            MOVEMENT movement = new MOVEMENT();
            DISPLAY display = new DISPLAY();
            
            while(true)
            {
                int playAbleObject = display.game_menu();
                if (playAbleObject == 0)
                {
                    settings.clear_screen();
                    gameplay.hangman_display();
                    if (SETTINGS.settingMode != 0) { gameplay.start_playing(); }
                } 
                else if(playAbleObject == 1) { movement.menu_select_movement(); }
                else if(playAbleObject == 2)
                {
                    settings.clear_screen();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\n\n\t\t\t\t   Q  U  I  T\n\n\n\n");
                    break;
                }
                settings.clear_screen();
            }
        }
    }
}
