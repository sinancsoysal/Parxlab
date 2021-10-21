using System;

namespace Parxlab.Entities
{
    public class Invoice:BaseEntity
    {
        public Guid ReservedId { get; set; }
        public virtual Reserved Reserved { get; set; }
        public DateTime StartTime{ get; set; }
        public DateTime EndTime{ get; set; }
        public double Price { get; set; }
    }
}
