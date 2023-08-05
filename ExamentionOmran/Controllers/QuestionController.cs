using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;
using Examention.EF.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Examention.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("{examId}")]
        public async Task<IActionResult> GetQuestionByExamId(int examId)
        {
            //var questions = _mapper.Map<IEnumerable<QuestionGetDto>>(await _unitOfWork.Questions.GetQuestionByExamId(examId));
            var questions = await _unitOfWork.Questions.GetQuestionByExamId(examId);
            if (questions == null)
                return BadRequest();
            var QuestionDto = new List<QuestionGetDto>();
            foreach (var quest in questions)
            {
                var question = new QuestionGetDto();
                question.ExamId = quest.ExamId;
                question.Id = quest.Id;
                question.Text = quest.Text;
                question.Choices = quest.Choices;
                question.CorrectChoiceId = quest.CorrectChoiceId;
                QuestionDto.Add(question);


                //var choices = _unitOfWork.Questions.ChoiceByQuetionId(question.Id);
                //question.Choices = (ICollection<Choice>)_mapper.Map<ICollection<ChoiceGetDto>>(choices);
            }
            return Ok(QuestionDto);
        }
        [HttpGet("GetQuestionById/{Id}")]
        public async Task<IActionResult> GetQuestionById(int Id)
        {
            if (Id <= 0)
                return StatusCode(50, "Id Is Not Vaild");
            var question =_mapper.Map<QuestionGetDto>(await _unitOfWork.Questions.GetById(Id));
            if (question == null)
                return BadRequest();
            
            return Ok(question);
        }
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create(QuestionCreateDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var question = _mapper.Map<Question>(questionDto);
            question.Choices = new List<Choice>();

            foreach (var choiceDto in questionDto.Choices)
            {
                var choice = _mapper.Map<Choice>(choiceDto);
                question.Choices.Add(choice);
            }

            await _unitOfWork.Questions.Create(question);

            return Ok(questionDto);
        }
        [HttpPut]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> EditQuestion(int Id, QuestionCreateDto questionDto)
        {
            if (Id < 0)
                return StatusCode(500, "Id Is NotVaild!");
            if (questionDto == null)
                return BadRequest();
            var oldQuestion = await _unitOfWork.Questions.GetById(Id);
            var question = _mapper.Map<Question>(questionDto);
            var newQuestion = _unitOfWork.Questions.Update(Id, question);
            return Ok(newQuestion);
        }
        [HttpDelete]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id < 0)
                return StatusCode(500, "Id Is NotVaild!");
            var question = await _unitOfWork.Questions.GetById(Id);
            if (question == null)
                return BadRequest();
            int Affect = await _unitOfWork.Questions.DeleteById(Id);
            if (Affect == 0)
                return BadRequest();
            return Ok(_mapper.Map<QuestionCreateDto>(question));


        }

    }
}
