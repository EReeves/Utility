using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Shared.Utility
{
    //Used for compressing data to send over the network.
    public class DataCompress
    {

        public static ushort TwoBytesToUShort(byte a, byte b)
        {
            var s = (ushort)((a << 8) | b); //AAAAAAAABBBBBBBB
            return s;
        }

        public static byte[] UShortToTwoBytes(ushort s)
        {
            var a = (byte)(s >> 8); //Shift A to right.
            var b = (byte)(s & 0xFF); //Remove A from left byte.
            return new byte[2]{a,b};
        }
    }
}
