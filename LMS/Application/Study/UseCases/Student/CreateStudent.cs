using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Application.User.Interfaces;
using LMS.Domain.Payment.Exceptions;
using LMS.Domain.Study.Entities;
using LMS.Domain.Study.Enums;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Student
{
    // Creates student 
    public class CreateStudent : BaseUseCase<CreateStudentDto, StudentEntity>
    {
        private IApplicationDbContext _context { get; set; }
        private IUserService _userService { get; set; }
        private IAccessPolicy _accessPolicy { get; set; }
        private IInstitutionAccessPolicy _insitutionPolicy { get; set; }

        public CreateStudent(
            IApplicationDbContext context, 
            IUserService userService, 
            IAccessPolicy accessPolicy, 
            IInstitutionAccessPolicy insitutionPolicy) {
            _context = context;
            _userService = userService;
            _accessPolicy = accessPolicy;
            _insitutionPolicy = insitutionPolicy; 
        }

        public async Task<StudentEntity> Execute(CreateStudentDto dto)
        {
            var purchase = await _context.Purchases
                .Include(x => x.CreatedBy)
                .Include(x => x.Transaction)
                .FirstOrDefaultAsync(x => x.Id == dto.PurchaseId);
            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);

            Guard.Against.Null(institution, message: "Given institution does not exists"); 
            Guard.Against.Null(purchase, message: "Given purchase id does not exists");

            // see ConfirmPurchase use case for details
            if (!purchase.Confirmed)
            {
                throw new PurchaseIsNotConfirmed(purchase); 
            }

            // verify permission 
            var currentUser = await _accessPolicy.GetCurrentUser();

            var member = await _insitutionPolicy.GetMember(currentUser.Id, institution.Id); 
            await _insitutionPolicy.EnforcePermission(PermissionEnum.extend, typeof(StudentEntity), member); 

            var student = await _context.Students.FirstOrDefaultAsync(
                x => x.Phone == dto.Phone && x.InstitutionMember.InstitutionId == dto.InstitutionId);
            if (student == null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Phone == dto.Phone);
                if (user == null)
                    user = await _userService.Create().Execute(new User.Dto.CreateUserDto()
                    {
                        EmailAddress = purchase.CreatedBy.Email,
                        Phone = dto.Phone,
                        Name = dto.Name,
                        Surname = dto.Surname,
                        Role = UserRoles.Other,
                    }); 

                student = StudentEntity.CreateFromUser(
                    user, purchase.CreatedBy, dto.InstitutionId, StudentStatus.Waiting);

                _context.Students.Add(student);
            } else
            {

                _context.Students.Update(student);
            }
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == purchase.CourseId);

            Guard.Against.Null(course, message: "This course does not exists"); 

            var studentCourse = StudentCourseEntity.Create(student.Id, course.StartsAt ?? DateTime.Now, purchase.Id, course.Id);
            student.Courses.Add(studentCourse); 

            _context.StudentCourses.Add(studentCourse); 
            await _context.SaveChangesAsync(); 

            return student;
        }
    }
}
