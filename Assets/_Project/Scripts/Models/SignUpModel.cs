namespace _Project.Models
{
    public struct SignUpModel
    {
        public string Email;
        public string Username;
        public string Password;

        public override string ToString()
        {
            return $"SignUpModel: Email: {Email}, Username: {Username}, Password: {Password}";
        }
    }
}