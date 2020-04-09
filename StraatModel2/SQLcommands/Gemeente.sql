CREATE TABLE [dbo].[Gemeente]
(
	[id] INT NOT NULL PRIMARY KEY,
	[naam] VARCHAR NOT NULL,
	[provincie] INT NOT NULL, 
    CONSTRAINT [FK_Gemeente_Provincie] FOREIGN KEY (provincie) REFERENCES Provincie(id)

)
