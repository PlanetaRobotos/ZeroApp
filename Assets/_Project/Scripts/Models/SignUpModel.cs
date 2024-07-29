namespace _Project.Scripts.Models
{
    public struct SignUpModel
    {
        public string Email;
        public string Username;
        public string Password;

        public override string ToString() => 
            $"SignUpModel: Email: {Email}, Username: {Username}, Password: {Password}";
    }
}