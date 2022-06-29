using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class UserManager:IUserService
    {
        public IResult ValidateCredentials(string username, string password)
        {
            if (username.Equals("admin") && password.Equals("admin123"))
                return new SuccessResult();

            return new ErrorResult();
        }
    }
}
