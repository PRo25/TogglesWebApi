using System.Collections.Generic;
using Toggles.DataAccess.DbEntities;

namespace Toggles.Repositories.UnitTests.DbEntities
{
    public class MockedToggleDbEntitiesCreator
    {
        public IList<ToggleDbEntity> CreateList(int nrOfItems = 3)
        {
            IList<ToggleDbEntity> toggles = new List<ToggleDbEntity>();
            for (int i = 0; i < nrOfItems; i++)
            {
                ToggleDbEntity toggle = this.CreateToggle(i.ToString());
                toggles.Add(toggle);
            }
            return toggles;
        }

        public ToggleDbEntity CreateToggle(string codeNameSuffix = "")
        {
            var toggle = new ToggleDbEntity()
            {
                CodeName = "TestToggleDbEntity" + codeNameSuffix,
                Values = new List<ToggleValueDbEntity>()
                {
                    this.CreateToggleValueDbEntity(),
                    new ToggleValueDbEntity()
                }
            };
            return toggle;
        }

        private ToggleValueDbEntity CreateToggleValueDbEntity()
        {
            var toggleValue = new ToggleValueDbEntity()
            {
                ApplicationCodeName = "TestApp",
                ApplicationVersion = "1.0",
                Value = true
            };
            return toggleValue;
        }
    }
}
