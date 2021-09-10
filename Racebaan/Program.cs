using System;
using System.Collections.Generic;
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
        #region graphics

        private static string[] _leftCornerUp =
        {
            "---\\  ",
            "    \\ ",
            "     \\",
            "     |",
            "     |",
            " 	  |"
        };
        private static string[] _rightCornerUp =
        {
            "  /---",
            " /    ",
            "/     ",
            "|     ",
            "|     ",
            "|     "

        };
        private static string[] _rightCornerDown =
        {
            "|     ",
            "|     ",
            "|     ",
            "\\     ",
            " \\    ",
            "  \\---"

        };
        private static string[] _leftCornerDown =
        {
            "     |",
            "     |",
            "     |",
            "	  /",
            "    /",
            "---/ "
        };
        private static string[] _straightVertical =
        {
            "|    |",
            "|    |",
            "|    |",
            "|    |",
            "|    |",
            "|    |"
        };
        private static string[] _straightHorizontal =
        {
            "------",
            "      ",
            "      ",
            "	   ",
            "	   ",
            "------"
        };
        private static string[] _finishHorizontal =
        {
            "|    |",
            "|    |",
            "|#  #|",
            "|#  #|",
            "|    |",
            "|    |"
        };
        private static string[] _finishVertical =
        {
            "------",
            "  ##  ",
            "      ",
            "	   ",
            "  ##  ",
            "------"
        };
        private static string[] _finishLeftCornerUp =
        {
            "---\\  ",
            "    \\ ",
            "    #\\",
            "     |",
            "#	  |",
            " 	  |"
        };
        private static string[] _finishRightCornerUp =
        {
            "  /---",
            " /    ",
            "/#    ",
            "|     ",
            "|    #",
            "|     "

        };
        private static string[] _finishRightCornerDown =
        {
            "|     ",
            "|    #",
            "|     ",
            "\\     ",
            " \\#   ",
            "  \\---"
        };
        private static string[] _finishLeftCornerDown =
        {
            "     |",
            "#    |",
            "     |",
            "	  /",
            "   #/ ",
            "---/  "
        };


        private static List<string[]> Leftcorners;
        private static List<string[]> Rightcorners;
        private static List<string[]> Straights;
        private static int Bearing;

        //makes lists for trackmaking used in DrawTrack
        public static void MakeLeftCornerList()
        {
            Leftcorners = new List<string[]>();
            Leftcorners.Add(_leftCornerUp);
            Leftcorners.Add(_rightCornerUp);
            Leftcorners.Add(_rightCornerDown);
            Leftcorners.Add(_leftCornerDown);

          
        }

        public static void MakeRightCornerList()
        {
            Rightcorners = new List<string[]>();
            Rightcorners.Add(_rightCornerUp);
            Rightcorners.Add(_leftCornerUp);
            Rightcorners.Add(_leftCornerDown);
            Rightcorners.Add(_rightCornerDown);
        }

        public static void MakeStraightsList()
        {
            Straights = new List<string[]>();
            Straights.Add(_straightVertical);
            Straights.Add(_straightHorizontal);
        }

            
        
            #endregion
        public static void Initialize()
        {
            MakeLeftCornerList();
            MakeRightCornerList();
            MakeStraightsList();
            
            DrawTrack(Data.CurrentRace.Track);

        }

        public static void DrawTrack(Model.Track track)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Bearing = 1;

            foreach (Model.Section s in track.Sections)
            {
                
            }


        }


    }
}

