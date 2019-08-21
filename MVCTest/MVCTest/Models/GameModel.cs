using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models
{
    public class GameModel
    {
        public int Turns { get; set; }
        public string[,] Board { get; set; }
        public string Player1Token { get; set; } 
        public string Player2Token { get; set; }
        public string winner { get; set; }


        //public void MakeMove(int Move)
        //{
        //    //Logica juego original (Crear player, check win)
        //}


        public GameModel()
        {
            Turns = 0;
            Player1Token = "X";
            Player2Token = "O";
        }

        // This function compares the moves made with all the possible winning conditions
        public bool IsThereAWinner(string[,] token)
        {            
            // Horizontal Winning Conditions
            if (token[0, 0] == token[1, 0] && token[2, 0] == token[0, 0])
            {
                winner = token[0, 0];
            }

            if (token[0, 1] == token[1, 1] && token[2, 1] == token[0, 1])
            {
                winner = token[0, 1];
            }

            if (token[0, 2] == token[1, 2] && token[2, 2] == token[0, 2])
            {
                winner = token[0, 2];
            }

            // Vertical Winning Conditions

            if (token[0, 0] == token[0, 1] && token[0, 2] == token[0, 0])
            {
                winner = token[0, 0];
            }

            if (token[1, 0] == token[1, 1] && token[1, 2] == token[1, 0])
            {
                winner = token[1, 0];
            }

            if (token[2, 0] == token[2, 1] && token[2, 2] == token[2, 0])
            {
                winner = token[2, 0];
            }

            // Diagonally Winning Conditions

            if (token[0, 0] == token[1, 1] && token[2, 2] == token[0, 0])
            {
                winner = token[0, 0];
            }

            if (token[2, 0] == token[1, 1] && token[0, 2] == token[2, 0])
            {
                winner = token[2, 0];
            }

            if (winner != null)
            {
                Console.WriteLine("Winner: " + winner);
                return true;//return en lugar de console. usar variable Winner desde el modelo
            }
            else
            {
                return false;
            }
        }

        public static bool wasTie = false;
        public static bool wasWinner = false;
        public  void ActionsAfterMove(int turns, string[,] board)
        {                        
            if (IsThereAWinner(board))
            {
                wasWinner = true;
                EndGame(board);
            }
            else if (turns == 9)
            {
                //Console.WriteLine("Tie!");
                wasTie = true;
            }
        }

        private static void EndGame(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++) // Checks coincidence through the X axis
            {
                for (int j = 0; j < board.GetLength(1); j++) // Checks coincidence through the Y axis
                {
                    if (board[i, j] != "X" && board[i, j] != "O")
                    {
                        board[i, j] = "";
                    }
                }
            }
        }
    }
}

//int turns = 0;

//Player player1 = new Player();
//Player player2 = new Player();
//public GameController()
//{
//    player1.Token = char.Parse("X");
//    player2.Token = char.Parse("O");
//}
//public ActionResult Index()
//{
//    string[,] board = new string[3, 3] { { "7", "4", "1" }, { "8", "5", "2" }, { "9", "6", "3" } };
//    GameModel Model = new GameModel();
//    Model.Board = board;

//    return View(Model);
//}

//public IActionResult PlaceTokenP1()
//{
//    HttpContext.Session.SetString("X", "X");
//    return View();
//}

//public IActionResult PlaceTokenP2()
//{
//    HttpContext.Session.SetString("O", "O");
//    return View();
//}

//public ActionResult Move(string choice)
//{
//    GameModel Model = new GameModel();
//    if (Tictactoe.console.Program.CheckValidMove(player1, board, choice))
//    {
//        Tictactoe.console.Program.ActionsAfterMove(ref turns, ref board);
//        Model.Board = board;
//    }

//    else
//        ViewBag["InvalidMove"] = "Invalid move.";
//    return View("Index", Model);
//}


