public class RegisterDto
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
