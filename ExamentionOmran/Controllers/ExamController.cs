using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;
using Examention.EF.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var examDto = _mapper.Map<ExamQuestionDto>(exam);
            examDto.Questions = (ICollection<Question>)_mapper.Map<IEnumerable<QuestionDto>>(exam.Questions);

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
        [HttpPost("{Create}")]
        public IActionResult Create(ExamDto exam)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Exam newExam = _mapper.Map<Exam>(exam);
            _unitOfWork.Exams.Create(newExam);
            return Ok(exam);
        }
        [HttpPost("CreateExamByQuestion")]
        public IActionResult CreateExamByQuestion(ExamQuestion examQuestion)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var exam = _mapper.Map<Exam>(examQuestion);
            var affect=_unitOfWork.Exams.Create(exam);
            if(affect!=null)
            return Ok(exam);
            return BadRequest();

        }
        [HttpPut]
        public IActionResult EditExam(int examId,ExamDto examDto)
        {
            var exam=_unitOfWork.Exams.GetById(examId);
            if (exam == null)
                return NotFound();
            var newExam=_mapper.Map<Exam>(examDto);
            var examAfterUpdate=_unitOfWork.Exams.Update(examId, newExam);
            if (examAfterUpdate != null)
                return Ok(examDto);
            return BadRequest();
        }

        [HttpDelete("{examId}")]
        public async Task<IActionResult> DeleteExam(int examId)
        {
            var exam = _unitOfWork.Exams.GetById(examId);
            if (exam == null)
                return NotFound();
            try
            {
                int affect = await _unitOfWork.Exams.DeleteById(examId);
                if(affect == 0)
                    return StatusCode(500, "I'm Sorry, An Error Occured While Delete exam");

                return Ok(exam);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "I'm Sorry, An Error Occured While Delete exam");
            }

        }



    }
}
