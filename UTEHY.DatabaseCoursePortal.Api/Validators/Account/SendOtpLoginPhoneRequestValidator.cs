using FluentValidation;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;

namespace UTEHY.DatabaseCoursePortal.Api.Validators.Account
{
    public class SendOtpLoginPhoneRequestValidator : AbstractValidator<SendOtpLoginPhoneRequest>
    {
        public SendOtpLoginPhoneRequestValidator()
        {
            RuleFor(x => x.Name)
           .MinimumLength(6).WithMessage("Tên người dùng phải có độ dài tối thiểu 6 ký tự")
           .When(x => !string.IsNullOrEmpty(x.Name)); ;

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Số điện thoại không được để trống")
           .MinimumLength(6).WithMessage("Số điện thoại có độ dài tối thiểu 9 ký tự");
        }
    }
}
