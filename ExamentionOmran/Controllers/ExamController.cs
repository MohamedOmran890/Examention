using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;
using Examention.EF.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examention.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExamController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exams = _mapper.Map<IEnumerable<ExamDto>>(await _unitOfWork.Exams.GetList());
            return Ok(exams);
        }
        [HttpGet("GetById/{Id:int}")]
        public async Task<IActionResult>GetById(int Id)
        {
            var exam=await _unitOfWork.Exams.GetById(Id);
            if(exam==null)
                return NoContent();
            var examDto = _mapper.Map<ExamDto>(exam);
            examDto.Questions = (ICollection<Question>)_mapper.Map<List<QuestionDto>>(exam.Questions);

            foreach(var question in examDto.Questions)
            {
                var choices = _unitOfWork.Questions.ChoiceByQuetionId(question.Id);
                question.Choices = (ICollection<Choice>)_mapper.Map<ICollection<ChoiceDto>>(choices);
            }
   
            return Ok(examDto);
        }
        [HttpGet("GetByLevel/{LevelId:int}")]
        public async Task<IActionResult>GetExamByLevel(int LevelId)
        {
            var exams = _mapper.Map<IEnumerable<ExamDto>>(await _unitOfWork.Exams.GetExamByLevel(LevelId));
            return Ok(exams);

        }


    }
}
