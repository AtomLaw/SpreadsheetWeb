#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <iostream>
#include <map>
#include <boost/filesystem.hpp>

class spreadsheet 
{
 public:
  spreadsheet(std::string filename);
  
  void save();
  bool load();
  bool exists();
  bool authenticate(std::string password);
  bool create(std::string password);
  bool is_loaded();
  int get_version();
  void increment_version();
  
  void update_cell(std::string cell, std::string contents);
  std::string get_cell_contents(std::string cell);

  std::string get_xml();


  std::string get_filename();
 private:
  bool loaded;
  int version;
  std::string filename;
  std::string password;

  std::map<std::string, std::string> cell_map;
};

#endif
