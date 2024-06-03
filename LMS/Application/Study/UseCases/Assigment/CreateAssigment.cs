using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CreateAssignment : BaseUseCase<CreateAssignmentDto, AssignmentEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IFileService _fileService { get; }

        public CreateAssignment(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IFileService fileService)
        {
            _fileService = fileService;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<AssignmentEntity> Execute(CreateAssignmentDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);

            await _institutionPolicy.EnforcePermission(PermissionEnum.write, typeof(AssignmentEntity), member);

            // performance eater!!! 
            var group = await _context.CourseGroups
                .Include(x => x.Students)
                    .ThenInclude(x => x.User)
                        .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == dto.AssignedGroupId);
            var teacher = await _context.Teachers
                .Include(x => x.User)
                    .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id);

            Guard.Against.Null(group, message: "Group does not exists");
            Guard.Against.Null(teacher, message: "Teacher does not exists"); 
            
            var course = await _context.Courses.FirstAsync(x => x.Id == group.CourseId); 
            var assignment = AssignmentEntity.Create(
                dto.Name,
                dto.StartDate,
                dto.EndDate,
                dto.Description,
                course.Id, 
                teacher, 
                group);
            
            if (dto.Attachments != null)
            {
                var files = await _fileService.UploadFiles().Execute(dto.Attachments);
                assignment.Attachments = files; 
            }
            foreach (var student in group.Students)
            {
                student.User.Permissions.AddPermissionWithCode(assignment, PermissionEnum.read);
                _context.Users.Update(student.User); 
            }
           

            teacher.User.Permissions.AddPermissionWithCode(assignment, PermissionEnum.all); 

            _context.Assigments.Add(assignment);
            _context.Users.Update(teacher.User); 
            await _context.SaveChangesAsync(); 

            return assignment;
        }
    }
}
