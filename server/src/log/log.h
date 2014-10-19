/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: main.cpp
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#ifndef LOG_H
#define LOG_H

#include <ctime>
#include <fstream>
#include <string>

class LogFile
{
    std::ofstream _ofs;
    char *_time;
    struct tm *_timeinfo;
    time_t _rawtime;

public:
    LogFile();
    LogFile(const char *path);
    ~LogFile();

    LogFile& operator<<(const std::string& message);

    template<typename... Args>
    LogFile& write(Args&&... args)
    {
        getCurrentTime();
        _ofs << _time << ' ';
        return pushWrite(std::forward<Args>(args)...);
    }

private:
    void getCurrentTime();

    template<typename T, typename... Args>
    LogFile& pushWrite(T&& arg, Args&&... args)
    {
        _ofs << arg;
        return pushWrite(std::forward<Args>(args)...);
    }

    LogFile& pushWrite() // Sentinel function for variadic write
    {
        _ofs << '\n';
        return *this;
    }

};

#endif /* LOG_H */

