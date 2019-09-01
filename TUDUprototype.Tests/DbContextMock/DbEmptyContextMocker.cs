using System;
using System.Collections.Generic;
using System.Text;
using TUDUprototype.Models;

namespace TUDUprototype.Tests.DbContextMock
{
    class DbEmptyContextMocker:DbContextMocker
    {
        public override void Seed(TUDUDbContext dbContext)
        {

        }
    }
}
