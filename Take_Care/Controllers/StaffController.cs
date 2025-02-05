using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;
using System.Linq;
using System.Web.Helpers;
using Take_Care.Models;
using XAct;
using Microsoft.Identity.Client;
using System.Security.Principal;
using System.Diagnostics.Metrics;

namespace Take_Care.Controllers
{
	public class StaffController : Controller
	{
		private readonly TakeCareContext _context;


		public StaffController(TakeCareContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SelfIntro()
		{
			return View();
		}

		public IActionResult ChangePassword()
		{
			return View();
		}	

		public IActionResult Record()
		{
			var userAccount = TempData["Account"];
			Console.WriteLine("接收:" + userAccount);
			TempData.Keep();

			// 使用 LINQ 查詢來擷取符合帳號的歷史資料
			var allHistoryQuery = from c in _context.Cases
								  join e in _context.Employers on c.EmployerId equals e.EmployerId
								  join m in _context.Employees on c.EmployeeId equals m.EmployeeId
								  join p in _context.PersonalInfos on c.EmployerId equals p.EmployerId
								  join s in _context.ServiceItems on c.ServiceName equals s.ServiceName
								  where m.Account == userAccount && c.CaseStatus == true
								  orderby c.StartDateTime
								  select new ServiceHistoryViewModel()
								  {
									  ServiceDate = c.StartDateTime.Value.Date, // 提取日期部分.Date
									  ServiceTime = c.StartDateTime.Value.TimeOfDay, // 提取時間部分.TimeOfDay
									  EmployerName = p.Name,
									  ServiceItem = c.ServiceName,
									  ServiceAmount = (int)(s.Price * 0.4m),
									  Account = m.Account
								  };

			// 執行LINQ查詢
			var userHistory = allHistoryQuery.ToList();
			Console.WriteLine("A:" + allHistoryQuery);
			//Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1[Take_Care.Models.ServiceHistoryViewModel]
			Console.WriteLine("B:" + userHistory);
			//System.Collections.Generic.List`1[Take_Care.Models.ServiceHistoryViewModel]

			Console.WriteLine($"Count of all history records: {allHistoryQuery.Count()}"); // 檢查所有歷史資料的數量(21)			
			Console.WriteLine($"Count of user history records: {userHistory.Count()}"); // 檢查用戶歷史資料的數量(21)


			// 計算所有歷史資料的金額總和
			var totalAmount = userHistory.Sum(c => c.ServiceAmount);

			// 傳遞金額總和到View
			ViewBag.TotalAmount = totalAmount;

			return View(userHistory);			
		}

		[HttpPost]
		public IActionResult Record(string account)
		{
			TempData["Account"] = account;
			Console.WriteLine("POST:" + TempData["Account"]);
			return RedirectToAction("Record", "Staff");			
		}		

		public IActionResult Schedule()
		{
			var userAccount = TempData["Account"];
			Console.WriteLine("接收:" + userAccount);
			TempData.Keep();

			// 使用 LINQ 查詢來擷取符合帳號的歷史資料
			var allScheduleQuery = from c in _context.Cases
								  join e in _context.Employers on c.EmployerId equals e.EmployerId
								  join m in _context.Employees on c.EmployeeId equals m.EmployeeId
								  join p in _context.PersonalInfos on c.EmployerId equals p.EmployerId
								  join s in _context.ServiceItems on c.ServiceName equals s.ServiceName
								  where m.Account == userAccount && c.CaseStatus == false								   
								  orderby c.StartDateTime
								  select new ServiceHistoryViewModel()
								  {
									  ServiceDate = c.StartDateTime.Value.Date, // 提取日期部分.Date
									  ServiceTime = c.StartDateTime.Value.TimeOfDay, // 提取時間部分.TimeOfDay
									  EmployerName = p.Name,
									  ServiceItem = c.ServiceName,
									  //ServiceAmount = (int)(s.Price * 0.4m),
									  Account = m.Account,
									  ClientAddress = (p.ResidentialAddress + p.ResidentialAddressSection),
									  Remark = c.Remark,
									  CaseID = c.CaseId
								  };

			// 執行LINQ查詢
			var userSchedule = allScheduleQuery.ToList();		

			return View(userSchedule);			
		}

		[HttpPost]
		public IActionResult Schedule(string account)
		{
			TempData["Account"] = account;
			Console.WriteLine("POST:" + TempData["Account"]);
			return RedirectToAction("Schedule", "Staff");
			
		}

		[HttpPost]
		public IActionResult CancelSchedule(int CaseID)
		{				
			var caseData = _context.Cases.Where(m => m.CaseId == CaseID ).FirstOrDefault();
			

			_context.Cases.Remove(caseData);
			_context.SaveChanges();

			return RedirectToAction("Schedule");
		}

		public IActionResult Notification()
		{
			return View();
		}

	}

}
