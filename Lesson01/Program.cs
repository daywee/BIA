using System;
using System.Diagnostics;
using System.Linq;

namespace Lesson01
{
    class Program
    {
        static void Main(string[] args)
        {
            var chars = Enumerable.Range('A', 13).Select(e => (char)e).ToArray();

            var sw = Stopwatch.StartNew();

            Permutation(new string(chars));
            //PermutationFastSwap(new string(chars));

            sw.Stop();

            Console.WriteLine($"{sw.Elapsed.TotalSeconds} s");
        }

        static void Permutation(string text)
        {
            Permutation(text.ToCharArray(), 1);
        }

        static void Permutation(char[] text, int index)
        {
            if (text.Length == index + 1)
            {
                //Console.WriteLine(new string(text));
            }

            for (int i = index; i < text.Length; i++)
            {
                char[] newText = new char[text.Length];
                text.CopyTo(newText, 0);

                char temp = text[index];
                newText[index] = text[i];
                newText[i] = temp;

                Permutation(newText, index + 1);
            }
        }

        static void PermutationFastSwap(string text)
        {
            PermutationFastSwap(text.ToCharArray(), 1);
        }

        static void PermutationFastSwap(char[] text, int index)
        {
            if (text.Length == index + 1)
            {
                //Console.WriteLine(new string(text));
            }

            for (int i = index; i < text.Length; i++)
            {
                char[] newText = new char[text.Length];
                text.CopyTo(newText, 0);

                if (index != i)
                    FastSwap(ref newText[index], ref newText[i]);

                PermutationFastSwap(newText, index + 1);
            }
        }

        static void FastSwap(ref char x, ref char y)
        {
            x = (char)(x ^ y);
            y = (char)(y ^ x);
            x = (char)(x ^ y);
        }
    }
}
