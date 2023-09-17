using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using 药店管理.mode_data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace 药店管理.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        [Route("auth/login")]
        public ActionResult Login(LoginModel credentials)
        {
            // 根据实际逻辑验证用户名和密码
            if (IsValidUser(credentials))
            {
                var token = GenerateJwtToken(credentials.Username);

                return Ok(new { token });
            }

            return Unauthorized();
        }
        private string GenerateJwtToken(string username)
        {
            // 设置 JWT 的相关参数
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("1234CustomKey1234567890123456789");                                                                                    // 设置密钥，请使用足够复杂和安全的密钥
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // 设置过期时间，请根据实际需求进行修改
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private bool IsValidUser(LoginModel credentials)
        {
            // 进行用户名和密码的验证逻辑，并返回验证结果
            using (var context = new MyDbContext())
            {
                return context.Theuserlogson.ToList().TakeWhile(x => x.Username == credentials.Username && x.Password == credentials.Password).Any();

            }
        }
        [HttpPost]
        [Route("/api/categories")]
        public ActionResult Categories()
        {
            using (var db = new MyDbContext())
            {

                var categoryList = db.Categories.ToList();
                return Ok(categoryList);
            }
        }

        [HttpPost]
        [Route("api/categories1")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (category == null) { return StatusCode(400); }
            using (var db = new MyDbContext())
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();

                    return Ok(new { message = "添加分类成功" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = "添加分类失败：" + ex.Message });
                }
            }
        }
        [HttpDelete("/api/categories2/{id}")]
        public IActionResult actiondelete(int id) {

            using (var db = new MyDbContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);

                if (category == null)
                {
                    return StatusCode(400);
                }

                db.Categories.Remove(category);
                db.SaveChanges();

                return StatusCode(200);
            }
        }
      [HttpPost("/api/categories/save")]
        public IActionResult Actionedit([FromBody] Category category)
        {
           if (category == null) return StatusCode(400);
          using(var db = new MyDbContext())
           {

              var cate = db.Categories.FirstOrDefault(x => x.Id == category.Id);
                if (cate == null) return StatusCode(400);
              cate.Name = category.Name;
                cate.Description = category.Description;
                cate.Remark = category.Remark;
                db.SaveChanges();
                return StatusCode(200);
           }
        }
        [HttpGet("your-backend-url")]
        public IActionResult Aactionget()
        {
            using(var db = new MyDbContext())
            {
                return Ok( db.Categories.Where(x=>x.Name!=null));
            }
        }
        [HttpPost("api/categories/save/hq")]
        public IActionResult Aactionget1([FromBody] Category1 category)
        {
            if (category == null)
            {
                return StatusCode(400);
            }

            using (var db = new MyDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 执行插入操作
                        db.Categories1.Add(category);
                        db.SaveChanges();

                        // 提交事务
                        transaction.Commit();

                        return StatusCode(200);
                    }
                    catch (Exception ex)
                    {
                        // 处理异常情况
                        transaction.Rollback();
                        Console.WriteLine("事务执行失败：" + ex.Message);
                        return StatusCode(500);
                    }
                }
            }
        }
        [HttpGet("/api/ddedate")]
        public IActionResult Aactionget2(int id)
        {
            using (var db = new MyDbContext())
            {
              db.Remove( db.Categories1.Where(x => x.Id == id).ToList());
                return Ok();
            }
        }
    }

}