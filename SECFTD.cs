using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;

namespace RegShoFinraDailyShorts
{
    class SECFTD
    {
        //https://www.sec.gov/files/data/fails-deliver-data/cnsfails202101b.zip



        /***************************************************************
        * 
        *      SEC Failure to Deliver Data
        * 
        ****************************************************************/
        public string website = new string("https://www.sec.gov/files/data/fails-deliver-data/");
        public string filenamePart1 = new string("cnsfails");
        //FP2 is the date in YYYYMM(a|b) format
        public string filenamePart3 = new string(".zip");
        public DateTime StartDate = DateTime.Now;
        public DateTime EndDate = new DateTime(1970, 01, 01);

        /***************************************************************
         * 
         *      Local Directory Data
         * 
         ****************************************************************/

        public string localDirectoryRoot = @"D:\Stocks\SEC\FTD\";

        /***************************************************************
         * 
         *      Concatenated String
         * 
         ****************************************************************/

        public string url = new string("");
        public string destination = new string("");

        /***************************************************************
         * 
         *      404 Error Handling
         *      
         *      The fail count needs to occur for ~two weeks straight before it stops.
         *      Stock Exchange is closed on weekends (see above) and holidays
         * 
         ****************************************************************/

        public bool ResultsNot404 = true;
        public int ResultsFailCount = 0;

        public SECFTD(DateTime Start, DateTime End, string Dir)
        {
            destination = Dir;
            StartDate = Start;
            EndDate = End;
        }

        public void Pull()
        {
            bool FirstHalfMonth = true;
            string filenamePart2 = new string("");

            while (ResultsNot404 && StartDate >= EndDate)
            {
                if (FirstHalfMonth)
                {
                    filenamePart2 = StartDate.ToString("yyyyMM") + "a";
                }
                else 
                {
                    filenamePart2 = StartDate.ToString("yyyyMM") + "b";
                }

                url = website + filenamePart1 + filenamePart2 + filenamePart3;
                destination = localDirectoryRoot + filenamePart1 + filenamePart2 + filenamePart3;

                
                
                Console.WriteLine("Pulling down file: {0}", url);

                using (WebClient client = new WebClient())
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
                            if (ResultsFailCount >= 5)
                            {
                                ResultsNot404 = false;
                            }
                        }
                    }
                }

                StartDate = StartDate.AddMonths(-1);

                //Do not abuse those that give us the snackies.
                Thread.Sleep(50);
            }

            Console.WriteLine("Loop ended. {0} failures occurred ending on {1}", ResultsFailCount, StartDate);
        }
    }
}
