using Model.Bills;
using Model.Tags;

namespace Model.Tenants;

public class User : EntityAudited
{
    public User(int id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }
    public List<Bill> Bills { get; }
    public List<Tag> Tags { get; }

    public User()
    {
        
    }
}
