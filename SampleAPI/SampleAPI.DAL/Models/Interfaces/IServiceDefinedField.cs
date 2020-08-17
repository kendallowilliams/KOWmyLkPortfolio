using System;
using static SampleAPI.DAL.Enums;

namespace SampleAPI.DAL.Models.Interfaces
{
    public interface IServiceDefinedField
    {
        string Name { get; set; }
        APIServiceDataTypes Type { get; set; }
        string Value { get; set; }
        int? GetIntValue();
        bool? GetBooleanValue();
        DateTime? GetDateTimeValue();
        string GetStringValue();
    }
}
