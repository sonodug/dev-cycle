namespace wpf_game_dev_cycle.Model
{
    public class Account
    {
        public System.Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Verification_Code { get; set; }
    }
}