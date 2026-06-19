IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260615154929_intialize', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [DepartMents] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    CONSTRAINT [PK_DepartMents] PRIMARY KEY ([Id])
);

CREATE TABLE [Employees] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Mobile] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [DeptId] uniqueidentifier NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_DepartMents_DeptId] FOREIGN KEY ([DeptId]) REFERENCES [DepartMents] ([Id])
);

CREATE INDEX [IX_Employees_DeptId] ON [Employees] ([DeptId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260618045106_AddEmployeeAndDepartment', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Users] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Employees] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [DepartMents] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260618045755_AddIsActiveColumn', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Jobs] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [DeptId] uniqueidentifier NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Jobs_DepartMents_DeptId] FOREIGN KEY ([DeptId]) REFERENCES [DepartMents] ([Id])
);

CREATE INDEX [IX_Jobs_DeptId] ON [Jobs] ([DeptId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260618055008_AddJobTable', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Users] ADD [Role] nvarchar(max) NULL;

ALTER TABLE [Jobs] ADD [Role] nvarchar(max) NULL;

ALTER TABLE [Employees] ADD [Role] nvarchar(max) NULL;

ALTER TABLE [DepartMents] ADD [Role] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260618133110_AddDepartmentToEmployee', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Leaves] (
    [Id] uniqueidentifier NOT NULL,
    [EmpId] uniqueidentifier NULL,
    [EmployeeId] uniqueidentifier NULL,
    [UserId] uniqueidentifier NULL,
    [IsPending] bit NOT NULL,
    [IsRejected] bit NOT NULL,
    [IsApproved] bit NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Reason] nvarchar(max) NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    [Role] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Leaves] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Leaves_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_Leaves_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);

CREATE INDEX [IX_Leaves_EmployeeId] ON [Leaves] ([EmployeeId]);

CREATE INDEX [IX_Leaves_UserId] ON [Leaves] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260619073415_AddLeaveEntity', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Attendances] (
    [Id] uniqueidentifier NOT NULL,
    [EmpId] uniqueidentifier NULL,
    [EmployeeId] uniqueidentifier NULL,
    [Date] datetime2 NOT NULL,
    [CheckInTime] datetime2 NULL,
    [CheckOutTime] datetime2 NULL,
    [TotalHours] float NULL,
    [Status] nvarchar(max) NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    [Role] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Attendances] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Attendances_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);

CREATE INDEX [IX_Attendances_EmployeeId] ON [Attendances] ([EmployeeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260619075719_AddAttendanceEntity', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [EmployeeDocuments] (
    [Id] uniqueidentifier NOT NULL,
    [EmpId] uniqueidentifier NULL,
    [EmployeeId] uniqueidentifier NULL,
    [DocumentType] nvarchar(max) NULL,
    [DocumentName] nvarchar(max) NULL,
    [DocumentPath] nvarchar(max) NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    [Role] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployeeDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployeeDocuments_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);

CREATE INDEX [IX_EmployeeDocuments_EmployeeId] ON [EmployeeDocuments] ([EmployeeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260619080541_AddEmployeeDocumentEntity', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Notices] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Content] nvarchar(max) NULL,
    [TargetDepartmentId] uniqueidentifier NULL,
    [ExpiryDate] datetime2 NULL,
    [OnCreate] datetime2 NOT NULL,
    [OnUpdate] datetime2 NULL,
    [Role] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Notices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notices_DepartMents_TargetDepartmentId] FOREIGN KEY ([TargetDepartmentId]) REFERENCES [DepartMents] ([Id])
);

CREATE INDEX [IX_Notices_TargetDepartmentId] ON [Notices] ([TargetDepartmentId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260619082218_AddNoticeEntity', N'10.0.9');

COMMIT;
GO

