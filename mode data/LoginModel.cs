namespace 药店管理.mode_data
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Category
    {
       
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
    }
    public class Category1
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public string? Remark1 { get; set; }
    }
}
