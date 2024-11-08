namespace Prova.MarQ.Domain.Entities
{
    public class Employee : Base
    {
        public string EmployeePin { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDocument { get; set; }
        public bool IsEmployeeDeleted { get; set; }
        public int CompanyId { get; set; }

        // Propriedade de navegação para a relação com Company
        public Company Company { get; set; }
        public ICollection<Clocking> Clocking { get; set; }
    }
}
