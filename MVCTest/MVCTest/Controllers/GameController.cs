using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;
using Tictactoe;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MVCTest.Controllers
{
    public class GameController : Controller
    {
        public IActionResult ResetGame()
        {
            if (HttpContext.Session.Keys.Contains("state"))
            {
                HttpContext.Session.Remove("state");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("state")))
            {
                GameModel model = new GameModel();
                string[,] board = new string[3, 3] { { "7", "4", "1" }, { "8", "5", "2" }, { "9", "6", "3" } };
                model.Board = board;
                HttpContext.Session.SetString("state", JsonConvert.SerializeObject(model));
                return View(model);
            }
            else
            {
                var model = JsonConvert.DeserializeObject<GameModel>(HttpContext.Session.GetString("state"));
                return View(model);
            }

        }
        [HttpGet]
        public ActionResult Move(string choice)
        {
            GameModel model = JsonConvert.DeserializeObject<GameModel>(HttpContext.Session.GetString("state"));
            string[,] board = model.Board;

            if (model.Turns <= 9)
            {
                for (int i = 0; i < board.GetLength(0); i++) // Checks coincidence through the X axis
                {
                    for (int j = 0; j < board.GetLength(1); j++) // Checks coincidence through the Y axis
                    {
                        if (board[i, j] == choice) // When coincidence is found replaces the initial value for the player´s token
                        {
                            if (model.Turns % 2 == 0)
                            {
                                board[i, j] = model.Player1Token;
                                model.Turns++;
                                model.ActionsAfterMove(model.Turns, model.Board);
                            }                            
                        }
                    }
                }
            }

            

            if (model.Turns < 9)
            {
                bool taken; //initially "False"
                Random random = new Random();
                bool IsTaken(int x, int y)
                {
                    if (board[x, y] != "X" && board[x, y] != "O")
                    {
                        return false;
                    }
                    else return true;
                }


                do
                {
                    // These variables store two random values, one for each axis
                    int x = random.Next(board.GetLength(0));
                    int y = random.Next(board.GetLength(1));

                    taken = IsTaken(x, y); // Checks if the position is available

                    if (!taken)
                    {
                        if (model.Turns % 2 != 0)
                        {
                            board[x, y] = model.Player2Token;
                            model.Turns++;
                            model.ActionsAfterMove(model.Turns, model.Board);
                        }
                        
                    }

                } while (taken == true);

                model.Board = board;
                HttpContext.Session.SetString("state", JsonConvert.SerializeObject(model));
                return View("Index", model);

            }
            else
            {
                model.Board = board;
                HttpContext.Session.SetString("state", JsonConvert.SerializeObject(model));
                return View("Index", model);
            }
            
        }
    }
}

