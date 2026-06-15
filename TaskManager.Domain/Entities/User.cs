namespace TaskManager.Domain.Entities
{
    #region User

    public class User
    {
        public Guid Id { get; private set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }

    #endregion
}