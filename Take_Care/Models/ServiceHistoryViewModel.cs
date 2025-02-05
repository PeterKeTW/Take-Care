using Take_Care.Controllers.APIController;
using Take_Care.Models;

namespace Take_Care.Models
{
	public class ServiceHistoryViewModel
	{
		public DateTime ServiceDate { get; set; }
		public TimeSpan ServiceTime { get; set; }
		public string? EmployerName { get; set; }
		public string? ServiceItem { get; set; }
		public int ServiceAmount { get; set; }
		public string? Account { get; set; }
        public string? ClientAddress { get; set; }
		public string? Remark { get; set; }
        public int CaseID { get; set; }
    }
}
