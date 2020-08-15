using SampleAPI.Web.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Models
{
    public abstract class BaseViewModel : IViewModel
    {
        public BaseViewModel()
        {
        }

        public BaseViewModel(Pages page)
        {
            CurrentPage = page;
        }

        public Pages CurrentPage { get; set; } = Pages.None;
    }
}