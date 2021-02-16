using System;
using System.IO;
using System.Net;
using System.Threading;


namespace RegShoFinraDailyShorts
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu() 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter the # value of your selction.");
                Console.WriteLine("\t1. Download Failure To Deliver aggregate data.");
                Console.WriteLine("\t2. Download Daily Shorts aggregate data.");
                Console.WriteLine("\tX. Exit.");
                Console.WriteLine("\n");
                /*
                Console.WriteLine("3. ");
                Console.WriteLine("4. ");
                Console.WriteLine("5. ");
                Console.WriteLine("6. ");
                Console.WriteLine("7. ");
                Console.WriteLine("8. ");
                Console.WriteLine("9. ");
                */


                switch (Console.ReadLine()) { 
                    case "1":
                        SECFTD FTDs = new SECFTD(DateTime.Now, new DateTime(1970, 01, 01), @"D:\Stocks\SEC\FTD");
                        FTDs.Pull();
                        break;

                    case "2":
                        RegShoFinraDailyShorts DailyShorts = new RegShoFinraDailyShorts(DateTime.Now, new DateTime(1970, 01, 01), @"D:\Stocks\RegShoFinra\DailyShorts");
                        DailyShorts.Pull();
                        break;

                    case "3": break;
                    case "X": 
                    case "x":
                        Environment.Exit(0);
                        break;
                }


            }
        
        }
    }
}
