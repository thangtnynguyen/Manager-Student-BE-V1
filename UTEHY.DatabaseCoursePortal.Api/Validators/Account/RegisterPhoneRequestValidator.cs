using FluentValidation;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;

namespace UTEHY.DatabaseCoursePortal.Api.Validators.Account
{
    public class RegisterPhoneRequestValidator : AbstractValidator<RegisterPhoneRequest>
    {
        public RegisterPhoneRequestValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Tên người dùng không được để trống")
           .MinimumLength(6).WithMessage("Tên người dùng phải có độ dài tối thiểu 6 ký tự");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Số điện thoại không được để trống")
           .MinimumLength(6).WithMessage("Số điện thoại có độ dài tối thiểu 9 ký tự");

            RuleFor(x => x.Otp)
           .NotEmpty().WithMessage("Mã OTP không được để trống")
           .MinimumLength(6).WithMessage("Mã OTP có độ dài tối thiểu 6 ký tự");
        }
    }
}
