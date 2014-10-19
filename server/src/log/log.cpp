/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: main.cpp
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#include "log.h"
using namespace std;

LogFile::LogFile() : _time(new char[21])
{
}

LogFile::LogFile(const char *path)
    : _ofs(path, ios::app)
    , _time(new char[20])
{
}

LogFile::~LogFile()
{
    if(_ofs.is_open())
        _ofs.close();

    delete [] _time;
}

LogFile& LogFile::operator<<(const string& message)
{
    getCurrentTime();
    _ofs << _time << " " << message << endl;
    return *this;
}

void LogFile::getCurrentTime()
{
    time(&_rawtime);
    _timeinfo = localtime(&_rawtime);
    strftime(_time, 20, "[%D %T]", _timeinfo);
}

