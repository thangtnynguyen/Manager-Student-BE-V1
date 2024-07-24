using FluentValidation;
using UTEHY.DatabaseCoursePortal.Api.Enums;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;

namespace UTEHY.DatabaseCoursePortal.Api.Validators.Auth
{
    public class LoginUsernameRequestValidator : AbstractValidator<LoginUsernameRequest>
    {
        public LoginUsernameRequestValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Tên người dùng không được để trống!");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Tên người dùng không được để trống!");

            RuleFor(request => request.Type)
            .InclusiveBetween(1, 3).WithMessage("Loại người dùng không hợp lệ. Giá trị phải từ 1 đến 3!");
        }
    }
}
