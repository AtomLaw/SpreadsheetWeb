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


// Message connection::process_string()
// {
  
//   std::string line;
//   int number_of_lines = line_buffer.size();
//   line = line_buffer.front();

//   switch (number_of_lines)
//   {
//     case 2:
      
//       if (line == "SAVE")
//       {

//       } else if (line == "LEAVE") 
//       {

//       } 

//     break;
    
//     case 3:
//       if (line == "CREATE")
//       {

//       } else if (line == "JOIN")
//       {

//       } else if (line == "UNDO")
//       {

//       }
      

//     break;
    
//     case 6:
      
//     break;
//   }
// }

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

      if (!line.empty() && line[line.size() - 1] == '\r')
        line.erase(line.size() - 1);

      std::cout << "Received string : " << line << std::endl;



      line_buffer.push_back(line);

      int number_of_lines = line_buffer.size();

      std::cout << "Number of lines in the linbe buffer: " << number_of_lines << std::endl;

      Message msg;
      std::string message;

      if (number_of_lines == 2)
      {
        //Process Save
        //Process Leave
        message = line_buffer.front();
        if (message == "SAVE")
        {
          // msg.type = MESSAGE_SAVE;


          //Clear the buffer
          line_buffer.clear();
        } else if (message == "LEAVE")
        {


          //Clear the buffer
          line_buffer.clear();
        } else
        {
          read_message(func);
          return;
        }

      }
      else if (number_of_lines == 3)
      {
        //Process create, join, undo
        message = line_buffer.front();
        // std::cout << "." << message << "." << std::endl;
        std::cout << "Length of msg: " << message.length() << std::endl;
        // if (strncmp(message.c_str(), "CREATE", 7) == 0)
        if (message == "CREATE")
        {

          msg.type = MESSAGE_CREATE;
          
          // std::cout << "Entered the create phase." << std::endl;

          char buffer[100];
          sscanf(line_buffer[1].c_str(), "Name:%s", buffer);
          msg.create.name = std::string(buffer);

          // std::cout << "name : " << msg.create.name << std::endl;

          char buff2[100];
          sscanf(line_buffer[2].c_str(), "Password:%s", buff2);
          msg.create.password = std::string(buff2);

          // std::cout << "password : " << msg.create.password << std::endl;

        } else if (message == "JOIN")
        {

        } else if (message == "UNDO")
        {

        } 
        else
        {
          read_message(func);
          return;
        }

      }
      else if (number_of_lines == 6)
      {

        //Process change
        message = line_buffer.front();
        if (message == "CHANGE")
        {

        } else
        {
          //ERROR, clear buffer
        }


      }
      else if (number_of_lines > 6)
      {
        //ERROR
      } 
      else
      {
        read_message(func);
        return;
      }

      
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
