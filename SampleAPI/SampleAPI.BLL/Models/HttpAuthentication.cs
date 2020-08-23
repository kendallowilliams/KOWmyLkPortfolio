using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.BLL.Models
{
    public class HttpAuthentication
    {
        public HttpAuthentication(string scheme, string parameter)
        {
            SetUserNameAndPassword(scheme, parameter);
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsValid { get; private set; }


        private void SetUserNameAndPassword(string scheme, string b64Parameter)
        {
            bool validScheme = "basic".Equals(scheme?.Trim(), StringComparison.OrdinalIgnoreCase);

            try 
            { 
                string parameter = Encoding.UTF8.GetString(Convert.FromBase64String(b64Parameter));
                IEnumerable<string> parts = parameter.Split(":".ToCharArray());

                UserName = parts.ElementAtOrDefault(0);
                Password = parts.ElementAtOrDefault(1);
                IsValid = true && validScheme;
            }
            catch(Exception)
            {
                IsValid = false;
            }
        }
    }
}
