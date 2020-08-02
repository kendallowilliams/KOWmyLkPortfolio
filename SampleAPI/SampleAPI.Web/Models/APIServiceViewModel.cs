using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using static SampleAPI.Web.Enums;

namespace SampleAPI.Web.Models
{
    [Export]
    public class APIServiceViewModel : BaseViewModel
    {
        [ImportingConstructor]
        public APIServiceViewModel() : base(Pages.APIService)
        {

        }
    }
}