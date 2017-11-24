namespace Game.Shared.Utility
{
    //Used for compressing data to send over the network.
    public class DataCompress
    {
        //Not really compression, but it can be used to store more values if you limit the byte inputs. i.e. if you never input 0, only 1+, you can still use the short as a byte.
        public static ushort TwoBytesToUShort(byte a, byte b)
        {
            var s = (ushort) ((a << 8) | b); //AAAAAAAABBBBBBBB
            return s;
        }

        public static byte[] UShortToTwoBytes(ushort s)
        {
            var a = (byte) (s >> 8); //Shift A to right.
            var b = (byte) (s & 0xFF); //Remove A from left byte.
            return new byte[2] {a, b};
        }
    }
}