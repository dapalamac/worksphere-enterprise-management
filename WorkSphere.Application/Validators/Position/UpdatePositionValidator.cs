using FluentValidation;
using WorkSphere.Application.DTOs.Position;

namespace WorkSphere.Application.Validators.Position;

public class UpdatePositionValidator : AbstractValidator<UpdatePositionRequest>
{
    public UpdatePositionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("La descripcion es obligatorio.");

    }
}
