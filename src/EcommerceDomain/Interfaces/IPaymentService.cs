using EcommerceDomain.Models;


namespace EcommerceDomain.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentIntentResult> CreatePaymentIntentAsync(
            decimal amount,
            string currency,
            string customerEmail,
            Dictionary<string, string>? metadata = null,
            CancellationToken cancellationToken = default);

        Task<PaymentIntentResult> ConfirmPaymentIntentAsync(
            string paymentIntentId,
            CancellationToken cancellationToken = default);

        Task<RefundResult> RefundPaymentAsync(
            string paymentIntentId,
            decimal? amount = null,
            CancellationToken cancellationToken = default);

        Task<CustomerResult> CreateCustomerAsync(
            string email,
            string name,
            CancellationToken cancellationToken = default);

        Task<SubscriptionResult> CreateSubscriptionAsync(
            string customerId,
            string priceId,
            CancellationToken cancellationToken = default);

        Task CancelSubscriptionAsync(
            string subscriptionId,
            CancellationToken cancellationToken = default);
    }
}
