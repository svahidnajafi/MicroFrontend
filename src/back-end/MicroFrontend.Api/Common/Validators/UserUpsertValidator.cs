using FluentValidation;
using MicroFrontend.Api.Common.Models;

namespace MicroFrontend.Api.Common.Validators;

public class UserUpsertValidator : AbstractValidator<UserDto>
{
    public UserUpsertValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty();
        RuleFor(e => e.Email).NotNull().NotEmpty();
    }

    public static UserUpsertValidator GetValidator() => new();
}