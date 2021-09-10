using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NUnit.Framework;
using NUnit.Framework.Internal;

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

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track t = new Track("TestTrack",Hardcoded.Track);
            _competition.Tracks.Enqueue(t);
            Assert.AreEqual(_competition.Tracks.First(), _competition.NextTrack());
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track t = new Track("TestTrack", Hardcoded.Track);
            _competition.Tracks.Enqueue(t);
            _competition.Tracks.Enqueue(t);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.IsNull(result);
        }
        
        [Test]
       public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track t1 = new Track("TestTrack1", Hardcoded.Track);
            Track t2 = new Track("TestTrack2", Hardcoded.Track);
            _competition.Tracks.Enqueue(t2);
            _competition.Tracks.Enqueue(t1);


            var first = _competition.Tracks.First();
            var next = _competition.NextTrack();

            Assert.AreEqual(first, next);

        }

    }
}
