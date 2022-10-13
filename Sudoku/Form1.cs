using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        private Random rng = new Random();
        private Sudoku _current = new Sudoku();

        List<Sudoku> _sudokus = new List<Sudoku>();
        public Form1()
        {
            InitializeComponent();
            CreatePlayField();
            LoadSudokus();
            
            NewGame();
        }
        private void CreatePlayField()
        {
            int linewidth = 5;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuFields sf = new SudokuFields();
                    sf.Left = j * sf.Width + (int)(Math.Floor((double)(j / 3))) * linewidth;
                    sf.Top = i * sf.Height + (int)(Math.Floor((double)(i / 3))) * linewidth;
                    panel1.Controls.Add(sf);
                }
            }
        }
        private void LoadSudokus()
        {
            _sudokus.Clear();
            using (StreamReader sr = new StreamReader("sudoku.csv", Encoding.Default))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    Sudoku s = new Sudoku();
                    s.Quiz = line[0];
                    s.Solution = line[1];
                    _sudokus.Add(s);
                }

            }
        }
        private Sudoku GetRandomQuiz()
        {
            int randomNumber = rng.Next(_sudokus.Count);
            return _sudokus[randomNumber];


        }
        private void NewGame()
        {
            _current = GetRandomQuiz();
            int counter = 0;

            foreach (var mezok in panel1.Controls.OfType<SudokuFields>())
            {
                mezok.Value = int.Parse(_current.Quiz[counter].ToString());

            }

        }
    }
}
