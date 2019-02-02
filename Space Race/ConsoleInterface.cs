using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {      
             DisplayIntroductionMessage();
            /*                    
             Set up the board in Board class (Board.SetUpBoard)
             Determine number of players - initally play with 2 for testing purposes 
             Create the required players in Game Logic class
              and initialize players for start of a game             
             loop  until game is finished           
                call PlayGame in Game Logic class to play one round
                Output each player's details at end of round
             end loop
             Determine if anyone has won
             Output each player's details at end of the game
           */
            Board.SetUpBoard();
            HowManyPlayers();
            SpaceRaceGame.SetUpPlayers();
            while (!GameOver())
            {
                PressEnterForRound();
                SpaceRaceGame.PlayOneRound();
                playerstats();
            }
                       
            PressEnter();

        }//end Main

   
        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void DisplayIntroductionMessage()
        {
            Console.WriteLine("\tWelcome to Space Race.\n");
            Console.WriteLine("\n");
            Console.WriteLine("\tThis game is for 2 to 6 players.");
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        static void PressEnter()
        {
            Console.Write("\nPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny

        //prompt the user to enter how many players
        static void HowManyPlayers()
        {         
            int userInput = 0;
            Console.WriteLine("\tHow Many Players? (2-6): \n");

            //whilst user is entering incorrect input, display error and prompt again
            while (true)
            {
                
                string input = Console.ReadLine();
                if(!int.TryParse(input, out userInput))
                {
                    Console.WriteLine("Error: invalid input. Must be number.");
                    Console.WriteLine("\tHow Many Players? (2-6): \n");
                }
                else if( userInput < 2 || userInput > 6)
                {
                    Console.WriteLine("Error: invalid number of players entered.");
                    Console.WriteLine("\tHow Many Players? (2-6): \n");
                }
                else
                {
                    break;
                }
            }
            //user input will now be the number of players in the game
            SpaceRaceGame.NumberOfPlayers = userInput;
        }

        //press enter to start a new round
        static void PressEnterForRound()
        {
            Console.Write("\nPress Enter to play a round...\n");
            Console.ReadLine();
        } // end PressAny

        //display player stats at end of each round
        static void playerstats()
        {
            Console.WriteLine("\tNext Round \n");
            for(int i =0; i < SpaceRaceGame.Players.Count; i++)
            {
                if (SpaceRaceGame.Players[i].HasPower)
                {
                    Console.Write("\t" + SpaceRaceGame.Players[i].Name + " on square " + SpaceRaceGame.Players[i].Position);
                    Console.Write(" with " + SpaceRaceGame.Players[i].RocketFuel + " yottawatt of power remaining \n");
                }
                else
                {
                    Console.Write("\t" + SpaceRaceGame.Players[i].Name + " has ran out of yottawatts !\n");
                }

            }
        }

        //display which players have finished
        static void Finished()
        {
            //display who has finished
            Console.WriteLine("\n \t The following player(s) finished the game\n");

            for(int i = 0; i < SpaceRaceGame.Players.Count; i++)
            {
                if(SpaceRaceGame.Players[i].AtFinish)
                {
                    Console.WriteLine("\t\t" + SpaceRaceGame.Players[i].Name + "\n");
                }
            }

            //display stats of all players at end game
            Console.WriteLine("\t Individual players finished with the below \n");

            for (int i = 0; i < SpaceRaceGame.Players.Count; i++)
            {
                Console.Write("\t\t" + SpaceRaceGame.Players[i].Name + " with ");
                Console.Write(SpaceRaceGame.Players[i].RocketFuel + " yattowatt of power at square ");
                Console.WriteLine(SpaceRaceGame.Players[i].Position + "\n" );
            }
        }

        //check if player wants to play again
        static bool PlayAgain()
        {
            Console.Write("Play Again? (Y or N): ");
            string userInput= Console.ReadLine();
            if(userInput == "Y" || userInput == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //check if any players have reached the end
        static bool GameOver()
        {
            for(int i=0; i < SpaceRaceGame.Players.Count; i++)
            {
                if (SpaceRaceGame.Players[i].AtFinish == true) {
                    Finished();
                    if (PlayAgain())
                    {
                        SpaceRaceGame.Players.Clear();
                        HowManyPlayers();
                        SpaceRaceGame.SetUpPlayers();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }



    }//end Console class
}
