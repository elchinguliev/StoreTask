create database StoreDb
GO
USE StoreDb

GO

CREATE TABLE Categories(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(30) NOT NULL,
)
GO
CREATE TABLE Product(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(30) NOT NULL,
[Prices] MONEY NOT NULL,
[CategoriesId] INT FOREIGN KEY REFERENCES Categories(Id),
[ImagePath] NVARCHAR(MAX) NOT NULL DEFAULT 'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685708146/sorry_rebcmv.jpg'
)

GO


INSERT INTO Categories([Name])
VALUES('Milk products'),('Deserts'),('Alcohol'),('Water'),('Other')
GO
INSERT INTO Product([Name],[CategoriesId],[Prices],[ImagePath])
VALUES('Yogurt',1,3.50,'https://img.freepik.com/free-photo/greek-yogurt-wooden-bowl-isolated-white-background_123827-22632.jpg'),
('Snikers',2,1.2,'https://images.prom.ua/3937605318_w600_h600_3937605318.jpg'),
('Efes beer',3,5,'https://st4.depositphotos.com/1063437/30038/i/1600/depositphotos_300386710-stock-photo-bottle-of-efes-pilsener-beer.jpg'),
('Sirab',4,0.5,'https://bazarstore.az/16228-large_default/sirab-qazsiz-su-500-ml-pet.jpg'),
('Cheese',1,2.3,'https://st.depositphotos.com/1008971/1813/i/450/depositphotos_18135373-stock-photo-two-blocks-of-cheese-on.jpg'),
('Milka',2,2.4,'https://tinjismar.com/storage/2022/12/MILKA.jpg'),
('Jack Daniels',3,53,'https://st2.depositphotos.com/1050070/9344/i/950/depositphotos_93445986-stock-photo-jack-daniels-is-a-brand.jpg'),
('Badamli',4,0.6,'https://bravomarket.online/upload/iblock/32c/32cb1de10663400ac338b7db46b59d75.jpg')
