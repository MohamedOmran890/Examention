using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;

namespace Examention.Api.AutoMapper
{
    public class ExamMapper:Profile
    {
        public ExamMapper()
        {

                CreateMap<QuestionCreateDto,Question>().ReverseMap()
                    .ForMember(dest=>dest.Choices,src=>src.MapFrom(opt=>opt.Choices));
                CreateMap<Question,QuestionGetDto>().ReverseMap()
                    .ForMember(dest => dest.Choices, src => src.MapFrom(opt => opt.Choices));
               CreateMap<ExamQuestionDto, Exam>()
                 .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
               CreateMap<Exam, ExamQuestionDto>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<ExamCreateDto,Exam>().ReverseMap();
                CreateMap<Exam,ExamGetDto>().ReverseMap();
                CreateMap<ChoiceCreateDto,Choice>().ReverseMap();
                CreateMap<Choice, ChoiceGetDto>().ReverseMap();
            CreateMap<ExamStudent, Grades>()
             .ForMember(dest => dest.FirstName, src => src.MapFrom(s => s.Student.User.FirstName))
             .ForMember(dest => dest.LastName, src => src.MapFrom(s => s.Student.User.LastName))
             .ForMember(dest => dest.Email, src => src.MapFrom(s => s.Student.User.Email));
            CreateMap<StudentGradeDto,ExamStudent>();
            CreateMap<RegisterDto,User>();
            CreateMap<RegisterStudentDto,User>();
            CreateMap<User,LoginDto>();
            CreateMap<ICollection<IEnumerable<Choice>>, ICollection<ChoiceGetDto>>();
        }
    }
}
