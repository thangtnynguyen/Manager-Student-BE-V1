using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using FluentValidation;

namespace UTEHY.DatabaseCoursePortal.Api.Validators.Banner
{
    public class EditBannerRequestValidator : AbstractValidator<EditBannerRequest>
    {
        public EditBannerRequestValidator() 
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id không được để trống");

            RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type không được để trống");

            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title không được để trống");

            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description không được để trống");

            RuleFor(x => x.Alt)
            .NotEmpty().WithMessage("Alt không được để trống");

            RuleFor(x => x.CtaTitle)
            .NotEmpty().WithMessage("CtaTitle không được để trống");

            RuleFor(x => x.LinkTo)
            .NotEmpty().WithMessage("LinkTo không được để trống");

            RuleFor(x => x.Properties)
            .NotEmpty().WithMessage("Properties không được để trống");

            RuleFor(x => x.Priority)
            .NotNull().WithMessage("Priority không được để trống");

            RuleFor(x => x.Expired)
            .NotEmpty().WithMessage("Expired không được để trống")
            .GreaterThan(DateTime.Now).WithMessage("Expired phải lớn hơn thời gian hiện tại");
        }
    }
}
