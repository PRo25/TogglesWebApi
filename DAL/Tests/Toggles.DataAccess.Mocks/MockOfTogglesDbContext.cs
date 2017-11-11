using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.DataAccess.DbEntities;

namespace Toggles.DataAccess.Mocks
{
    public class MockOfTogglesDbContext
    {
        public TogglesDbContext Object { get; private set; }

        public MockOfTogglesDbContext()
        {
            this.CreateMockedObject();
        }

        public MockOfTogglesDbContext(IList<ToggleDbEntity> mockedToggleDbEntities)
            : this()
        {
            this.AddMockedToggleDbEntitiesToDbContext(mockedToggleDbEntities);
        }

        public MockOfTogglesDbContext(IList<ToggleValueDbEntity> mockedToggleValueDbEntities)
            : this()
        {
            this.AddMockedToggleValueDbEntitiesToDbContext(mockedToggleValueDbEntities);
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
            this.Object.SaveChanges(true);
            this.DetachAllDbEntities();
        }

        private void DetachAllDbEntities()
        {
            IList<EntityEntry> entries = this.Object.ChangeTracker.Entries().ToList();
            foreach (EntityEntry entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        private void AddMockedToggleValueDbEntitiesToDbContext(IList<ToggleValueDbEntity> mockedToggleValueDbEntities)
        {
            this.Object.ToggleValues.AddRange(mockedToggleValueDbEntities);
            this.Object.SaveChanges(true);
            this.DetachAllDbEntities();
        }
    }
}
