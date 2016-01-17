using System;
using System.Diagnostics.Eventing.Reader;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Mappings.NHibernate;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace MyUniversity.ApiStart
{
    public class DatabaseInitialization
    {
        public static FluentConfiguration GetConfig()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ShowSql()
                        .ConnectionString(c => c.FromConnectionStringWithKey("MyUniversityDb")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CourseMapping>()
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .ExposeConfiguration(
                    c =>
                        c.EventListeners.PreUpdateEventListeners =
                            new IPreUpdateEventListener[] {new NHibernateCustomPreEventListener()})
                .ExposeConfiguration(
                    c =>
                        c.EventListeners.PreInsertEventListeners =
                            new IPreInsertEventListener[] {new NHibernateCustomPreEventListener()});
        }

        public static void Run(AppSettingConstant.DbFrameworkType dbFrameworkUse)
        {
            switch (dbFrameworkUse)
            {
                case AppSettingConstant.DbFrameworkType.Nhibernate:
                    GetConfig().BuildConfiguration();
                    break;

                case AppSettingConstant.DbFrameworkType.EntityFramework:
                    // Database.SetInitializer(new MyUniversityDbInitializer());
                    break;
            }
        }
    }

    public class NHibernateCustomPreEventListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var entity = @event.Entity;
            if (entity != null)
            {
                var updatedOn = DateTime.UtcNow;
                var updatedBy = EntityConstant.UpdatedBy;
                Set(@event.Persister, @event.State, "UpdatedOn", updatedOn);
                Set(@event.Persister, @event.State, "UpdatedBy", updatedBy);
                SetRelative(@event.Session, @event.Persister, @event.State, @event.Entity);
            }
            return false;
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var entity = @event.Entity as EntityBase;
            if (entity != null)
            {
                var createdOn = DateTime.UtcNow;
                var createdBy = EntityConstant.UpdatedBy;
                Set(@event.Persister, @event.State, "CreatedOn", createdOn);
                Set(@event.Persister, @event.State, "CreatedBy", createdBy);
                entity.CreatedBy = createdBy;
                entity.CreatedOn = createdOn;
                SetRelative(@event.Session, @event.Persister, @event.State, @event.Entity);
            }
            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }

        private void SetRelative(IEventSource session, IEntityPersister persister, object[]state, object entity)
        {
            var entityType = entity.GetType();
            Department department;
            switch (entityType.Name)
            {
                case "Course":
                    department = session.Load<Department>((entity as Course).DepartmentId);
                    Set(persister, state, "Department", department);
                    (entity as Course).Department = department;
                    break;
                case "StudentProfile":
                    department = session.Load<Department>((entity as StudentProfile).DepartmentId);
                    Set(persister, state, "Department", department);
                    (entity as StudentProfile).Department = department;
                    break;
                case "InstructorProfile":
                    department = session.Load<Department>((entity as InstructorProfile).DepartmentId);
                    Set(persister, state, "Department", department);
                    (entity as InstructorProfile).Department = department;
                    break;
            }
        }
    }
}