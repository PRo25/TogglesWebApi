using System;
using System.Collections.Generic;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts;
using Toggles.Repositories.Contracts;

namespace Toggles.BusinessRules
{
    public class TogglesLoader : ITogglesLoader
    {
        private ITogglesReadRepository togglesReadRepository;

        public TogglesLoader(ITogglesReadRepository togglesReadRepository)
        {
            this.togglesReadRepository = togglesReadRepository;
        }

        public IList<Toggle> GetAll()
        {
            IList<Toggle> result = this.togglesReadRepository.GetAll();
            return result;
        }

        public Toggle GetById(Guid id)
        {
            Toggle toggle = this.togglesReadRepository.GetById(id);
            return toggle;
        }
    }
}
