using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generateRandom
{
    class Program
    {
        //random seed
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("The most straightforward way to generate a normal random");
            // call the simplest way to generate normal distribution
            double generateData = generateNormalDistribution();
            Console.WriteLine("The simplest algorithm to generate random is " + generateData);

            Console.WriteLine("===========================================================");
            Console.WriteLine("Genrate random using Box_Muller");

            List<double> results = boxMuller();
            Console.WriteLine("The first data is " + results[0]);
            Console.WriteLine("The second data is " + results[1]);

            Console.WriteLine("===========================================================");
            Console.WriteLine("Genrate random using jointly normal");
            Console.WriteLine("Please enter a number as correlation value between  -1 to 1");
            double rho = Convert.ToDouble(Console.ReadLine());
            if (rho <= 1 && rho >= -1)
            {
                results = jointlyNorm(results[0], results[1], rho);
                Console.WriteLine("The first jointly normal  is " + results[0]);
                Console.WriteLine("The second jointly normal  is " + results[1]);
                Console.ReadLine();
            }

            Console.WriteLine("===========================================================");
            Console.WriteLine("Genrate random using j polar rejection");
            results = polarRejection();
            Console.WriteLine("The polar rejection data is " + results[0]);
            Console.WriteLine("The polar rejection data is " + results[1]);



        }

        /// <summary>
        /// The most straightforward way to generate a normal random is the following algorithm
        /// </summary>
        /// <returns></returns>
        static double generateNormalDistribution()
        {
            double randomNumber = 0, generateData;
            double totalNumber = 0;
            // Add the twelve values together
            for (int j = 1; j <= 12; j++)
            {
                //Generate twelve uniform random numbers between 0 and 1
                randomNumber = random.NextDouble();
                totalNumber += randomNumber;

            }
            // Subtract six from the total
            generateData = totalNumber - 6;
            return generateData;
        }

        /// <summary>
        /// use Box-Muller method to generate normally distributed random variables.
        /// </summary>
        /// <returns></returns>
        static List<double> boxMuller()
        {
            //store the generate result
            List<double> results = new List<double>();
            // Generate two uniform random values between 0 and 1
            double rand1 = random.NextDouble();
            double rand2 = random.NextDouble();
            //Plug them into the two equations below to get two
            double z1 = Math.Sqrt(-2 * Math.Log(rand1)) * Math.Cos(2 * Math.PI * rand2);
            double z2 = Math.Sqrt(-2 * Math.Log(rand1)) * Math.Sin(2 * Math.PI * rand2);
            results.Add(z1);
            results.Add(z2);
            return results;
        }
        /// <summary>
        /// calculating normal random values is called the polar rejection method
        /// </summary>
        /// <returns></returns>
        static List<double> polarRejection()
        {
            //store the generate result
            List<double> results = new List<double>();

            double w, c, x1, x2;


            // If w > 1, repeat generating 2 uniform variables, if not, continue
            do
            {
                x1 = random.NextDouble();
                x2 = random.NextDouble();
                w = x1 * x1 + x2 * x2;

            } while (w > 1);

            c = Math.Sqrt(-2 * Math.Log(w) / w);

            double z1 = c * x1;
            double z2 = c * x2;

            results.Add(z1);
            results.Add(z2);
            return results;

        }

        /// <summary>
        /// <summary>
        /// A method that use 2 normally distributed random variables to generate 2 jointly normal random variables.
        /// </summary>
        /// <param name="rv1"></param> The 1st normally distributed random variable you want to use in this method.
        /// <param name="rv2"></param> The 2nd normally distributed random variable you want to use in this method.
        /// <param name="rho"></param> The correlation between 2 jointly normal random variables.
        /// <returns>The 2nd jointly normal random variable that will return.</returns>
        static List<double> jointlyNorm(double rv1, double rv2, double rho)
        {
            List<double> results = new List<double>();

            double num1 = rv1;
            double num2 = rho * rv1 + Math.Sqrt(1 - rho * rho) * rv2;
            results.Add(num1);
            results.Add(num2);
            return results;
        }
    }
}