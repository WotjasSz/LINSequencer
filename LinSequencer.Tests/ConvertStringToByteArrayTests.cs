using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using LINSequencerLib.SupportingFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LinSequencer.Tests
{
    public class ConvertStringToByteArrayTests
    {
        [Fact]
        public void Execute_ShouldConvertHexToByteArrayCorrectly()
        {
            // Arrange
            var sequence = new SequenceModel(); // Assuming you have a SequenceModel class
            var step = new ConvertStringToByteArray(sequence)
            {
                InputString = "4A6F686E"
            };

            // Act
            var result = step.Execute();

            // Assert
            Assert.Equal(EStepResult.Success, result);
            Assert.Equal("4A 6F 68 6E", ConvertStringToByteArray.ByteArrayToHexString(step.Output));
        }

        [Fact]
        public void Execute_ShouldHandleOddLengthHexStringsCorrectly()
        {
            // Arrange
            var sequence = new SequenceModel(); // Assuming you have a SequenceModel class
            var step = new ConvertStringToByteArray(sequence)
            {
                InputString = "4A6F68E"
            };

            // Act
            var result = step.Execute();

            // Assert
            Assert.Equal(EStepResult.Success, result);
            Assert.Equal("04 A6 F6 8E", ConvertStringToByteArray.ByteArrayToHexString(step.Output));
        }
    }
}
