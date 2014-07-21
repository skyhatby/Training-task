using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model
{
    public class NamedEntity : Entity<int>
    {
        public virtual string Name { get; set; }
    }
}
