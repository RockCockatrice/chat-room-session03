using System;
using System.Text;

namespace ChatCoreTest
{
  internal class Program
  {
    private static byte[] m_PacketData;
    private static uint m_Pos;
    private static uint a_Pos;
    private static uint b_Pos;
    private static uint c_Pos;

    public static void Main(string[] args)
    {
      m_PacketData = new byte[1024];
      m_Pos = 0;

      Write(109);
      Write(109.99f);
      Write("Hello!");

      Console.Write($"Output Byte array(length:{m_Pos}): ");
      for (var i = 0; i < m_Pos; i++)
      {
        Console.Write(m_PacketData[i] + ", ");
      }
            _Read();
            Console.WriteLine("complete");
            Console.ReadLine();
    }

    // write an integer into a byte array
    private static bool Write(int i)
    {
      // convert int to byte array
      var bytes = BitConverter.GetBytes(i);
      _Write(bytes);
      return true;
    }

    // write a float into a byte array
    private static bool Write(float f)
    {
      // convert int to byte array
      var bytes = BitConverter.GetBytes(f);
      _Write(bytes);
      return true;
    }

    // write a string into a byte array
    private static bool Write(string s)
    {
      // convert string to byte array
      var bytes = Encoding.Unicode.GetBytes(s);

      // write byte array length to packet's byte array
      if (Write(bytes.Length) == false)
      {
        return false;
      }

      _Write(bytes);
      return true;
    }

    // write a byte array into packet's byte array
    private static void _Write(byte[] byteData)
    {
      // converter little-endian to network's big-endian
      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(byteData);
      }

      byteData.CopyTo(m_PacketData, m_Pos);
      m_Pos += (uint)byteData.Length;
    }
    private static void _Read ()
    {
            byte[] bytes1 = new byte[4];
            byte[] bytes2 = new byte[4];
            byte[] bytes3 = new byte[16];
            for (int i = 0; i < 4; i++)
            {
                bytes1[i] = m_PacketData[i];
            }
            a_Pos += (uint)bytes1.Length;
            for (int i = 0; i < 3; i++)
            {
                bytes2[i] = m_PacketData[i + 4];
            }
            b_Pos += (uint)bytes2.Length;
            for (int i = 0; i < m_Pos - 8; i++)
            {
                bytes3[i] = m_PacketData[i + 8];
            }
            c_Pos += (uint)bytes3.Length;
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes1);
                Array.Reverse(bytes2);
                Array.Reverse(bytes3);
            }
            Console.WriteLine("\n");
            var bytes11 = BitConverter.ToInt32(bytes1, 0);
            Console.WriteLine($"Length = {a_Pos} : {bytes11}");
            var bytes22 = BitConverter.ToSingle(bytes2, 0);
            Console.WriteLine($"Length = {b_Pos} : {bytes22}");
            var bytes33 = Encoding.Unicode.GetString(bytes3);
            Console.WriteLine($"Length = {c_Pos} : {bytes33}");
        }
    }
}
