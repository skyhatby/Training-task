using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using WLN.Test.Project.DAL.Mappings;
using WLN.Test.Project.Model.Exceptions;

namespace WLN.Test.Project.DAL
{
    public static class SessionFactoryManager
    {
        private const string ConnectionName = "WLN";

        private static readonly Lazy<ISessionFactory> SessionFactoryLazy =
            new Lazy<ISessionFactory>(CreateSessionFactory);

        public static ISessionFactory GetSessionFactory()
        {
            return SessionFactoryLazy.Value;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var config =
                    MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(ConnectionName));
                return Fluently.Configure().Database(config).Mappings(ConfigureMappings).BuildSessionFactory();
            }
            catch (FluentConfigurationException ex)
            {
                throw ex.PotentialReasons.Count == 0
                    ? new ConnectionException(ex.Message, ex.InnerException)
                    : new ConnectionException(string.Join(Environment.NewLine, ex.PotentialReasons), ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new ConnectionException(ex.Message, ex);
            }
        }

        private static void ConfigureMappings(MappingConfiguration mapping)
        {
            // Membership
            mapping.FluentMappings.Add<RoleMap>();
            mapping.FluentMappings.Add<UserMap>();
        }
    }
}
