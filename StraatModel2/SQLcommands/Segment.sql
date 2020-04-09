CREATE TABLE [dbo].[Segment] (
    [id]           INT NOT NULL,
    [beginKnoopId] INT NOT NULL,
    [eindKnoopId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Segment_beginKnoop] FOREIGN KEY ([beginKnoopId]) REFERENCES [dbo].[Knoop] ([id]),
    CONSTRAINT [FK_Segment_eindKnoop] FOREIGN KEY ([eindKnoopId]) REFERENCES [dbo].[Knoop] ([id])
);

