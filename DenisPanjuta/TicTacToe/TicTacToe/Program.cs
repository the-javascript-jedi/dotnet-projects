using System;
using System.Text;

namespace TicTacToe
{
    class Program
    {
        //the playfield
        static char[,] playField ={
            { '1','2','3'},
            { '4','5','6'},
            { '7','8','9'},
        };
        static void Main(string[] args)
        {
            int player = 2;
            int input = 0;
            bool inputCorrect = true;          
            //Console.ReadKey();
            do
            {
              
                //check whch player turn it is to play
                if (player == 2)
                {
                    player = 1;                    
                    EnterXorO(player, input);
                }
                else if(player == 1)
                {
                    player = 2;
                    EnterXorO(player, input);
                }
                //Create the tic tac toe fields
                SetField();
                //Enter user input
                do
                {
                    Console.Write("\nPlayer {0}: Choose your field!",player);
                    input = Convert.ToInt32(Console.ReadLine());
                }
                //as long as input is incorrect we need to try again
                while (!inputCorrect);
            } while (true);
        }

        public static void SetField()
        {
            Console.Clear();
            Console.WriteLine("     |      |     ");
            //TODO replace numbers with variables
            Console.WriteLine("  {0}  |   {1}  |  {2}", playField[0, 0], playField[0, 1], playField[0, 2]);
            Console.WriteLine("_____|______|_____");
            Console.WriteLine("     |      |     ");
            //TODO replace numbers with variables
            Console.WriteLine("  {0}  |   {1}  |  {2}", playField[1, 0], playField[1, 1], playField[1, 2]);
            Console.WriteLine("_____|______|_____");
            Console.WriteLine("     |      |     ");
            //TODO replace numbers with variables
            Console.WriteLine("  {0}  |   {1}  |  {2}", playField[2, 0], playField[2, 1], playField[2, 2]);
            Console.WriteLine("_____|______|_____");
            Console.WriteLine("     |      |     ");
            Console.WriteLine();
        }
        //create input
        public static void EnterXorO(int player,int input)
        {
            char playerSign = ' ';
            if (player == 1)
            {
                playerSign = 'X';
            }
            else
            {
                playerSign = 'O';
            }
            switch (input)
            {
                case 1: playField[0, 0] = playerSign; break;
                case 2: playField[0, 1] = playerSign; break;
                case 3: playField[0, 2] = playerSign; break;
                case 4: playField[1, 0] = playerSign; break;
                case 5: playField[1, 1] = playerSign; break;
                case 6: playField[1, 2] = playerSign; break;
                case 7: playField[2, 0] = playerSign; break;
                case 8: playField[2, 1] = playerSign; break;
                case 9: playField[2, 2] = playerSign; break;
            }
        }
    }
}
