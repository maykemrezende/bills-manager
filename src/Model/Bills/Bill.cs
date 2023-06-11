using System.Globalization;

namespace Model.Bills
{
    public class Bill
    {
        public Bill()
        {

        }

        public Bill(string name, Money price, int month, int year, bool isPaid = false)
        {
            Name = name.ToUpper();
            Price = price;
            IsPaid = isPaid;
            CreationDateUtc = DateTime.UtcNow;
            Code = GetCode();
            Period = GetPeriod(month, year);
        }

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public bool IsPaid { get; private set; }
        public Money Price { get; private set; }
        public Period Period { get; private set; }
        public DateTime CreationDateUtc { get; set; }

        public void Pay()
        {
            IsPaid = true;
        }

        public void Update(string name, int month, int year, Money price)
        {
            Name = name; 
            Price = price;
            Period = GetPeriod(month, year);
        }

        private static string GetCode()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            return $"BLL{guid.Substring(Random.Shared.Next(1, guid.Length - 4), 3).ToUpper()}";
        }

        private static Period GetPeriod(int month, int year)
        {
            var monthName = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(month).ToUpper();
            return new Period(month, year, monthName);
        }
    }
}
