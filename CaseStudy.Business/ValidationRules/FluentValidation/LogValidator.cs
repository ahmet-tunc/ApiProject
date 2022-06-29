using CaseStudy.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.ValidationRules.FluentValidation
{
    public class LogValidator:AbstractValidator<Log>
    {
        public LogValidator()
        {
            //RuleFor(l => l.Method).Length(200);
            //RuleFor(l => l.LogDetail).Length(500);
        }
    }
}
