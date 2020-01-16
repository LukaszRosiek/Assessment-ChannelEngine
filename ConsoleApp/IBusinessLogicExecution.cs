using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IBusinessLogicExecution
    {
        Task RunAsync();
    }
}
