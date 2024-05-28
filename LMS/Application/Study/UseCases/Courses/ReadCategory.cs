using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;

namespace LMS.Application.Study.UseCases.Courses
{
    public class GetCategory : BaseUseCase<GetCategoryDto, CategoryEntity>
    {
        public GetCategory() { }

        public async Task<CategoryEntity> Execute(GetCategoryDto dto)
        {
            return;
        }
    }

    public class GetCategoriesList : BaseUseCase<GetCategoriesListDto, ICollection<CategoryEntity>>
    {
        public GetCategoriesList() { }

        public async Task<ICollection<CategoryEntity>> Execute(GetCategoriesListDto dto)
        {
            return;
        }
    }
}
