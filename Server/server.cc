#include "server.h"

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
    std::cout << "Received Create Message" << std::endl;
    break;
  default:
    std::cout << "ERROR" << std::endl;
  }
  conn->read_message(boost::bind(&server::handle_message, this,
				 _1, _2));
}
