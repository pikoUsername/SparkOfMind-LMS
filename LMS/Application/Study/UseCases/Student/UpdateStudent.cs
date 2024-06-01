
using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Student
{
    public class UpdateStudent : BaseUseCase<UpdateStudentDto, bool>
    {
        private IInstitutionAccessPolicy _insitutionPolicy; 
        private IApplicationDbContext _context;

        public UpdateStudent(
            IApplicationDbContext context,
            IInstitutionAccessPolicy insitutionPolicy)
        {
            _context = context;
            _insitutionPolicy = insitutionPolicy;
        }


        public async Task<bool> Execute(UpdateStudentDto dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == dto.StudentId);

            Guard.Against.NotFound(dto.StudentId, student);

            var member = await _insitutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _insitutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.edit, typeof(StudentEntity), member, student.Id); 
            
            if (dto.BirthDate != null)
            {
                student.BirthDate = DateOnly.FromDateTime((DateTime)dto.BirthDate); 
            }
            if (dto.Address != null)
            {
                student.Address = dto.Address; 
            }

            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
