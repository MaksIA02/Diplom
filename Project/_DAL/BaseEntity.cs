using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public class BaseEntity<TKey>
    {
        public TKey? Id { get; set; }
		public string? IdentityUser { get; set; }

        public DateTime? DateTime { get; set; }
	}
}
