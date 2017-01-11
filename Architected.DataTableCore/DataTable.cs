using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;

namespace Architected.DataTableCore
{
    public class DataTable
    {
        public List<DataTableColumn> Columns { get; } = new List<DataTableColumn>();

        public List<Dictionary<string, object>> Rows { get; } = new List<Dictionary<string, object>>();

        public Dictionary<string, object> NewRow()
        {
            return new Dictionary<string, object>();
        }

        public List<SqlDataRecord> CreateDataRecords()
        {
            var dataRecords = new List<SqlDataRecord>();

            foreach (var t in Rows)
            {
                var record = CreateTemplateRecord();

                for (var j = 0; j < Columns.Count; j++)
                {
                    if (Columns[j].Type == SqlDbType.VarChar || Columns[j].Type == SqlDbType.NVarChar)
                    {
                        var a = t[Columns[j].Name] as string;
                        if (a != null) record.SetString(j, a);
                    }
                    else if (Columns[j].Type == SqlDbType.Int)
                    {
                        var b = Convert.ToInt32(t[Columns[j].Name]);
                        record.SetInt32(j, b);
                    }
                    else if (Columns[j].Type == SqlDbType.BigInt)
                    {
                        var c = Convert.ToInt64(t[Columns[j].Name]);
                        record.SetInt64(j, c);
                    }
                    else if (Columns[j].Type == SqlDbType.UniqueIdentifier
                        )
                    {
                        var d = (Guid)t[Columns[j].Name];
                        record.SetGuid(j, d);
                    }
                    else if (Columns[j].Type == SqlDbType.Bit)
                    {
                        var e = Convert.ToBoolean(t[Columns[j].Name]);
                        record.SetBoolean(j, e);
                    }
                }

                dataRecords.Add(record);
            }

            return dataRecords;
        }

        public SqlDataRecord CreateTemplateRecord()
        {
            var metaDataList = new List<SqlMetaData>();

            Columns.ForEach(c =>
            {
                SqlMetaData metaData;
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
