
/*
*Server outline for spreadsheet web program.
*/
#include <cstdlib>
#include <list>
#include <iostream>
#include <boost/bind.hpp>
#include <boost/asio.hpp>
#include <map>
#include "spreadsheet.h"


using boost::asio::ip::tcp;

class server;
class session;



//**************************************
//CONNECTION CLASS
//**************************************



class connection 
{
public:
  connection(boost::asio::io_service& io_service, server & server_)
    : socket_(io_service) 
  {
    my_server = &server_;
    is_part_of_session = false;
  }

  tcp::socket& socket()
  {
    return socket_;
  }

  //Starts the connection
  void start()
  {

    std::cout << "Socket connected." << std::endl;

    // socket_.async_read_some(boost::asio::buffer(data_, max_length),
    //     boost::bind(&connection::handle_read, this,
    //       boost::asio::placeholders::error,
    //       boost::asio::placeholders::bytes_transferred));
    boost::asio::async_read_until(socket_, buffer, '\n', 
                                  boost::bind(&connection::handle_read, this,
                                  boost::asio::placeholders::error,
                                  boost::asio::placeholders::bytes_transferred));
  }

  //Adds this connection to a session
  bool add_to_session(session & session_)
  {
    my_session = &session_;
  }

  //Broadcasts a message to the connected socket
  void broadcast_message(std::string message)
  {

    std::string line = "You are hearing me talk.";

    boost::asio::async_write(socket_,
              boost::asio::buffer(line),
              boost::bind(&connection::handle_write, this,
              boost::asio::placeholders::error));
  }

private:
  // void handle_read(const boost::system::error_code& error,
  //     size_t bytes_transferred)
  void handle_read(const boost::system::error_code & error, std::size_t size)
  {
    if (!error)
    {
      std::cout << "Number of bytes transferred:  " << size << std::endl;

      // boost::asio::async_write(socket_,
      //     boost::asio::buffer(data_, bytes_transferred),
      //     boost::bind(&connection::handle_write, this,
      //       boost::asio::placeholders::error));
      
      //Parse the string up to the delimiter
      std::istream is(&buffer);
      std::string line;
      std::getline(is, line);

      std::cout << "Received string : " << line << std::endl;

      boost::asio::async_write(socket_,
              boost::asio::buffer(line, size),
              boost::bind(&connection::handle_write, this,
              boost::asio::placeholders::error));


    }
    else
    {
      delete this;
    }
  }

  void handle_write(const boost::system::error_code& error)
  {
    if (!error)
    {
      // socket_.async_read_some(boost::asio::buffer(data_, max_length),
      //     boost::bind(&connection::handle_read, this,
      //       boost::asio::placeholders::error,
      //       boost::asio::placeholders::bytes_transferred));



      

      boost::asio::async_read_until(socket_, buffer, '\n',
                                  boost::bind(&connection::handle_read, this, 
                                  boost::asio::placeholders::error,
                                  boost::asio::placeholders::bytes_transferred));

    }
    else
    {
      delete this;
    }
  }

  //Member variables

  tcp::socket socket_;
  // enum { max_length = 1024 };
  // char data_[max_length];
  boost::asio::streambuf buffer; //Holds incoming bytes
  // boost::system::error_code error;
  server * my_server; //The server this connection is a part of
  bool is_part_of_session; //Tells whether this connection is part of a session
  session * my_session; //The session the connection is a part of
};



//**************************************
//SERVER CLASS
//**************************************


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

  //Creates a session and adds the connection to that session
  bool create_session(connection & connection_, std::string filename)
  {
    // session session_(filename); //Create the session
    // session_.join
    return true;
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



//**************************************
//SESSION CLASS
//**************************************

class session 
{

public:

  session(std::string filename) : my_spreadsheet(filename)
  {
    filename_ = filename;
  }

  //Joins a connection to the session
  void join(connection::connection & a_connection)
  {
    connections.push_back(&a_connection);
  }

private:

  std::string filename_;
  spreadsheet::spreadsheet my_spreadsheet;
  std::list<connection *> connections;


};


int main()
{
  try
  {

    boost::asio::io_service io_service;

    using namespace std; 
    server::server s(io_service, 1984); //Open a server on port 1984

    io_service.run();
  }
  catch (std::exception& e)
  {
    std::cerr << "Exception: " << e.what() << "\n";
  }

  return 0;
}

//Code includes example from:
//
// async_tcp_echo_server.cpp
// ~~~~~~~~~~~~~~~~~~~~~~~~~
//
// Copyright (c) 2003-2012 Christopher M. Kohlhoff (chris at kohlhoff dot com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
//