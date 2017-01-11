# DataTableCore
DataTable implementation for .net core when using ado.net

For a user defined type such as:
```
CREATE TYPE [dbo].[MySimpleType] AS TABLE
(
	[ColA] [bigint],
	[ColB] [uniqueidentifier],
	[ColC] [varchar](max),
	[ColD] [nvarchar](100),
	[ColE] [bit]
)
```
Create a DataTable object as follows:
```
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
```
Attach parameter to SqlCommand as follows:
```
var p = command.Parameters.Add("@ItemTable", SqlDbType.Structured);
p.Direction = ParameterDirection.Input;
p.TypeName = "MySimpleType";
p.Value = dataTable.CreateDataRecords();
```
I will be working on support for more data types in the coming weeks.


If you would like to install the sample schema for the xunit tests in the solution.  Run the script below on your SQL Server instance.

```
CREATE TYPE [dbo].[MySimpleType] AS TABLE
(
	[ColA] [bigint],
	[ColB] [uniqueidentifier],
	[ColC] [varchar](max),
	[ColD] [nvarchar](100),
	[ColE] [bit]
)
GO

CREATE TABLE [dbo].[SimpleTable]
(
	[Id] int identity(1,1) primary key,
	[ColA] [bigint],
	[ColB] [uniqueidentifier],
	[ColC] [varchar](max),
	[ColD] [nvarchar](100), 
	[ColE] [bit]
)
GO

CREATE PROCEDURE [dbo].[SaveSimpleTable]
(
	@ItemTable [dbo].[MySimpleType] READONLY
)
AS

SET NOCOUNT ON

INSERT INTO dbo.SimpleTable
(
	[ColA],
	[ColB],
	[ColC],
	[ColD],
	[ColE]
)
SELECT
	mst.[ColA],
	mst.[ColB],
	mst.[ColC],
	mst.[ColD],
	mst.[ColE]

FROM @ItemTable mst
GO 

```
Any issues or suggestions please raise and issue.  Thanks  
