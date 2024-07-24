using FluentValidation;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;

namespace UTEHY.DatabaseCoursePortal.Api.Validators.Account
{
    public class LoginEmailRequestValidator : AbstractValidator<LoginEmailRequest>
    {
        public LoginEmailRequestValidator()
        {
            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email không được để trống")
           .EmailAddress().WithMessage("Email không đúng định dạng");

            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password không được để trống")
           .Length(4, 40).WithMessage("Password phải có độ dài từ 4 - 40 ký tự");
        }
    }
}
