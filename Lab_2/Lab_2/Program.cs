using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            float fx1 = (float)6.5, fx2 = (float)2.5;
            IEEE x1 = new IEEE((float)6.5);
            IEEE x2 = new IEEE((float)2.5);
            Console.WriteLine("Введені числа: \nX1(" + fx1 + "): " + x1.ToString() + "\nX2(" + fx2 + "): " + x2.ToString());
            Console.WriteLine(Multiply(x1, x2));
            Console.ReadKey();
        }

        static float Multiply(IEEE x1, IEEE x2)
        {
            uint sign = 0, exp = 0; ulong mantissa = 0;
            sign = (x1.Sign == x2.Sign) ? (uint)0 : 1;
            Console.WriteLine("Множимо знаки. Отримано: " + Convert.ToString(sign, 2));
            exp = x1.Exp + x2.Exp - 127;
            Console.WriteLine("Додаємо і нормалізуємо експоненти. Отримано: " + Convert.ToString(exp, 2));
            mantissa = (ulong)x1.Mantissa * (ulong)x2.Mantissa;
            Console.WriteLine("Множимо мантиси. Отримано: " + Convert.ToString((long)mantissa, 2));
            mantissa >>= 24;
            Console.WriteLine("Зсуваємо мантису на 24 знаки. Отримано: " + Convert.ToString((long)mantissa, 2));
            if(mantissa >> 23 == 1)
            {
                exp += 1;
                Console.WriteLine("Найзначиміший біт мантиси -- 1. Робимо поправку експоненти. Експонента: " + Convert.ToString(exp, 2));
            }
            IEEE iEEE = new IEEE(sign, exp, (uint)mantissa);
            Console.WriteLine(iEEE.ToString());
            return (iEEE.ToFloat());
        }
    }

    struct IEEE
    {
        public uint Sign { get; private set; }
        public uint Exp { get; private set; }
        public uint Mantissa { get; private set; }

        public unsafe IEEE(float number)
        {
            uint num = *((uint*)&number);
            Mantissa = (1 << 23);
            Mantissa += num & 8388607; // 2**24 - 1
            num >>= 23;
            Exp = num & 255;
            num >>= 8;
            Sign = num & 1;
        }

        public IEEE(uint sign, uint exp, uint mantissa)
        {
            Sign = sign; Exp = exp; Mantissa = mantissa;
        }
        
        public unsafe float ToFloat()
        {
            uint res = 0;
            res += Sign;
            res <<= 8;
            res += Exp;
            res <<= 23;
            res += Mantissa & 8388607;
            return *((float*)&res);
        }

        public override string ToString()
        {
            return "Sign: " + Convert.ToString(Sign, 2) + " Exp: " + Convert.ToString(Exp, 2) +
                " Mantissa: " + Convert.ToString(Mantissa, 2) + "; ";
        }
    }

    
}
