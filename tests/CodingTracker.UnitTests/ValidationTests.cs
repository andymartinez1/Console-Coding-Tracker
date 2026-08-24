using CodingTracker.Utils;
using Xunit;

namespace CodingTracker.Tests;

public class ValidationTests
{
    [Theory]
    [InlineData("2025-01-01 00:00", "yyyy-MM-dd HH:mm")]
    [InlineData("2025-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss")]
    [InlineData("01/01/2025 0:00 AM", "MM/dd/yyyy h:mm tt")]
    [InlineData("Wednesday, January 01, 2025", "dddd, MMMM dd, yyyy")]
    public void IsValidDate_ValidDateFormat_ReturnsTrue(string date, string format)
    {
        var result = Validation.IsValidDate(date, format);

        Assert.True(result);
    }

    [Theory]
    [InlineData("2025-01-01", "yyyy-MM-dd HH:mm")]
    [InlineData("2025-01-0100:00:00", "yyyy-MM-dd HH:mm:ss")]
    [InlineData("01/01/25 ", "MM/dd/yyyy h:mm tt")]
    [InlineData("Wednesday, January 01", "dddd, MMMM dd, yyyy")]
    [InlineData("Wednesday, January 01, 2025", null)]
    [InlineData(null, "yyyy-MM-dd HH:mm")]
    [InlineData(null, null)]
    public void IsValidDate_InvalidDateFormat_ReturnsFalse(string date, string format)
    {
        var result = Validation.IsValidDate(date, format);

        Assert.False(result);
    }

    [Theory]
    [InlineData("2025-01-01 09:00", "2025-01-01 13:00")]
    [InlineData("2025-02-02 11:00", "2025-02-02 14:00")]
    [InlineData("2025-03-03 08:00", "2025-03-03 12:00")]
    public void IsStartDateBeforeEndDate_ValidDates_ReturnsTrue(string startDate, string endDate)
    {
        var result = Validation.IsStartDateBeforeEndDate(startDate, endDate);

        Assert.True(result);
    }

    [Theory]
    [InlineData("2025-01-01 09:00", "2025-01-01 00:00")]
    [InlineData("2025-02-02 11:00", "2025-02-02 10:00")]
    [InlineData("2025-03-03 08:00", "2025-03-03 07:59")]
    public void IsStartDateBeforeEndDate_InvalidDates_ReturnsFalse(string startDate, string endDate)
    {
        var result = Validation.IsStartDateBeforeEndDate(startDate, endDate);

        Assert.False(result);
    }
}