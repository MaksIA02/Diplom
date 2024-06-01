using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
	public class Goal : BaseEntity<Guid>
	{
		public string? Name { get; set; }
		public decimal Amount { get; set; }
		public Card? Card { get; set; }
		public DateOnly Date { get; set; }
	}
}
