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
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetQuestionByExamId(int examId)
        {
            var questions = _mapper.Map<IEnumerable<QuestionDto>>(await _unitOfWork.Questions.GetAll(examId));
            if (questions == null)
                return BadRequest();
            return Ok(questions);
        }
        [HttpGet("GetQuestionById/{Id}")]
        public async Task<IActionResult> GetQuestionById(int Id)
        {
            if (Id <= 0)
                return StatusCode(50, "Id Is Not Vaild");
            var question = await _unitOfWork.Questions.GetById(Id);
            if (question == null)
                return BadRequest();
            return Ok(question);
        }
        [HttpPost]
        public async Task<IActionResult> Create(QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var question = await _unitOfWork.Questions.Create(_mapper.Map<Question>(questionDto));
            return Ok(question);
        }
        [HttpPut]
        public async Task<IActionResult> EditQuestion(int Id, QuestionDto questionDto)
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
            return Ok(_mapper.Map<QuestionDto>(question));


        }

    }
}
