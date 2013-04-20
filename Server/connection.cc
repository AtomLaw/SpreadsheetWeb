#include <cstdlib>
#include <iostream>
#include <boost/bind.hpp>
#include <boost/asio.hpp>
#include <map>



using boost::asio::ip::tcp;

class server;

class connection //connection
{
public:
  connection(boost::asio::io_service& io_service, server & server_)
    : socket_(io_service) 
  {
    my_server = &server_;
  }

  tcp::socket& socket()
  {
    return socket_;
  }

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
      
      //Parse the string
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

  tcp::socket socket_;
  // enum { max_length = 1024 };
  // char data_[max_length];
  boost::asio::streambuf buffer; //Holds incoming bytes
  // boost::system::error_code error;
  server * my_server; //The server this connection is a part of
};