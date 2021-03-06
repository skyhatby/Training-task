﻿using FluentNHibernate.Mapping;
using WLN.Test.Project.Model.MemberhshipModels;

namespace WLN.Test.Project.DAL.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Schema("[Membership]");
            Table("[User]");
            Id(x => x.Id).GeneratedBy.HiLo("[dbo].[HiLo]", "NextHi", "100");

            Map(x => x.Name).Length(128).Not.Nullable();
            Map(x => x.Password).Length(128).Not.Nullable();
            Map(x => x.PasswordSalt).Length(128).Not.Nullable();

            HasManyToMany(x => x.Roles)
                .Schema("[Membership]")
                .Table("[UserRole]")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId")
                .AsSet();
        }
    }
}
