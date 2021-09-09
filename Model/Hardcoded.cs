using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Hardcoded
    {
        //#############Temporary hardcodded data################
        public static Dictionary<string, SectionTypes[]> _tracks;

        public static string[] TrackNames =
        {
        "Track 1",
        "Track 2",
        "Track 3",
        "Track 4",
        "Track 5",
        "Track 6"
        };

        // static List<SectionTypes[]> Tracklist;
        public static SectionTypes[] Track =
        {
            SectionTypes.StartGrid,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.LeftCorner,
            SectionTypes.Finish
        };

        public static string[] ParticipantNames =
        {
            "Trainer 1",
            "Trainer 2",
            "Trainer 3"
        };


    }
}
