using FluentNHibernate.Mapping;
using WLN.Test.Project.Model.AuthModels;

namespace WLN.Test.Project.DAL.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Map(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Password);
            Map(x => x.PasswordSault);
            References(x => x.Role);
        }
    }
}
