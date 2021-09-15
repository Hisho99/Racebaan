using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Controller;


namespace Racebaan
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Data.Initialize();
            Data.NextRace();
            Visualization.DrawTrack(Data.CurrentRace.Track);
            
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }

    public static class Visualization
    {


        
        public static void Initialize()
        {
            TrackLoader t = new TrackLoader();
            t.AssembleTrack(Data.CurrentRace.Track);
            DrawTrack(Data.CurrentRace.Track);

        }

        


        public static void DrawTrack(Model.Track track)
        {
            Console.BackgroundColor = ConsoleColor.Green;


          
            
            foreach (Model.Section s in track.Sections)
            {


            }


        }


    }
}

