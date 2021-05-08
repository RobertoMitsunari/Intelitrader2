using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickFix;

namespace QuickFixTeste
{
    class MessageTranslator
    {
        public void TranslateMessage(Message msg)
        {
            //8, 9, 35, 34, 49, 52, 56, 98, 108, 7, 16, 10
            Dictionary<string, string> codigos =
                new Dictionary<string, string>();
            codigos.Add("8", "BeginString");
            codigos.Add("9", "BodyLenght");
            codigos.Add("35", "MsgType");
            codigos.Add("34", "MsgSeqNum");
            codigos.Add("49", "SenderCompID");
            codigos.Add("52", "SendingTime");
            codigos.Add("56", "TargetCompID");
            codigos.Add("98", "EncryptMethod");
            codigos.Add("108", "HeartBtInt");
            codigos.Add("7", "BeginSeqNo");
            codigos.Add("16", "EndSeqNo");
            codigos.Add("10", "CheckSum");

            StringBuilder sb = new StringBuilder();
            String arroz = msg.ToString();
            String[] vetorMsg = msg.ToString().Split("\x0001");
            String[] div;
            foreach (String parte in vetorMsg)
            {
                if (!parte.Equals(""))
                {
                    div = parte.Split("=");
                    sb.Append(codigos[div[0]] + "=" + div[1] + "|");
                }
                
            }
            System.Console.WriteLine(sb);
            
        }
    }
}
