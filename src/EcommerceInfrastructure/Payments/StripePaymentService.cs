using Stripe;
using StripeSubscription = Stripe.Subscription;
using EcommerceDomain.Interfaces;
using EcommerceDomain.Models;

namespace EcommerceInfrastructure.Payments
{
    public class StripePaymentService : IPaymentService
    {
        private readonly PaymentIntentService _paymentIntentService;
        private readonly RefundService _refundService;
        private readonly CustomerService _customerService;
        private readonly SubscriptionService _subscriptionService;

        public StripePaymentService()
        {
            _paymentIntentService = new PaymentIntentService();
            _refundService = new RefundService();
            _customerService = new CustomerService();
            _subscriptionService = new SubscriptionService();
        }

        // ─── Payment Intents ─────────────────────────────────────
        public async Task<PaymentIntentResult> CreatePaymentIntentAsync(
            decimal amount,
            string currency,
            string customerEmail,
            Dictionary<string, string>? metadata = null,
            CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe uses cents
                Currency = currency.ToLower(),
                ReceiptEmail = customerEmail,
                Metadata = metadata,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                }
            };

            var intent = await _paymentIntentService.CreateAsync(options, cancellationToken: cancellationToken);

            return new PaymentIntentResult(
                intent.Id,
                intent.ClientSecret,
                intent.Status,
                amount,
                currency);
        }

        public async Task<PaymentIntentResult> ConfirmPaymentIntentAsync(
            string paymentIntentId,
            CancellationToken cancellationToken = default)
        {
            var intent = await _paymentIntentService.ConfirmAsync(
                paymentIntentId, cancellationToken: cancellationToken);

            return new PaymentIntentResult(
                intent.Id,
                intent.ClientSecret,
                intent.Status,
                intent.Amount / 100m,
                intent.Currency);
        }

        // ─── Refunds ─────────────────────────────────────────────
        public async Task<RefundResult> RefundPaymentAsync(
            string paymentIntentId,
            decimal? amount = null,
            CancellationToken cancellationToken = default)
        {
            var options = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId,
                Amount = amount.HasValue ? (long)(amount.Value * 100) : null
            };

            var refund = await _refundService.CreateAsync(options, cancellationToken: cancellationToken);

            return new RefundResult(
                refund.Id,
                refund.Status,
                refund.Amount / 100m);
        }

        // ─── Customers ───────────────────────────────────────────
        public async Task<CustomerResult> CreateCustomerAsync(
            string email,
            string name,
            CancellationToken cancellationToken = default)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Name = name
            };

            var customer = await _customerService.CreateAsync(options, cancellationToken: cancellationToken);

            return new CustomerResult(customer.Id, customer.Email, customer.Name);
        }

        // ─── Subscriptions ───────────────────────────────────────
        public async Task<SubscriptionResult> CreateSubscriptionAsync(
            string customerId,
            string priceId,
            CancellationToken cancellationToken = default)
        {
            var options = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = new List<SubscriptionItemOptions>
            {
                new() { Price = priceId }
            },
                PaymentBehavior = "default_incomplete",
                PaymentSettings = new SubscriptionPaymentSettingsOptions
                {
                    SaveDefaultPaymentMethod = "on_subscription"
                },
                Expand = new List<string> { "latest_invoice.payment_intent" }
            };

            StripeSubscription subscription = await _subscriptionService.CreateAsync(
                options, cancellationToken: cancellationToken);
            var currentPeriodEnd = subscription.Items.Data.First().CurrentPeriodEnd;
            return new SubscriptionResult(
     subscription.Id,
     subscription.Status,
     subscription.CustomerId,
     priceId,
    currentPeriodEnd
 );
        }

        public async Task CancelSubscriptionAsync(
            string subscriptionId,
            CancellationToken cancellationToken = default)
        {
            await _subscriptionService.CancelAsync(
                subscriptionId, cancellationToken: cancellationToken);
        }
    }
}
