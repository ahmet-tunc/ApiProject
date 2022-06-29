using CaseStudy.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface IUserService
    {
        IResult ValidateCredentials(string username, string password);
    }
}
