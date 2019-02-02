using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();

            Board.SetUpBoard();
            ResizeGUIGameBoard();
            SetUpGUIGameBoard();
            SetupPlayersDataGridView();
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            PrepareToPlay();
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        //  ******************* Uncomment - Remove Block Comment below
        //                         once you've added the TableLayoutPanel to your form.
        //
        //       You will have to replace "tableLayoutPanel" by whatever (Name) you used.
        //
        //        Likewise with "playerDataGridView" by your DataGridView (Name)
        //  ******************************************************************************************


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard

        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {
            // Code needs to be added here to do the mapping
           
            screenRow = (NUM_OF_ROWS-1) - (squareNum / (NUM_OF_COLUMNS));

            //if the row is an ascending row
            if (screenRow % 2 == 0 || screenRow == 0)
            {
                int smallestNumber = 48 - (NUM_OF_COLUMNS * screenRow);
                screenCol = squareNum - smallestNumber;
            }
            else
            //if the row is a descending row
            {
                int smallestNumber = 48 - (NUM_OF_COLUMNS * screenRow);
                screenCol = (NUM_OF_COLUMNS - 1) - (squareNum - smallestNumber);

            }

        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            // Store the SelectedItem property of the ComboBox in a string
            string Players = this.numOfPlayers.SelectedItem.ToString();

            // Parse string to a number
            int numberOfPlayers;
            int.TryParse(Players, out numberOfPlayers);
            // Set the NumberOfPlayers in the SpaceRaceGame class to that number
            SpaceRaceGame.NumberOfPlayers = numberOfPlayers;

        }//end DetermineNumberOfPlayers

        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlay()
        {
            // More code will be needed here to deal with restarting 
            // a game after the Reset button has been clicked. 
            //
            // Leave this method with the single statement 
            // until you can play a game through to the finish square
            // and you want to implement the Reset button event handler.
            //

            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);

        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)
        {
            int screenRow;
            int screenCol;

            // Uncomment the following lines once you've added the tableLayoutPanel to your form. 
            //     and delete the "return null;" 
            //
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);
                       
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            // Code needs to be added here.
            int squareNum = SpaceRaceGame.Players[playerNumber].Position;
            //     delete the "return -1;" once body of method has been written 
            return squareNum;
        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
            // Uncomment the following line once you've added the tableLayoutPanel to your form.
                  tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                //       determine the square number of the player
                int playerSquareNum = GetSquareNumberOfPlayer(i);
                //       retrieve the SquareControl object with that square number
                SquareControl square = SquareControlAt(playerSquareNum);

                //       using the typeOfGuiUpdate, update the appropriate element of 
                //          the ContainsPlayers array of the SquareControl object.
                //          
                if(typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
                {
                    square.ContainsPlayers[i] = true;
                }   
                else if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer)
                {
                    square.ContainsPlayers[i] = false;
                }

            }
            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        //---BUTTON PROGRAMMING BELOW--//

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void diceButton_Click(object sender, EventArgs e)
        {
            //disable exit button until round finished, make player names uneditable, disable numOfPlayers combo box
            exitButton.Enabled = false;
            playersDataGridView.Columns[1].ReadOnly = true;
            numOfPlayers.Enabled = false;

            //remove players from current gui location, play a round and add them to their new locations
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            SpaceRaceGame.PlayOneRound();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();

            //enable reset button after a round is played
            resetButton.Enabled = true;

            //if any players have reached the end, display message of who won, disable the dice button and enable exit 
            for(int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                if (SquareControlAt(55).ContainsPlayers[i] == true)
                {
                    string message = "";

                    for (int player = 0; player < SquareControlAt(55).ContainsPlayers.Length; player++)
                    {

                        if(SquareControlAt(55).ContainsPlayers[player] == true)
                        {
                            message += "\t" + SpaceRaceGame.Players[player].Name + "\n";
                        }

                    }
                    MessageBox.Show("The following have won the game:\n" + message);

                    diceButton.Enabled = false;
                    exitButton.Enabled = true;
                    
                }                
            }
        
        }

        private void resetButton_Click(object sender, EventArgs e)
        {

            //reset players
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            for(int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                SpaceRaceGame.Players[i].Location = Board.Squares[0];
                SpaceRaceGame.Players[i].Position = Board.Squares[0].Number;
                SpaceRaceGame.Players[i].RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                SpaceRaceGame.Players[i].HasPower = true;
                SpaceRaceGame.Players[i].AtFinish = false;

            }
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();
            //reset buttons
            resetButton.Enabled = false;
            diceButton.Enabled = false;
            playersDataGridView.Columns[1].ReadOnly = false;
            numOfPlayers.Enabled = true;
            groupBox.Enabled = true;
            

        }

        private void numOfPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetermineNumberOfPlayers();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            diceButton.Enabled = true;
            groupBox.Enabled = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            diceButton.Enabled = true;
            groupBox.Enabled = false;
        }
    }// end class
}
