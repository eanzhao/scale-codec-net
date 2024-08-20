using System.Numerics;
using System.Runtime.InteropServices;
using AElf;
using Scale.Encoders;
using Shouldly;

namespace Scale.Tests;

public class EncodersTests
{
    [Theory]
    [InlineData(0, "0x00")]
    [InlineData(1, "0x04")]
    [InlineData(42, "0xa8")]
    [InlineData(69, "0x1501")]
    [InlineData(65535, "0xfeff0300")]
    public void CompactIntegerTypeEncoderTest(ulong value, string hexValue)
    {
        new CompactIntegerTypeEncoder().Encode(value).ToHex(true).ShouldBe(hexValue);
    }
    
    [Theory]
    [InlineData("100000000000000", "0x0b00407a10f35a")]
    public void CompactBigIntegerTypeEncoderTest(string value, string hexValue)
    {
        var bigInteger = BigInteger.Parse(value);
        new CompactIntegerTypeEncoder().Encode(bigInteger).ToHex(true).ShouldBe(hexValue);
    }

    [Theory]
    [InlineData(100000000000000, "0x00407a10f35a0000000000000000000000000000000000000000000000000000")]
    [InlineData(100, "0x6400000000000000000000000000000000000000000000000000000000000000")]
    public void IntegerTypeEncoderTest(ulong value, string hexValue)
    {
        new IntegerTypeEncoder().Encode(value).ToHex(true).ShouldBe(hexValue);
    }

    [Theory]
    [InlineData("test", "0x1074657374")]
    public void StringTypeEncoderTest(string value, string hexValue)
    {
        new StringTypeEncoder().Encode(value).ToHex(true).ShouldBe(hexValue);
    }
}