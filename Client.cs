using System;
using System.Collections.Generic;
using System.Text;

namespace лаб_7
{
    class Client
    {
        Server server;
        public event EventHandler<procEventArgs> request;
        public Client(Server server)
        {
            this.server = server;
            this.request += server.proc;
        }
        protected virtual void OnProc(procEventArgs e)
        {
            EventHandler<procEventArgs> handler = request;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void Send(int id)
        {
            procEventArgs args = new procEventArgs();
            args.id = id;
            OnProc(args);
        }
    }
}
