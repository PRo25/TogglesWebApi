USE [TogglesDataBase]
GO

CREATE TABLE [dbo].[Toggles](
	[Id] [uniqueidentifier] NOT NULL,
	[CodeName] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_Toggles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Toggles] ADD  CONSTRAINT [DF_Toggles_Id]  DEFAULT (newid()) FOR [Id]
GO


CREATE TABLE [dbo].[ToggleValues](
	[Id] [uniqueidentifier] NOT NULL,
	[ToggleId] [uniqueidentifier] NOT NULL,
	[Value] [bit] NOT NULL,
	[ApplicationCodeName] [nvarchar](64) NOT NULL,
	[ApplicationVersion] [nvarchar](32) NULL,
 CONSTRAINT [PK_ToggleValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ToggleValues] ADD  CONSTRAINT [DF_ToggleValues_Value]  DEFAULT ((0)) FOR [Value]
GO

ALTER TABLE [dbo].[ToggleValues]  WITH CHECK ADD  CONSTRAINT [FK_ToggleValues_Toggles] FOREIGN KEY([ToggleId])
REFERENCES [dbo].[Toggles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ToggleValues] CHECK CONSTRAINT [FK_ToggleValues_Toggles]
GO


/* Insert Data */

DECLARE @isButtonBlueToggleId uniqueidentifier;
DECLARE @isButtonGreenToggleId uniqueidentifier;
DECLARE @isButtonRedToggleId uniqueidentifier;
SET @isButtonBlueToggleId = NEWID();
SET @isButtonGreenToggleId = NEWID();
SET @isButtonRedToggleId = NEWID();

INSERT INTO [dbo].[Toggles] ([Id],[CodeName],[Description])
     VALUES (@isButtonBlueToggleId,'isButtonBlue','Blue buttons'),
			(@isButtonGreenToggleId,'isButtonGreen','Green buttons'),
			(@isButtonRedToggleId,'isButtonRed','Red buttons');

INSERT INTO [dbo].[ToggleValues] ([Id],[ToggleId],[Value],[ApplicationCodeName],[ApplicationVersion])
     VALUES (NEWID(), @isButtonBlueToggleId, 1, 'Global', NULL),
			(NEWID(), @isButtonGreenToggleId, 1, 'Abc', '1.0'),
			(NEWID(), @isButtonRedToggleId, 1, 'Global', NULL),
			(NEWID(), @isButtonRedToggleId, 0, 'Abc', '1.0');
GO

