using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using Microsoft.AspNetCore.Components.Forms;

namespace LMS.Application.Payment.UseCases
{
    public class SolveProblem : BaseUseCase<SolveProblemDto, bool>
    {
        public SolveProblem() { }

        public async Task<bool> Execute(SolveProblemDto dto)
        {
            return new();
        }
    }
}
