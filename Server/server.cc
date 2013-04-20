//Server class
#include <cstdlib>
#include <iostream>
#include <boost/bind.hpp>
#include <boost/asio.hpp>
#include <map>


using boost::asio::ip::tcp;

class connection;

class server
{

//Map of filenames to 
public:
  server(boost::asio::io_service& io_service, short port)
    : io_service_(io_service),
      acceptor_(io_service, tcp::endpoint(tcp::v4(), port))
  {
    start_accept(); //First callback method, creates a new connection
  }

private:
  void start_accept()
  {
    connection* new_connection = new connection(io_service_, *this);
    acceptor_.async_accept(new_connection->socket(),
        boost::bind(&server::handle_accept, this, new_connection,
          boost::asio::placeholders::error));
  }

  void handle_accept(connection* new_connection,
      const boost::system::error_code& error)
  {
    if (!error)
    {
      new_connection->start();
    }
    else
    {
      delete new_connection;
    }

    start_accept();
  }

  boost::asio::io_service& io_service_;
  tcp::acceptor acceptor_;

  //Holds a list of sessions mapped to a filenames
  std::map<std::string, session> sessions;

};