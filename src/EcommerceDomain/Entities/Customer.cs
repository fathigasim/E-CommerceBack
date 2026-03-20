

using EcommerceDomain.Entities;

namespace MediaRTutorialDomain.Entities
{


    public class Customer
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string? Phone { get; private set; }
        public string? Address { get; private set; }
        public string? City { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Country { get; private set; }
        public bool IsActive { get; private set; } = true;

        public string FullName => $"{FirstName} {LastName}";

        // Navigation
        public ICollection<Order> Orders { get; private set; } = new List<Order>();

        private Customer() { }

       
    }
}
