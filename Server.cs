using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace лаб_7
{
    class Server
    {
        object threadLock = new object();
        public PoolRecord[] pool;
        public int requestCount = 0; //Количество полученных запросов
        public int processedCount = 0; //Количество обработанных запросов
        public int rejectedCount = 0; //Количество отклоненных запросов
        public int delayTime;
        public Server(int n, int time)
        {
            pool = new PoolRecord[n];
            delayTime = time;
        }
        public void proc(object sender, procEventArgs e)
        {
            lock(threadLock)
            {
                Console.WriteLine("Заявка с номером: {0}", e.id);
                requestCount++;
                for (int i = 0; i < pool.Length; i++)
                {
                    if(!pool[i].in_use)
                    {
                        pool[i].in_use = true;
                        pool[i].thread = new Thread(new ParameterizedThreadStart(Answer));
                        pool[i].thread.Start(e.id);
                        processedCount++;
                        Console.WriteLine("Принята");
                        return;
                    }
                }
                rejectedCount++;
                Console.WriteLine("Отклонена");
            }
        }
        public void Answer(object ob)
        {
            Thread.Sleep(delayTime);
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].thread == Thread.CurrentThread)
                {
                    pool[i].in_use = false;
                }
            }
        }
    }
}
