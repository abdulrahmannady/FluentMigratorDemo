IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'FluentMigratorDemo')
        CREATE DATABASE FluentMigratorDemo
        COLLATE Arabic_100_CS_AI;