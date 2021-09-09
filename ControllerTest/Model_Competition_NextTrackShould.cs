using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition { get; set; }

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }



        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Assert.IsNull(_competition.NextTrack());
        }


    }
}
