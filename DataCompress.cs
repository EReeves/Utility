using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        public static unsafe void Woah()
        {
            char* str = stackalloc char[6];
            for (var i = 7; i > 1; i--)
            {
                char? c = null;
                switch (i)
                {
                    case 7:
                        c = 'f';
                        break;
                    case 6:
                        c = 'e';
                        break;
                    case 5:
                        c = 'd';
                        break;
                    case 4:
                        c = 'c';
                        break;
                    case 3:
                        c = 'b';
                        break;
                    case 2:
                        c = 'a';
                        break;
                    default:
                        throw new Exception("ohno");
                }

                Debug.Assert(c != null, nameof(c) + " != null");
                *(str + i - 2) = c.Value;
            }

            var ch = 's';
            var cnt = -1;
            do
            {
                cnt++;
                ch = *(str + cnt);
            } while (ch != '\0');

            var length = cnt;
            var evenOrOdd = ((length & 1) == 0) ? "even" : "odd";
            Console.WriteLine(evenOrOdd);
        }
    }
}