using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domein.AccessEntities;
using FluentValidation;

namespace Application.Validations
{
    public  class UserValidation : AbstractValidator<AUser>
    {
        public UserValidation() {
            RuleFor(x => x.Username)
             .NotEmpty()
             .NotNull()
             .MaximumLength(20)
             .MinimumLength(5)
             .WithMessage("Username is not valid");

            RuleFor(x => x.Password)
              .NotEmpty()
              .NotNull()
             // .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
                .WithMessage("Password is not valid")
              .MinimumLength(6)
              .WithMessage("Password is not valid");

        }

    }
}
