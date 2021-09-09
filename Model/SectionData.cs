﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SectionData
    {
        public IParticipant Left { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceLeft { get; set; }
        public int DistanceRight { get; set; }

        public SectionData() 
        {
            
        }
        public SectionData(IParticipant left, IParticipant right, int distanceLeft, int distanceRight)
        {
            Left = left;
            Right = right;
            DistanceLeft = distanceLeft;
            DistanceRight = distanceRight;
        }


    }
}