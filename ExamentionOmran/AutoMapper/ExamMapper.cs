using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;

namespace Examention.Api.AutoMapper
{
    public class ExamMapper:Profile
    {
        public ExamMapper()
        {
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Choice,ChoiceDto>().ReverseMap();
        }
    }
}
