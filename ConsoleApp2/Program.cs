using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
         

        }

        static int[] inversePermutation(int[] arr, int N)
        {
            int[] res = new int[N];
            for (int i = 0; i < arr.Length; i++)
            {
                res[arr[i] - 1] = i + 1;
            }
            return res;
        }
    }

    
}
