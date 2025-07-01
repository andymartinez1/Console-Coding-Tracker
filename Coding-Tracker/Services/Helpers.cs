using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Coding_Tracker.Services;

public static class Helpers
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .First()
            ?.GetCustomAttribute<DisplayAttribute>()
            ?.Name;
    }
}
