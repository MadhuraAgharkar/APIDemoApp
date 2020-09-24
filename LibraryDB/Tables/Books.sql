CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[Name] VARCHAR(500) NOT NULL,
	[AuthorId] INT NOT NULL,
	[GenreId] INT NOT NULL,
    [CountryId] INT NOT NULL,
	[LanguageId] INT NOT NULL,
	[ISBN] VARCHAR(500) NOT NULL,
	[CurrentEdition] INT NOT NULL,
	CONSTRAINT [FK_Books_Authors] FOREIGN KEY (AuthorId) REFERENCES [Authors]([Id]),
	CONSTRAINT [FK_Books_Genres] FOREIGN KEY(GenreId) REFERENCES [Genres]([Id]),
	CONSTRAINT [FK_Books_Countries] FOREIGN KEY (CountryId) REFERENCES [Countries]([Id]),
    CONSTRAINT [FK_Books_Languages] FOREIGN KEY (LanguageId) REFERENCES [Languages]([Id]),
)
