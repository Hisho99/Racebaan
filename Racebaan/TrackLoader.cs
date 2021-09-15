using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controller;
using Microsoft.CSharp.RuntimeBinder;

namespace Racebaan
{
    public class TrackLoader
    {

        #region graphics

        //to shorten variable names _leftCornerNorthRightcornerEast > _lcnrce
        private static string[] _lcnRce =
        {
            "---\\  ",
            "    \\ ",
            "     \\",
            "     |",
            "     |",
            " 	  |"
        };

        private static string[] _lcwRcn =
        {
            "  /---",
            " /    ",
            "/     ",
            "|     ",
            "|     ",
            "|     "

        };

        private static string[] _lcsRcw =
        {
            "|     ",
            "|     ",
            "|     ",
            "\\     ",
            " \\    ",
            "  \\---"

        };

        private static string[] _lceRcs =
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

        public static string[] _startHorizontal =
        {
            "------",
            "      ",
            "  |   ",
            "	|  ",
            "      ",
            "------"
        };

        public static string[] _blank =
        {
            "      ",
            "      ",
            "      ",
            "      ",
            "      ",
            "      "
        };




        #endregion
        public List<string[]> Leftcorners;
        public List<string[]> Rightcorners;
        public List<string[]> Straights;
        public List<string[]> Finishes;
        public List<string[]> Startgrids;


        //makes lists for trackmaking used in DrawTrack
        public void MakeLeftCornerList()
        {
            Leftcorners = new List<string[]>();
            Leftcorners.Add(_lcnRce);
            Leftcorners.Add(_lceRcs);
            Leftcorners.Add(_lcsRcw);
            Leftcorners.Add(_lcwRcn);

        }

        public void MakeRightCornerList()
        {
            Rightcorners = new List<string[]>();
            Rightcorners.Add(_lcwRcn);
            Rightcorners.Add(_lcnRce);
            Rightcorners.Add(_lceRcs);
            Rightcorners.Add(_lcsRcw);
        }

        public void MakeStraightsList()
        {
            Straights = new List<string[]>();
            Straights.Add(_straightVertical);
            Straights.Add(_straightHorizontal);
        }

        public void MakeFinishList()
        {
            Finishes = new List<string[]>();
            Finishes.Add(_finishHorizontal);
            Finishes.Add(_finishVertical);
        }

        public void MakeStartList()
        {
            Startgrids.Add(_startVertical);
            Startgrids.Add(_startHorizontal);
        }

        public void Makelitst()
        {
            MakeLeftCornerList();
            MakeRightCornerList();
            MakeStraightsList();
            MakeFinishList();
            MakeStartList();
        }

        // Dictionary for Visualation of racetrack
        private static List<string[]> trackSequence;
        private static int[,] trackCoördinates;
        public string[,] assembledTrack;

        public enum Bearings
        {
            North,
            East,
            South,
            West
        };

        
        public int[] coördinates = new int[2] { 0, 0 };
        public Queue<int[]> q = new Queue<int[]>();

        //variables to compensate for negative coördinates, ultimately creating only positive coördinates


        //compas to know which orientation a section should be drawn
        Bearings b = Bearings.North;

        private void LowestCoördinate(Queue<int[]> q)
        {
            int offsetX = 0;
            int offsetY = 0;
            foreach (int[] cö in q)
            {
                offsetX = Math.Min(offsetX, cö[1]);
                offsetY = Math.Min(offsetY, cö[0]);
            }

            offsetY = Math.Abs(offsetY);
            offsetX = Math.Abs(offsetX);
            foreach (int[] c in q)
            {
                c[1] = c[1] + offsetX;
                c[0] = c[0] + offsetY;
            }

        }

        public string[,] CoördinateBinder(Queue<int[]> ints, List<string[]> stringslist)
        {
            assembledTrack = new string[,] { };
            int[] tempi;
            int c0;
            int c1;
            foreach (string[] strings in stringslist)
            {
                tempi = ints.Dequeue();
                c0 = tempi[0]*6;
                c1 = tempi[1];

                for (int i = 0; i < 5; i++)
                {
                    assembledTrack[c0, c1] = strings[1];
                    c0++;
                }
                
            }

            return assembledTrack;
        }


    public void AssembleTrack(Model.Track track)
        {

            foreach (Model.Section s in track.Sections)
            {
                switch (s.SectionType)
                {
                    case Model.SectionTypes.StartGrid:
                    {
                        switch (b)
                        {
                            case Bearings.North:
                            {
                                trackSequence.Add(Startgrids[0]);

                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] + 1;
                                break;
                            }
                            case Bearings.East:
                            {
                                trackSequence.Add(Startgrids[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] + 1;
                                break;
                            }
                            case Bearings.South:
                            {
                                trackSequence.Add(Startgrids[0]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] - 1;
                                break;
                            }
                            case Bearings.West:
                            {
                                trackSequence.Add(Startgrids[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] - 1;
                                break;
                            }
                        }

                        break;
                    }
                    case Model.SectionTypes.Straight:
                    {
                        switch (b)
                        {
                            case Bearings.North:
                            {
                                trackSequence.Add(Straights[0]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] + 1;
                                break;
                            }
                            case Bearings.East:
                            {
                                trackSequence.Add(Straights[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] + 1;
                                break;
                            }
                            case Bearings.South:
                            {
                                trackSequence.Add(Straights[2]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] - 1;
                                break;
                            }
                            case Bearings.West:
                            {
                                trackSequence.Add(Straights[3]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] - 1;
                                break;
                            }
                        }

                        break;
                    }
                    case Model.SectionTypes.LeftCorner:
                    {
                        switch (b)
                        {
                            case Bearings.North:
                            {
                                trackSequence.Add(Leftcorners[0]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] + 1;
                                b = Bearings.West;
                                break;
                            }
                            case Bearings.East:
                            {
                                trackSequence.Add(Leftcorners[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] + 1;
                                b = Bearings.North;
                                break;
                            }
                            case Bearings.South:
                            {
                                trackSequence.Add(Leftcorners[2]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] - 1;
                                b = Bearings.East;
                                break;
                            }
                            case Bearings.West:
                            {
                                trackSequence.Add(Leftcorners[3]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] - 1;
                                b = Bearings.South;
                                break;
                            }
                        }

                        break;
                    }
                    case Model.SectionTypes.RightCorner:
                    {
                        switch (b)
                        {
                            case Bearings.North:
                            {
                                trackSequence.Add(Rightcorners[0]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] + 1;
                                b = Bearings.East;
                                break;
                            }
                            case Bearings.East:
                            {
                                trackSequence.Add(Rightcorners[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] + 1;

                                b = Bearings.South;
                                break;
                            }
                            case Bearings.South:
                            {
                                trackSequence.Add(Rightcorners[2]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] - 1;
                                b = Bearings.West;
                                break;
                            }
                            case Bearings.West:
                            {
                                trackSequence.Add(Rightcorners[3]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] - 1;
                                b = Bearings.North;
                                break;
                            }
                        }

                        break;
                    }
                    case Model.SectionTypes.Finish:
                    {
                        switch (b)
                        {
                            case Bearings.North:
                            {
                                trackSequence.Add(Finishes[0]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] + 1;
                                break;
                            }
                            case Bearings.East:
                            {
                                trackSequence.Add(Finishes[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] + 1;
                                break;
                            }
                            case Bearings.South:
                            {
                                trackSequence.Add(Finishes[0]);
                                q.Enqueue(coördinates);
                                coördinates[1] = coördinates[1] - 1;
                                break;
                            }
                            case Bearings.West:
                            {
                                trackSequence.Add(Finishes[1]);
                                q.Enqueue(coördinates);
                                coördinates[0] = coördinates[0] - 1;
                                break;
                            }
                        }

                        break;
                    }
                }
            }
            
            LowestCoördinate(q);
            CoördinateBinder(q, trackSequence);

        }
    }
}


