using FluentNHibernate.Mapping;
using WLN.Test.Project.Model.AuthModels;

namespace WLN.Test.Project.DAL.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Schema("[Membership]");
            Table("[Role]");
            Map(x => x.Id);
            Map(x => x.Name).Length(128).Not.Nullable(); ;
        }
    }
}
