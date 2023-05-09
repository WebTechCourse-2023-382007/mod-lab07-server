using System;
using System.Threading;
using System.IO;

namespace лаб_7
{
    struct PoolRecord
    {
        public Thread thread;
        public bool in_use;
    }
    class Program
    {
        static void Main(string[] args)
        {
            string forfile = "Результат расчета по формулам:\n";
            int L = 10;
            int M = 2;
            int time = 100;
            int n = 10;
            Server s = new Server(n/2, time);
            Client client = new Client(s);
            for(int i=0; i<n; i++)
            {
                client.Send(i + 1);
                Thread.Sleep(1000/L);
            }
            forfile =forfile + "Интенсивность поступления заявок: " + L + "\n";
            forfile = forfile + "Интенсивность обработки заявок: " + M + "\n";
            forfile = forfile + "Потоков: " + s.pool.Length+"\n";
            forfile = forfile + "Заявок принято: " + s.processedCount+"\n";
            forfile = forfile + "Заявок отклонено: " + s.rejectedCount+"\n";
            Console.WriteLine(forfile);
            double p = L / M;
            double buf=0;
            for (int i = 0; i <= s.pool.Length; i++)
                buf = buf + Math.Pow(p, i) / Factr(i);
            double P0 = 1 / buf;
            forfile = forfile + "Вероятность простоя: " + P0+"\n";
            double Pn = Math.Pow(p, s.pool.Length) / Factr(s.pool.Length) * P0;
            forfile = forfile + "Вероятность отказа: " + Pn+"\n";
            double Q = 1 - Pn;
            forfile = forfile + "Относительная пропускная способность: " + Q+"\n";
            double A = L * Q;
            forfile = forfile + "Абсолютная пропускная способность: " + A+"\n";
            double k = A / M;
            forfile = forfile + "Среднее число занятых потоков: " + k+"\n";
            //Console.WriteLine(forfile);
            StreamWriter sr = new StreamWriter("results.txt");
            sr.WriteLine(forfile);
            sr.Close();
        }
        static int Factr(int n)
        {
            if (n == 0)
                return 1;
            else
            {
                int k = 1;
                for (int i = 1; i <= n; i++)
                    k = k * i;
                return k;
            }
        }
        static void WritetoFile()
        {
            StreamWriter sr = new StreamWriter("results.txt");
            string buf="Ave";
            sr.WriteLine(buf);
            /*do
            {
                buf = Console.ReadLine();
                sr.WriteLine(buf);
            }
            while (buf != "");*/
            //buf = Console.ReadLine();
            //sr.WriteLine(buf);
            //Console.WriteLine(buf);
            sr.Close();
        }
    }
}
