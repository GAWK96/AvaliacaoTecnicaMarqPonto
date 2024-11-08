namespace Prova.MarQ.Domain.Entities
{
    public class Company : Base
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDocument { get; set; }
        public bool IsCompanyDeleted { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
