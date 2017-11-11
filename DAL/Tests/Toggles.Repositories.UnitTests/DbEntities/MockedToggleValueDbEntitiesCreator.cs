using System;
using System.Collections.Generic;
using Toggles.DataAccess.DbEntities;

namespace Toggles.Repositories.UnitTests.DbEntities
{
    public class MockedToggleValueDbEntitiesCreator
    {
        public IList<ToggleValueDbEntity> CreateList(int nrOfItems = 3)
        {
            IList<ToggleValueDbEntity> toggleValues = new List<ToggleValueDbEntity>();
            for (int i = 0; i < nrOfItems; i++)
            {
                ToggleValueDbEntity toggleValue = i == 0
                    ? this.CreateGlobalToggleValueDbEntity()
                    : this.CreateToggleValueDbEntity();
                toggleValues.Add(toggleValue);
            }
            return toggleValues;
        }

        public ToggleValueDbEntity CreateToggleValueDbEntity()
        {
            return this.CreateToggleValueDbEntity("TestApp", "1.0");
        }

        private ToggleValueDbEntity CreateToggleValueDbEntity(string appCodeName, string appVersion = null)
        {
            var toggle = new ToggleDbEntity()
            {
                CodeName = Guid.NewGuid().ToString()
            };
            var toggleValue = new ToggleValueDbEntity()
            {
                ApplicationCodeName = appCodeName,
                ApplicationVersion = appVersion,
                Value = true,
                Toggle = toggle,
                ToggleId = toggle.Id
            };
            return toggleValue;
        }

        public ToggleValueDbEntity CreateGlobalToggleValueDbEntity()
        {
            return this.CreateToggleValueDbEntity(ToggleValueDbEntity.GLOBAL_APPLICATION_CODE_NAME);
        }
    }
}
