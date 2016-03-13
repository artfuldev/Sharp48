using System;

namespace Sharp48.Solvers.Extensions
{
    public static class TileExtensions
    {
        public static byte GetLog2Value(this uint powerOf2)
            => powerOf2 <= 0 ? (byte) 0 : Convert.ToByte(Math.Log(powerOf2)/Math.Log(2));
    }
}