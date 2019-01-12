using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Naive("acaabcaab", "aab");
            long result = RabinKorp("acaabcaab", "aab");
            Console.WriteLine(result);
        }

        static void Naive(string T, string P)
        {
            int n = T.Length;
            int m = P.Length;

            for (int s = 0; s < n - m + 1; s++)
            {
                bool flag = true;
                for (int i = 0; i < m; i++)
                {
                    if (P[i] != T[i + s])
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                    Console.WriteLine("found at position:{0}", s);
            }
        }

        static long RabinKorp(string T, string P)
        {
            int M = P.Length;
            int R = 10;
            int Q = 420921;

            int RM = 1;
            for (int i = 1; i <= M - 1; i++)
                RM = (R * RM) % Q;

            long pathHash = hash(P, M, R, Q);

            int N = T.Length;
            long txtaHash = hash(T, M, R, Q);
            if (pathHash == txtaHash)
                return 0;

            for (int i = M; i < N; i++)
            {
                txtaHash = (txtaHash + Q - RM * T[i - M] % Q) % Q;
                txtaHash = (txtaHash * R + T[i]) % Q;
                if (pathHash == txtaHash)
                    return i - M + 1;
             }

            return -1;
        }

        static long hash(String key, int M, int R, int Q)
        {
            long h = 0;
            for (int j = 0; j < M; j++)
                h = (R * h + key[j] % Q);

            return h;
        }
    }
}
