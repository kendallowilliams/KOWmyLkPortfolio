using System;
using static SampleAPI.DAL.Enums;

namespace SampleAPI.BLL.Interfaces
{
    public interface IServiceDefinedField
    {
        string Name { get; set; }
        APIServiceDataTypes Type { get; set; }
        string Value { get; set; }
    }
}
