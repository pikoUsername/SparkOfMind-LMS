using LMS.Application.Payment.UseCases;

namespace LMS.Application.Payment.Interfaces
{
    public interface ITransactionService
    {
        HandleTransaction HandleTransaction();
        GetTransactionsList GetTransactionsList();
        UpdateTransaction UpdateTransaction();
        GetTransactionProviders GetTransactionProviders();
        HandleGatewayResult HandleGatewayResult();
    }
}
