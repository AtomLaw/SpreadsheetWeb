#ifndef CONNECTION_H
#define CONNECTION_H

#include <cstdlib>
#include <iostream>
#include <boost/bind.hpp>
#include <boost/asio.hpp>
#include <boost/function.hpp>
#include <map>
#include <queue>
#include <vector>

enum MESSAGE_TYPE
  {
    MESSAGE_CREATE,
    MESSAGE_NEW
  };

union Message
{
  MESSAGE_TYPE type;
  /* struct */
  /* { */
  /*   MESSAGE_TYPE type; */
  /*   std::string name; */
  /*   std::string password; */
  /* } create; */
};

using boost::asio::ip::tcp;

class connection 
{
public:
  connection(tcp::socket *socket);

  tcp::socket& get_socket();

  //Starts the connection
  void start();

  bool poll();
  
  Message get_message();

  void read_message(boost::function<void(Message, connection*)> func);

  void send_message(std::string message);

private:
  // void handle_read(const boost::system::error_code& error,
  //     size_t bytes_transferred)
  void handle_read(const boost::system::error_code & error, std::size_t size, boost::function<void(Message, connection*)> func);

  void handle_write(const boost::system::error_code& error);

  //Member variables
  std::queue<Message> message_queue;

  std::vector<std::string> line_buffer;

  tcp::socket *socket;
  // enum { max_length = 1024 };
  // char data_[max_length];
  boost::asio::streambuf buffer; //Holds incoming bytes
  // boost::system::error_code error;
};


#endif
