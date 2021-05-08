using Acceptor;
using QuickFix;
using System;

namespace QuickFixTeste
{
    class Program
    {
        private const string HttpServerPrefix = "http://127.0.0.1:5080/";
        [STAThread]
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Iniciando.....");
                SessionSettings settings = new SessionSettings("../../../executor.cfg");
                IApplication myApp = new MyQuickFixApp();
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new FileLogFactory(settings);
                ThreadedSocketAcceptor acceptor = new ThreadedSocketAcceptor(
                    myApp,
                    storeFactory,
                    settings,
                    logFactory);
                HttpServer srv = new HttpServer(HttpServerPrefix, settings);

                //Iniciando acceptor
                acceptor.Start();
                //Iniciando servidor http
                srv.Start();
                Console.WriteLine("Rodando em: " + HttpServerPrefix);
                Console.WriteLine("Aperte enter para finalizar");
                Console.ReadLine();
                //Finalizando acceptor e servidor http
                srv.Stop();
                acceptor.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.ToString());
            }

        }
    }
}

