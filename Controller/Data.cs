using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        static Competition Competition { get; set; }
        static Race CurrentRace;


        //Starts generation of a Competition
        public static void Initialize()
        {
            Competition = new Competition();
            Addparticipants();
            AddTracks();
        }

        //## for future track generation
        public static void GenerateTracks()
        {

        }
        //## for future Track name generation
        public static void GenerateTrackNames()
        {

        }

        // Adds participants to Competition Participants list 
        public static void Addparticipants()
        {
            Random rand = new Random();
            int r = rand.Next(3, 10);
            Type type = typeof(TeamColors);
            Array tclvals = type.GetEnumValues();
            for (int i = 0; i < r; i++)
            {
                int tcl = rand.Next(tclvals.Length);
                TeamColors tclval = (TeamColors)tclvals.GetValue(tcl);

                Umamusume uma = new Umamusume();
                Trainer trainer = new Trainer(uma,tclval);
                Competition.Participants.Add(trainer);
            }
        }

        // Adds Tracks to Competition Track Queue 
        // Based on Tracks and TrackNames array above
        public static void AddTracks()
        {
            Random rand = new Random();
            int r = rand.Next(2,10);
            int t = rand.Next(Hardcoded.TrackNames.Length);
            for (int i = 0; i < r; i++)
            {
                Track track = new Track(Hardcoded.TrackNames[t],Hardcoded.Track);
                Competition.Tracks.Enqueue(track);

            }
        }

        // Updates value of CurrentRace value based on Competition Track list
        public static void NextRace()
        {
            Track t = Competition.NextTrack();
            Console.WriteLine("track:" + t.Name);
            if (t == null){
                Race r = new Race(t, Competition.Participants);
                CurrentRace = r;
                
            }

        }

    }
}
