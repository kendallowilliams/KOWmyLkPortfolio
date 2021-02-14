using DevTools.BLL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.BLL.Services.Interfaces
{
    public interface IConsoleService
    {
        string Execute(string executablePath, string args);
    }
}
