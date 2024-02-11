using Model.Bills;
using Model.Tenants;
using Model.Users;

namespace Model.Tags
{
    public class Tag : EntityAudited, IMayBeUserSpecific
    {
        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDateUtc { get; private set; }

        public List<Bill> Bills { get; }
        public int? UserId { get; private set; }
        public User User { get; }

        public Tag()
        {
            
        }

        public Tag(string name, int? userId)
        {
            Code = GetCode();
            Name = name.ToUpper();
            CreationDateUtc = DateTime.UtcNow;
            UserId = userId;
        }

        public void Update(string name)
        {
            Name = name.ToUpper();
        }

        private string GetCode()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            return $"TAG{guid.Substring(Random.Shared.Next(1, guid.Length - 4), 3).ToUpper()}";
        }
    }
}
