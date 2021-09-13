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

        private static string[] _startVertical =
        {

            "|    |",
            "|    |",
            "|  - |",
            "| -  |",
            "|    |",
            "|    |"
        };

        private static string[] _startHorizontal =
        {
            "------",
            "      ",
            "  |   ",
            "	|  ",
            "      ",
            "------"
        };

        private static string[] _blank =
        {
            "      ",
            "      ",
            "      ",
            "      ",
            "      ",
            "      "
        };


        private static List<string[]> Leftcorners;
        private static List<string[]> Rightcorners;
        private static List<string[]> Straights;
        private static List<string[]> Finishes;
        private static List<string[]> Startgrids;


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

        public static void MakeFinishList()
        {
            Finishes = new List<string[]>();
            Finishes.Add(_finishHorizontal);
            Finishes.Add(_finishVertical);
        }

        public static void MakeStartList()
        {
            Startgrids.Add(_startVertical);
            Startgrids.Add(_startHorizontal);
        }



        #endregion

        private enum Bearings
        {
            North,
            East,
            South,
            West
        };

        // Multi-dimentional array for Visualation of racetrack
        private static string[][] AssebledTrack;
        public static void Initialize()
        {
            MakeLeftCornerList();
            MakeRightCornerList();
            MakeStraightsList();
            MakeFinishList();
            MakeStartList();
            
            DrawTrack(Data.CurrentRace.Track);

        }

        public static string[][] IncreaseDownLimitArray(string[][] s)
        {
            return s;
        }

        public static void DrawTrack(Model.Track track)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            int Bearing = 1;
            int[] Coördinates = new int[2] {0, 0};

            Bearings b = Bearings.North;

            foreach (Model.Section s in track.Sections)
            {
                //Establish current Bearings 
                switch (Bearing)
                {
                    case 1:
                    {
                        b = Bearings.North;

                        break;
                    }
                    case 2:
                    {
                        b = Bearings.East;

                        break;
                    }
                    case 3:
                    {
                        b = Bearings.South;

                        break;
                    }
                    case 4:
                    {
                        b = Bearings.West;

                        break;
                    }
                }

                //place sections into string[][] array based on sectiontype, bearings and coordinates
                switch (s.SectionType)
                {
                    case Model.SectionTypes.StartGrid:
                    {
                     

                        break;
                    }
                    case Model.SectionTypes.Straight:
                    {

                        break;
                    }
                    case Model.SectionTypes.LeftCorner:
                    {

                        break;
                    }
                    case Model.SectionTypes.RightCorner:
                    {

                        break;
                    }
                    case Model.SectionTypes.Finish:
                    {

                        break;
                    }

                    default: break;


                }
            }


        }


    }
}

