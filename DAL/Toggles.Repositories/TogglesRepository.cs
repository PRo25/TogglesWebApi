using System;
using System.Collections.Generic;
using System.Text;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts;

namespace Toggles.Repositories
{
    public class TogglesRepository : RepositoryBase<Toggle>, ITogglesRepository
    {
        private ITogglesReadRepository togglesReadRepository;

        public TogglesRepository(ITogglesReadRepository togglesReadRepository)
        {
            this.togglesReadRepository = togglesReadRepository;
        }

        public IList<Toggle> GetAll()
        {
            return this.togglesReadRepository.GetAll();
        }

        public Toggle GetById(Guid id)
        {
            return this.togglesReadRepository.GetById(id);
        }

        public bool HasAnyByCodeName(string codeName)
        {
            return this.togglesReadRepository.HasAnyByCodeName(codeName);
        }

        public bool HasAnyById(Guid id)
        {
            return this.togglesReadRepository.HasAnyById(id);
        }

        protected override Func<Toggle, bool> GetIdentityEqualityPredicate(Toggle businessEntity)
        {
            return t => t.Id == businessEntity.Id;
        }
    }
}
