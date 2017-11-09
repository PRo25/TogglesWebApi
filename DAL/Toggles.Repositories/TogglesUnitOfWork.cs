using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using Toggles.BusinessEntities;
using Toggles.DataAccess;
using Toggles.DataAccess.DbEntities;
using Toggles.Repositories.Contracts;

namespace Toggles.Repositories
{
    public class TogglesUnitOfWork : ITogglesUnitOfWork
    {
        private TogglesDbContext togglesDbContext;

        public ITogglesRepository TogglesRepository { get; private set; }

        public IClientApplicationToggleValuesRepository ClientApplicationToggleValuesRepository { get; private set; }

        public TogglesUnitOfWork(TogglesDbContext togglesDbContext,
            ITogglesRepository togglesRepository,
            IClientApplicationToggleValuesRepository applicationToggleValuesRepository)
        {
            this.togglesDbContext = togglesDbContext;
            this.TogglesRepository = togglesRepository;
            this.ClientApplicationToggleValuesRepository = applicationToggleValuesRepository;
        }

        public int SaveChanges()
        {
            this.ApplyChangesToDbContext();
            int nrOfSavedChanges = this.togglesDbContext.SaveChanges();
            return nrOfSavedChanges;
        }

        private void ApplyChangesToDbContext()
        {
            this.ApplyToggleChangesToDbContext();
            this.ApplyApplicationToggleValueChangesToDbContext();
        }

        private void ApplyToggleChangesToDbContext()
        {
            this.ApplyChangedToggles(this.TogglesRepository.Added,
                (dbEntity) => this.togglesDbContext.Toggles.Add(dbEntity));
            this.ApplyChangedToggles(this.TogglesRepository.Updated,
                (dbEntity) => this.togglesDbContext.Toggles.Update(dbEntity));
            this.ApplyChangedToggles(this.TogglesRepository.Deleted,
                (dbEntity) => this.AttachDbEntityAsDeleted(dbEntity));
        }

        private void ApplyChangedToggles(IList<Toggle> changedToggles, Action<ToggleDbEntity> applyAction)
        {
            foreach (Toggle businessEntity in changedToggles)
            {
                ToggleDbEntity dbEntity = this.ConvertToDbEntity(businessEntity);
                applyAction(dbEntity);
            }
        }

        private ToggleDbEntity ConvertToDbEntity(Toggle businessEntity)
        {
            ToggleDbEntity dbEntity = new ToggleDbEntity()
            {
                CodeName = businessEntity.CodeName,
                Description = businessEntity.Description,
                Id = businessEntity.Id
            };
            return dbEntity;
        }

        private void AttachDbEntityAsDeleted(object dbEntity)
        {
            EntityEntry entityEntry = this.togglesDbContext.Entry(dbEntity);
            entityEntry.State = EntityState.Deleted;
        }

        private void ApplyApplicationToggleValueChangesToDbContext()
        {
            this.ApplyChangedApplicationToggleValues(this.ClientApplicationToggleValuesRepository.Added,
                (dbEntity) => this.togglesDbContext.ToggleValues.Add(dbEntity));
            this.ApplyChangedApplicationToggleValues(this.ClientApplicationToggleValuesRepository.Updated,
                (dbEntity) => this.togglesDbContext.ToggleValues.Update(dbEntity));
            this.ApplyChangedApplicationToggleValues(this.ClientApplicationToggleValuesRepository.Deleted,
                (dbEntity) => this.AttachDbEntityAsDeleted(dbEntity));
        }

        private void ApplyChangedApplicationToggleValues(IList<ClientApplicationToggleValue> changedAppToggleValues,
            Action<ToggleValueDbEntity> applyAction)
        {
            foreach (ClientApplicationToggleValue businessEntity in changedAppToggleValues)
            {
                ToggleValueDbEntity dbEntity = this.ConvertToDbEntity(businessEntity);
                applyAction(dbEntity);
            }
        }

        private ToggleValueDbEntity ConvertToDbEntity(ClientApplicationToggleValue businessEntity)
        {
            ToggleValueDbEntity dbEntity = new ToggleValueDbEntity()
            {
                Id = businessEntity.Id,
                ApplicationCodeName = businessEntity.Application.CodeName,
                ApplicationVersion = businessEntity.Application.Version,
                ToggleId = businessEntity.ToggleId,
                Value = businessEntity.Value
            };
            return dbEntity;
        }
    }
}
