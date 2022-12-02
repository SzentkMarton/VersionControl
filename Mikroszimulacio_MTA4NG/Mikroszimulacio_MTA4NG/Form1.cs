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
        Random rng = new Random(1234);
        List<int> MalePopulation = new List<int>();
        List<int> FemalePopulation = new List<int>();



        public Form1()
        {
            InitializeComponent();
            
        }
        public void Simulation()
        {
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BP = GetBP(@"C:\Temp\születés.csv");
            DP = GetDP(@"C:\Temp\halál.csv");

            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                for (int i = 0; i < Population.Count(); i++)
                {
                    SimStep(year, Population[i]);
                }
                int numMales = (from x in Population
                                where x.Gender == Gender.Male && x.IsAlive
                                select x).Count();
                int numFemales = (from x in Population
                                  where x.Gender == Gender.Female && x.IsAlive
                                  select x).Count();
                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, numMales, numFemales));

            }
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

        private void SimStep(int year, Person person)
        {
            if (!person.IsAlive) return;
            byte age = (byte)(year - person.BirthYear);
            double pDeath = (from x in DP
                             where x.gender == person.Gender && x.Age == age
                             select x.ProbOfDeath).FirstOrDefault();
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                double pBirth = (from x in BP
                                 where x.Age == age
                                 select x.ProbOfBirth).FirstOrDefault();
                if (rng.NextDouble() <= pBirth)
                {
                    Person newborn = new Person();
                    newborn.BirthYear = year;
                    newborn.NumberOfChildren = 0;
                    newborn.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(newborn);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "C:/Temp";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            {
                textBox1.Text = ofd.FileName;
            };




        }
    }
}
