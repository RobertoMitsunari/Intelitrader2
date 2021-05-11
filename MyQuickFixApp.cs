 using QuickFix;
using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFixTeste
{
    public class MyQuickFixApp : MessageCracker, IApplication
    {


        int orderID = 0;
        int execID = 0;

        private string GenOrderID() { return (++orderID).ToString(); }
        private string GenExecID() { return (++execID).ToString(); }
        private MessageTranslator mst = new MessageTranslator();

        #region Application Methods
        public void FromAdmin(Message message, SessionID sessionID)
        {
            //Console.WriteLine("OUT: " + message);
        }
        public void ToAdmin(Message message, SessionID sessionID)
        {
            //Console.WriteLine("OUT: " + message);
            Console.WriteLine(mst.TranslateMessage(message));
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN: " + message);
            Crack(message, sessionID);
        }
        public void ToApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT: " + message);
        }

        public void OnCreate(SessionID sessionID) { }

        public void OnLogon(SessionID sessionID) {

        }

        public void OnLogout(SessionID sessionID) { }

        #endregion




        public void OnMessage(QuickFix.FIX44.NewOrderSingle ord, SessionID sessionID)
        {

            Symbol symbol = ord.Symbol;
            Side side = ord.Side;
            OrdType ordType = ord.OrdType;
            OrderQty orderQty = ord.OrderQty;
            Price price = new Price(10);
            //q isso?
            ClOrdID clOrdID = ord.ClOrdID;
            QuickFix.FIX44.ExecutionReport exReport = new QuickFix.FIX44.ExecutionReport(
               new OrderID(GenOrderID()),
               new ExecID(GenExecID()),
               new ExecType(ExecType.FILL),
               new OrdStatus(OrdStatus.FILLED),
               symbol,
               side,
               new LeavesQty(0),
               new CumQty(orderQty.getValue()),
               new AvgPx(price.getValue())
            );
            exReport.ClOrdID = clOrdID;
            exReport.Symbol = symbol;
            exReport.OrderQty = orderQty;
            exReport.LastQty = new LastQty(orderQty.getValue());
            exReport.LastPx = new LastPx(price.getValue());

            if (ord.IsSetAccount())
            {
                exReport.Account = ord.Account;
            }
            try
            {
                Session.SendToTarget(exReport, sessionID);
                
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void OnMessage(QuickFix.FIX44.Logon ord, SessionID sessionID)
        {
            Console.WriteLine("eeee");
        }
    }

}
