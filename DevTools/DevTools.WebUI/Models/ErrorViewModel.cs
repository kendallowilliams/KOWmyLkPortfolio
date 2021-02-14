using DevTools.WebUI.Models.Interfaces;
using System;

namespace DevTools.WebUI.Models
{
    public class ErrorViewModel : IViewModel
    {
        public ErrorViewModel()
        {
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}