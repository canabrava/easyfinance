namespace RegisterSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] Salt { get; set; } 
        public required DateTime RegisterDate { get; set; }
    }
}
