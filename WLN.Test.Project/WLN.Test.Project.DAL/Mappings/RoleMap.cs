using FluentNHibernate.Mapping;
using WLN.Test.Project.Model.MemberhshipModels;

namespace WLN.Test.Project.DAL.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Schema("[Membership]");
            Table("[Role]");
            Id(x => x.Id).GeneratedBy.Assigned();

            Map(x => x.Name).Length(128).Not.Nullable(); ;
        }
    }
}
