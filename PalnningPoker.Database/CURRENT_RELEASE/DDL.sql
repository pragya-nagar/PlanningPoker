IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[User] (
		[UserId]			BIGINT			IDENTITY(1001,1)NOT NULL,
		[UserTypeId]		INT				NULL,
		[FirstName]			NVARCHAR(256)	NOT NULL,
		[LastName]			NVARCHAR(256)	NULL,
		[Email]				NVARCHAR(512)	NOT NULL,
		[RoleId]			INT				NULL,  
		[PasswordHash]		VARBINARY(1024)	NULL,
		[PasswordSalt]		VARBINARY(1024)	NULL,
		[SupervisorId]		BIGINT			NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			Users_PK		PRIMARY KEY ([UserId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[UserRole] (
		[RoleId]			INT				NOT NULL,
		[RoleName]			NVARCHAR(256)	NOT NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			UserRole_PK		PRIMARY KEY ([RoleId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UserType]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[UserType] (
		[UserTypeId]		INT				NOT NULL,
		[UserTypeName]		NVARCHAR(256)	NOT NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			UserType_PK		PRIMARY KEY ([UserTypeId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[ResetPassword]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[ResetPassword] (
		[ResetPasswordId]		BIGINT			IDENTITY(1001,1)NOT NULL,
		[UserId]				BIGINT			NOT NULL,
		[ResetToken]			NVARCHAR(512)	NOT NULL,
		[IsActive]				BIT				NOT NULL, 
		[CreatedBy]				BIGINT			NOT NULL, 
		[CreatedOn]				DATETIME		NOT NULL, 
		[UpdatedBy]				BIGINT			NOT NULL, 
		[UpdatedOn]				DATETIME		NOT NULL, 
		[RowGuid]				UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT				ResetPassword_PK		PRIMARY KEY ([ResetPasswordId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Game]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[Game] (
		[GameId]						BIGINT			IDENTITY(1001,1)NOT NULL,
		[GameName]						NVARCHAR(MAX)	NOT NULL,
		[Description]					NVARCHAR(MAX)	NULL,
		[IsChangeVote]					BIT				NULL,
		[IsDefinitionOfEstimation]		BIT				NULL,
		[IsStoryTimer]					BIT				NULL,
		[IsBot]							BIT				NULL,
		[IsActive]						BIT				NOT NULL, 
		[CreatedBy]						BIGINT			NOT NULL, 
		[CreatedOn]						DATETIME		NOT NULL, 
		[UpdatedBy]						BIGINT			NOT NULL, 
		[UpdatedOn]						DATETIME		NOT NULL, 
		[RowGuid]						UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT						Game_PK		PRIMARY KEY ([GameId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[GameSession]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[GameSession] (
		[GameSessionId]		BIGINT			IDENTITY(1001,1)NOT NULL,
		[GameId]			BIGINT			NOT NULL,
		[SessionTime]		DATETIME		NOT NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			GameSession_PK		PRIMARY KEY ([GameSessionId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UserStory]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[UserStory] (
		[UserStoryId]		BIGINT			IDENTITY(1001,1)NOT NULL,
		[GameId]			BIGINT			NOT	NULL,
		[GameSessionId]		BIGINT			NULL,
		[UserStory]			NVARCHAR(MAX)	NOT NULL,
		[Description]		NVARCHAR(MAX)	NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			UserStory_PK		PRIMARY KEY ([UserStoryId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[InviteUser]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[InviteUser] (
		[InviteUserId]		BIGINT			IDENTITY(1001,1)NOT NULL,
		[GameId]			BIGINT			NOT	NULL,
		[GameSessionId]		BIGINT			NULL,
		[UserId]			BIGINT			NULL,
		[Email]				NVARCHAR(512)	NOT NULL,
		[IsAccepted]		BIT				NULL,
		[Reason]			NVARCHAR(1024)	NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			InviteUser_PK		PRIMARY KEY ([InviteUserId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[HtmlTemplate]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[HtmlTemplate] (
		[TamplateId]		BIGINT			NOT NULL,
		[TamplateTypeId]	INT				NULL,
		[TamplateName]		NVARCHAR(512)	NOT NULL,
		[Heading]			NVARCHAR(1024)	NULL,
		[Subject]			NVARCHAR(1024)	NULL,
		[HtmlText]			NVARCHAR(max)	NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			HtmlTemplate_PK		PRIMARY KEY ([TamplateId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Email]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[Email] (
		[EmailId]			BIGINT			IDENTITY(1001,1)NOT NULL,
		[EmailTypeId]		INT				NULL,
		[FromEmail]			NVARCHAR(256)	NULL,
		[FromName]			NVARCHAR(256)	NULL,
		[ToEmail]			NVARCHAR(256)	NULL,
		[ToName]			NVARCHAR(256)	NULL,
		[CCEmail]			NVARCHAR(MAX)	NULL,
		[BCCName]			NVARCHAR(MAX)	NULL,
		[Subject]			NVARCHAR(2048)	NULL,
		[EmailText]			NVARCHAR(max)	NULL,
		[Attachment]		NVARCHAR(2048)	NULL,		
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			Email_PK		PRIMARY KEY ([EmailId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UserSession]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[UserSession] (
		[SessionId]			[bigint]		IDENTITY(1001,1) NOT NULL,
		[UserId]			BIGINT			NOT NULL,
		[SessionToken]		NVARCHAR(1024)	NOT NULL,
		[IsActive]			BIT				NOT NULL, 
		[CreatedBy]			BIGINT			NOT NULL, 
		[CreatedOn]			DATETIME		NOT NULL, 
		[UpdatedBy]			BIGINT			NOT NULL, 
		[UpdatedOn]			DATETIME		NOT NULL, 
		[RowGuid]			UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			UserSession_PK		PRIMARY KEY ([SessionId])
	)
	END
GO

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UserStoryEstimate]') AND type in (N'U'))
	BEGIN
		CREATE TABLE [dbo].[UserStoryEstimate] (
		[UserStoryEstimateId]		BIGINT			IDENTITY(1001,1)NOT NULL,
		[UserStoryId]				BIGINT			NOT	NULL,
		[UserId]					BIGINT			NOT	NULL,
		[KeyWordTypeId]				INT				NULL,
		[Keyword]					NVARCHAR(1024)	NULL,
		[BotPoints]					INT				NULL,
		[UserPoints]				INT				NULL,
		[Reason]					NVARCHAR(MAX)	NULL,
		[IsActive]					BIT				NOT NULL, 
		[CreatedBy]					BIGINT			NOT NULL, 
		[CreatedOn]					DATETIME		NOT NULL, 
		[UpdatedBy]					BIGINT			NOT NULL, 
		[UpdatedOn]					DATETIME		NOT NULL, 
		[RowGuid]					UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT			UserStoryEstimate_PK		PRIMARY KEY ([UserStoryEstimateId])
	)
	END
GO









