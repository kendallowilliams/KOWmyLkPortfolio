using System;

namespace SampleAPI.BLL.Interfaces
{
    public interface IServiceDefinedField
    {
        string Name { get; set; }
        Type Type { get; set; }
        string Value { get; set; }
    }
}
