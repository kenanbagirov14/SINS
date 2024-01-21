using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Web;
using System.Collections;
using System.Threading;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Addressing;
using Microsoft.Web.Services3.Messaging;
using System.Xml.Serialization;
using System.Xml;

namespace NIS.ServiceCore.MailOperations
{
  enum RState
   {
      METHOD, URL, URLPARM, URLVALUE, VERSION, 
      HEADERKEY, HEADERVALUE, BODY, OK
   };

   enum RespState
   {
      OK = 200, 
      BAD_REQUEST = 400,
      NOT_FOUND = 404
   }

   public struct HTTPRequestStruct
   {
      public string Method;
      public string URL;
      public string Version;
      public Hashtable Args;
      public bool Execute;
      public Hashtable Headers;
      public int BodySize;
      public byte[] BodyData;
   }

   public struct HTTPResponseStruct
   {
      public int status;
      public string version;
      public Hashtable Headers;
      public int BodySize;
      public byte[] BodyData;
   }

   /// <SUMMARY>

   /// Summary description for CsHTTPRequest.

   /// </SUMMARY>

   public class CSHTTPRequest
   {
       private TcpClient _client;

       private RState _parserState;

       private HTTPRequestStruct _httpRequest;

       private HTTPResponseStruct _httpResponse;

       private SendNotificationResultType _result;

       //private Notify _parent;
       
       byte[] myReadBuffer;


       //public CSHTTPRequest(TcpClient Client, Notify Parent)
       //{
       //    _client = Client;
       //    _parent = Parent;

       //    _httpResponse.BodySize = 0;
       //}

       public void Process()
       {
           
           myReadBuffer = new byte[_client.ReceiveBufferSize];
           String myCompleteMessage = "";
           int numberOfBytesRead = 0;

           NetworkStream ns = _client.GetStream();

           string hValue = "";
           string hKey = "";

           try
           {
               // binary data buffer index

               int bfndx = 0;

               // Incoming message may be larger than the buffer size.

               do
               {
                   numberOfBytesRead = ns.Read(myReadBuffer, 0,
                                               myReadBuffer.Length);
                   myCompleteMessage =
                      String.Concat(myCompleteMessage,
                         Encoding.ASCII.GetString(myReadBuffer, 0,
                                                  numberOfBytesRead));

                   // read buffer index

                   int ndx = 0;
                   do
                   {
                       switch (_parserState)
                       {
                           case RState.METHOD:
                               if (myReadBuffer[ndx] != ' ')
                                   _httpRequest.Method += (char)myReadBuffer[ndx++];
                               else
                               {
                                   ndx++;
                                   _parserState = RState.URL;
                               }
                               break;
                           case RState.URL:
                               if (myReadBuffer[ndx] == '?')
                               {
                                   ndx++;
                                   hKey = "";
                                   _httpRequest.Execute = true;
                                   _httpRequest.Args = new Hashtable();
                                   _parserState = RState.URLPARM;
                               }
                               else if (myReadBuffer[ndx] != ' ')
                                   _httpRequest.URL += (char)myReadBuffer[ndx++];
                               else
                               {
                                   ndx++;
                                   _httpRequest.URL
                                            = HttpUtility.UrlDecode(_httpRequest.URL);
                                   _parserState = RState.VERSION;
                               }
                               break;
                           case RState.URLPARM:
                               if (myReadBuffer[ndx] == '=')
                               {
                                   ndx++;
                                   hValue = "";
                                   _parserState = RState.URLVALUE;
                               }
                               else if (myReadBuffer[ndx] == ' ')
                               {
                                   ndx++;

                                   _httpRequest.URL
                                            = HttpUtility.UrlDecode(_httpRequest.URL);
                                   _parserState = RState.VERSION;
                               }
                               else
                               {
                                   hKey += (char)myReadBuffer[ndx++];
                               }
                               break;
                           case RState.URLVALUE:
                               if (myReadBuffer[ndx] == '&')
                               {
                                   ndx++;
                                   hKey = HttpUtility.UrlDecode(hKey);
                                   hValue = HttpUtility.UrlDecode(hValue);
                                   _httpRequest.Args[hKey] =
                                        _httpRequest.Args[hKey] != null ?
                                            _httpRequest.Args[hKey] + ", " + hValue :
                                            hValue;
                                   hKey = "";
                                   _parserState = RState.URLPARM;
                               }
                               else if (myReadBuffer[ndx] == ' ')
                               {
                                   ndx++;
                                   hKey = HttpUtility.UrlDecode(hKey);
                                   hValue = HttpUtility.UrlDecode(hValue);
                                   _httpRequest.Args[hKey] =
                                        _httpRequest.Args[hKey] != null ?
                                           _httpRequest.Args[hKey] + ", " + hValue :
                                           hValue;

                                   _httpRequest.URL
                                           = HttpUtility.UrlDecode(_httpRequest.URL);
                                   _parserState = RState.VERSION;
                               }
                               else
                               {
                                   hValue += (char)myReadBuffer[ndx++];
                               }
                               break;
                           case RState.VERSION:
                               if (myReadBuffer[ndx] == '\r')
                                   ndx++;
                               else if (myReadBuffer[ndx] != '\n')
                                   _httpRequest.Version += (char)myReadBuffer[ndx++];
                               else
                               {
                                   ndx++;
                                   hKey = "";
                                   _httpRequest.Headers = new Hashtable();
                                   _parserState = RState.HEADERKEY;
                               }
                               break;
                           case RState.HEADERKEY:
                               if (myReadBuffer[ndx] == '\r')
                                   ndx++;
                               else if (myReadBuffer[ndx] == '\n')
                               {
                                   ndx++;
                                   if (_httpRequest.Headers["Content-Length"] != null)
                                   {
                                       _httpRequest.BodySize =
                                Convert.ToInt32(_httpRequest.Headers["Content-Length"]);
                                       _httpRequest.BodyData
                                                = new byte[_httpRequest.BodySize];
                                       _parserState = RState.BODY;
                                   }
                                   else
                                       _parserState = RState.OK;

                               }
                               else if (myReadBuffer[ndx] == ':')
                                   ndx++;
                               else if (myReadBuffer[ndx] != ' ')
                                   hKey += (char)myReadBuffer[ndx++];
                               else
                               {
                                   ndx++;
                                   hValue = "";
                                   _parserState = RState.HEADERVALUE;
                               }
                               break;
                           case RState.HEADERVALUE:
                               if (myReadBuffer[ndx] == '\r')
                                   ndx++;
                               else if (myReadBuffer[ndx] != '\n')
                                   hValue += (char)myReadBuffer[ndx++];
                               else
                               {
                                   ndx++;
                                   _httpRequest.Headers.Add(hKey, hValue);
                                   hKey = "";
                                   _parserState = RState.HEADERKEY;
                               }
                               break;
                           case RState.BODY:
                               // Append to request BodyData

                               Array.Copy(myReadBuffer, ndx,
                                  _httpRequest.BodyData,
                                  bfndx, numberOfBytesRead - ndx);
                               bfndx += numberOfBytesRead - ndx;
                               ndx = numberOfBytesRead;
                               if (_httpRequest.BodySize <= bfndx)
                               {
                                   _parserState = RState.OK;
                               }
                               break;
                           //default:

                           //   ndx++;

                           //   break;


                       }
                   }
                   while (ndx < numberOfBytesRead);

                   Thread.Sleep(500);
               }
               while (ns.DataAvailable);

               if (_httpRequest.BodyData != null)
               {

                   string soapMessage = Encoding.ASCII.GetString(_httpRequest.BodyData);

                   ParseBody(soapMessage);

                   MemoryStream ms = new MemoryStream();

                   XmlSerializer mySerializer = new XmlSerializer(_result.GetType());
                   mySerializer.Serialize(ms, _result);

                   // Send the result
                   _httpResponse.version = "HTTP/1.1";
                   _httpResponse.BodyData = ms.ToArray();

                   _httpResponse.Headers = new Hashtable();
                  // _httpResponse.Headers.Add("Server", _parent.Name);
                   _httpResponse.Headers.Add("Date", DateTime.Now.ToString("r"));
                   _httpResponse.Headers.Add("Content-Type", "text/xml; charset=utf-8");
                   _httpResponse.Headers.Add("Content-Length", _httpResponse.BodyData.Length);
                   _httpResponse.Headers.Add("Connection", "close");


                   string HeadersString = _httpResponse.version + " "
                      + "200 OK" + "\r\n";

                   foreach (DictionaryEntry Header in _httpResponse.Headers)
                   {
                       HeadersString += Header.Key + ": " + Header.Value + "\r\n";
                   }

                   HeadersString += "\r\n";

                   byte[] bHeadersString = Encoding.ASCII.GetBytes(HeadersString);

                   // Send headers   
                   ns.Write(bHeadersString, 0, bHeadersString.Length);


                   // Send body
                   if (_httpResponse.BodyData != null)
                       ns.Write(_httpResponse.BodyData, 0,
                                _httpResponse.BodyData.Length);


               }
           }
         catch (Exception e) 
         {
           // _parent.WriteLog(e.ToString());
         }
         finally 
         {
            ns.Close();
            _client.Close();
         }
      }

       private void ParseBody(string Body)
       {
           
           SoapEnvelope soapEnvelope = new SoapEnvelope();
                   
           soapEnvelope.InnerXml = Body;
           
           Type type = typeof(SendNotificationResponseType);

           XmlRootAttribute xRoot = new XmlRootAttribute();
           xRoot.ElementName = "SendNotification";
           xRoot.Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages";
           XmlSerializer serializer = new XmlSerializer(type, xRoot);
           XmlNodeReader reader = new XmlNodeReader(soapEnvelope.Body.FirstChild);


           SendNotificationResponseType obj = (SendNotificationResponseType)serializer.Deserialize(reader);

           if (obj.ResponseMessages != null)
           {
               //PushNotification pn = new PushNotification(_parent);

               //_result = pn.ProcessNotification(obj);

           }

       }

   }



}
