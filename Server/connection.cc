#include "connection.h"

connection::connection(tcp::socket *socket)
{
  this->socket = socket;
}

tcp::socket& connection::get_socket()
{
  return *(this->socket);
}

//Starts the connection
void connection::start()
{
  
  std::cout << "Socket connected." << std::endl;
  
  // socket_.async_read_some(boost::asio::buffer(data_, max_length),
  //     boost::bind(&connection::handle_read, this,
  //       boost::asio::placeholders::error,
  //       boost::asio::placeholders::bytes_transferred));
  //  boost::asio::async_read_until(*socket, buffer, '\n', 
  //				boost::bind(&connection::handle_read, this,
  //					    boost::asio::placeholders::error,
  //					    boost::asio::placeholders::bytes_transferred));
}


//Broadcasts a message to the connected socket
void connection::send_message(std::string message)
{
  
  std::string line = "You are hearing me talk.";
  
  boost::asio::async_write(*socket,
			   boost::asio::buffer(message),
			   boost::bind(&connection::handle_write, this,
				       boost::asio::placeholders::error));
}

// void handle_read(const boost::system::error_code& error,
//     size_t bytes_transferred)
void connection::handle_read(const boost::system::error_code & error, std::size_t size, boost::function<void(Message, connection*)> func)
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

      Message msg;
      msg.type = MESSAGE_CREATE;
      func(msg ,this);
      // boost::asio::async_write(*socket,
      //         boost::asio::buffer(line, size),
      //         boost::bind(&connection::handle_write, this,
      //         boost::asio::placeholders::error));


    }
    else
    {
      delete this;
    }
}

void connection::handle_write(const boost::system::error_code& error)
{
  if (!error)
    {
      // socket_.async_read_some(boost::asio::buffer(data_, max_length),
      //     boost::bind(&connection::handle_read, this,
      //       boost::asio::placeholders::error,
      //       boost::asio::placeholders::bytes_transferred));
      
      
      
      
      
      // boost::asio::async_read_until(*socket, buffer, '\n',
      // 				    boost::bind(&connection::handle_read, this, 
      // 						boost::asio::placeholders::error,
      // 						boost::asio::placeholders::bytes_transferred));
      
    }
  else
    {
      delete this;
    }
}

void connection::read_message(boost::function<void(Message, connection*)> func)
{
      boost::asio::async_read_until(*socket, buffer, '\n',
				    boost::bind(&connection::handle_read, this,
						boost::asio::placeholders::error,
						boost::asio::placeholders::bytes_transferred,
						func
));
}
