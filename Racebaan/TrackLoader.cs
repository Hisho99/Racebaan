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
        public static string[] _lcnRce =
        {
            "---\\  ",
            "    \\ ",
            "     \\",
            "     |",
            "     |",
            " 	  |"
        };

        public static string[] _lcwRcn =
        {
            "  /---",
            " /    ",
            "/     ",
            "|     ",
            "|     ",
            "|     "

        };

        public static string[] _lcsRcw =
        {
            "|     ",
            "|     ",
            "|     ",
            "\\     ",
            " \\    ",
            "  \\---"

        };

        public static string[] _lceRcs =
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

        public static string[] _straightHorizontal =
        {
            "------",
            "      ",
            "      ",
            "	   ",
            "	   ",
            "------"
        };

        public static string[] _finishHorizontal =
        {
            "|    |",
            "|    |",
            "|#  #|",
            "|#  #|",
            "|    |",
            "|    |"
        };

        public static string[] _finishVertical =
        {
            "------",
            "  ##  ",
            "      ",
            "	   ",
            "  ##  ",
            "------"
        };

        public static string[] _finishLeftCornerUp =
        {
            "---\\  ",
            "    \\ ",
            "    #\\",
            "     |",
            "#	  |",
            " 	  |"
        };

        public static string[] _finishRightCornerUp =
        {
            "  /---",
            " /    ",
            "/#    ",
            "|     ",
            "|    #",
            "|     "

        };

        public static string[] _finishRightCornerDown =
        {
            "|     ",
            "|    #",
            "|     ",
            "\\     ",
            " \\#   ",
            "  \\---"
        };

        public static string[] _finishLeftCornerDown =
        {
            "     |",
            "#    |",
            "     |",
            "	  /",
            "   #/ ",
            "---/  "
        };

        public static string[] _startVertical =
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

        #endregion

        public static List<string[]> Leftcorners = new List<string[]> { _lcnRce, _lceRcs, _lcsRcw, _lcwRcn };
        public static List<string[]> Rightcorners = new List<string[]> { _lcwRcn, _lcnRce, _lceRcs, _lcsRcw };
        public static List<string[]> Straights = new List<string[]> { _straightVertical, _straightHorizontal, _straightVertical, _straightHorizontal };
        public static List<string[]> Finishes = new List<string[]> { _finishVertical, _finishHorizontal, _finishVertical, _finishHorizontal};
        public static List<string[]> Startgrids = new List<string[]>{_startVertical,_startHorizontal,_startVertical,_startHorizontal};

        public void AssignXY(Model.Track track)
        {
            int compas = 0;
            int x = 0;
            int y = 0;
            foreach (Model.Section s in track.Sections)
            {
                switch (s.SectionType)
                {
                    case Model.SectionTypes.LeftCorner:
                            LeftCorners(s, compas, x, y);  
                            break;
                    case Model.SectionTypes.RightCorner:
                            RightCorners(s, compas, x, y);
                            break;
                    default:
                        Straight(s, compas, x, y);
                        break;
                }
            }
            NegativeXYCheck(track.Sections);
        }

        public void LeftCorners(Model.Section c, int i, int x, int y)
        {
            /*North = -y
              East  = -x
              South = +y
              West  = +x*/
            switch (i)
            {
                case 0:
                        y--;
                        break;
                case 1:
                        x--;
                        break;
                case 2:
                        y++;
                        break;
                case 3:
                        x++;
                        break;
                default:
                    break;
            }
            i--;
            if (i < 0)
            {
                i = 3;
            }
            c.Xval = x;
            c.Yval = y;

        }
        public void RightCorners(Model.Section c, int i, int x, int y)
        {
            /*North = +y
              East  = +x
              South = -y
              West  = -x*/
            switch (i)
            {
                case 0:
                        y++;
                        break;
                case 1:
                        x++;
                        break;
                case 2:
                        y--;
                        break;
                case 3:
                        x--;
                        break;
                default:
                    break;
            }
            i++;
            if (i > 3)
            {
                i = 0;
            }
            c.Xval = x;
            c.Yval = y;
        }
        public void Straight(Model.Section s, int i, int x, int y)
        {
            /*North = -x
              East  = +y
              South = +x
              West  = -y*/
            switch (i)
            {
                case 0:
                        x--;
                    break;
                case 1:
                        y++;
                        break;
                case 2:
                        x++;
                        break;
                case 3:
                        y--;
                        break;
                default:
                    break;
            }
            s.Xval = x;
            s.Yval = y;
        }
        public void NegativeXYCheck(LinkedList<Model.Section> s)
        {
            int offsetX = 0;
            int offsetY = 0;

            foreach (Model.Section sec in s)
            {
                offsetX = Math.Min(offsetX, sec.Xval);
                offsetY = Math.Min(offsetY, sec.Yval);
            }

            if (offsetX < 0 || offsetY < 0)
            {
                offsetY = Math.Abs(offsetY);
                offsetX = Math.Abs(offsetX);
                foreach (Model.Section sec in s)
                {
                    sec.Xval = sec.Xval + offsetX;
                    sec.Yval = sec.Yval + offsetY;
                } 
            }
        }


        //variables to compensate for negative coördinates, ultimately creating only positive coördinates


        //compas to know which orientation a section should be drawn

        /*        public int HighestYCoördinate(int[,] ints)
                {
                    int maximum = 0;
                    for (int i = 0; i < ints.Length / 2; i++)
                    {

                        maximum = Math.Max(maximum, ints[i, 1]);
                        maximum = Math.Max(maximum, ints[i, 0]);
                    }
                    return maximum;
                }*/

        /*public void CoördinateBinder(int[,] ints, List<string[]> stringslist)
        {
            int max = HighestYCoördinate(ints);
            AssembledTrack = new string[ints.GetUpperBound(0)*6,max] ;
            
            int x = 0;
            int c0;
            int c1;
            foreach (string[] strings in stringslist)
            {
               
                c0 = ints[x,0]*6;
                
                c1 = ints[x,1];

                for (int i = 0; i < 6; i++)
                {
                    AssembledTrack[c0, c1] = strings[i];
                    c0++;
                }
               
                x++;
            }

        }*/

        /* public void AssembleTrack(Model.Track track)
             {
                 Makelitst();
                 trackCoördinates = new int[track.Sections.Count,2] ;
                 Bearings b = Bearings.North;
                 coördinates = new int[2] { 0, 0};
                 int xcopy = 0;
                 int ycopy = 0;
                 int i = 0;
                 trackSequence = new List<string[]> { };
                 foreach (Model.Section s in track.Sections)
                 {
                     xcopy = coördinates[0];
                     ycopy = coördinates[1];
                     switch (s.SectionType)
                     {
                         case Model.SectionTypes.StartGrid:
                         {
                             switch (b)
                             {
                                 case Bearings.North:
                                 {
                                     trackSequence.Add(Startgrids[0]);

                                     trackCoördinates[i,0] = xcopy;
                                     trackCoördinates[i, 1] = ycopy;
                                     coördinates[0] = coördinates[0] - 1;
                                     break;
                                 }
                                 case Bearings.East:
                                 {
                                     trackSequence.Add(Startgrids[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] + 1;
                                     break;
                                 }
                                 case Bearings.South:
                                 {
                                     trackSequence.Add(Startgrids[0]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] + 1;
                                     break;
                                 }
                                 case Bearings.West:
                                 {
                                     trackSequence.Add(Startgrids[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] - 1;
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
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] - 1;
                                     break;
                                 }
                                 case Bearings.East:
                                 {
                                     trackSequence.Add(Straights[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] + 1;
                                     break;
                                 }
                                 case Bearings.South:
                                 {
                                     trackSequence.Add(Straights[0]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] + 1;
                                     break;
                                 }
                                 case Bearings.West:
                                 {
                                     trackSequence.Add(Straights[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] - 1;
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
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] - 1;
                                     b = Bearings.West;
                                     break;
                                 }
                                 case Bearings.East:
                                 {
                                     trackSequence.Add(Leftcorners[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] - 1;
                                     b = Bearings.North;
                                     break;
                                 }
                                 case Bearings.South:
                                 {
                                     trackSequence.Add(Leftcorners[2]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] + 1;
                                     b = Bearings.East;
                                     break;
                                 }
                                 case Bearings.West:
                                 {
                                     trackSequence.Add(Leftcorners[3]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] + 1;
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
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] + 1;
                                     b = Bearings.East;
                                     break;
                                 }
                                 case Bearings.East:
                                 {
                                     trackSequence.Add(Rightcorners[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] + 1;

                                     b = Bearings.South;
                                     break;
                                 }
                                 case Bearings.South:
                                 {
                                     trackSequence.Add(Rightcorners[2]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] - 1;
                                     b = Bearings.West;
                                     break;
                                 }
                                 case Bearings.West:
                                 {
                                     trackSequence.Add(Rightcorners[3]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
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
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] - 1;
                                     break;
                                 }
                                 case Bearings.East:
                                 {
                                     trackSequence.Add(Finishes[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] + 1;
                                     break;
                                 }
                                 case Bearings.South:
                                 {
                                     trackSequence.Add(Finishes[0]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[0] = coördinates[0] + 1;
                                     break;
                                 }
                                 case Bearings.West:
                                 {
                                     trackSequence.Add(Finishes[1]);
                                             trackCoördinates[i, 0] = xcopy;
                                             trackCoördinates[i, 1] = ycopy;
                                             coördinates[1] = coördinates[1] - 1;
                                     break;
                                 }
                             }

                             break;
                         }
                     }
                     i++;
                 }
                 LowestCoördinate(trackCoördinates);
                 CoördinateBinder(trackCoördinates, trackSequence);
             }*/
    }
}
