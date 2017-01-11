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

----------------------------------------------------------------
-- Create a temporary table variable to hold the output actions.
----------------------------------------------------------------
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