using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SampleAPI.DAL.Enums;

namespace SampleAPI.BLL.Repositories
{
    public class APIServiceRepository
    {
        public static IEnumerable<APIServiceDataTypes> GetAPIServiceDataTypes()
        {
            return Enum.GetNames(typeof(APIServiceDataTypes))
                       .Select(name => (APIServiceDataTypes)Enum.Parse(typeof(APIServiceDataTypes), name));
        }
    }
}
