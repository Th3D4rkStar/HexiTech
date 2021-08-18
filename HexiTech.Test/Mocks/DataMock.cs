namespace HexiTech.Test.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using HexiTech.Data;

    public static class DataMock
    {
        public static HexiTechDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<HexiTechDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

                return new HexiTechDbContext(dbContextOptions);
            }
        }
    }
}