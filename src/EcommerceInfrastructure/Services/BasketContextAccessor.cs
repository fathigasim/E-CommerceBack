using EcommerceDomain.Interfaces;
using Microsoft.AspNetCore.Http;


namespace EcommerceInfrastructure.Services
{
    public class BasketContextAccessor : IBasketContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string BasketCookieName = "ShoppingCommerceBasket";
        private readonly TimeSpan _cookieExpiry = TimeSpan.FromDays(7);

        public BasketContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetBasketId()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[BasketCookieName];
        }

        public void SetBasketId(string basketId)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.Add(_cookieExpiry),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(BasketCookieName, basketId, options);
        }

        public void ClearBasketId()
        {
            // ✅ Properly delete the cookie from browser
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(-1) // Expire in the past
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(BasketCookieName, cookieOptions);
        }
    }
}
