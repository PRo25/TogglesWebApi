using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.BusinessEntities;
using Toggles.BusinessEntities.Exceptions;
using Toggles.DataAccess;
using Toggles.DataAccess.DbEntities;
using Toggles.Repositories.Contracts;

namespace Toggles.Repositories
{
    public class TogglesReadRepository : ITogglesReadRepository
    {
        private TogglesDbContext togglesDbContext;

        public TogglesReadRepository(TogglesDbContext togglesDbContext)
        {
            this.togglesDbContext = togglesDbContext;
        }

        public IList<Toggle> GetAll()
        {
            IQueryable<ToggleDbEntity> dbQuery = this.QueryAllDbEntities();
            IList<Toggle> toggles = this.ProjectToBusinessEntities(dbQuery);
            return toggles;
        }

        private IQueryable<ToggleDbEntity> QueryAllDbEntities()
        {
            return this.togglesDbContext.Toggles.AsNoTracking();
        }

        private IList<Toggle> ProjectToBusinessEntities(IQueryable<ToggleDbEntity> dbQuery)
        {
            IList<Toggle> toggles =
                (from t in dbQuery
                 select new Toggle()
                 {
                     CodeName = t.CodeName,
                     Description = t.Description,
                     Id = t.Id,
                     Values = this.ProjectToApplicationToggleValues(t.Values)
                 }).ToList();
            return toggles;
        }

        private IList<ClientApplicationToggleValue> ProjectToApplicationToggleValues(
            ICollection<ToggleValueDbEntity> toggleValuesDbEntities)
        {
            IList<ClientApplicationToggleValue> applicationToggleValues =
                (from tv in toggleValuesDbEntities
                 select new ClientApplicationToggleValue()
                 {
                     Id = tv.Id,
                     Value = tv.Value,
                     ToggleId = tv.ToggleId,
                     Application = new ClientApplication()
                     {
                         CodeName = tv.ApplicationCodeName,
                         Version = tv.ApplicationVersion
                     }
                 }).ToList();
            return applicationToggleValues;
        }

        public Toggle GetById(Guid id)
        {
            IQueryable<ToggleDbEntity> dbQuery = this.QueryDbEntitiesById(id);
            Toggle toggle = this.ProjectToBusinessEntities(dbQuery).SingleOrDefault();
            if (toggle == null)
            {
                throw new EntityNotFoundException(typeof(Toggle), id.ToString());
            }
            return toggle;
        }

        private IQueryable<ToggleDbEntity> QueryDbEntitiesById(Guid id)
        {
            IQueryable<ToggleDbEntity> dbQuery = this.QueryAllDbEntities().Where(t => t.Id == id);
            return dbQuery;
        }

        public bool HasAnyById(Guid id)
        {
            bool hasAny = this.QueryDbEntitiesById(id).Any();
            return hasAny;
        }

        public bool HasAnyByCodeName(string codeName)
        {
            bool hasAny = this.QueryDbEntitiesByCodeName(codeName).Any();
            return hasAny;
        }

        private IQueryable<ToggleDbEntity> QueryDbEntitiesByCodeName(string codeName)
        {
            IQueryable<ToggleDbEntity> dbQuery = this.QueryAllDbEntities().Where(t => t.CodeName == codeName);
            return dbQuery;
        }
    }
}
