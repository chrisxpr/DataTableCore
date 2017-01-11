using System;
using System.Data;
using Architected.DataTableCore;
using System.Data.SqlClient;

namespace Architected.SampleDataAccess
{
    public class DataAccessLayer
    {
        private string _connectionString;

        public DataAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveDataTable()
        {
            var dataTable = new DataTableCore.DataTable();

            dataTable.Columns.Add(new DataTableColumn("ColA", SqlDbType.BigInt));
            dataTable.Columns.Add(new DataTableColumn("ColB", SqlDbType.UniqueIdentifier));
            dataTable.Columns.Add(new DataTableColumn("ColC", SqlDbType.VarChar));
            dataTable.Columns.Add(new DataTableColumn("ColD", SqlDbType.NVarChar));
            dataTable.Columns.Add(new DataTableColumn("ColE", SqlDbType.Bit));

            var dr = dataTable.NewRow();

            dr["ColA"] = 1;
            dr["ColB"] = Guid.NewGuid();
            dr["ColC"] = "Hello";
            dr["ColD"] = "Hello Wide";
            dr["ColE"] = true;

            dataTable.Rows.Add(dr);

            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {

                command.Connection = sqlConnection;
                command.Connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.SaveSimpleTable";

                var p = command.Parameters.Add("@ItemTable", SqlDbType.Structured);
                p.Direction = ParameterDirection.Input;
                p.TypeName = "MySimpleType";
                p.Value = dataTable.CreateDataRecords();
                
                command.ExecuteNonQuery();
            }
        }
    }
}
