using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using System.Security.Cryptography.X509Certificates;
using de.lipowsky.BLC;

namespace LinSequencer.Tests
{
    public class BabyLinTest
    {
        [Fact]
        public void FillSignalsByByteArray_Passing()
        {
            string expected = "varwr 0 1 [1 2 3 4];";

            byte[]arr = new byte[4] {1,2,3,4 };
            string actual = FillSignalsWithArray(1, arr);

            Assert.Equal(expected, actual);
        }

        public string FillSignalsWithArray(int startSig, byte[] data)
        {
            string arr = "";
            foreach (var item in data)
            {
                arr += item < 128 ? $"{item} " : " ";  //Checking if letter is ASCII if not put space
            }
            //string command = $"varwr 0 {startSig} [{arr}];";
            //return BabyLin.BLC_sendCommand(_linChannel.GetChannelHandle(), command);
            return $"varwr 0 {startSig} [{arr.TrimEnd()}];";
        }
    }
}
