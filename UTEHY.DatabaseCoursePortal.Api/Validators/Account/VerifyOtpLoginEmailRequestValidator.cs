using FluentValidation;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;

namespace UTEHY.DatabaseCoursePortal.Api.Validators.Account
{
    public class VerifyOtpLoginEmailRequestValidator : AbstractValidator<VerifyOtpLoginEmailRequest>
    {
        public VerifyOtpLoginEmailRequestValidator()
        {
            RuleFor(x => x.Name)
           .MinimumLength(6).WithMessage("Tên người dùng phải có độ dài tối thiểu 6 ký tự")
           .When(x => !string.IsNullOrEmpty(x.Name)); ;

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email không được để trống")
           .EmailAddress().WithMessage("Email không đúng định dạng")
           .MinimumLength(6).WithMessage("Email có độ dài tối thiểu 9 ký tự");

            RuleFor(x => x.Otp)
           .NotEmpty().WithMessage("Mã OTP không được để trống")
           .MinimumLength(6).WithMessage("Mã OTP có độ dài tối thiểu 9 ký tự");
        }
    }
}
