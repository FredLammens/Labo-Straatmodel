CREATE TABLE [dbo].[Map] (
    [id]      INT NOT NULL,
    [segment] INT NOT NULL,
    [graaf]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Map_Graaf] FOREIGN KEY ([graaf]) REFERENCES [dbo].[Graaf] ([Id]),
    CONSTRAINT [FK_Map_Segment] FOREIGN KEY ([segment]) REFERENCES [dbo].[Segment] ([id])
);

