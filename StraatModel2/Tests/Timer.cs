using Labo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StraatModel2.Tests
{
    class Timer
    {
        public Timer()
        {
            //--------------------stopwatch-------------------------------------------------
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //List<Straat> straten = Factories.StraatFactory();
            //List<Gemeente> gemeentes = Factories.GemeenteFactory();
            List<Provincie> provincies = Factories.ProvincieFactory(@"C:\Users\Biebem\Downloads"); 
            stopWatch.Stop();
            //Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadLine();
            //-----------stopwatch--------------------------------------------------------
        }
    }
}
