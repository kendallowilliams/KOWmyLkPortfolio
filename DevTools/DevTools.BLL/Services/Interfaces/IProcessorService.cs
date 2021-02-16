﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.BLL.Services.Interfaces
{
    public interface IProcessorService
    {
        Task ProcessScaffoldDbContexts();

        Task ProcessScaffoldDbContextItem(Guid id);
    }
}