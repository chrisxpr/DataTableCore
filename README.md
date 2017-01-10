# DataTableCore
DataTable implementation for .net core when using ado.net

For a user defined type such as:
```
CREATE TYPE [dbo].[MySimpleType] AS TABLE
(
	[ColA] [bigint],
	[ColB] [uniqueidentifier],
	[ColC] [varchar](max),
	[ColD] [nvarchar](100)
)
```
Create a DataTableCore object as follows:
```
var dataTableCore = new DataTableCore();

dataTableCore.Columns.Add(new DataTableCoreColumn("ColA", SqlDbType.BigInt));
dataTableCore.Columns.Add(new DataTableCoreColumn("ColB", SqlDbType.UniqueIdentifier));
dataTableCore.Columns.Add(new DataTableCoreColumn("ColC", SqlDbType.VarChar));
dataTableCore.Columns.Add(new DataTableCoreColumn("ColD", SqlDbType.NVarChar, 100));
dataTableCore.Columns.Add(new DataTableCoreColumn("ColE", SqlDbType.Bit));

foreach (var item in assetGlobalIdList)
{
    var dr = dataTableCore.NewRow();
    dr["ColA"] = 12345;
    dr["ColB"] = Guid.Newguid();
    dr["ColC"] = "My normal string";
    dr["ColD"] = "My unicode string";
	dr["ColE"] = true;

    dataTableCore.Rows.Add(dr);
}
```
Attach parameter to SqlCommand as follows:
```
var p = command.Parameters.Add("@TableParam", SqlDbType.Structured);
p.Direction = ParameterDirection.Input;
p.TypeName = "MySimpleType";
p.Value = dataTableCore.CreateDataRecords();
```
I will be working on support for more data types in the coming weeks.

Any issues or suggestions please raise and issue.  Thanks
