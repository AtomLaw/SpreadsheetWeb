
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


#include "connection.h"

//**************************************
//CONNECTION CLASS
//**************************************


#include "server.h"

#include "session.h"

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
