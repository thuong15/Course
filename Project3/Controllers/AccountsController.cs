using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project3.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project3.Controllers
{
    public class AccountsController : Controller
    {
        private readonly TestContext _testContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment v;
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public AccountsController(TestContext testContext, IHttpContextAccessor contextAccessor, IWebHostEnvironment v)
        {
            _testContext = testContext;
            _contextAccessor = contextAccessor;
            this.v = v;
        }
        [HttpGet]
        public IActionResult Login()
        {
            //TempData["Errol"] = "Loi roi";
            return View();
        }

        [HttpPost]
        public IActionResult Login(String user, String password)
        {
			string hasdPassword = GetMd5Hash(password);
			var checkTk = _testContext.Accounts.SingleOrDefault(tk => tk.UserName == user && tk.Password == hasdPassword);
            if(checkTk != null)
            {
                if(checkTk.RoleId == 1)
                {
                    if (checkTk != null)
                    {
                        var claims = new List<Claim>
                {
                    new Claim("name",checkTk.FullName),
                    new Claim("avatar", checkTk.Avatar),
                    new Claim("idusser", checkTk.UserId.ToString()),
                    
					//mai làm check tk user
				};
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        // Đăng nhập người dùng
                        HttpContext.SignInAsync(claimsPrincipal);
                    }
                    String kiemtra = JsonConvert.SerializeObject(checkTk);
                    _contextAccessor.HttpContext.Session.SetString("TkList", kiemtra);
                    HttpContext.Session.SetString("user", kiemtra);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if(checkTk.RoleId == 2)
                {
                    if (checkTk != null)
                    {
                        var claims = new List<Claim>
                {
                    new Claim("name",checkTk.FullName),
                    new Claim("avatar", checkTk.Avatar),
                    new Claim("roleid",checkTk.RoleId.ToString()),
                    new Claim("idusser", checkTk.UserId.ToString()),
                    
					//mai làm check tk user
				};
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        // Đăng nhập người dùng
                        HttpContext.SignInAsync(claimsPrincipal);
                    }

                        String kiemtra = JsonConvert.SerializeObject(checkTk);
                    _contextAccessor.HttpContext.Session.SetString("TkList", kiemtra);
                    HttpContext.Session.SetString("user", kiemtra);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                else
                {
                    return View();
                }
            }

            return View();
        }

		// Hàm mã hoá MD5 giống như đã viết ở trên
		public static string GetMd5Hash(string input)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder builder = new StringBuilder();

				for (int i = 0; i < data.Length; i++)
				{
					builder.Append(data[i].ToString("x2"));
				}

				return builder.ToString();
			}
		}

		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,UserName,Password,RoleId,FullName,Email,Address,Phone,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                if (account.Avatar == null || account.Avatar.Length == 0)
                {
                    account.Avatar = "Avt.PNG";
                }
				account.Password = GetMd5Hash(account.Password);
                
                _testContext.Accounts.Add(account);
                await _testContext.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

    }
}
