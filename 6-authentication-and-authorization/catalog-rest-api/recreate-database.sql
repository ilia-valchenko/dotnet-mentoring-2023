CREATE TABLE Category
(
    Id NVARCHAR(36) PRIMARY KEY,
    Name NVARCHAR(50) NULL,
    ImageUrl NULL,
    ParentCategoryId NVARCHAR(36) NULL
);

CREATE TABLE Product
(
    Id NVARCHAR(36) PRIMARY KEY,
    CategoryId NVARCHAR(36),
    Name NVARCHAR(50) NULL,
	ImageUrl TEXT NULL,
    Description TEXT NULL,
    Price NUMERIC,
    Amount INT,
    FOREIGN KEY(CategoryId) REFERENCES Category(Id)
);
