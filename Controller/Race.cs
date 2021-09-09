using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random { get; set; }
        private Dictionary<Section, SectionData> _positions;


        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
        }


        public SectionData GetSectionData(Section section)
        {
            if (_positions.TryGetValue(section, out SectionData d))
            { return d; }
            else
            {


                SectionData sd = new SectionData();
                _positions[section] = sd;
                return sd;
            }
        }

        // Randomizes equipment 
        public void RandomizeEquipment()
        {
            foreach (IParticipant p in Participants)
            {
                p.Equipment.Quality = _random.Next(1,10);
                p.Equipment.Performane = _random.Next(1,10);
            }
        }



    }
}
