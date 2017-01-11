using Architected.SampleDataAccess;
using System;
using Xunit;

namespace Architected.SampleDataAccessUT
{
    public class UnitTest
    {
        [Fact]
        public void CallDataAccessLayer()
        {
            var connectionString = "Data Source=<Enter your Server Here>;Initial Catalog=DataTableCoreTest;Integrated Security=True;";

            var dataAccessLayer = new DataAccessLayer(connectionString);

            dataAccessLayer.SaveDataTable();

            Assert.True(true);

        }
    }
}
