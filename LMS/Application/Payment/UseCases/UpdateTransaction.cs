using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace LMS.Application.Payment.UseCases
{
    public class UpdateTransaction : BaseUseCase<UpdateTransactionDto, TransactionEntity>
    {
        public UpdateTransaction() { }

        public async Task<TransactionEntity> Execute(UpdateTransactionDto dto)
        {
            return new();
        }
    }
}
