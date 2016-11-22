using System;
using System.Collections.Generic;
using System.Linq;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal.Entities;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;

namespace MyUniversity.Dal
{
    public class NHibernateEventListener : DefaultSaveOrUpdateEventListener
    {

        public class CustomSaveOrUpdateEventListener : DefaultSaveOrUpdateEventListener
        {
            protected override object PerformSave(object entity, object id, IEntityPersister persister, bool useIdentityColumn, object anything,
           IEventSource source, bool requiresImmediateIdAccess)
            {
                var entityValue = entity as EntityBase;
                if (entityValue != null)
                {
                    if (entityValue.CreatedBy == null || entityValue.CreatedBy.Equals(string.Empty))
                    {
                        entityValue.CreatedBy = EntityConstant.CreatedBy;
                        entityValue.CreatedOn = DateTime.Now;
                    }
                    else
                    {
                        entityValue.UpdatedBy = EntityConstant.UpdatedBy;
                        entityValue.UpdatedOn = DateTime.Now;
                    }
                }

                foreach (var property in entity.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(entity, null);
                    if (propertyValue == null)
                    {
                        continue;
                    }
                    if (propertyValue.GetType().IsSubclassOf(typeof(EntityBase)))
                    {
                        var value = propertyValue as EntityBase;
                        value.CreatedBy = EntityConstant.CreatedBy;
                        value.CreatedOn = DateTime.Now;
                    }
                }

                return base.PerformSave(entityValue, id, persister, useIdentityColumn, anything, source, requiresImmediateIdAccess);
            }
        }

        public class CustomDeleteEventListener : DefaultDeleteEventListener
        {
            protected override void DeleteEntity(IEventSource session, object entity, EntityEntry entityEntry,
                bool isCascadeDeleteEnabled,
                IEntityPersister persister, ISet<object> transientEntities)
            {
                var entityValue = entity as EntityBase;
                if (entityValue != null)
                {
                    entityValue.Deactive = true;
                    entityValue.UpdatedBy = EntityConstant.UpdatedBy;
                    entityValue.UpdatedOn = DateTime.Now;
                }
                base.DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled, persister, transientEntities);
            }
        }

        public class CustomPostLoadEventListener : DefaultPostLoadEventListener
        {
            public override void OnPostLoad(PostLoadEvent @event)
            {
                base.OnPostLoad(@event);
            }
        }
    }
}