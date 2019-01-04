using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GenTreeApp.API.DTOs.Tree;

namespace GenTreeApp.API.Logic.Validators
{
    public class TreeCreationValidator : AbstractValidator<TreeCreationDto>
    {
        public TreeCreationValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
