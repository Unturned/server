//
//  Author:
//    Paál Gyula paalgyula@gmail.com
//
//  Copyright (c) 2015, (c) GW-Systems Kft. All Rights Reserved!
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//------------------------------------------------------------------------------
// <auto-generated>
//     Ezt a kódot eszköz generálta.
//     Futásidejű verzió:4.0.30319.0
//
//     Ennek a fájlnak a módosítása helytelen viselkedést eredményezhet, és elvész, ha
//     a kódot újragenerálják.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using System.Text;

using System.IO;
using System.IO.Pipes;

using System.Net;
using System.Net.Sockets;

namespace RemoteConsole
{
    // State object for reading client data asynchronously
    public class StateObject {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();  
    }
    
    public class RemoteConsole : MonoBehaviour {
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
                
        public IEnumerator StartListening() {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            
            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            
            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp );
            
            // Bind the socket to the local endpoint and listen for incoming connections.
            try {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                
                while (true) {
                    // Set the event to nonsignaled state.
                    allDone.Reset();
                    
                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept( 
                                         new AsyncCallback(AcceptCallback),
                                         listener );
                    
                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
                
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

            yield return null;
        }

        void Start() 
        {
            //StartCoroutine("StartListening");
        }
        
        public static void AcceptCallback(IAsyncResult ar) {
            // Signal the main thread to continue.
            allDone.Set();
            
            // Get the socket that handles the client request.
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            
            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
                                 new AsyncCallback(ReadCallback), state);
        }
        
        public static void ReadCallback(IAsyncResult ar) {
            String content = String.Empty;
            
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.workSocket;
            
            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            
            if (bytesRead > 0) {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer,0,bytesRead));
                
                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1) {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                                      content.Length, content );
                    // Echo the data back to the client.
                    Send(handler, content);
                } else {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                         new AsyncCallback(ReadCallback), state);
                }
            }
        }
        
        private static void Send(Socket handler, String data) {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            
            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                              new AsyncCallback(SendCallback), handler);
        }
        
        private static void SendCallback(IAsyncResult ar) {
            try {
                // Retrieve the socket from the state object.
                Socket handler = (Socket) ar.AsyncState;
                
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

    }
}