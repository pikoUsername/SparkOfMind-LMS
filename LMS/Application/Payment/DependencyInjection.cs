using LMS.Application.Payment.Interfaces;
using LMS.Application.Payment.Services;
using LMS.Application.Payment.UseCases;

namespace LMS.Application.Payment
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<PaymentAdapterFactory>();

            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletService>();

            return services;
        }
    }
}
