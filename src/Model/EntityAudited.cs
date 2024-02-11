namespace Model;

public class EntityAudited
{
    public DateTime CreationDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    public DateTime DeleteDate { get; private set; }
    public int CreationUserId { get; private set; }
    public int UpdateUserId { get; private set; }
    public int DeleteUserId { get; private set; }

    public void Create(int userId)
    {
        CreationDate = DateTime.Now;
        CreationUserId = userId;
    }

    public void Delete(int userId)
    {
        DeleteDate = DateTime.Now;
        DeleteUserId = userId;
    }

    public void Update(int userId)
    {
        UpdateDate = DateTime.Now;
        UpdateUserId = userId;
    }
}
