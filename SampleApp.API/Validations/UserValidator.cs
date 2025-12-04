

using FluentValidation;
using SampleApp.API.Entities;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Login)
            .NotEmpty().WithMessage("Имя обязательно")
            .Length(2, 50).WithMessage("Имя должно быть от 2 до 50 символов")
            .Must(StartsWithCapitalLetter).WithMessage("Имя должно начинаться с заглавной буквы");
            
    }

    private bool StartsWithCapitalLetter(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return char.IsUpper(name[0]);
    }
}