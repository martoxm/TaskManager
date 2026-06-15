namespace TaskManager.Domain.Entities
{
    #region User

    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

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