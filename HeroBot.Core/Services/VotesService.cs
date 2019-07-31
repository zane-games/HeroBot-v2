using Discord.WebSocket;
using HeroBot.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HeroBot.Core.Services
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket { get; set; } = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer { get; set; } = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb { get; set; } = new StringBuilder();
    }

    public class AsynchronousSocketListener : IVoteService
    {
        // Thread signal.  
        public static ManualResetEvent allDone { get; set; } = new ManualResetEvent(false);
        private readonly DiscordShardedClient _discord;

        public event IVoteService.OnVoteHandler VoteHandler;

        public AsynchronousSocketListener(DiscordShardedClient discordShardedClient)
        {
            _discord = discordShardedClient;
            new Thread(() => { StartListening(); }).Start();
            Console.WriteLine("Lunched socket connector");
        }

        public void StartListening()
        {

            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 9005);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(IPAddress.Any.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            Send(state.workSocket, "HeroBot Sharded Protocol");
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                Console.WriteLine(content);
                if (content.EndsWith("<EOF>"))
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine(String.Format("Read {0} bytes from socket.",
                        content.Length));
                    state.sb.Clear();
                    try
                    {
                        var dunamic = JsonConvert.DeserializeObject<dynamic>(content.Replace("<EOF>",String.Empty));
                        VoteHandler.Invoke(new DblVote()
                        {
                            user = _discord.GetUser((ulong)dunamic.userId),
                            isPrimed = dunamic.isPrimed
                        });
                    }
                    catch (Exception exx) { Console.WriteLine(exx); }
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine(String.Format("Sent {0} bytes to client.", bytesSent));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
