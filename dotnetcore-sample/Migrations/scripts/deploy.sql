IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226123844_Initialize')
BEGIN
    CREATE TABLE [Customers] (
        [CustomerID] nvarchar(5) NOT NULL,
        [CompanyName] nvarchar(40) NOT NULL,
        [ContactName] nvarchar(30) NULL,
        [Phone] nvarchar(24) NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226123844_Initialize')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190226123844_Initialize', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227025450_AddAge')
BEGIN
    ALTER TABLE [Customers] ADD [Age] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227025450_AddAge')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190227025450_AddAge', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227031125_AddScore')
BEGIN
    ALTER TABLE [Customers] ADD [Score] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227031125_AddScore')
BEGIN
    UPDATE dbo.Customers SET Score = Age
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227031125_AddScore')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190227031125_AddScore', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227062639_AddAddress')
BEGIN
    ALTER TABLE [Customers] ADD [Address] nvarchar(150) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190227062639_AddAddress')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190227062639_AddAddress', N'2.2.2-servicing-10034');
END;

GO

