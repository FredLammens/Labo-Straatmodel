CREATE TABLE [dbo].[Map] (
    [segment] INT NOT NULL,
    [graaf]   INT NOT NULL,
    CONSTRAINT [FK_Map_Graaf] FOREIGN KEY ([graaf]) REFERENCES [dbo].[Graaf] ([id]),
    CONSTRAINT [FK_Map_Segment] FOREIGN KEY ([segment]) REFERENCES [dbo].[Segment] ([id])
);
