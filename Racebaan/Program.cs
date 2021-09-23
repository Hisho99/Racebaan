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
            DrawTrack(Data.CurrentRace.Track);
        }

        


        public static void DrawTrack(Model.Track track)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            int compas =  0;
            TrackLoader t = new TrackLoader();
            t.AssignXY(track);

            foreach (Model.Section sec in track.Sections)
            {
                if (sec.SectionType == Model.SectionTypes.LeftCorner)
                {
                    LeftCornerWriter(sec, compas);
                }else if(sec.SectionType == Model.SectionTypes.RightCorner){
                    RightCornerWriter(sec, compas);
                }
                else
                {
                    StraightWriter(sec, compas);
                }
            }





        }

        public static void LeftCornerWriter(Model.Section s, int compas)
        {
            int x = s.Xval;
            int y = s.Yval;
            string[] str;
            str = TrackLoader.Leftcorners[compas];
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(x * 6, y * 6 + i);
                Console.Write(str[i]);
            }

        }

        public static void RightCornerWriter(Model.Section s, int compas)
        {
            int x = s.Xval;
            int y = s.Yval;
            string[] str;
            str = TrackLoader.Rightcorners[compas];
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(x * 6, y * 6 + i);
                Console.Write(str[i]);
            }
        }

        public static void StraightWriter(Model.Section s, int compas)
        {
            int x = s.Xval;
            int y = s.Yval;
            string[] str;
            switch (s.SectionType)
            {
                case Model.SectionTypes.Straight:
                    str = TrackLoader.Straights[compas];
                    for (int i = 0; i < 6; i++)
                    {
                        Console.SetCursorPosition(x * 6, y * 6 + i);
                        Console.Write(str[i]);
                    }
                    break;
                case Model.SectionTypes.StartGrid:
                    str = TrackLoader.Startgrids[compas];
                    for (int i = 0; i < 6; i++)
                    {
                        Console.SetCursorPosition(x * 6, y * 6 + i);
                        Console.Write(str[i]);
                    }
                    break;
                case Model.SectionTypes.Finish:
                    str = TrackLoader.Finishes[compas];
                    for (int i = 0; i < 6; i++)
                    {
                        Console.SetCursorPosition(x * 6, y * 6 + i);
                        Console.Write(str[i]);
                    }
                    break;
                default:
                    break;
            }
        }

        /*  Console.Cursor(x*6, y*6)
        COnsole.Write(_lcnRce[0])
        Console.Cursor(x*6,y*6+1)
        Console.Write(_lcnRce[1])*/
    }
}

