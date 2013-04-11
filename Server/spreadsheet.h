#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <iostream>
#include <map>


class spreadsheet 
{
 public:
  spreadsheet(std::string filename, std::string password);
  
  void save();
  bool load();
  bool exists();
  bool is_validated();
  bool is_loaded();
  int get_version();
  
  void update_cell(std::string cell, std::string contents);
  std::string get_cell_contents(std::string cell);

  std::string get_xml();

 private:
  bool is_loaded;
  int version;
  std::string filename;

  std::map<std::string, std::string> cell_map;
};



#endif
