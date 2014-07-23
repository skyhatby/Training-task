using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model
{
    /// <summary>
    /// T is the type of Id of the Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityWhithName<T> : Entity<T>
    {
        public virtual string Name { get; set; }
    }
}
