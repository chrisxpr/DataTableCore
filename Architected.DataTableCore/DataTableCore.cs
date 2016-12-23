using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Architected.DataTableCore
{
    public class DataTableCore
    {
        private List<DataTableCoreColumn> _columns = new List<DataTableCoreColumn>();
        private List<Dictionary<string, object>> _rows = new List<Dictionary<string, object>>();

        public List<DataTableCoreColumn> Columns
        {
            get { return _columns; }
        }

        public List<Dictionary<string, object>> Rows
        {
            get { return _rows; }
        }

        public Dictionary<string, object> NewRow()
        {
            return new Dictionary<string, object>();
        }

        public List<SqlDataRecord> CreateDataRecords()
        {
            var dataRecords = new List<SqlDataRecord>();

            for (var i = 0; i < _rows.Count; i++)
            {
                var record = CreateTemplateRecord();

                for (var j = 0; j < _columns.Count; j++)
                {
                    if (_columns[j].Type == SqlDbType.VarChar || _columns[j].Type == SqlDbType.NVarChar)
                    {
                        var a = _rows[i][_columns[j].Name] as string;
                        if (a != null) record.SetString(j, a);
                    }
                    else if (_columns[j].Type == SqlDbType.Int)
                    {
                        var b = Convert.ToInt32(_rows[i][_columns[j].Name]);
                        record.SetInt32(j, b);
                    }
                    else if (_columns[j].Type == SqlDbType.BigInt)
                    {
                        var c = Convert.ToInt64(_rows[i][_columns[j].Name]);
                        record.SetInt64(j, c);
                    }
                    else if (_columns[j].Type == SqlDbType.UniqueIdentifier
                        )
                    {
                        var d = (Guid)_rows[i][_columns[j].Name];
                        record.SetGuid(j, d);
                    }
                }

                dataRecords.Add(record);
            }

            return dataRecords;
        }

        public SqlDataRecord CreateTemplateRecord()
        {
            var metaDataList = new List<SqlMetaData>();

            _columns.ForEach(c =>
            {
                SqlMetaData metaData = null;
                if (c.Type == SqlDbType.NVarChar || c.Type == SqlDbType.VarChar)
                {

                    metaData = c.Size > 0

                        ? new SqlMetaData(c.Name, c.Type, c.Size)
                        : new SqlMetaData(c.Name, c.Type, SqlMetaData.Max);
                }
                else
                {
                    metaData = new SqlMetaData(c.Name, c.Type);
                }

                metaDataList.Add(metaData);
            }
            );
            return new SqlDataRecord(metaDataList.ToArray());
        }

    }
}
