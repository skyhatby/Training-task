using FluentNHibernate.Mapping;
using WLN.Test.Project.Model.AuthModels;

namespace WLN.Test.Project.DAL.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Map(x => x.Id);
            Map(x => x.Name);
        }
    }
}
