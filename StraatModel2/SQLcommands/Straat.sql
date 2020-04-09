CREATE TABLE [dbo].[Straat]
(
	[id] INT NOT NULL PRIMARY KEY,
	[naam] VARCHAR NOT NULL,
	[graaf] INT NOT NULL,
	[gemeente] INT NOT NULL, 
    CONSTRAINT [FK_Straat_Graaf] FOREIGN KEY (graaf) REFERENCES Graaf(id), 
    CONSTRAINT [FK_Straat_Gemeente] FOREIGN KEY (gemeente) REFERENCES Gemeente(id)
)
