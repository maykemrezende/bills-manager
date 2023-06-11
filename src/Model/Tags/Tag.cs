using Model.Bills;

namespace Model.Tags
{
    public class Tag
    {
        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDateUtc { get; private set; }

        public List<Bill> Bills { get; }

        public Tag(string name)
        {
            Code = GetCode();
            Name = name.ToUpper();
            CreationDateUtc = DateTime.UtcNow;
        }

        public void Update(string name)
        {
            Name = name;
        }

        private string GetCode()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            return $"TAG{guid.Substring(Random.Shared.Next(1, guid.Length - 4), 3).ToUpper()}";
        }
    }
}
