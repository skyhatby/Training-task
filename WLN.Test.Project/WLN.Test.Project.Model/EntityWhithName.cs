using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model
{
    public class EntityWhithName : Entity<long>
    {
        public virtual string Name { get; set; }
    }
}
