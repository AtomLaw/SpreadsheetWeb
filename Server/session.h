#ifndef SESSION_H
#define SESSION_H

#include <vector>

#include "connection.h"
#include "spreadsheet.h"


//**************************************
//SESSION CLASS
//**************************************

struct undo_oper
{
  std::string cell;
  std::string contents;
};

class session 
{

public:

  session(spreadsheet* ss);
  virtual ~session();

  //Joins a connection to the session
  void join(connection *connection);
  void leave(connection *connection);

  int get_num_of_connections();
  
  spreadsheet* get_spreadsheet();

  void handle_message(Message msg, connection *conn, bool error);

 private:
  bool is_connected(connection *conn);

  spreadsheet *ss;
  std::vector<connection *> connections;
  std::vector<undo_oper> undo_stack;


};

#endif
