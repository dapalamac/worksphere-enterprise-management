using FluentValidation;
using WorkSphere.Application.DTOs.Employees;

namespace WorkSphere.Application.Validators.Employees;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("El apellido es obligatorio.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Debe ingresar un correo electrónico válido.");

        RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("El salario debe ser mayor que cero.");

        RuleFor(x => x.Phone)
        .NotEmpty()
        .WithMessage("El teléfono es obligatorio.")
        .Length(10, 15)
        .WithMessage("El teléfono debe tener entre 10 y 15 caracteres.");

        RuleFor(x => x.DepartmentId)
    .NotEmpty()
    .WithMessage("Debe seleccionar un departamento.");

        RuleFor(x => x.PositionId)
    .NotEmpty()
    .WithMessage("Debe seleccionar un cargo.");

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
        .WithMessage("La fecha de contratación no puede ser futura.");

    }
}