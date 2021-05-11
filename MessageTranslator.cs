using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickFix;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickFixTeste
{
    class MessageTranslator
    {

        public string TranslateMessage(Message msg)
        {

            string[] vetorMsg = msg.ToString().Split("\x0001");
            string[] div;
            var msgJ = new Menssagem();
            foreach (string parte in vetorMsg)
            {
                if (!string.IsNullOrEmpty(parte))
                {
                    div = parte.Split("=");
                    switch (div[0])
                    {
                        case
                            "8":
                            msgJ.BeginString = div[1];
                            break;
                        case
                            "9":
                            msgJ.BodyLenght = div[1];
                            break;
                        case
                            "35":
                            msgJ.MsgType = div[1];
                            break;
                        case
                            "34":
                            msgJ.MsgSeqNum = div[1];
                            break;
                        case
                            "49":
                            msgJ.SenderCompID = div[1];
                            break;
                        case
                            "52":
                            msgJ.SendingTime = div[1];
                            break;
                        case
                            "56":
                            msgJ.TargetCompID = div[1];
                            break;
                        case
                            "98":
                            msgJ.EncryptMethod = div[1];
                            break;
                        case
                            "108":
                            msgJ.HeartBtInt = div[1];
                            break;
                        case
                            "7":
                            msgJ.BeginSeqNo = div[1];
                            break;
                        case
                            "16":
                            msgJ.EndSeqNo = div[1];
                            break;
                        case
                            "10":
                            msgJ.CheckSum = div[1];
                            break;
                        case
                            "112":
                            msgJ.TestReqID = div[1];
                            break;
                    }
                }

            }
            string jsonMsg = JsonSerializer.Serialize(msgJ);
            return jsonMsg;
        }


    }
}
