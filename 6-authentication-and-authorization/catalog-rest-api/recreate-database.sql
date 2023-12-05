CREATE TABLE Category
(
    Id NVARCHAR(36) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    ImageUrl TEXT NULL,
    ParentCategoryId NVARCHAR(36) NULL
);

CREATE TABLE Product
(
    Id NVARCHAR(36) PRIMARY KEY,
    CategoryId NVARCHAR(36) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
	ImageUrl TEXT NULL,
    Description TEXT NULL,
    Price NUMERIC NOT NULL,
    Amount INT NOT NULL,
    FOREIGN KEY(CategoryId) REFERENCES Category(Id)
);
