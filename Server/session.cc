#include "session.h"
#include <iostream>

session::session(spreadsheet* ss)
{
  this->ss = ss;
  this->ss->load();
}

session::~session()
{
  this->ss->save();
  delete this->ss;
}

//Joins a connection to the session
void session::join(connection *connection)
{
  std::cout << "User joined Session" << std::endl;

  
  connection->read_message(boost::bind(&session::handle_message, this, _1, _2));
  connections.push_back(connection);
}


void session::handle_message(Message msg, connection *conn)
{
  switch(msg.type)
    {
    case MESSAGE_CREATE:
      break;
    case MESSAGE_JOIN:
      break;
    case MESSAGE_CHANGE:
      if(ss->get_version() == msg.change.version)
	{
	  ss->update_cell(msg.change.cell, msg.change.content);
	  ss->increment_version();

	  std::ostringstream out;
	  out << "UPDATE\n"
	      << "Name:" << msg.change.name << "\n"
	      << "Version:" << ss->get_version() << "\n"
	      << "Cell:" << msg.change.cell << "\n"
	      << "Length:" << msg.change.content.length()<< "\n"
	      << msg.change.content << "\n";

	  std::vector<connection*>::iterator i;
	  for(i = connections.begin(); i != connections.end(); i++)
	    {
	      (*i)->send_message(out.str());
	    }

	  std::ostringstream out_response;
	  out_response << "CHANGE OK\n"
		       << "Name:" << msg.change.name << "\n"
		       << "Version:" << ss->get_version() << "\n";
	  conn->send_message(out_response.str());
	}
      else
	{
	  std::ostringstream out;
	  out << "CHANGE WAIT\n"
	      << "Name:" << msg.change.name << "\n"
	      << "Version:" << ss->get_version() << "\n";
	  conn->send_message(out.str());
	}

      break;
    case MESSAGE_UNDO:
      break;
    case MESSAGE_SAVE:
      {
      this->ss->save();
      std::ostringstream out;
      out << "SAVE OK\n"
	  << "Name:" << msg.save.name << "\n";
      conn->send_message(out.str());
      
      }
      break;
    case MESSAGE_LEAVE:
      return;
      break;
    default:
      break;
    }
  
  conn->read_message(boost::bind(&session::handle_message, this, _1, _2));
}
