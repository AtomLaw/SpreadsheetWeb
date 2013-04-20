#include <iostream>
#include "spreadsheet.h"
// #include "main.cc"

class connection;

class session 
{

public:

	session(std::string filename) : my_spreadsheet(filename)
	{
		filename_ = filename;
	}

	// void join(connection::connection & a_connection)
	// {

	// }



private:

	std::string filename_;
	spreadsheet::spreadsheet my_spreadsheet;

};