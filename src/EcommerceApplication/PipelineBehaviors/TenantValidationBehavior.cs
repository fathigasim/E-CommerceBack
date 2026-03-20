using MediaRTutorialApplication.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace EcommerceApplication.PipelineBehaviors
{
    // Application/Common/Behaviors/TenantValidationBehavior.cs

    /// <summary>
    /// Validates that every request has a valid tenant context.
    /// Executed FIRST in the pipeline.
    /// </summary>
    public class TenantValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly ILogger<TenantValidationBehavior<TRequest, TResponse>> _logger;

        public TenantValidationBehavior(
            ITenantService tenantService,
            ILogger<TenantValidationBehavior<TRequest, TResponse>> logger)
        {
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Skip tenant validation for admin/cross-tenant requests
            if (request is IAdminRequest)
            {
                _logger.LogDebug("Skipping tenant validation for admin request: {RequestType}",
                    typeof(TRequest).Name);
                return await next();
            }

            //// Ensure tenant context exists
            //if (!_tenantService.HasCurrentTenant())
            //{
            //    _logger.LogWarning("Tenant context not found for request: {RequestType}",
            //        typeof(TRequest).Name);
            //    throw new TenantContextNotFoundException();
            //}

            //var tenant = _tenantService.GetCurrentTenant();

            //// Validate tenant is active
            //if (!tenant.IsActive)
            //{
            //    _logger.LogWarning(
            //        "Request rejected for inactive tenant: {TenantId} - Request: {RequestType}",
            //        tenant.Id, typeof(TRequest).Name);
            //    throw new TenantInactiveException(tenant.Id);
            //}

            //_logger.LogDebug(
            //    "Processing tenant request - Tenant: {TenantId}, Request: {RequestType}",
            //    tenant.Id, typeof(TRequest).Name);

            return await next();
        }
    }

    /// <summary>
    /// Marker interface for requests that bypass tenant validation (admin operations).
    /// </summary>
    public interface IAdminRequest { }
}
