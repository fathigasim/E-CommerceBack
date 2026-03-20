using EcommerceApplication.Common.Settings;
using MediaRTutorialApplication.Features.Payment.Commands.HandleWebhook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace MediaRTutorial.Controllers
{
   

    [ApiController]
    [Route("api/[controller]")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StripeWebhookController> _logger;
        private readonly string _webhookSecret;

        public StripeWebhookController(
            IMediator mediator,
            IOptions<StripeSettings> stripeSettings,
            ILogger<StripeWebhookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _webhookSecret = stripeSettings.Value.WebhookSecret;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> HandleWebhook()
        {
            // 1. Read the raw body
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // 2. Verify signature (CRITICAL for security)
                // before
                //var stripeEvent = EventUtility.ConstructEvent(
                //    json,
                //    Request.Headers["Stripe-Signature"],
                //    _webhookSecret);

                // After
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _webhookSecret,
                    throwOnApiVersionMismatch: false  // ← Add this
                );
                _logger.LogInformation("Stripe event received: {Type}", stripeEvent.Type);

                // 3. Handle relevant event types
                switch (stripeEvent.Type)
                {
                    //case Events.PaymentIntentSucceeded:
                    //case Events.PaymentIntentPaymentFailed:
                    //case Events.PaymentIntentCanceled:
                    case "payment_intent.succeeded":
                    case "payment_intent.payment_failed":
                    case "payment_intent.canceled":
                        {
                            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                            var command = new HandleStripeWebhookCommand(
                                stripeEvent.Type,
                                paymentIntent!.Id,
                                paymentIntent.Status,
                                paymentIntent.AmountReceived);

                            await _mediator.Send(command);
                            break;
                        }

                    //case Events.ChargeRefunded:
                    case "ChargeRefunded":
                        {
                            var charge = stripeEvent.Data.Object as Charge;

                            var command = new HandleStripeWebhookCommand(
                                stripeEvent.Type,
                                charge!.PaymentIntentId,
                                charge.Status,
                                charge.AmountRefunded);

                            await _mediator.Send(command);
                            break;
                        }

                    //case Events.CustomerSubscriptionCreated:
                    //case Events.CustomerSubscriptionDeleted:
                    case "CustomerSubscriptionCreated":
                    case "CustomerSubscriptionDeleted":
                        // Handle subscription events
                        _logger.LogInformation("Subscription event: {Type}", stripeEvent.Type);
                        break;

                    default:
                        _logger.LogInformation("Unhandled event type: {Type}", stripeEvent.Type);
                        break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe webhook signature verification failed");
                return BadRequest();
            }
        }
    }
}
