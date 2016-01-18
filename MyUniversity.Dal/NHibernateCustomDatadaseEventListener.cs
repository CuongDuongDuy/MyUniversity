using System;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal.Entities;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace MyUniversity.Dal
{
    public class NHibernateCustomPreEventListener : IPreUpdateEventListener, IPreInsertEventListener, IPostLoadEventListener
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

        private void SetRelative(IEventSource session, IEntityPersister persister, object[] state, object entity)
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

        public void OnPostLoad(PostLoadEvent @event)
        {
            var entity = @event.Entity;
            var entityType = entity.GetType();
            switch (entityType.Name)
            {
                case "Course":
                    (entity as Course).DepartmentId = (entity as Course).Department.Id;
                    break;
                case "StudentProfile":
                    (entity as StudentProfile).DepartmentId = (entity as StudentProfile).Department.Id;
                    break;
                case "InstructorProfile":
                    (entity as InstructorProfile).DepartmentId = (entity as InstructorProfile).Department.Id;
                    break;
            }
        }
    }
}