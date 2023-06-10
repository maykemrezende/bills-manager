namespace Model.Bills
{
    public class Bill
    {
        public Bill()
        {

        }

        public Bill(string name, Money price, bool isPaid = false)
        {
            Name = name;
            Price = price;
            IsPaid = isPaid;
            CreationDateUtc = DateTime.UtcNow;
            Code = GetCode();
        }

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public bool IsPaid { get; private set; }
        public Money Price { get; private set; }
        public DateTime CreationDateUtc { get; set; }

        public void Pay()
        {
            IsPaid = true;
        }

        private static string GetCode()
        {
            var guid = Guid.NewGuid().ToString().Replace(".", "");
            return $"BLL{guid.Substring(Random.Shared.Next(1, guid.Length - 4), 3).ToUpper()}";
        }
    }
}
