USE MVCLibrary;
GO
CREATE TABLE dbo.Library
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(150) NULL,
    PhoneNumber VARCHAR(20) NULL,
    EmailAddress VARCHAR(100) NULL,
    LibraryNumber SMALLINT NOT NULL
);
GO
CREATE TABLE dbo.Category
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL
);
GO
CREATE TABLE dbo.Book
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    AuthorName NVARCHAR(150) NOT NULL,
    Price DECIMAL(6, 2) NOT NULL,
    LibraryId INT NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT Library_Book_FK
        FOREIGN KEY (LibraryId)
        REFERENCES dbo.Library (Id),
    CONSTRAINT Category_Book_FK
        FOREIGN KEY (CategoryId)
        REFERENCES dbo.Category (Id)
);
GO
CREATE TABLE dbo.BookDetail
(
    Id INT PRIMARY KEY IDENTITY,
    PublishDateTime DATETIME NULL,
    CountEdition TINYINT NULL,
    Description NVARCHAR(400) NULL,
    BookId INT NOT NULL,
    CONSTRAINT Book_BookDetail_FK
        FOREIGN KEY (BookId)
        REFERENCES dbo.Book (Id)
);


