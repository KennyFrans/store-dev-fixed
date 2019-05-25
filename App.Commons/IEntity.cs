using System;
using System.Collections.Generic;
using System.Text;

namespace App.Commons
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
