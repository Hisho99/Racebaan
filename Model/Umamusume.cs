    using System;

namespace Model
{
    public class Umamusume : IEquipment
    {
        public int Quality { get; set; }
        public int Performane { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }

        public Umamusume()
        {
            Random rand = new Random();
            Quality = rand.Next(1, 100);
            Performane = rand.Next(1, 100);
            Speed = rand.Next(1, 100);
            IsBroken = rand.NextDouble() >= 0.1;
        }
        public Umamusume(int quality, int performane, int speed, bool isBroken)
        {
            Quality = quality;
            Performane = performane;
            Speed = speed;
            IsBroken = isBroken;
        }
    }
}
