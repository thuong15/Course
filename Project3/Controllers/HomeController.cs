using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Project3.Models;
using Project3.ViewModels;

namespace Project3.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly TestContext _context;
		public HomeController(ILogger<HomeController> logger, TestContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var check = _context.Blogs.ToList();
			ViewData["centres"] = _context.Centres.ToList();
			return View(check);
		}

		public IActionResult Blogs(int id)
		{
			var check = _context.Blogs.ToList();
			ViewBag.idblog = id;
			if (id == 0)
			{
				ViewBag.idblog = check.FirstOrDefault().BlogId;
			}
			return View(check);
		}


		//public IActionResult Blogss(int id)
		//{
		//	var check = _context.Blogs.FirstOrDefault(t=>t.BlogId==id);
		//	return View(check);
		//}

		[HttpGet]
		public IActionResult Feedbacks()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Feedbacks(String name, String phone, String message, FeedBack feedBack)
		{
			feedBack.Name = name;
			feedBack.Phone = phone;
			feedBack.Message = message;
			_context.FeedBacks.Add(feedBack);
			_context.SaveChanges();
			return View();
		}


		public IActionResult Courses()
		{
			var check = _context.Courses.Include(cd => cd.CourseDetails).ToList();

			ViewData["basic"] = _context.CourseDetails.Where(t => t.CourseDetailName.Equals("basic")).Include(cd => cd.Course).ToList();
            ViewData["up"] = _context.CourseDetails.Where(t => t.CourseDetailName.Equals("up")).Include(cd => cd.Course).ToList();
            return View(check);
		}
		[HttpGet]
		public IActionResult OrderCourses(int id)
		{
			var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
			{
				var test = _context.OrderCourses.SingleOrDefault(t => t.CourseId == id && t.AccountId == idusser);
				if (test != null)
				{
					return RedirectToAction("Topicses", new { id = id });
				}

			}
			else
			{
				return RedirectToAction("Login", "Accounts");
			}

			var check = _context.Courses.Where(t => t.CourseId == id).ToList();
			return View(check);
		}
		[HttpPost]
		public IActionResult OrderCourses(int id, OrderCourse ordercourse)
		{
			var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
			{
				ordercourse.AccountId = idusser;
				ordercourse.CourseId = id;
				ordercourse.CreatDate = DateTime.Now;
				_context.OrderCourses.Add(ordercourse);
				_context.SaveChanges();
				return RedirectToAction("Topicses", new { id = id });
			}
			else
			{
				return RedirectToAction("Login", "Accounts", new { area = "Accounts" });
			}
			return View();
		}

		public IActionResult Topicses(int id, string? mss)
		{
			var check = _context.Topics
		.Include(t => t.CourseDetails)
		.ThenInclude(cd => cd.Course)
		.Where(t => t.CourseDetails.Course.CourseId == id)
		.ToList();
			if (mss!= null)
			{
				ViewBag.mss = "T";
			}

			ViewData["akfh"] = _context.Courses
				.Include(t => t.CourseDetails)
				.ThenInclude(h => h.Topics)
				.Where(t => t.CourseId == id);
			return View(check);
		}


		public IActionResult Questions(int id)
		{
            var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			int.TryParse(permissionClaim.Value, out int idusser);
            
                var check = _context.Examsses.Where(t => t.TopicId == id&& t.UserId == idusser).FirstOrDefault();
			if(check != null){

				return RedirectToAction("topicses", "home", new {id = id, mss = "T"});
			}
			var questions = _context.Questions.Where(t => t.TopicId == id).Include(q => q.Options).ToList();
			ViewBag.TopicId = id;
			return View(questions);
		}

		public IActionResult CheckQuestions(List<String> questionID, Examss examss, int TopicId)
		{

			var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
			{
				var viewModels = new List<Class>();
				int diem = 0;
				foreach (var item in questionID)
				{
					string[] tokens = item.Split(' ');
					string firstElement = tokens[0];
					var checkDapAn = _context.Questions
			.Where(t => t.QuestionId == int.Parse(firstElement))
			.Include(t => t.Options)
			.FirstOrDefault(t => t.Options.Any(o => o.IsCorrect));

					if (checkDapAn != null)
					{
						var dung = int.Parse(firstElement) + " + " + checkDapAn.Options.Where(t => t.IsCorrect == true).FirstOrDefault().OptionText;
						if (dung.Equals(item))
						{
							diem++;
						}
						viewModels.Add(new Class
						{
							QuestionId = int.Parse(firstElement),
							QuestionText = checkDapAn.QuestionText, // Assuming there's a property for the question text in the Question model.
							IsCorrect = dung.Equals(item)
						});

					}

				}
				examss.Point = diem;
				examss.TopicId = TopicId;
				examss.UserId = idusser;

				_context.Examsses.Add(examss);
				_context.SaveChanges();
				ViewBag.diem = diem;
				//ViewD.questionResults = viewModels;
				ViewData["a"] = viewModels;

            }



			return View();
		}

		

		public IActionResult Examsses()
		{
			var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
			{
				var check = _context.Examsses.Where(t =>t.UserId == idusser)
					.Include(e => e.Topic)
					.ThenInclude(t => t.CourseDetails)
					.ThenInclude(cd => cd.Course)
					.ToList();
				return View(check);
			}
			else
			{
				return RedirectToAction("Login", "Accounts", new { area = "Accounts" });
			}

			return View();
		}

		[HttpGet]
		public IActionResult Profile()
		{
			var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
			{
				var check = _context.Accounts

		.Include(account => account.OrderCourses)
		.ThenInclude(orderCourse => orderCourse.Course)
		.Where(account => account.UserId == idusser).FirstOrDefault();

				//var check = _context.Accounts.FirstOrDefault(t => t.UserId == idusser);

				ViewData["Thuong"] = _context.Accounts

		.Include(account => account.OrderCourses)
		.ThenInclude(orderCourse => orderCourse.Course)
		.Where(account => account.UserId == idusser).ToList();

				return View(check);
			}


			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Profile(int id, [Bind("UserId,UserName,Password,RoleId,FullName,Email,Birthday,Address,Avatar,Phone,LastLogin,CreateDate")] Account account)
		{
			var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
			if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
			{


				if (ModelState.IsValid == false)
				{
					try
					{

						_context.Accounts.Update(account);
						await _context.SaveChangesAsync();
					}
					catch (DbUpdateConcurrencyException)
					{

						throw;

					}
					return RedirectToAction(nameof(Profile));
				}
			}

			return View();
		}

		[HttpGet]
		public IActionResult Admissions()
		{

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Admissions([Bind("AdmissionId,FullName,Email,Address,Phone,Birthday,Maths,Englishs")] Admission admission)
		{
			if (ModelState.IsValid)
			{
				var permissionClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idusser");
				if (permissionClaim != null && int.TryParse(permissionClaim.Value, out int idusser))
				{
					admission.AccountId = idusser;

					_context.Add(admission);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(admission));
				}
			}

			return View(admission);
		}



		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}