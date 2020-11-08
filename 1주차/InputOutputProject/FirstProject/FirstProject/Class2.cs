using System;
using FirstProject222;

namespace FirstProject
{
    class Program
    {
        public static int var1;
        private static int var2;
        private static bool toggle = false;

        static void Main(string[] args)
        {
            toggle = !toggle;
            Console.WriteLine(toggle);

            toggle = !toggle;
            Console.WriteLine(toggle);

            toggle = !toggle;
            Console.WriteLine(toggle);

            toggle = !toggle;
            Console.WriteLine(toggle);

            toggle = !toggle;
            Console.WriteLine(toggle);
        }

        private static float Sum(float a, float b)
        {
            return a + b;
        }
    }
}
