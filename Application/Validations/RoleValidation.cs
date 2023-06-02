using Domein.AccessEntities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation() {
            RuleFor(x => x.Name)
          .NotEmpty()
          .NotNull()
          .MaximumLength(15)
          .MinimumLength(4)
          .WithMessage("Name of the role is invalid");
        }
    }
}
