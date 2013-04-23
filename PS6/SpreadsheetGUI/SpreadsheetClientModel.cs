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
        private string hostName;

		private Queue<string> buffer;

		/*
		 * Register Events to handle calls from the server 
		 */

		public event Action<String, String> CreateOKEvent;

		public event Action<String, String> CreateFailEvent;

		public event Action<String, String> JoinFailEvent;

		public event Action<String, String, int, String> JoinOKEvent;

		public event Action<String, String> ChangeOKEvent;

		public event Action<String, String> ChangeFailEvent;

        public event Action<String, String> ChangeWaitEvent;

        public event Action<String, String, String, int, String> UndoOKEvent;

        public event Action<String, String> UndoFailEvent;

		public event Action<String, String> UndoEndEvent;

		public event Action<String, String> UndoWaitEvent;

		public event Action<String, String, String, int, String> UpdateEvent;

		public event Action<String> SaveOKEvent;

		public event Action<String, String> SaveFailEvent;

		public event Action<String> Debug;

		public event Action NullMessageReceivedEvent;

		public event Action<Exception> ConnectionErrorEvent;

        public event Action ErrorEvent;


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
            buffer = new Queue<string>();
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
            this.hostName = hostName;
			//Try to connect with the given server and the default port.
			try
			{
				if (socket == null)
				{
					TcpClient client = new TcpClient(hostName, serverPort);
					socket = new StringSocket(client.Client, UTF8Encoding.Default);
					//socket.BeginSend("STUMP" + "\n", (e, o) => { }, null);
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
			string msg, name, password, content, cell, version, xml;
            int length;

			/*Not Done with swithc-cases below */

			//I need to implement a queue of strings
			//The queue will look much like that of the server
			buffer.Enqueue(line);

			Debug(line);

			if(buffer.Count == 1)
			{
				msg = buffer.Peek();
                if (msg == null)
                {
                    //close the connection
                    buffer.Clear();
                    this.CloseConnection();
                    ConnectToServer(hostName);
                    return;
                    //NullMessageReceivedEvent();
                }
                else if (msg.StartsWith("ERROR"))
                {
                    buffer.Clear();
                    ErrorEvent();
                }
			}
            else if (buffer.Count == 2)
            {
                msg = buffer.Peek();
                if (msg.StartsWith("SAVE OK"))
                {
                    buffer.Dequeue();
                    name = buffer.Dequeue().Substring(5);

                    buffer.Clear();
                    SaveOKEvent(name);
                }
            }
			else if(buffer.Count == 3)
			{
				msg = buffer.Peek();
				if (msg.StartsWith("CREATE FAIL"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    msg = buffer.Dequeue();

                    buffer.Clear();
				    CreateFailEvent(name, msg);
				}
				else if (msg.StartsWith("CREATE OK"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    password = buffer.Dequeue().Substring(9);

                    buffer.Clear();
                    CreateOKEvent(name, password);
				}
				else if (msg.StartsWith("JOIN FAIL"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    msg = buffer.Dequeue();

                    buffer.Clear();
                    JoinFailEvent(name, msg);
				}
				else if (msg.StartsWith("CHANGE OK"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    version = buffer.Dequeue().Substring(8);

                    buffer.Clear();
                    ChangeOKEvent(name, version);
				}
				else if (msg.StartsWith("CHANGE WAIT"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
                    version = buffer.Dequeue().Substring(8);

                    buffer.Clear();
                    ChangeWaitEvent(name, version);
				}
				else if (msg.StartsWith("CHANGE FAIL"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
                    version = buffer.Dequeue().Substring(8);

                    buffer.Clear();
                    ChangeFailEvent(name, version);
				}
				else if (msg.StartsWith("UNDO END"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
                    version = buffer.Dequeue().Substring(8);

                    buffer.Clear();
                    UndoEndEvent(name, version);
				}
				else if (msg.StartsWith("UNDO WAIT"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
                    version = buffer.Dequeue().Substring(8);

                    buffer.Clear();
                    UndoWaitEvent(name, version);
				}
				else if (msg.StartsWith("UNDO FAIL"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    msg = buffer.Dequeue();

                    buffer.Clear();
                    UndoFailEvent(name, msg);
				}
				else if (msg.StartsWith("SAVE FAIL"))
				{
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    msg = buffer.Dequeue();

                    buffer.Clear();
                    SaveFailEvent(name, msg);
				}
			}
            else if(buffer.Count == 5)
            {
                msg = buffer.Peek();
			    if (msg.StartsWith("JOIN OK"))
			    {
				    buffer.Dequeue();
				    name = buffer.Dequeue().Substring(5);
				    version = buffer.Dequeue().Substring(8);
				    length = Convert.ToInt32(buffer.Dequeue().Substring(7));
				    xml = buffer.Dequeue();

                    buffer.Clear();
                    JoinOKEvent(name, version, length, xml);
			    }
            }
            else if(buffer.Count == 6)
            {
                msg = buffer.Peek();
				if(msg.StartsWith("UNDO OK"))
				{
					buffer.Dequeue();
					name = buffer.Dequeue().Substring(5);
					version = buffer.Dequeue().Substring(8);
					cell = buffer.Dequeue().Substring(5);
					length = Convert.ToInt32(buffer.Dequeue().Substring(7));
					content = buffer.Dequeue();

                    buffer.Clear();
                    UndoOKEvent(name, version, cell, length, content);
				}
				else if (msg.StartsWith("UPDATE"))
				{
					buffer.Dequeue();
					name = buffer.Dequeue().Substring(5);
					version = buffer.Dequeue().Substring(8);
					cell = buffer.Dequeue().Substring(5);
					length = Convert.ToInt32(buffer.Dequeue().Substring(7));
					content = buffer.Dequeue();

                    buffer.Clear();
                    UpdateEvent(name, version, cell, length, content);
				}
            }
            else if(buffer.Count > 7)
            {
				buffer.Clear();
            }

            if (socket != null)
				socket.BeginReceive(LineReceived, null);
					
		}
			
			
			//string message, name;
            //if (line == null)
            //{
            //    //close the connection
            //    buffer.Clear();
            //    this.CloseConnection();
            //    NullMessageReceivedEvent();
            //}
            //else if (line.StartsWith("CREATE FAIL"))
            //{
            //    CreateFailEvent(line);
            //}
            //else if (line.StartsWith("CREATE OK"))
            //{
            //    CreateOKEvent(line);
            //}
            //else if (line.StartsWith("JOIN FAIL"))
            //{
            //    JoinFailEvent(line);
            //}
            //else if (line.StartsWith("JOIN OK"))
            //{
            //    JoinOKEvent(line);
            //}
            //else if (line.StartsWith("CHANGE FAIL"))
            //{
            //    ChangeFailEvent(line);
            //}
            //else if (line.StartsWith("CHANGE OK"))
            //{
            //    ChangeOKEvent(line);
            //}
            //else if (line.StartsWith("CHANGE WAIT"))
            //{
            //    ChangeWaitEvent(line);
            //}
            //else if (line.StartsWith("UNDO OK"))
            //{
            //    UndoOKEvent(line);
            //}
            //else if (line.StartsWith("UNDO END"))
            //{
            //    UndoEndEvent(line);
            //}
            //else if (line.StartsWith("UNDO WAIT"))
            //{
            //    UndoWaitEvent(line);
            //}
            //else if (line.StartsWith("UNDO FAIL"))
            //{
            //    UndoFailEvent(line);
            //}
            //else if (line.StartsWith("UPDATE"))
            //{
            //    UpdateEvent(line);
            //}
            //else if (line.StartsWith("SAVE OK"))
            //{
            //    SaveOKEvent(line);
            //}
            //else if (line.StartsWith("SAVE FAIL"))
            //{
            //    SaveFailEvent(line);
            //}
		   
            //if (socket != null)
            //    socket.BeginReceive(LineReceived, null);
	}  

}
