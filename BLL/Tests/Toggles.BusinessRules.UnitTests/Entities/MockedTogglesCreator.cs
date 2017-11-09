using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.UnitTests.Entities
{
    public class MockedTogglesCreator
    {
        public IList<Toggle> CreateList(int nrOfItems = 3)
        {
            IList<Toggle> toggles = new List<Toggle>();
            for (int i = 0; i < nrOfItems; i++)
            {
                Toggle toggle = this.CreateToggle(i.ToString());
                toggles.Add(toggle);
            }
            return toggles;
        }

        public Toggle CreateToggle(string codeNameSuffix = "")
        {
            var toggle = new Toggle()
            {
                CodeName = "TestToggle" + codeNameSuffix,
                Values = new List<ClientApplicationToggleValue>()
                {
                    this.CreateClientApplicationToggleValue(),
                    new ClientApplicationToggleValue(),
                    new ClientApplicationToggleValue()
                }
            };
            return toggle;
        }

        private ClientApplicationToggleValue CreateClientApplicationToggleValue()
        {
            var toggleValue = new ClientApplicationToggleValue()
            {
                Application = new ClientApplication()
                {
                    CodeName = "TestApp",
                    Version = "1.0"
                },
                Value = true
            };
            return toggleValue;
        }
    }
}
