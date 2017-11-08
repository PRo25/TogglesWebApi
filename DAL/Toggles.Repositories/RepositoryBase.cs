using Common.Generic.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggles.Repositories
{
    public abstract class RepositoryBase<TBusinessEntity>
        where TBusinessEntity : class
    {
        private IList<TBusinessEntity> added = new List<TBusinessEntity>();
        public IList<TBusinessEntity> Added
        {
            //returns a list copy to avoid that the original list is changed outside the repository.
            get => this.added.ToList();
        }

        private IList<TBusinessEntity> updated = new List<TBusinessEntity>();
        public IList<TBusinessEntity> Updated
        {
            //returns a list copy to avoid that the original list is changed outside the repository.
            get => this.updated.ToList();
        }

        private IList<TBusinessEntity> deleted = new List<TBusinessEntity>();
        public IList<TBusinessEntity> Deleted
        {
            //returns a list copy to avoid that the original list is changed outside the repository.
            get => this.deleted.ToList();
        }

        public void Add(TBusinessEntity businessEntity)
        {
            this.ValidateIfCanAdd(businessEntity);
            this.added.AddOrReplace(businessEntity,
                this.GetIdentityEqualityPredicate(businessEntity));
        }

        private void ValidateIfCanAdd(TBusinessEntity businessEntity)
        {
            if (this.IsUpdated(businessEntity) || this.IsDeleted(businessEntity))
            {
                throw new InvalidOperationException($"Cannot add {typeof(TBusinessEntity).Name} because it's "
                    + $"already updated or deleted.");
            }
        }

        private bool IsUpdated(TBusinessEntity businessEntity)
        {
            return this.IsInList(businessEntity, this.updated);
        }

        private bool IsInList(TBusinessEntity businessEntity, IList<TBusinessEntity> list)
        {
            Func<TBusinessEntity, bool> identityEqualityPredicate = this.GetIdentityEqualityPredicate(businessEntity);
            bool isInList = list.Any(identityEqualityPredicate);
            return isInList;
        }

        protected abstract Func<TBusinessEntity, bool> GetIdentityEqualityPredicate(TBusinessEntity businessEntity);

        private bool IsDeleted(TBusinessEntity businessEntity)
        {
            return this.IsInList(businessEntity, this.deleted);
        }

        public void Update(TBusinessEntity businessEntity)
        {
            this.ValidateIfCanUpdate(businessEntity);
            this.updated.AddOrReplace(businessEntity,
                this.GetIdentityEqualityPredicate(businessEntity));
        }

        private void ValidateIfCanUpdate(TBusinessEntity businessEntity)
        {
            if (this.IsAdded(businessEntity) || this.IsDeleted(businessEntity))
            {
                throw new InvalidOperationException($"Cannot update {typeof(TBusinessEntity).Name} because it's "
                    + $"already added or deleted.");
            }
        }

        private bool IsAdded(TBusinessEntity businessEntity)
        {
            return this.IsInList(businessEntity, this.added);
        }

        public void Delete(TBusinessEntity businessEntity)
        {
            Func<TBusinessEntity, bool> identityEqualityPredicate = this.GetIdentityEqualityPredicate(businessEntity);

            if (this.IsAdded(businessEntity))
            {
                //if the entity is in the Added items list them it isn't persisted in the DB yet, so it's only necessary 
                //to remove it from that list.
                this.added.RemoveWhere(identityEqualityPredicate);
            }
            else
            {
                this.updated.RemoveWhere(identityEqualityPredicate);
                this.deleted.AddIfNotExists(businessEntity, identityEqualityPredicate);
            }
        }
    }
}
