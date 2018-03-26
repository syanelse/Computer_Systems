using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ilon = System.Int32;
using System.Threading.Tasks;

namespace Lab_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.Write("Путь к исходному файлу: ");
                string strIn = @"C:\Users\Seashell\Desktop\Универ\CS\Lab_1\Part_2\1.txt";//@Console.ReadLine();
                Console.Write("Путь к закодированому файлу: ");
                string strOut = @"C:\Users\Seashell\Desktop\Универ\CS\Lab_1\Part_2\2.txt";//@Console.ReadLine();
                ConvertToBase64(strIn, strOut);
                Console.ReadKey();
            }
        }
        static void ConvertToBase64(string source, string destination)
        {
            using (File.Create(destination))
            { }
            BinaryReader r = new BinaryReader(File.Open(source, FileMode.Open));
            int[] converted = new int[4];
            int k = -1;
            Int64 threeOctets = 0;
            // kak tebe takoe,
            ilon mask /*???*/
                = 16515072; //111111 000000 000000 000000

            while (r.BaseStream.Position != r.BaseStream.Length)
            {
                threeOctets = 0;
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        threeOctets |= r.ReadByte();
                        threeOctets <<= 8;
                    }
                    catch
                    {
                        k = i;
                        while (i < 3)
                        {
                            threeOctets <<= 8;
                            i++;
                        }
                    }
                }
                threeOctets >>= 8;

                //mask = 16515072; //111111 000000 000000 000000
                mask = 63; // 000000 000000 000000 111111

                for (int i = 3; i >= 0; i--)
                {
                    converted[i] = ((int)threeOctets & mask);
                    threeOctets >>= 6;
                }
                if (k != -1)
                {
                    k++;
                    while (k < 4)
                    {
                        converted[k] = 64;
                        k++;
                    }
                }
                WriteFile(destination, converted);

               
            } r.Close();
        }

        static void WriteFile(string path, int[] converted)
        {
            StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Append), Encoding.ASCII);
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            for (int i = 0; i < 4; i++)
            {
                writer.Write(alphabet[converted[i]]);
            }
            writer.Close();

        }
    }
}

