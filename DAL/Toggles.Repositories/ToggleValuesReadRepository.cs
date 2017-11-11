using Common.Generic.Collections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggles.BusinessEntities;
using Toggles.DataAccess;
using Toggles.DataAccess.DbEntities;
using Toggles.Repositories.Contracts;

namespace Toggles.Repositories
{
    public class ToggleValuesReadRepository : IToggleValuesReadRepository
    {
        private TogglesDbContext togglesDbContext;

        public ToggleValuesReadRepository(TogglesDbContext togglesDbContext)
        {
            this.togglesDbContext = togglesDbContext;
        }

        public IList<ToggleValue> GetByApplication(ClientApplication application)
        {
            IList<ToggleValueDbEntity> dbEntities = this.GetDbEntitiesByApplication(application);
            IList<ToggleValue> toggleValues = this.GetApplicationToggleValuesFromDbEntities(dbEntities, application);
            IList<ToggleValue> allToggleValues =
                this.GetGlobalToggleValuesFromDbEntitiesExcept(dbEntities, toggleValues);
            toggleValues.AddRange(allToggleValues);
            return toggleValues;
        }

        private IList<ToggleValueDbEntity> GetDbEntitiesByApplication(ClientApplication application)
        {
            IList<ToggleValueDbEntity> dbEntities =
                (from tv in this.togglesDbContext.ToggleValues
                 where tv.ApplicationCodeName == ToggleValueDbEntity.GLOBAL_APPLICATION_CODE_NAME
                 || (tv.ApplicationCodeName == application.CodeName
                     && tv.ApplicationVersion == application.Version)
                 select tv)
                .Include(tv => tv.Toggle)
                .ToList();
            return dbEntities;
        }

        private IList<ToggleValue> GetApplicationToggleValuesFromDbEntities(IList<ToggleValueDbEntity> dbEntities,
            ClientApplication application)
        {
            IEnumerable<ToggleValueDbEntity> appToggleValueDbEntities =
                dbEntities.Where(dbe => dbe.ApplicationCodeName == application.CodeName);
            IList<ToggleValue> appToggleValues = this.ProjectToBusinessEntities(appToggleValueDbEntities);
            return appToggleValues;
        }

        private IList<ToggleValue> ProjectToBusinessEntities(IEnumerable<ToggleValueDbEntity> toggleValueDbEntities)
        {
            IList<ToggleValue> toggleValues =
                (from tv in toggleValueDbEntities
                 select new ToggleValue()
                 {
                     Id = tv.Id,
                     ToggleId = tv.ToggleId,
                     Toggle = new Toggle()
                     {
                         Id = tv.Toggle.Id,
                         CodeName = tv.Toggle.CodeName,
                         Description = tv.Toggle.Description
                     },
                     Value = tv.Value
                 }).ToList();
            return toggleValues;
        }

        private IList<ToggleValue> GetGlobalToggleValuesFromDbEntitiesExcept(IList<ToggleValueDbEntity> dbEntities,
            IList<ToggleValue> exceptToggleValues)
        {
            IEnumerable<ToggleValueDbEntity> globalToggleValueDbEntities =
                from tv in dbEntities
                where tv.ApplicationCodeName == ToggleValueDbEntity.GLOBAL_APPLICATION_CODE_NAME
                && !exceptToggleValues.Any(etv => etv.Toggle.Id == tv.ToggleId)
                select tv;
            IList<ToggleValue> globalToggleValues = this.ProjectToBusinessEntities(globalToggleValueDbEntities);
            return globalToggleValues;
        }
    }
}
