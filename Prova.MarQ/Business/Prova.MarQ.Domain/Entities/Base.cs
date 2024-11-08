namespace Prova.MarQ.Domain.Entities
{
    public abstract class Base
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
