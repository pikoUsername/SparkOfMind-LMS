﻿using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;

namespace LMS.Application.Study.UseCases.Schedule
{
    public class DeleteSchedule : BaseUseCase<DeleteScheduleDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }


        public DeleteSchedule(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(DeleteScheduleDto dto)
        {
            return true;
        }
    }
}