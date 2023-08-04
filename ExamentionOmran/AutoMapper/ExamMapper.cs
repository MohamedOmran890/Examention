using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;

namespace Examention.Api.AutoMapper
{
    public class ExamMapper:Profile
    {
        public ExamMapper()
        {
            CreateMap<Exam, ExamQuestionDto>().ReverseMap();
            CreateMap<ExamDto,Exam>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Choice,ChoiceDto>().ReverseMap();
            CreateMap<ExamStudent, Grades>()
             .ForMember(dest => dest.FirstName, src => src.MapFrom(s => s.Student.User.FirstName))
             .ForMember(dest => dest.LastName, src => src.MapFrom(s => s.Student.User.LastName))
             .ForMember(dest => dest.Email, src => src.MapFrom(s => s.Student.User.Email));
            CreateMap<StudentGradeDto,ExamStudent>();
            CreateMap<RegisterDto,User>();
            CreateMap<RegisterStudentDto,User>();
            CreateMap<User,LoginDto>();
        }
    }
}
