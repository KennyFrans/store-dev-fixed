using System;

namespace App.Commons
{

    public class EntityBase
    {
        public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object other)
        {
            var another = (EntityBase)other;
            return Id == another.Id;
        }
    }
}
