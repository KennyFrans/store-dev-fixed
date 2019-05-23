using System;
using System.Collections.Generic;
using System.Text;

namespace App.Commons
{
    public class EntityBase
    {
        public virtual int Id { get; set; }

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
