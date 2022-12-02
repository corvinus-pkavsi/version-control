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
using week_09.Entities;

namespace week_09
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbabilities> birthProbabilities = new List<BirthProbabilities>();
        List<DeathProbabilities> deathProbabilities = new List<DeathProbabilities>();

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            birthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            deathProbabilities = GetDeathPropabilities(@"C:\Temp\halál.csv");

        }

        public List<Person> GetPopulation(string csvpath) 
        {
            List<Person> population = new List<Person>();

            using(StreamReader sr = new StreamReader(csvpath, Encoding.Default)) 
            {
                while (!sr.EndOfStream) 
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }


            }

            return population;
        }

        public List<BirthProbabilities> GetBirthProbabilities(string csvpath) 
        {
            List<BirthProbabilities> birthProbabilities = new List<BirthProbabilities>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new BirthProbabilities()
                    {
                        Age = int.Parse(line[0]),
                        NmbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])

                    });

                }
            }
            return birthProbabilities;
        }
        public List<DeathProbabilities> GetDeathPropabilities(string csvpath)
        {
            List<DeathProbabilities> deathProbabilities = new List<DeathProbabilities>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbabilities()
                    {
                        Age = int.Parse(line[1]),
                        Gender = (Gender)Enum.Parse(typeof(Gender),line[0]),
                        P = double.Parse(line[2])

                    });

                }
            }
            return deathProbabilities;
        }
    }
}
