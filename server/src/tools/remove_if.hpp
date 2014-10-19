#ifndef REMOVE_IF_HPP
#define REMOVE_IF_HPP

#include <vector>

namespace tools
{
    template<typename T, typename F>
    void remove_if(std::vector<T>& container, F conditionFunctor)
    {
        for(auto it = container.cbegin(); it != container.cend(); ++it)
        {
            if(conditionFunctor(*it))
                container.erase(it);
        }
    }
};

#endif /* REMOVE_IF_HPP */

