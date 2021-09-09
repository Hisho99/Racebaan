using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Trainer : IParticipant
    { 
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }


        public Trainer(IEquipment equipment, TeamColors teamColor)
        {
            Name = Hardcoded.ParticipantNames[Randomizer.rand.Next(Hardcoded.ParticipantNames.Length)];
            Points = 0;
            Equipment = equipment;
            TeamColor = teamColor;
        }

        public Trainer(string name, int points, IEquipment equipment, TeamColors teamColor)
        {
            Name = name ;
            Points = points;
            Equipment = equipment;
            TeamColor = teamColor;
        }
    }
}
