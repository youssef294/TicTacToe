using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        /// <summary>
        /// Holds the current results of cell in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is a player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool MgameEnded;
        #endregion


        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }



        #endregion

        #region Events
        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure player 1 starts the game
            mPlayer1Turn = true;

            // Interate every button on the grid...
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;           
                button.Foreground = Brushes.Red;
            });

            // Make sure the game hasn't finished
            MgameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished.
            if (MgameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set the button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Green;

            // Toggle the players turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        #endregion

        #region Winnners check
        /// <summary>
        /// Checks if there is a winner of 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horinzontal Wins
            // Check for horizontal wins
            // - Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
                       
            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            
            // - Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            // - Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical wins
            // - Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            // - Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            // - Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins
            // Check for vertical wins
            // Top Left Bottom Right
            // - Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            // Top Right Bottom Left
            // - Column 0
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])

            {
                // Game ends
                MgameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No winners


            // Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game ended
                MgameEnded = true;

                // Turn all cells black
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
            #endregion
        }
    }
}
