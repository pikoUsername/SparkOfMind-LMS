using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Student
{
    public class CreateStudent : BaseUseCase<CreateStudentDto, StudentEntity>
    {
        private IApplicationDbContext _context { get; set; } 

        public CreateStudent(IApplicationDbContext context, IUserService userServic ) {
            _context = context;
        }

        public async Task<StudentEntity> Execute(CreateStudentDto dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Phone == dto.Phone);
            if (student == null)
            {
                student = StudentEntity.CreateFromUser(); 
            }

            return new();
        }
    }
}
