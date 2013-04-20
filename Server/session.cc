#include "session.h"

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
  connection->read_message(boost::bind(&session::handle_message, this, _1, _2));
  connections.push_back(connection);
}


void session::handle_message(Message msg, connection *conn)
{
  switch(msg.type)
    {
    case MESSAGE_CREATE:
      break;
    default:
      break;
    }
  
  conn->read_message(boost::bind(&session::handle_message, this, _1, _2));
}
