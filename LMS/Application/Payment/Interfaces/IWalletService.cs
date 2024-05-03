using LMS.Application.Payment.UseCases;

namespace LMS.Application.Payment.Interfaces
{
    public interface IWalletService
    {
        BalanceOperation BalanceOperation();
        GetWallet GetWallet();
        BlockWallet BlockWallet();
    }
}
