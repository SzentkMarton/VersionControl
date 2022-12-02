using Mikroszimulacio_MTA4NG.Entities;
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

namespace Mikroszimulacio_MTA4NG
{
    public partial class Form1 : Form

    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BP = new List<BirthProbability>();
        List<DeathProbability> DP = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BP = GetBP(@"C:\Temp\születés.csv");
            DP = GetDP(@"C:\Temp\halál.csv");
        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> pop = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    pop.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NumberOfChildren = int.Parse(line[2])

                    });


                }
            }
            return pop;

        }
        public List<BirthProbability> GetBP(string csvpath)
        {
            List<BirthProbability> bp = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    bp.Add(new BirthProbability()
                    {
                       Age=int.Parse(line[0]),
                       numOfChildren=int.Parse(line[1]),
                       ProbOfBirth=double.Parse(line[2])

                    });


                }
            }
            return bp;

        }
        public List<DeathProbability> GetDP(string csvpath)
        {
            List<DeathProbability> dp = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    dp.Add(new DeathProbability()
                    {
                        gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[0]),
                        ProbOfDeath = double.Parse(line[2])

                    });


                }
            }
            return dp;

        }
    }
}
