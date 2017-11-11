using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.DataAccess.DbEntities;

namespace Toggles.DataAccess.Mocks
{
    public class MockOfTogglesDbContext
    {
        public TogglesDbContext Object { get; private set; }

        protected MockOfTogglesDbContext()
        {
            this.CreateMockedObject();
        }

        public MockOfTogglesDbContext(IList<ToggleDbEntity> mockedToggleDbEntities)
            : this()
        {
            this.AddMockedToggleDbEntitiesToDbContext(mockedToggleDbEntities);
        }

        private void CreateMockedObject()
        {
            //use in memory database and defines its name with a new Guid to make sure each test uses a new database.
            var options = new DbContextOptionsBuilder<TogglesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var mockedDbContext = new TogglesDbContext(options);
            this.Object = mockedDbContext;
        }

        private void AddMockedToggleDbEntitiesToDbContext(IList<ToggleDbEntity> mockedToggleDbEntities)
        {
            this.Object.Toggles.AddRange(mockedToggleDbEntities);
            this.Object.SaveChanges();
        }
    }
}
