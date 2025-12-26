using System.ComponentModel.DataAnnotations;

namespace Backend4.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class MinAgeAttribute : ValidationAttribute
{
    private readonly int _minAge;

    public MinAgeAttribute(int minAge) => _minAge = minAge;

    public override bool IsValid(object? value)
    {
        if (value is null) return false;

        DateTime date;
        if (value is DateTime dt)
            date = dt;
        else if (value is DateTime? ndt && ndt.HasValue)
            date = ndt.Value;
        else
            return false;

        var today = DateTime.Today;
        var age = today.Year - date.Year;
        if (date.Date > today.AddYears(-age)) age--;

        return age >= _minAge;
    }
}
