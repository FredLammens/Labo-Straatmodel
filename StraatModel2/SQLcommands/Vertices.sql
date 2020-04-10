CREATE TABLE [dbo].[Vertices] (
    [verticeX]  FLOAT (53) NOT NULL,
    [verticeY]  FLOAT (53) NOT NULL,
    [segment] INT        NOT NULL,
    CONSTRAINT [FK_Vertices_Punt] FOREIGN KEY ([verticeX], [verticeY]) REFERENCES [dbo].[Punt] ([x], [y]),
    CONSTRAINT [FK_Vertices_Segment] FOREIGN KEY ([segment]) REFERENCES [dbo].[Segment] ([id])
);

