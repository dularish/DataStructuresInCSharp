using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks
{
    public interface CustomStack<T>
    {
        void push(T value);
        object pop();
        object peek();
        //int getCountOfElements();
        void display();
    }
}
