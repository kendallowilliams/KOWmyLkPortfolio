using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.BLL.Repositories
{
    public class APIServiceRepository
    {
        public static IEnumerable<Type> GetAPIServiceDataTypes()
        {
            yield return typeof(bool);
            yield return typeof(int);
            yield return typeof(string);
        }
    }
}
