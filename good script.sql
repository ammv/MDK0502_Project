CREATE TABLE [dbo].[ClientType](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name [nvarchar](70) NOT NULL)

CREATE TABLE [dbo].[TypeRawMaterial](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name [nvarchar](50) NOT NULL)

CREATE TABLE [dbo].[OrderState](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name [nvarchar](70) NOT NULL)

CREATE TABLE [dbo].[Country](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name [nvarchar](50) NOT NULL)

CREATE TABLE [dbo].[City](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	CountryID [int] FOREIGN KEY REFERENCES Country(ID),
	Name [nvarchar](50) NOT NULL)

-- Адрес
CREATE TABLE [dbo].[Address](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	CountryID int FOREIGN KEY REFERENCES Country(ID),
	CityID int FOREIGN KEY REFERENCES City(ID),
	Street nvarchar(50) not null,
	HouseNumber int not null)

-- Поставщик
CREATE TABLE [dbo].[Vendor](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ScopeOfDelivery int not null,
	DeliveryDate date not null,
	DeliveryName nvarchar(100),
	Name [nvarchar](100) NULL,
	AddressID int FOREIGN KEY REFERENCES Address(ID))

-- Клиент аля заказщик
CREATE TABLE [dbo].[Client](
	[ID] [int] PRIMARY KEY IDENTITY(1,1),
	[LastName] [nvarchar](70) NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[MiddleName] [nvarchar](70) NULL,
	Phone [nvarchar](17) NOT NULL,
	ClientTypeID int FOREIGN KEY REFERENCES ClientType(ID)
	)

-- Единица измерения
CREATE TABLE [dbo].[UnitOfMeasurement](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](3) NOT NULL)

-- Сырье
CREATE TABLE [dbo].[RawMaterial](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name [nvarchar](50) NOT NULL,
	TypeRawMaterialID [int] FOREIGN KEY REFERENCES TypeRawMaterial(ID),
	[Count] int not null,
	Guarantee int not null,
	UnitOfMeasurementID int not null FOREIGN KEY REFERENCES UnitOfMeasurement(ID),
	UnitCost decimal(10,2) NOT NULL)

-- Тип заказа
CREATE TABLE [dbo].[Order](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ProductName nvarchar(100) not null,
	Note nvarchar(300) not null,
	[Count] int not null,
	OrderDate date not null,
	[State] int FOREIGN KEY REFERENCES OrderState(ID),
	ClientID int FOREIGN KEY REFERENCES Client(ID))

-- Тип продукции
CREATE TABLE [dbo].[ProductType](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(100) NOT NULL)

-- Реализация
CREATE TABLE [dbo].[Realization](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Volume int NOT NULL,
	RealizationDate date NOT NULL,
	ProductTypeID INT FOREIGN KEY REFERENCES ProductType(ID) NOT NULL,
	ReleaseNumber int not null,
	UnitPrice decimal(10,2)
)

-- Цех
CREATE TABLE [dbo].[Workshop]
(
	ID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] nvarchar(100) not null,
)

-- Оборудование
CREATE TABLE [dbo].[Equipment]
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	[Name] nvarchar(100) not null,
	[Count] int not null,
	[Description] nvarchar(300)
)

-- Оборудование в цеху
CREATE TABLE [dbo].[WorkshopEquipment]
(
	WorkshopID int FOREIGN KEY REFERENCES Workshop(ID),
	EquipmentID INT FOREIGN KEY REFERENCES Equipment(ID),
	[Count] int not null
)

-- Тип участка цеха
CREATE TABLE [dbo].[RegionTypeOfWork]
(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name nvarchar(50) not null
)

-- Участки цеха
CREATE TABLE [dbo].[Region]
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	WorkshopID int FOREIGN KEY REFERENCES Workshop(ID),
	[Name] nvarchar(50) not null,
	TypeOfWork int FOREIGN KEY REFERENCES RegionTypeOfWork(ID)
)

-- Конечный продукт
CREATE TABLE [dbo].[Product]
(
	ID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] nvarchar(100) not null,
	ReleaseNumber int not null,
	Cost decimal(10,2) not null,
	Guarantee int not null
)

-- Деталь
CREATE TABLE [dbo].[Detail](
	ID [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	RegionID int FOREIGN KEY REFERENCES Region(ID),
	[Name] [nvarchar](50) NOT NULL,
	Size decimal(10,2) not null)

-- Изделие
CREATE TABLE [dbo].[Item]
(
	ID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] nvarchar(100) not null,
	RegionID int FOREIGN KEY REFERENCES Region(ID),
	Size decimal(10,2) NOT NULL,
	[Count] int NOT NULL
)

-- Участок контроля качества
CREATE TABLE [dbo].[QualityControl]
(
	ID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	RegionID int FOREIGN KEY REFERENCES Region(ID),
	[DetailID] int FOREIGN KEY REFERENCES Detail(ID),
	ItemID int FOREIGN KEY REFERENCES Item(ID),
	ProductID int FOREIGN KEY REFERENCES Product(ID),
	GOST nvarchar(25) not null
)

-- Участок сборка
CREATE TABLE [dbo].[Assembling]
(
	ID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	RegionID int FOREIGN KEY REFERENCES Region(ID),
	ItemName nvarchar(100) not null,
	FastenerName nvarchar(100) not null,
	AccessoriesName nvarchar(100) not null,
	FastenerCount int not null,
	AccessoriesCount nvarchar(100) not null
)

-- Участок покрытия
CREATE TABLE [dbo].[Coverage]
(
	ID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	RegionID int FOREIGN KEY REFERENCES Region(ID),
	Size decimal(10,2) not null,
	[Count] int
)

-- Участок сушки
CREATE TABLE [dbo].[Drying]
(
	RegionID int FOREIGN KEY REFERENCES Region(ID),
	Size decimal(10,2) not null,
	[Count] int
)

