using System.Text.RegularExpressions;

namespace MSSAWordle
{

    /**Needs
     x A file that stores 100-1000 5 letter words
     * Ability to get a new word (random) to play the game
     * Ability to get the user input for the current row
     * Ability to compare to current word
     * Ability to display winning message or failing message
    */

    public partial class Form1 : Form
    {
        private Random rand = new Random();
        private int CurrentOffset = 1;
        private string CurrentWord = string.Empty;
        private const string WORD_FILE_PATH = @"C:\MGS\blgorman_github\IntroToProgramming\WordleClone\MSSAWordle\MSSAWordle\WordsForWordle.txt";
        private List<string> WordList = new List<string>();
        private List<TextBox> currentBoxes = new List<TextBox>();
        public Form1()
        {
            InitializeComponent();
            WordList = GetAllWords();
            StartNewGame();
        }

        private void StartNewGame()
        {
            //get a new word from word list [open file, get all words, choose one at random]
            CurrentWord = WordList[rand.Next(WordList.Count)];
            //reset offset
            CurrentOffset = 1;
            //enable submit
            btnSubmit.Enabled = true;
        }

        private List<string> GetAllWords()
        { 
            //open the file
            List <string> allWords = new List<string>();
            using (StreamReader reader = new StreamReader(WORD_FILE_PATH))
            {
                while (!reader.EndOfStream)
                {
                    //read it each line into the wordlist 
                    var nextLine = reader.ReadLine();
                    allWords.Add(nextLine);
                }
            }
            return allWords;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //1) Get the next user-entered word:
            var userWord = GetInput();
            
            //2) validate it:
            if (!ValidateInput(userWord))
            {
                //display error msg
                return;
            }

            //successful, check against current
            bool isCorrect = IsCorrectWord(userWord);

            //color each text box (gray, yellow, green)
            //gray if not in word
            //yellow if good char but in wrong place
            //green if good char and in correct place
            for (int i = 0; i < currentBoxes.Count(); i++)
            {
                ColorBox(i, currentBoxes[i]);
            }

            //If correct cool, we're done [disable submit]
            if (isCorrect)
            {
                EndGame();
                return;
            }

            //increment the offset (row)
            CurrentOffset++;
            if (CurrentOffset > 6)
            {
                MessageBox.Show("Sorry you didn't win this time!" +
                    "The correct word was: " + CurrentWord);
                btnSubmit.Enabled = false;
            }
        }

        private void ColorBox(int index, TextBox t)
        {
            //validate each index, 

            //if not in word -> gray
            if (!CurrentWord.Contains(t.Text, StringComparison.OrdinalIgnoreCase))
            {
                t.BackColor = Color.Gray;
            }
            //if in word but not at index -> yellow
            else if (CurrentWord[index].ToString().ToLower() != t.Text.ToLower())
            {
                t.BackColor = Color.Yellow;
            }
            else
            {
                //if in word and in index -> green
                t.BackColor = Color.LightGreen;
            }
        }

        private void EndGame()
        {
            //display message
            MessageBox.Show("Congratulations, you win!");
            //disable the submit Input
            btnSubmit.Enabled = false;
        }

        private string GetInput()
        {
            //userWord = textBox1.Text; //(possible method to collect input?)
 
            currentBoxes = new List<TextBox>();
          
            string tempString = String.Empty;
            //1) What line are we on?
            switch (CurrentOffset)
            {
                case 1:
                    tempString = textBox1.Text 
                            + textBox2.Text 
                            + textBox3.Text 
                            + textBox4.Text 
                            + textBox5.Text;
                    currentBoxes.Add(textBox1);
                    currentBoxes.Add(textBox2);
                    currentBoxes.Add(textBox3);
                    currentBoxes.Add(textBox4);
                    currentBoxes.Add(textBox5);
                    break;
                case 2:
                    tempString = textBox6.Text
                            + textBox7.Text
                            + textBox8.Text
                            + textBox9.Text
                            + textBox10.Text;
                    currentBoxes.Add(textBox6);
                    currentBoxes.Add(textBox7);
                    currentBoxes.Add(textBox8);
                    currentBoxes.Add(textBox9);
                    currentBoxes.Add(textBox10);
                    break;
                case 3:
                    tempString = textBox11.Text
                            + textBox12.Text
                            + textBox13.Text
                            + textBox14.Text
                            + textBox15.Text;
                    currentBoxes.Add(textBox11);
                    currentBoxes.Add(textBox12);
                    currentBoxes.Add(textBox13);
                    currentBoxes.Add(textBox14);
                    currentBoxes.Add(textBox15);
                    break;
                case 4:
                    tempString = textBox16.Text
                            + textBox17.Text
                            + textBox18.Text
                            + textBox19.Text
                            + textBox20.Text;
                    currentBoxes.Add(textBox16);
                    currentBoxes.Add(textBox17);
                    currentBoxes.Add(textBox18);
                    currentBoxes.Add(textBox19);
                    currentBoxes.Add(textBox20);
                    break;
                case 5:
                    tempString = textBox21.Text
                            + textBox22.Text
                            + textBox23.Text
                            + textBox24.Text
                            + textBox25.Text;
                    currentBoxes.Add(textBox21);
                    currentBoxes.Add(textBox22);
                    currentBoxes.Add(textBox23);
                    currentBoxes.Add(textBox24);
                    currentBoxes.Add(textBox25);
                    break;
                case 6:
                    tempString = textBox26.Text
                            + textBox27.Text
                            + textBox28.Text
                            + textBox29.Text
                            + textBox30.Text;
                    currentBoxes.Add(textBox26);
                    currentBoxes.Add(textBox27);
                    currentBoxes.Add(textBox28);
                    currentBoxes.Add(textBox29);
                    currentBoxes.Add(textBox30);
                    break;
            }
            //return the word from the User:
            return tempString;
        }

        private bool ValidateInput(string input)
        {            
            Regex rx = new Regex("^[a-zA-Z]+"); 

            if (input.Length == 5 && rx.IsMatch(input)) {

                return true;

            }
            //if not successful, 
            //display message (invalid/incomplete word)
            MessageBox.Show("Please enter a valid, five-letter word.");
            return false;
        }

        private bool IsCorrectWord(string attempt)
        {
            //return CurrentWord == wordTheyEntered
            if (CurrentWord.Equals(attempt, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            } 
            //if (attempt == CurrentWord) return true;
            return false;
        }

        private void btnNewWord_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void btnTestValues_Click(object sender, EventArgs e)
        {
            textBox1.Text = "A";
            textBox2.Text = "B";
            textBox3.Text = "C";
            textBox4.Text = "D";
            textBox5.Text = "E";
            textBox6.Text = "F";
            textBox7.Text = "G";
            textBox8.Text = "H";
            textBox9.Text = "I";
            textBox10.Text = "J";
            textBox11.Text = "K";
            textBox12.Text = "L";
            textBox13.Text = "M";
            textBox13.BackColor = Color.Yellow;
            textBox14.Text = "N";
            textBox15.Text = "O";
            textBox16.Text = "P";
            textBox17.Text = "Q";
            textBox18.Text = "R";
            textBox19.Text = "S";
            textBox20.Text = "T";
            textBox21.Text = "U";
            textBox22.Text = "V";
            textBox23.Text = "W";
            textBox24.Text = "X";
            textBox25.Text = "Y";
            textBox25.BackColor = Color.LightGreen;
            textBox26.Text = "Z";
            textBox27.Text = "0";
            textBox28.Text = "1";
            textBox29.Text = "2";
            textBox30.Text = "3";
        }

        private void btnCheat_Click(object sender, EventArgs e)
        {
            //CurrentWord = WordList[rand.Next(WordList.Count)];
            btnCheat.Text = CurrentWord;
        }
    }
}