#include "server.h"
#include "spreadsheet.h"

#include <sstream>
//Server class


server::server(boost::asio::io_service& io_service, short port)
  : io_service_(io_service),
    acceptor(io_service, tcp::endpoint(tcp::v4(), port))
{
  start_accept(); //First callback method, creates a new connection
}

//Creates a session and adds the connection to that session
bool server::create_session(connection & connection_, std::string filename)
{
  // session session_(filename); //Create the session
  // session_.join
  return true;
}

void server::start_accept()
{
  tcp::socket *socket = new tcp::socket(io_service_);
  //  connection* new_connection = new connection(io_service_, *this);
  acceptor.async_accept(*socket,
			 boost::bind(&server::handle_accept, this, socket,
				     boost::asio::placeholders::error));
}

void server::handle_accept(tcp::socket *socket,
			   const boost::system::error_code& error)
{
  if (!error)
    {
      connection *nc = new connection(socket);
      nc->read_message(boost::bind(&server::handle_message, this,
				   _1, _2));
    }
  else
    {
      delete socket;
    }
  
  start_accept();
}

void server::handle_message(Message msg, connection* conn)
{
  switch(msg.type){
  case MESSAGE_CREATE:
    {    std::cout << "Received Create Message" << std::endl;
    spreadsheet ss(msg.create.name);
    if(ss.exists())
      {
	std::ostringstream out;
	out << "CREATE FAIL\n"
	     << "Name:" << msg.create.name << "\n"
	    << "The spreadsheet already exists!\n";
	conn->send_message(out.str());
      }
    else
      {
	ss.create(msg.create.password);

	std::ostringstream out;
	out << "CREATE OK\n"
	    << "Name:" << msg.create.name << "\n"
	    << "Password:" << msg.create.password << "\n";
	conn->send_message(out.str());
      }	
    conn->read_message(boost::bind(&server::handle_message, this,
				 _1, _2));
    }
    break;


  case MESSAGE_JOIN:
    {
      spreadsheet ss(msg.join.name);
      if(ss.exists())
	{
	  if(ss.authenticate(msg.join.password))
	    {
	      if(sessions.find(msg.join.name) != sessions.end())
		{
		  sessions[msg.join.name]->join(conn);
		}
	      else
		{
		  sessions[msg.join.name] = new session(new spreadsheet(msg.join.name));
		  sessions[msg.join.name]->join(conn);
		}

	      std::string xml = ss.get_xml();
	      int length = xml.length();
	      std::ostringstream out;
	      out << "JOIN OK\n"
		  << "Name:" << msg.join.name << "\n"
		  << "Version:" << ss.get_version() << "\n"
		  << "Length:" << length << "\n"
		  << xml << "\n";
	      conn->send_message(out.str()); 
	      return;
	    }
	  else
	    {
	      std::ostringstream out;
	      out << "JOIN FAIL\n"
		  << "Name:" << msg.join.name << "\n"
		  << "User not valid!\n";
	      conn->send_message(out.str()); 
	    }
	}
      else
	{
	  std::ostringstream out;
	  out << "JOIN FAIL\n"
	      << "Name:" << msg.join.name << "\n"
	      << "Spreadsheet does not exist!\n";
	  conn->send_message(out.str());
	}
    }  
    std::cout << "Received join request" << std::endl;
    break;
  default:
    std::cout << "ERROR" << std::endl;
    
  }
  conn->read_message(boost::bind(&server::handle_message, this,
				 _1, _2));

}
