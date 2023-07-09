using System.Collections.Generic;
using System.Linq;
using UserData;

public class UserRepository
{
    private readonly UserContext _userContext;

    public UserRepository()
    {
        _userContext = new UserContext();
    }

    public User GetUser(int userId)
    {
        return _userContext.Users.FirstOrDefault(u => u.UserId == userId);
    }


    public void CreateUser(User newUser)
    {
        _userContext.Add(newUser);
        _userContext.SaveChanges();
    }

    public List<User> GetAllUsers()
    {
        return _userContext.Users.ToList();
    }

    public void DeleteUser(int userId)
    {
        User user = _userContext.Users.FirstOrDefault(u => u.UserId == userId);
        if (user != null)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }
    }

    public void UpdateUser(User user)
    {
        _userContext.Users.Update(user);
        _userContext.SaveChanges();
    }


}