using FluentValidation;
using WorkSphere.Application.DTOs.Employees;

namespace WorkSphere.Application.Validators.Employees;

public class EmployeeFilterValidator : AbstractValidator<EmployeeFilter>
{
    public EmployeeFilterValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("El número de página debe ser mayor o igual a 1.");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(100)
            .WithMessage("No puedes pedir mas de 100.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("No puedes pedir menos de un registro.");

        RuleFor(x => x.SortBy)
            .Must(x => string.IsNullOrWhiteSpace(x) || new[] { "FirstName", "LastName", "CreatedAt" }.Contains(x))
            .WithMessage("La opcion no esta disponible");

    }
}
