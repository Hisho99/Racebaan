using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set;  }


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
            int r = Randomizer.r.Next(3, 10);
            Type type = typeof(TeamColors);
            Array tclvals = type.GetEnumValues();
            for (int i = 0; i < r; i++)
            {
                int tcl = Randomizer.r.Next(tclvals.Length);
                TeamColors tclval = (TeamColors)tclvals.GetValue(tcl);

                Umamusume uma = new Umamusume();
                Trainer trainer = new Trainer(uma,tclval);
                Competition.Participants.Add(trainer);

 
            }
        }

        // Adds Tracks to Competition Track Queue 
        // Based on Tracks and TrackNames array in Hardcoded.cs
        public static void AddTracks()
        {
            
            int rng = Randomizer.r.Next(2,10);
            for (int i = 0; i < rng; i++)
            {
                int t = Randomizer.r.Next(Hardcoded.TrackNames.Length);
                Track track = new Track(Hardcoded.TrackNames[t],Hardcoded.Track);
                Competition.Tracks.Enqueue(track);

            }
            ;
        }

        // Updates value of CurrentRace value based on Competition Track list 
        // Also checks if there is no new Track
        public static void NextRace()
        {
            Track t = Competition.NextTrack();
            if (t != null){
                CurrentRace = new Race(t, Competition.Participants);
            }

        }

    }
}
