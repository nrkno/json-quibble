using System;
using System.Linq;
using Xunit;

namespace Quibble.CSharp.UnitTests
{
    public class JsonDiffTests
    {
        [Fact]
        public void TestOfValuesTrueVsFalse()
        {
            var v1 = JsonParse.Parse("true");
            var v2 = JsonParse.Parse("false");
            var diffs = JsonDiff.OfValues(v1, v2).ToList();
            Assert.NotEmpty(diffs);
            var kindDiff = (Diff.Type) diffs.Single();
            Assert.Equal("$", kindDiff.Item.Path);
            Assert.Equal(JsonValue.True, kindDiff.Left);
            Assert.Equal(JsonValue.False, kindDiff.Right);
        }
        
        [Fact]
        public void TestOfDocuments1Vs2()
        {
            var v1 = JsonParse.Parse("1");
            var v2 = JsonParse.Parse("2");
            var diffs = JsonDiff.OfValues(v1, v2).ToList();
            Assert.NotEmpty(diffs);
            var valueDiff = (Diff.Value)diffs.Single();
            Assert.Equal("$", valueDiff.Path);
            Assert.Equal(JsonValue.NewNumber(Tuple.Create<double, string>(1, "1")), valueDiff.Left);
            Assert.Equal(JsonValue.NewNumber(Tuple.Create<double, string>(2, "2")), valueDiff.Right);
        }
    }
}
