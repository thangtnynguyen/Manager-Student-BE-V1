using System.ComponentModel;

namespace UTEHY.DatabaseCoursePortal.Api.Enums
{
    public enum TypeLessonContent
    {
        [Description("Link")]
        Link = 1,

        [Description("File")]
        File = 2,

        [Description("Video")]
        Video = 3,
    }
}
