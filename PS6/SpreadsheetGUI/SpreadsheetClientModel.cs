using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomNetworking;
using System.Net.Sockets;


namespace SpreadsheetClient
{
    class SpreadsheetClientModel
    {
        //socket used to communicate with the server
        private StringSocket socket;

        /*
         * Register Events to handle calls from the server 
         */

        public event Action<String> CreateOKEvent;

        public event Action<String> CreateFailEvent;

        public event Action<String> JoinFailEvent;

        public event Action<String> JoinOKEvent;

        public event Action<String> ChangeOKEvent;

        public event Action<String> ChangeFailEvent;

        public event Action NullMessageReceivedEvent;

        public event Action<Exception> ConnectionErrorEvent;

        public event Action<String> UndoOKEvent;

        public event Action<String> UndoFailEvent;

        public event Action<String> UndoEndEvent;

        public event Action<String> UpdateEvent;

        public event Action<String> SaveOKEvent;

        public event Action<String> SaveFailEvent;

        //bool tracks client connection
        private bool isConnected;

        //declare public Getter for IsConnected value.
        public bool IsConnected { get { return isConnected; } set { } }

        //Protocol port is 1984
        private int serverPort = 1984;


        /// <summary>
        /// Instantiates that there is no connection yet, and the socket is null, awaiting
        /// action.
        /// </summary>
        public SpreadsheetClientModel()
        {
            isConnected = false;
            socket = null;
        }


        /// <summary>
        /// Closes the socket connection between the client and server.
        /// </summary>
        public void CloseConnection()
        {
            //If the socket isn't null, shutdown the socket
            //reset the socket to null, and reset the connection to false.
            if (socket != null)
            {
                socket.ShutDown();
                socket = null;
                isConnected = false;
            }
            else
                return; //continue to close.
        }


        /// <summary>
        /// Connects the Client to the server taking a server hostname
        /// and a player name.
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="playerName"></param>
        public void ConnectToServer(string hostName)
        {
            //Try to connect with the given server and the default port.
            try
            {
                if (socket == null)
                {
                    TcpClient client = new TcpClient(hostName, serverPort);
                    socket = new StringSocket(client.Client, UTF8Encoding.Default);
                    socket.BeginSend("STUMP", (e, o) => { }, null);
                    isConnected = true;
                    socket.BeginReceive(LineReceived, null);
                }
            }
            //If the connection can't be established, close it and handle the error.
            catch (Exception e)
            {
                this.CloseConnection();
                ConnectionErrorEvent(e);
            }
        }


        /// <summary>
        /// Send a line of text to the server.
        /// </summary>
        /// <param name="line"></param>
        public void SendMessage(String line)
        {
            if (socket != null)
            {
                socket.BeginSend(line + "\n", (e, p) => { }, null);
            }
        }


        /// <summary>
        /// Takes the sent string from the server with the exception and payload.
        /// Parse the string and handle its intention.
        /// If the String is null, close the connection.
        /// If the String begins with 'CREATE FAIL', 
        /// If the String begins with 'CREATE OK', 
        /// If the String begins with 'JOIN FAIL', 
        /// If the String begins with 'JOIN OK', 
        /// If the String begins with 'CHANGE FAIL', 
        /// If the String begins with 'CHANGE OK', 
        /// If the String begins with 'UNDO FAIL', 
        /// If the String begins with 'UNDO OK', 
        /// If the String begins with 'UNDO END', 
        /// If the String begins with 'UPDATE', 
        /// If the String begins with 'SAVE OK', 
        /// If the String begins with 'SAVE FAIL', 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="e"></param>
        /// <param name="p"></param>
        private void LineReceived(String line, Exception e, object p)
        {
            //string message, name;
            if (line == null)
            {
                //close the connection
                this.CloseConnection();
                NullMessageReceivedEvent();
            }
            else if (line.StartsWith("CREATE FAIL"))
            {
                //close the model and inform the player that his opponent disconnected
                this.CloseConnection();
                CreateFailEvent(line);
            }
            else if (line.StartsWith("CREATE OK"))
            {
                CreateOKEvent(line);
            }
            //If the message is a starting message
            else if (line.StartsWith("JOIN FAIL"))
            {
                JoinFailEvent(line);
            }
            //if the time is being sent
            else if (line.StartsWith("JOIN OK"))
            {
                JoinOKEvent(line);
            }
            else if (line.StartsWith("CHANGE FAIL"))
            {
                ChangeFailEvent(line);
            }
            else if (line.StartsWith("CHANGE OK"))
            {
                ChangeOKEvent(line);
            }
            else if (line.StartsWith("UNDO OK"))
            {
                UndoOKEvent(line);
            }
            else if (line.StartsWith("UNDO END"))
            {
                UndoEndEvent(line);
            }
            else if (line.StartsWith("UNDO FAIL"))
            {
                UndoFailEvent(line);
            }
            else if (line.StartsWith("UPDATE"))
            {
                UpdateEvent(line);
            }
            else if (line.StartsWith("SAVE OK"))
            {
                SaveOKEvent(line);
            }
            else if (line.StartsWith("SAVE FAIL"))
            {
                SaveFailEvent(line);
            }
           
            if (socket != null)
                socket.BeginReceive(LineReceived, null);
        }  

    }
}
