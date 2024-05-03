using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace LMS.Application.Payment.UseCases
{
    public class UpdatePurchase : BaseUseCase<UpdatePurchaseDto, PurchaseEntity>
    {
        public UpdatePurchase() { }

        public async Task<PurchaseEntity> Execute(UpdatePurchaseDto dto)
        {
            return new();
        }
    }
}
