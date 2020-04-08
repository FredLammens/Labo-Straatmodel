CREATE TABLE [dbo].[Knoop]
(
	[id] INT NOT NULL PRIMARY KEY,
	[knoopX] FLOAT(53) NOT NULL,
	[knoopY] FLOAT(53) NOT NULL,
    CONSTRAINT [FK_Knoop_Punt] FOREIGN KEY (knoopX,knoopY) REFERENCES Punt (x,y),
);
