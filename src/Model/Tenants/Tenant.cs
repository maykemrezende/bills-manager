namespace Model.Tenants;

public class Tenant : EntityAudited
{
    public Tenant(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

    public Tenant()
    {
        
    }
}
