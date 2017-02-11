namespace BL.DTO
{
    public class AuthorModel
    {
        public string UserName { get; set; }

        public string Role { get; set; }

        public string Avatar { get; set; }

        public string Fio { get; set; }

        public AuthorModel(){}

        public AuthorModel(string userName, string role, string avatar, string fio = "")
        {
            UserName = userName;
            Role = role;
            Avatar = avatar;
            Fio = fio;
        }
    }
}
