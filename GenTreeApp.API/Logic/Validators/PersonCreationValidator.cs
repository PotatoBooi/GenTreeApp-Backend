using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GenTreeApp.API.DTOs.Person;

namespace GenTreeApp.API.Logic.Validators
{
    public class PersonCreationValidator:AbstractValidator<PersonCreationDto>
    {
        public PersonCreationValidator()
        {
            RuleFor(n => n.Name).MaximumLength(50).NotEmpty();
            RuleFor(n => n.Surname).MaximumLength(50).NotEmpty();
            RuleFor(s => s.Sex).NotEmpty();
            
        }
    }
}
