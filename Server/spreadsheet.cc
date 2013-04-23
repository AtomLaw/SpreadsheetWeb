#include "spreadsheet.h"
#include <sstream>
#include <iostream>
#include <fstream>

spreadsheet::spreadsheet(std::string filename)
{
  this->loaded = false;
  this->version = 0;
  this->filename = filename;
  this->cell_map = std::map<std::string, std::string>();
  this->password = "";
}
  
void spreadsheet::save()
{
  std::ofstream file;
  file.open(this->filename.c_str(), std::ios::out);
  
  if(!file) return;

  file << this->password << " " << this->version << std::endl;
  std::map<std::string, std::string>::iterator i;
  for(i = this->cell_map.begin(); i != this->cell_map.end(); ++i)
  {
    file << (*i).first << " " << (*i).second << std::endl;
  }

  file.close();
}

bool spreadsheet::load()
{
  std::ifstream file;
  file.open(this->filename.c_str(), std::ios::in);

  if(!file) return false;

  std::string file_password;
  int file_version;

  file >> file_password >> file_version;

  this->password = file_password;
  //  this->version = file_version;

  while(!file.eof())
    {
      std::string cell;
      std::string contents;

      file >> cell;
      std::getline(file, contents);
      this->update_cell(cell, contents);
    }
  
  file.close();
  this->loaded = true;
 return true;
}
bool spreadsheet::exists()
{ 
  return boost::filesystem::exists(this->filename);
}

bool spreadsheet::create(std::string password)
{
  this->password = password;
  this->version = 0;
  this->save();
 return true;
}
bool spreadsheet::authenticate(std::string password)
{
  std::ifstream file;
  file.open(this->filename.c_str(), std::ios::in);

  if(!file) return false;

  std::string file_password;
  int file_version;

  file >> file_password >> file_version;
  
  file.close();

  return password == file_password;
}

bool spreadsheet::is_loaded() 
{
  return loaded;
}

int spreadsheet::get_version()
{ 
      return version;
}

void spreadsheet::update_cell(std::string cell, std::string contents)
{
  if(cell != "" && contents != "")
    this->cell_map[cell] = contents;
}
std::string spreadsheet::get_cell_contents(std::string cell)
{
  return this->cell_map[cell];
}

std::string spreadsheet::get_xml()
{
  std::ostringstream out;
  out << "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
      << "<spreadsheet version=\"" << get_version() << "\">";

  std::map<std::string, std::string>::iterator i;
  for(i = this->cell_map.begin(); i != this->cell_map.end(); ++i)
    {
      out << "<cell>"
	  << "<name>" << (*i).first << "</name>"
	  << "<contents>" << (*i).second << "</contents>"
	  << "</cell>";
    }
  
  out << "</spreadsheet>";
  return out.str();
}

void spreadsheet::increment_version()
{
  this->version++;
}

