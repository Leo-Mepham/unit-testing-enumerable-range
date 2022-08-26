namespace DataAccess
{
    public interface IUserRepository
    {
        public User GetById(int id);
        public User? GetByUserId(Guid userId);
        public int GetUserCount();
    }
}
