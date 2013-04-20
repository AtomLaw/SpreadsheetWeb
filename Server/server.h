#ifndef SERVER_H
#define SERVER_H


#include <cstdlib>
#include <iostream>
#include <boost/bind.hpp>
#include <boost/asio.hpp>
#include <map>



#include "connection.h"

class session;

using boost::asio::ip::tcp;
//**************************************
//SERVER CLASS
//**************************************

class server
{

//Map of filenames to 
public:
  server(boost::asio::io_service& io_service, short port);


  //Creates a session and adds the connection to that session
  bool create_session(connection & connection_, std::string filename);


private:
  void start_accept();

  void handle_accept(tcp::socket* socket,
		     const boost::system::error_code& error);

  void handle_message(Message msg, connection* conn);

  boost::asio::io_service& io_service_;
  tcp::acceptor acceptor;

  //Holds a list of sessions mapped to a filenames
  std::map<std::string, session*> sessions;
};

#endif
