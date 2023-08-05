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
    [Authorize]
    public class GradeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public GradeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetGradeByExamId/{examId}")]
        public async Task<IActionResult> GetGradeByExamId(int examId)
        {
            if (examId <= 0)
                return StatusCode(500, "Id Is NotVaild !!!");
            try
            {
                var examStudent = await _unitOfWork.ExamStudents.GetGradeForExam(examId);
                var examgrade = _mapper.Map<IEnumerable<Grades>>(examStudent);

                return Ok(examgrade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred !!!");
            }
        }
        [HttpGet("GetGradeForStudent")]
        public async Task<IActionResult> GetGradeForStudent(string studentId)
        {
            if (studentId == null)
                return BadRequest();
            try
            {
                var studentGrade =  _mapper.Map<IEnumerable<Grades>>(await _unitOfWork.ExamStudents.GetGradeForStudent(studentId));
                      
            return Ok(studentGrade);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An Error occurred");
            }
         
        }
        [HttpGet("GetStudentGradeInExam")]
        public async Task<IActionResult>GetStudentGradeInExam([FromQuery] int examId, [FromQuery] string studentId)
        {
            if(studentId == null||examId<0)
                return BadRequest();
            var StudenGrade =_mapper.Map<Grades>(await _unitOfWork.ExamStudents.GetStudentGradeInExam(studentId, examId));
            return Ok(StudenGrade);
        }
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult>Create(StudentGradeDto StudentgradeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var examStudent = await _unitOfWork.ExamStudents.Create(_mapper.Map<ExamStudent>(StudentgradeDto));

            return Ok(StudentgradeDto);
        }




    }

}
