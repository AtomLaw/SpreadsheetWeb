#ifndef SESSION_H
#define SESSION_H

#include "connection.h"
#include "spreadsheet.h"

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

#endif
