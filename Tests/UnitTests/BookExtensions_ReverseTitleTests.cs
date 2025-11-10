using SearchSystemDomain.Extensions;
using Serilog;
using System.Diagnostics;
using Xunit;

namespace Tests.UnitTests
{
    public class BookExtensions_ReverseTitleTests
    {
        [Theory]
        [InlineData("Moby Dick", "kciD yboM")]
        [InlineData("A", "A")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("12345", "54321")]
        [InlineData("Racecar", "racecaR")]
        public void ReverseTitle_ViaArrayReverse_Correctness(string input, string expected)
        {
            Assert.Equal(expected, input?.ReverseTitle_ViaArrayReverse());
        }

        [Theory]
        [InlineData("Moby Dick", "kciD yboM")]
        [InlineData("A", "A")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("12345", "54321")]
        [InlineData("Racecar", "racecaR")]
        public void ReverseTitle_ViaManualLoop_Correctness(string input, string expected)
        {
            Assert.Equal(expected, input?.ReverseTitle_ViaManualLoop());
        }

        [Theory]
        [InlineData("Moby Dick", "kciD yboM")]
        [InlineData("A", "A")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("12345", "54321")]
        [InlineData("Racecar", "racecaR")]
        public void ReverseTitle_ViaStringBuilder_Correctness(string input, string expected)
        {
            Assert.Equal(expected, input?.ReverseTitle_ViaStringBuilder());
        }

        [Fact]
        public void ReverseTitle_PerformanceDiff()
        {
            string sample = "Moby Dick";
            int iterations = 1000000;

            var swArray = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                _ = sample.ReverseTitle_ViaArrayReverse();
            swArray.Stop();
            Log.Information("ReverseTitle_ViaArrayReverse: {Elapsed} ms", swArray.ElapsedMilliseconds); // 20ms - THE WINNER

            var swManual = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                _ = sample.ReverseTitle_ViaManualLoop();
            swManual.Stop();
            Log.Information("ReverseTitle_ViaManualLoop: {Elapsed} ms", swManual.ElapsedMilliseconds); // 31ms

            var swBuilder = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                _ = sample.ReverseTitle_ViaStringBuilder();
            swBuilder.Stop();
            Log.Information("ReverseTitle_ViaStringBuilder: {Elapsed} ms", swBuilder.ElapsedMilliseconds); // 36ms

            Assert.True(swArray.ElapsedMilliseconds < 2000, "ReverseTitle_ViaArrayReverse should be fast.");
            Assert.True(swManual.ElapsedMilliseconds < 2000, "ReverseTitle_ViaManualLoop should be fast.");
            Assert.True(swBuilder.ElapsedMilliseconds < 2000, "ReverseTitle_ViaStringBuilder should be fast.");
        }
    }
}