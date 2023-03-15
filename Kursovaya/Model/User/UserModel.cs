namespace Kursovaya.Model.User;

public partial class UserModel
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
