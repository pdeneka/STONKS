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
            /***************************************************************
             * 
             *      FINRA Short File Data
             * 
             ****************************************************************/
            string website = new string("http://regsho.finra.org/");
            string filenamePart1 = new string("CNMSshvol");
            //FP2 is the date in YYYYMMDD format
            string filenamePart3 = new string(".txt");
            DateTime StartDate = DateTime.Now;

            /***************************************************************
             * 
             *      Local Directory Data
             * 
             ****************************************************************/

            string localDirectoryRoot = @"C:\Your\Path\Here\";

            /***************************************************************
             * 
             *      Concatenated String
             * 
             ****************************************************************/

            string url = new string("");
            string destination = new string("");

            /***************************************************************
             * 
             *      404 Error Handling
             *      
             *      The fail count needs to occur for ~two weeks straight before it stops.
             *      Stock Exchange is closed on weekends (see above) and holidays
             * 
             ****************************************************************/

            bool ResultsNot404 = true;
            int ResultsFailCount = 0;

            while (ResultsNot404)
            {
                url = website + filenamePart1 + StartDate.ToString("yyyyMMdd") + filenamePart3;
                destination = localDirectoryRoot + filenamePart1 + StartDate.ToString("yyyyMMdd") + filenamePart3;

                Console.WriteLine("Pulling down file: {0}", url);

                using (WebClient client = new WebClient())
                {
                    if ((int)StartDate.DayOfWeek > 0 && (int)StartDate.DayOfWeek < 6) // Sunday & Saturday respectively
                    {
                        try
                        {
                            client.DownloadFile(url, destination);
                            Console.WriteLine("\tSuccess! Downloading file {0}", url);
                            ResultsFailCount = 0;
                        }
                        catch (WebException wex)
                        {
                            if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                            {
                                Console.WriteLine("Unable to download {0}.  404 Not found.", url);

                                ResultsFailCount++;
                                if (ResultsFailCount >= 10)
                                {
                                    ResultsNot404 = false;
                                }
                            }
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Date {0} falls on weekend.", StartDate.ToString("yyyyMMdd"));
                    }
                }
                StartDate = StartDate.AddDays(-1);

                //Do not abuse hosts
                Thread.Sleep(50);
            }

            Console.WriteLine("Loop ended. {0} failures occurred ending on {1}", ResultsFailCount, StartDate);
        }
    }
}
