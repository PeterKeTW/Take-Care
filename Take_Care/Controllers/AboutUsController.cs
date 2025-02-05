using Microsoft.AspNetCore.Mvc;
using Take_Care.Models;

namespace Take_Care.Controllers
{
	public class AboutUsController : Controller
	{
		// 機構簡介
		public IActionResult Index()
		{
			return View();
		}
		
		// 員工介紹
		private TakeCareContext _context;
		public AboutUsController(TakeCareContext context)
		{
			_context = context;
		}
		public IActionResult Staff()
		{
			List<Employee> employees = _context.Employees.ToList();
			return View(employees);
		}
	}
}
