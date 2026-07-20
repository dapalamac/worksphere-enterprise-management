using FluentValidation;
using WorkSphere.Application.DTOs.Position;

namespace WorkSphere.Application.Validators.Position;

public class CreatePositionValidator : AbstractValidator<CreatePositionRequest>
{
    public CreatePositionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("La descripcion es obligatorio.");

    }
}

