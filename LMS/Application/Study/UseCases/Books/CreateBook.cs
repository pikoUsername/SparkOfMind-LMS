﻿using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.AspNetCore.Components.Forms;
using System.Runtime.InteropServices;

namespace LMS.Application.Study.UseCases.Books
{
    public class CreateBook : BaseUseCase<CreateBookDto, BookEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }
        private IUser _user { get; }

        public CreateBook(
            IApplicationDbContext dbContext, 
            IInstitutionAccessPolicy institutionPolicy, 
            IAccessPolicy accessPolicy, 
            IUser user) {
            _user = user; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
            _accessPolicy = accessPolicy; 
        }

        public async Task<BookEntity> Execute(CreateBookDto dto)
        {
            var user = await _accessPolicy.GetCurrentUser(); 

            // only insitution member can post books for their students
            // and institution member does not have exclusive rights to delete, edit, or etc. 
            var member = await _institutionPolicy.GetMember(user.Id, dto.InstitutionId); 
            await _institutionPolicy.EnforcePermission(Domain.User.Enums.PermissionEnum.write, nameof(BookEntity), member);

            BookEntity book; 
            if (dto.IsOnline)
            {
                Guard.Against.Null(dto.Link, message: "Your online book does not have link to it"); 

                book = BookEntity.CreateOnline(
                    dto.InstitutionId, dto.Name, dto.Description, dto.Link, dto.CourseId); 
            } else
            {
                book = BookEntity.Create(dto.InstitutionId, dto.Name, dto.Description, dto.Author, dto.CourseId); 
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync(); 

            return book;
        }
    }
}
