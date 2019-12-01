using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetIO
{
    public class NetWriter
    {
        private Stream s;

        /* Function:        NetWriter (constructor)
         * Parameters:      Stream stream, the stream to write to
         * Description:     Initialises this instance of the class.
         */
        public NetWriter(Stream stream)
        {
            s = stream;
            if (!s.CanWrite)
                throw new ArgumentException("Stream passed to NetWriter cannot be written to");

        }

        /* Function:        writeByte
         * Parameters:      byte b, the byte to write to the stream
         * Description:     Writes a single byte, b, to the stream.
         */
        public void writeByte(byte b)
        {
            s.WriteByte(b);
        }

        /* Function:        writeByteArray
         * Parameters:      byte[] b, the array to write from
         *                  int offset, the offset of the array to write from
         *                  int length, the number of bytes to write from the array
         * Description:     Writes length bytes to the stream from the array starting at offset.
         */
        public void writeByteArray(ref byte[] b, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
                writeByte(b[i]);
        }

        /* Function:        writeInt16
         * Parameters:      int i, the integer to write to the stream
         * Decription:      Writes a 16-bit integer to the stream in Network Byte Order.
         */
        public void writeInt16(Int16 i)
        {
            byte[] bytes;
            bytes = BitConverter.GetBytes(i);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            writeByteArray(ref bytes, 0, 2);
        }

        /* Function:        writeInt32
         * Parameters:      int i, the integer to write to the stream
         * Decription:      Writes a 32-bit integer to the stream in Network Byte Order.
         */
        public void writeInt32(int i)
        {
            byte[] bytes;
            bytes = BitConverter.GetBytes(i);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            writeByteArray(ref bytes, 0, 4);
        }

        /* Function:        writeString
         * Parameters:      string s, the string to write to the stream
         * Description:     Writes a UTF-8 encoded string to the stream in the following format: (int32 length)(char[] UTF8chars)
         */
        public void writeString(string s)
        {
            char[] chars = s.ToCharArray();
            byte[] output = Encoding.UTF8.GetBytes(chars);
            writeInt32(output.Length);
            writeByteArray(ref output, 0, output.Length);
        }
    }
}
