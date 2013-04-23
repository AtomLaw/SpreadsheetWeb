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

  
  connections.push_back(connection);
}

void session::leave(connection *conn)
{
  std::vector<connection*>::iterator i;
  for(i = connections.begin(); i != connections.end();)
  {
    if((*i) == conn)
    {
     i = connections.erase(i);
   }
   else
   {
     i++;
   }
 }

 std::cout << "Successfully reached end of session::leave" << std::endl;
}


void session::handle_message(Message msg, connection *conn, bool error)
{
  switch(msg.type)
    {
    case MESSAGE_CREATE:
      break;
    case MESSAGE_JOIN:
      break;
    case MESSAGE_CHANGE:
      if(!this->is_connected(conn))
	{
	   std::ostringstream out;
	   out << "CHANGE FAIL\n"
	       << "Name:" << msg.change.name << "\n"
	       << "User is not part of this session" << "\n";
	   conn->send_message(out.str());
	       
	}
      
      
      if(ss->get_version() == msg.change.version)
      {
       //Add message to the undo stack
       undo_oper operation;
       operation.cell = msg.change.cell;
       operation.contents = ss->get_cell_contents(msg.change.cell);
       undo_stack.push_back(operation);

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
      if(!this->is_connected(conn))
	{
	   std::ostringstream out;
	   out << "UNDO FAIL\n"
	       << "Name:" << msg.change.name << "\n"
	       << "User is not part of this session" << "\n";
	   conn->send_message(out.str());
	       
	}

      std::cout << "Processing UNDO message..." << std::endl;
    //If there are no unsaved changes
    if (undo_stack.size() == 0)
    {
      std::cout << "...No changes to undo!" << std::endl;
      std::ostringstream out;
      out << "UNDO END\n"
      << "Name:" << msg.undo.name << "\n"
      << "Version:" << ss->get_version() << "\n";
      conn->send_message(out.str());
    } 
    else if (ss->get_version() == msg.undo.version)
    {
      std::cout << "...Undoing last change" << std::endl;
      //Get the last change to the spreadsheet
      undo_oper operation_to_undo = undo_stack.back();
      undo_stack.pop_back();

      //Update the cell in the spreadsheet
      ss->update_cell(operation_to_undo.cell, operation_to_undo.contents);
      ss->increment_version();

      std::ostringstream out;
      out << "UNDO OK\n"
      << "Name:" << msg.undo.name << "\n"
      << "Version:" << ss->get_version() << "\n"
      << "Cell:" << operation_to_undo.cell << "\n"
      << "Length:" << operation_to_undo.contents.length() << "\n"
      << operation_to_undo.contents << "\n";
      conn->send_message(out.str());

      //Update all other clients
      std::ostringstream out_msg;
       out_msg << "UPDATE\n"
       << "Name:" << msg.undo.name << "\n"
       << "Version:" << ss->get_version() << "\n"
       << "Cell:" << operation_to_undo.cell << "\n"
       << "Length:" << operation_to_undo.contents.length()<< "\n"
       << operation_to_undo.contents << "\n";


      
       std::vector<connection*>::iterator i;
       for(i = connections.begin(); i != connections.end(); i++)
       {
         (*i)->send_message(out_msg.str());
       }


    }
    else //Client code is out of date
    {
      std::cout << "...Version Mismatch" << std::endl;
      std::ostringstream out;
      out << "UNDO WAIT\n"
      << "Name:" << msg.undo.name << "\n"
      << "Version:" << ss->get_version() << "\n";
      conn->send_message(out.str());

    }
      break;

      case MESSAGE_SAVE:
      {
      if(!this->is_connected(conn))
	{
	   std::ostringstream out;
	   out << "SAVE FAIL\n"
	       << "Name:" << msg.change.name << "\n"
	       << "User is not part of this session" << "\n";
	   conn->send_message(out.str());
	       
	}

        this->ss->save();
        std::ostringstream out;
        out << "SAVE OK\n"
        << "Name:" << msg.save.name << "\n";
        conn->send_message(out.str());
      
        //Clear the undo stack?
        undo_stack.clear();
      }
      break;
    case MESSAGE_LEAVE:
      this->leave(conn);
      break;
    default:
      break;
    }

}

spreadsheet* session::get_spreadsheet()
{
  return ss;
}
bool session::is_connected(connection *conn)
{
  std::vector<connection*>::iterator i;
  for(i = connections.begin(); i != connections.end(); i++)
    {
      if(conn == (*i))
	return true;
    }
  return false;
}

int session::get_num_of_connections()
{
  return connections.size();
}
