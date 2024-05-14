using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace LMS.Application.Payment.UseCases
{
    public class CreateTransaction : BaseUseCase<CreateTransactionDto, TransactionEntity>
    {
        public CreateTransaction() { }

        public async Task<TransactionEntity> Execute(CreateTransactionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
