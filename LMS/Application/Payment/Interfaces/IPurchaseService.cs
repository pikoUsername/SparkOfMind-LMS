using LMS.Application.Payment.UseCases;

namespace LMS.Application.Payment.Interfaces
{
    public interface IPurchaseService
    {
        CreatePurchase CreatePurchase();
        ConfirmPurchase ConfirmPurchase();
        GetPurchasesList GetPurchasesList();
        UpdatePurchase UpdatePurchase();
        SolveProblem SolveProblem();
    }
}
