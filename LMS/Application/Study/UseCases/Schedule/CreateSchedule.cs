using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.Study.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Schedule
{
    public class CreateSchedule : BaseUseCase<CreateScheduleDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }


        public CreateSchedule(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(CreateScheduleDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(Domain.User.Enums.PermissionEnum.write, typeof(ScheduleEntity), member); 

            var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.GroupId == dto.GroupId);
            if (schedule != null && dto.IsRecurring && schedule.IsRecurring)
                throw new ScheduleIsAlreadyAssignedToGroup(schedule.Id);

            var days = CreateDays(dto.Days, dto.GroupId);
            var newSchedule = ScheduleEntity.Create(dto.Name, dto.RepeatAfter, days, dto.IsRecurring, dto.StartDate, dto.EndDate); 

            _context.Schedules.Add(newSchedule);
            await _context.SaveChangesAsync(); 

            return true;
        }

        public ICollection<DayScheduleEntity> CreateDays(ICollection<CreateDayDto> days, Guid groupId)
        {

            List<DayScheduleEntity> newDays = [];

            foreach (var day in days)
            {
                List<DayLessonEntity> newLessons = [];
                foreach (var lesson in day.Lessons)
                {
                    newLessons.Add(DayLessonEntity.Create(
                        lesson.Name, 
                        lesson.CourseId, 
                        lesson.StartDate, 
                        lesson.EndDate, 
                        lesson.TeacherId, 
                        groupId, 
                        lesson.Room));
                }

                newDays.Add(DayScheduleEntity.Create(day.Lessons[0].StartDate, day.Lessons[-1].EndDate, day.Day, dto.GroupId));
            }

            return newDays; 
        }
    }
} 