namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public User GetById(int id)
        {
            return _userContext
                .Users
                .Single(u => u.Id == id);
        }

        public User? GetByUserId(Guid userId)
        {
            return _userContext
                .Users
                .FirstOrDefault(u => u.UserId == userId);
        }

        public int GetUserCount()
        {
            return _userContext
                .Users
                .Count();
        }
    }
}
