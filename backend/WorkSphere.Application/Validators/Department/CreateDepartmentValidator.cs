using FluentValidation;
using WorkSphere.Application.DTOs.Department;

namespace WorkSphere.Application.Validators.Department;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentRequest>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("La descripcion es obligatorio.");

    }
}
