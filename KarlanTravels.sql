USE master
GO

IF DB_ID('KarnelTravelsDB') IS NOT NULL
 DROP DATABASE KarnelTravelsDB
GO

CREATE DATABASE KarnelTravelsDB
GO

USE KarnelTravelsDB
GO
IF OBJECT_ID('dbo.Country', 'U') IS NOT NULL
  DROP TABLE dbo.Genre
CREATE TABLE Country(
	CountryId NVARCHAR(40) PRIMARY KEY,
	CountryName NVARCHAR(255) NOT NULL,
	Continent NVARCHAR(40) NOT NULL,
	RegionCode NVARCHAR(20) NOT NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.City', 'U') IS NOT NULL
  DROP TABLE dbo.City
CREATE TABLE City(
	CityId NVARCHAR(40) PRIMARY KEY,
	CityName NVARCHAR(255) NOT NULL,
	CountryId NVARCHAR(40) FOREIGN KEY REFERENCES Country(CountryId) NOT NULL,
	PostalCode NVARCHAR(20) NOT NULL,
	CityNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.FacilityType', 'U') IS NOT NULL
  DROP TABLE dbo.FacilityType
CREATE TABLE FacilityType(
	FacilityTypeId NVARCHAR(40) PRIMARY KEY,
	FacilityTypeName NVARCHAR(255) NOT NULL,
	FacilityTypeNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.Category', 'U') IS NOT NULL
  DROP TABLE dbo.Category
CREATE TABLE Category(
	CategoryId NVARCHAR(40) PRIMARY KEY,
	CategoryName NVARCHAR(255) NOT NULL,
	CategoryNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

IF OBJECT_ID('dbo.SubCategory', 'U') IS NOT NULL
  DROP TABLE dbo.SubCategory
CREATE TABLE SubCategory(
	SubCategoryId NVARCHAR(40) PRIMARY KEY,
	SubCategoryName NVARCHAR(255) NOT NULL,
	SubCategoryNote NVARCHAR(255) NULL,
	CategoryId NVARCHAR(40) FOREIGN KEY REFERENCES Category(CategoryId) NOT NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.TransactionType', 'U') IS NOT NULL
  DROP TABLE dbo.TransactionType
CREATE TABLE TransactionType(
	TransactionTypeId NVARCHAR(40) PRIMARY KEY,
	TransactionTypeName NVARCHAR(40) NOT NULL,
	TransactionTypeNote NVARCHAR(255) NULL,
	TransactionPriceRate FLOAT NOT NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.AdminRole', 'U') IS NOT NULL
  DROP TABLE dbo.AdminRole
CREATE TABLE AdminRole(
	RoleId NVARCHAR(40) PRIMARY KEY,
	RoleName NVARCHAR(50) NOT NULL,
	RoleNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.Bank', 'U') IS NOT NULL
  DROP TABLE dbo.Bank
CREATE TABLE Bank(
	BankId NVARCHAR(40) PRIMARY KEY,
	BankName NVARCHAR(255) NOT NULL,
	SwiftCode NVARCHAR(255) UNIQUE NOT NULL,
	CountryId NVARCHAR(40) FOREIGN KEY REFERENCES Country(CountryId) NOT NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.BankAccount', 'U') IS NOT NULL
  DROP TABLE dbo.BankAccount
CREATE TABLE BankAccount(
	BankAccountId INT PRIMARY KEY IDENTITY (1,1),
	AccountName NVARCHAR(255) NOT NULL,
	AccountNumber NVARCHAR(40) NOT NULL UNIQUE,
	BankId NVARCHAR(40) FOREIGN KEY REFERENCES Bank(BankId) NOT NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.Customer', 'U') IS NOT NULL
  DROP TABLE dbo.Customer
CREATE TABLE Customer(
	CustomerId INT PRIMARY KEY IDENTITY (1,1),
	Username NVARCHAR(255) UNIQUE NOT NULL,
	Email NVARCHAR(255) UNIQUE NULL,
	Phone NVARCHAR(20) UNIQUE NULL,
	BankAccountId INT FOREIGN KEY REFERENCES BankAccount(BankAccountId) UNIQUE NULL,
	CityId NVARCHAR(40) FOREIGN KEY REFERENCES City(CityId) NOT NULL,
	UserPassword NVARCHAR(255) NOT NULL,
	AmountToPay MONEY NOT NULL DEFAULT 0,
	CustomerNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.[Admin]', 'U') IS NOT NULL
  DROP TABLE dbo.[Admin]
CREATE TABLE [Admin](
	AdminId INT PRIMARY KEY IDENTITY (1,1),
	AdminName NVARCHAR(30) UNIQUE NOT NULL,
	AdminPassword NVARCHAR(255) NOT NULL,
	RoleId NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES AdminRole(RoleId),
	AdminNote NVARCHAR(255) NULL,
	IsActive BIT NOT NULL DEFAULT 0,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.Facility', 'U') IS NOT NULL
	DROP TABLE dbo.Facility
CREATE TABLE Facility(					
	FacilityId NVARCHAR(40) PRIMARY KEY,
	FacilityName NVARCHAR(255) NOT NULL,
	FacilityTypeId NVARCHAR(40) FOREIGN KEY REFERENCES FacilityType(FacilityTypeId) NOT NULL,	
	FacilityLocation NVARCHAR(255) NOT NULL,
	FacilitySocials NVARCHAR(255) NULL,
	FacilityPhone NVARCHAR(255) NULL,
	FacilityEmail NVARCHAR(255) NULL,
	CityId NVARCHAR(40) FOREIGN KEY REFERENCES City(CityId) NOT NULL,
	Quantity INT NOT NULL DEFAULT 0,
	FacilityImage NVARCHAR(255) NULL,
	ServiceNote NVARCHAR(255) NOT NULL,
	FacilityAvailability BIT NOT NULL DEFAULT 0,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.TouristSpot', 'U') IS NOT NULL
	 DROP TABLE dbo.TouristSpot
CREATE TABLE TouristSpot(
	TouristSpotId NVARCHAR(40) PRIMARY KEY,
	TouristSpotName NVARCHAR(255) NOT NULL,
	CityId NVARCHAR(40) FOREIGN KEY REFERENCES City(CityId) NOT NULL,
	SubCategoryId NVARCHAR(40) FOREIGN KEY REFERENCES SubCategory(SubCategoryId) NOT NULL,
	TouristSpotLocation NVARCHAR(255) NOT NULL,
	TouristSpotRating FLOAT NOT NULL DEFAULT 1,
	OpenHour BIGINT NOT NULL,
	ClosingHour BIGINT NOT NULL,
	TouristSpotAvailability BIT NOT NULL DEFAULT 0,
	TouristSpotImage NVARCHAR(255) NULL,
	Cord_Lat NVARCHAR(20) NULL,
	Cord_Long NVARCHAR(20) NULL,
	TouristSpotNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.Tour', 'U') IS NOT NULL
  DROP TABLE dbo.Tour
CREATE TABLE Tour(
	TourId NVARCHAR(40) PRIMARY KEY,
	TourName NVARCHAR(255) NOT NULL,
	TourAvailability BIT NOT NULL DEFAULT 0,
	TourStart DATETIME NOT NULL,
	TourEnd DATETIME NOT NULL,
	TourPrice MONEY NOT NULL,
	CategoryId1 NVARCHAR(40) FOREIGN KEY REFERENCES Category(CategoryId) NOT NULL,
	CategoryId2 NVARCHAR(40) FOREIGN KEY REFERENCES Category(CategoryId) NULL,
	MaxBooking INT NOT NULL,
	BookTimeLimit INT NOT NULL,
	TourRating FLOAT NOT NULL DEFAULT 1,
	TourImage NVARCHAR(255) NULL,
	TourNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.TourDetail', 'U') IS NOT NULL
  DROP TABLE dbo.TourDetail
CREATE TABLE TourDetail(
	TourDetailId INT PRIMARY KEY IDENTITY(1,1),
	TourDetailName NVARCHAR(255) NOT NULL,
	Activity NVARCHAR(255) NOT NULL,
	ActivityTimeStart DATETIME NOT NULL,
	ActivityTimeEnd DATETIME NOT NULL,
	TouristSpotId NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES TouristSpot(TouristSpotId),
	FacilityId NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES Facility(FacilityId),
	ActivityNote NVARCHAR(255) NULL,
	TourId NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES Tour(TourId),
	Deleted BIT NOT NULL DEFAULT 0
)

GO

IF OBJECT_ID('dbo.TransactionRecord', 'U') IS NOT NULL
  DROP TABLE dbo.TransactionRecord
CREATE TABLE TransactionRecord(
	TransactionRecordId INT PRIMARY KEY IDENTITY(1,1),
	TransactionTypeId NVARCHAR(40) FOREIGN KEY REFERENCES TransactionType(TransactionTypeId) NOT NULL,
	TourId NVARCHAR(40) FOREIGN KEY REFERENCES Tour(TourId) NOT NULL,
	CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID) NOT NULL,
	TransactionFee MONEY NOT NULL,
	Paid BIT NOT NULL DEFAULT 0,
	RecordedTime DATETIME NOT NULL,
	AdminID INT FOREIGN KEY REFERENCES [Admin](AdminID) NULL,
	TransactionNote NVARCHAR(255) NULL,
	Deleted BIT NOT NULL DEFAULT 0
)


ON [PRIMARY]
GO


INSERT INTO Country(CountryId, CountryName, Continent, RegionCode) VALUES
(N'VN', N'VietNam', N'Asia', N'+84'),
(N'CN', N'China', N'Asia', N'+86')


INSERT INTO City(CityId, CityName, CountryId, PostalCode) VALUES
(N'VN_HN', N'Hà Nội', N'VN', N'100000'),
(N'VN_CB', N'Cao Bằng', N'VN', N'270000'),
(N'VN_MC', N'Mai Châu', N'VN', N'350000'),
(N'VN_VY', N'Vĩnh Yên', N'VN', N'280000')


INSERT INTO Bank(BankId, BankName, SwiftCode, CountryId) VALUES
(N'B_VN_TCB', N'TECHCOMBANK', N'VTCBVNVX', N'VN'),
(N'B_VN_VCB', N'VIETCOMBANK', N'BFTVVNVX', N'VN'),
(N'B_VN_BIVD', N'BIDV', N'BIDVVNVX', N'VN'),
(N'B_CN_JCB', N'JPMORGAN CHASE BANK', N'CHASCN22', N'CN'),
(N'B_CN_BOK', N'BANK OF KUNLUN', N'CKLBCNBJ', N'CN'),
(N'B_CN_BIVD', N'CHINA CONSTRUCTION BANK', N'PCBCCNBJ', N'CN')


INSERT INTO AdminRole(RoleId, RoleName, RoleNote) VALUES
(N'TEMP', N'Temporary account', N'Have no access'),
(N'SALE_MG',N'Saler', N'Manage sales, trasactions and user accounts, cannot access tour database'),
(N'TOUR_MG', N'Tour Manager', N'Manage the tour database, cannot access sales and users'),
(N'SN_MG', N'Senior Manager', N'Have access to all divisions, and can access deleted items')


INSERT INTO [Admin](AdminName, AdminPassword ,RoleId) VALUES
(N'NTA', N'40bd001563085fc35165329ea1ff5c5ecbdbbeef', N'SN_MG'),
(N'VHL', N'51eac6b471a284d3341d8c0c63d0f1a286262a18', N'SALE_MG'),
(N'VCM', N'fc1200c7a7aa52109d762a9f005b149abef01479', N'TOUR_MG')


INSERT INTO TransactionType(TransactionTypeId, TransactionTypeName, TransactionTypeNote, TransactionPriceRate) VALUES
(N'REFUND', N'Refund deposit', N'Refund the deposit amount when the customer cancels the booking', 0.3),
(N'DEPOSIT', N'Put a deposit', N'Leave 30% of the tour price as a deposit', 0.3),
(N'PURCHSE', N'Purchase tour', N' Pay the remaining 70% of booking price', 0.7),
(N'CANCL_EARL', N'Cancel tour early', N'No fee charges, full refund', 0),
(N'CANCL_LATE', N'Cancel tour late', N'(Few days before tour starts) Half refund', 0.3)


INSERT INTO FacilityType(FacilityTypeId, FacilityTypeName) VALUES
(N'UNTP', N'Untyped'),
(N'HTEL', N'Hotel'),
(N'RSRT', N'Resort'),
(N'LODG', N'Lodging house'),
(N'RSTR', N'Restaurant'),
(N'SHP', N'Merchandise shop'),
(N'BNK', N'Bank'),
(N'STA_VHC', N'Vehicle station'),
(N'STA_TRN', N'Train station'),
(N'STA_DCK', N'Ships docking station'),
(N'AIRPRT', N'Airport')


INSERT INTO Category(CategoryId, CategoryName, CategoryNote) VALUES
(N'UNCG', N'Uncategorized', NULL),
(N'NATR', N'Natural', N'Water bodies, highlands, caves,...'),
(N'HIST', N'Historical', N'Pagodas, temples, museums...'),
(N'CULTR', N'Cultural', N'Villages, festival sites,...')


INSERT INTO SubCategory(SubCategoryId, SubCategoryName, SubCategoryNote, CategoryId) VALUES
(N'UNSCG', N'Uncategorized', NULL, N'UNCG'),
(N'WTER', N'Water bodies', N'Rivers, lakes, ponds, waterfalls,...', N'NATR'),
(N'TEMPL', N'Temples/Pagodas', NULL, N'HIST'),
(N'CAVE', N'Caves/grottos', NULL, N'NATR'),
(N'VILLG', N'Villages', NULL, N'CULTR'),
(N'HGHLN', N'Highlands', N'Mountains, hills', N'NATR')


INSERT INTO Tour(TourId, TourName, TourAvailability, TourStart, TourEnd, TourPrice, TourNote, TourImage, TourRating, MaxBooking, BookTimeLimit, CategoryId1, CategoryId2) VALUES
(N'T_HNtoCB01', N'Hà Nội - Cao Bằng', 1, CAST(N'2022-08-03T06:00:00.000' AS DateTime), CAST(N'2022-08-04T21:00:00.000' AS DateTime), 2350000.0000, NULL, '/assets/images/T_HNtoCB01.png', 4.2, 40, 60, N'NATR', N'HIST'),
(N'T_HNtoMC01', N'Hà Nội - Mai Châu', 1, CAST(N'2022-09-12T07:00:00.000' AS DateTime), CAST(N'2022-09-13T18:00:00.000' AS DateTime), 1580000.0000, NULL, '/assets/images/T_HNtoMC01.png', 3.6, 30, 30, N'CULTR', N'NATR'),
(N'T_HNtoTD01', N'Hà Nội - Tam Đảo', 1, CAST(N'2022-12-04T07:00:00.000' AS DateTime), CAST(N'2022-12-05T18:30:00.000' AS DateTime), 1350000.0000, NULL, '/assets/images/T_HNtoTD01.png', 4.9, 20, 30, N'NATR', N'CULTR')


INSERT INTO TouristSpot(TouristSpotId, TouristSpotName, CityId, SubCategoryId, TouristSpotRating, TouristSpotAvailability, TouristSpotLocation, TouristSpotImage, OpenHour, ClosingHour, Cord_Lat, Cord_Long) VALUES 
(N'none', N'none', N'VN_HN', N'UNSCG', 0, 1, N'none', NULL, 0, 0, NULL, NULL),
(N'TS_CB_BB01', N'Ba Bể Lake', N'VN_CB', N'WTER', 4, 1, N'At the center of The National Garden of Ba Bể, Nam Mẫu Commune, Ba Bể District', N'/assets/images/TS_CB_BB01.png', 252000000000, 684000000000, N'22.413690640121835', N'105.61364174880788'),
(N'TS_HN_TMC01', N'Trái tim Mộc Châu Tea Hill', N'VN_HN', N'HGHLN', 4.5, 1, N'VMRP+7HQ, Unnamed Road, TT. NT Mộc Châu, Mộc Châu', N'/assets/images/TS_HN_TMC01.png', 0, 0, N'20.890727609551', N'104.68646126880073'),
(N'TS_HN_B01', N'Bạc Waterfall,  Sao stream', N'VN_HN', N'WTER', 4.9, 1, N'XCQR+QXM, Provincial Road 446, Yên Bình, Thạch Thất', N'/assets/images/TS_HN_B01.png', 252000000000, 648000000000, N'21.453373782682352', N'105.64400081299175'),
(N'TS_HN_CC01', N'Cát Cát Village', N'VN_HN', N'VILLG', 3.6, 1, N'San Sả Hồ, Sa Pa', N'/assets/images/TS_HN_CC01.png', 216000000000, 648000000000, N'22.33011940702077', N'103.8318837060974'),
(N'TS_CB_PTL01', N'Phật tích Trúc Lâm Pagoda of Bản Giốc', N'VN_CB', N'TEMPL', 2.5, 1, N'Old National Highway 3, Sông Hiến Ward', N'/assets/images/TS_CB_PTL01.png', 0, 0, N'22.680747374910645', N'106.23360256883093'),
(N'TS_CB_NN01', N'Ngườm Ngao Cave', N'VN_CB', N'CAVE', 4.2, 1, N'Ngườm Ngao cave entrance, Đàm Thuỷ, Trùng Khánh', N'/assets/images/TS_CB_NN01.png', 252000000000, 648000000000, N'22.845199855325074', N'106.70619115534286'),
(N'TS_CB_BG02', N'Bản Giốc Waterfall', N'VN_CB', N'WTER', 3.8, 1, N'TL 211, Đàm Thuỷ, Trùng Khánh', N'/assets/images/TS_CB_BG02.png', 0, 0, N'22.854787336930077', N'106.7240985976703'),
(N'TS_CB_D01', N'Đuổm Buddhist Temple', N'VN_CB', N'TEMPL', 3.3, 1, N'QP34+4MH, Chảo village, Phú Lương, Thái Nguyên, Bắc Kạn', N'/assets/images/TS_CB_D01.png', 0, 0, N'21.75296525820971', N'105.70672133997864'),
(N'TS_CB_AM01', N'An Mạ Buddhist Temple', N'VN_CB', N'TEMPL', 4.4, 1, N'CJ68+R59, Nam Mẫu, Ba Bể, Bắc Kạn', N'/assets/images/TS_CB_AM01.png', 0, 0, N'22.41296071581092', N'105.6155436966015'),
(N'TS_HN_P01', N'Puông Cave', N'VN_HN', N'CAVE', 4.7, 1, N'FM63+V9F, Khang Ninh, Ba Bể, Bắc Kạn', N'/assets/images/TS_HN_P01.png',  324000000000, 612000000000, N'22.462386382210422', N'105.65345765533614'),
(N'TS_MC_TK01', N'Thung Khe Pass', N'VN_MC', N'HGHLN', 2.5, 1, N'M47P+XRX, Phú Cường, Tân Lạc, Hòa Bình', N'/assets/images/TS_MC_TK01.png', 432000000000, 863400000000, N'20.66610999129284', N'105.13666031533224'),
(N'TS_HN_DY01', N'Dải Yếm Waterfall', N'VN_HN', N'WTER', 4.1, 1, N'RH8R+3WM, QL43, Mường Sang, Mộc Châu, Sơn La', N'/assets/images/TS_HN_DY01.png',  0, 0, N'20.8154682846861', N'104.59237938133306')


INSERT INTO Facility(FacilityId, FacilityName, FacilityTypeId, FacilityLocation, CityId, Quantity, ServiceNote, FacilityAvailability, FacilityImage) VALUES
(N'none', N'none', N'UNTP', N'', N'VN_HN', 0, 'none', 1, NULL),
(N'FC_HN_HV01', N'Hải Vân Bus Station', N'STA_VHC', N'23, Nguyễn Khuyến road', N'VN_HN', 20, N'29 seats per 20 bus', 1, N'/assets/images/FC_HN_HV01.png'),
(N'FC_HN_NN01', N'Nguyễn Nhật Bus Station', N'STA_VHC', N'25 Văn Quán street', N'VN_HN', 7, N'16 seats per 7 bus', 1, N'/assets/images/FC_HN_NN01.png'),
(N'FC_MC_HT01', N'Hợp Thủy Restaurant', N'RSTR', N'Subdivision 2, Mai Châu district', N'VN_MC', 20, N'4 seats per 20 tables', 1, N'/assets/images/FC_MC_HT01.png'),
(N'FC_VY_NA01', N'Nguyệt Anh Hotel', N'HTEL', N'Block 1, Tam Đảo district', N'VN_VY', 12, N'15 double rooms, 15 tables with 4 seats', 1, N'/assets/images/FC_VY_NA01.png'),
(N'FC_VY_GTD01', N'Gió Tam Đảo Cafe', N'RSTR', N'Tam Đảo district, Vĩnh Phúc province', N'VN_VY', 16, N'4 seats per 16 tables', 1, N'/assets/images/FC_VY_GTD01.png'),
(N'FC_VY_PN01', N'Phố Núi Restaurant', N'RSTR', N'Near Hợp Châu town''s people''s Committee , Tam Đảo district', N'VN_VY', 6, N'3 seats per 6 tables', 1, N'/assets/images/FC_VY_PN01.png'),
(N'FC_HN_BND01', N'Bảo Ngọc Diamond Hotel', N'HTEL', N'1st floor, 124 Bế Văn Đàn street, Hợp Giang sub-district', N'VN_CB', 10, N'10 double rooms, 12 four seats tables', 1, N'/assets/images/FC_HN_BND01.png'),
(N'FC_CB_TL01', N'Thành Loan Hotel', N'HTEL', N'131 Vườn Cam street', N'VN_CB', 9, N'9 triple rooms, 4 ten seats tables', 1, N'/assets/images/FC_CB_TL01.png'),
(N'FC_HN_TN01', N'Thành Nam Bus Station', N'STA_VHC', N'29 Mộ Lao street', N'VN_HN', 16, N'42 seats per 16 buses', 1, N'/assets/images/FC_HN_TN01.png'),
(N'FC_CB_BG01', N'Bản Giốc Restaurant', N'RSTR', N'Bản Giốc hamlet, Đàm Thủy commune, Trùng Khánh district', N'VN_CB', 18, N'7 seats per 18 tables', 1, N'/assets/images/FC_CB_BG01.png'),
(N'FC_MC_EE01', N'88 Hotel', N'HTEL', N'102 Hoàng Quốc Việt street', N'VN_MC', 5, N'5 double rooms, 2 seats per 5 tables', 1, N'/assets/images/FC_MC_EE01.png'),
(N'FC_MC_BCE01', N'Ba Chị Em Restaurant', N'RSTR', N'Chiềng Sại hamlet', N'VN_MC', 15, N'2 seats per 15 tables', 1, N'/assets/images/FC_MC_BCE01.png')


INSERT INTO TourDetail(TourId, TourDetailName, TouristSpotId, FacilityId, Activity, ActivityTimeStart, ActivityTimeEnd, ActivityNote) VALUES
(N'T_HNtoCB01', N'Tour starts', N'none', N'FC_HN_HV01', N'Board the bus and head to Đuồm Pagoda', CAST(N'2022-08-03T06:00:00.000' AS DateTime), CAST(N'2022-08-03T09:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit Đuồm Pagoda', N'TS_CB_D01', N'none', N'Sightseeing at the Đuồm Buddist Temple', CAST(N'2022-08-03T09:30:00.000' AS DateTime), CAST(N'2022-08-03T11:00:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Head to Thành Loan Hotel', CAST(N'2022-08-03T11:00:00.000' AS DateTime), CAST(N'2022-08-03T11:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Have lunch and rest', N'none', N'FC_CB_TL01', N'Check in, have lunch and rest at Thành Loan Hotel', CAST(N'2022-08-03T11:30:00.000' AS DateTime), CAST(N'2022-08-03T14:00:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Move to Ba Bể Lake', CAST(N'2022-08-03T14:00:00.000' AS DateTime), CAST(N'2022-08-03T14:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit Ba Bể Lake', N'TS_CB_BB01', N'none', N'Enjoy the nature of Ba Bể Lake and its surrounding jungles', CAST(N'2022-08-03T14:30:00.000' AS DateTime), CAST(N'2022-08-03T15:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Make way to Puông Cave', CAST(N'2022-08-03T15:30:00.000' AS DateTime), CAST(N'2022-08-03T16:45:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit Puông Cave', N'TS_HN_P01', N'none', N'Sightseeing at Puông Cave', CAST(N'2022-08-03T16:45:00.000' AS DateTime), CAST(N'2022-08-03T18:00:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Return to the hotel', N'none', N'FC_HN_HV01', N'Board the bus and head back to Thành Loan Hotel', CAST(N'2022-08-03T18:00:00.000' AS DateTime), CAST(N'2022-08-03T18:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Have dinner', N'none', N'FC_CB_TL01', N'Have dinner and stay the night at the hotel', CAST(N'2022-08-03T18:30:00.000' AS DateTime), CAST(N'2022-08-04T05:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Made way to Bản Giốc Waterfall, have breakfast on the way', CAST(N'2022-08-04T05:30:00.000' AS DateTime), CAST(N'2022-08-04T10:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit Bản Gióc Waterfall', N'TS_CB_BG02', N'none', N'Sightseeing at Bản Giốc Waterfall', CAST(N'2022-08-04T10:30:00.000' AS DateTime), CAST(N'2022-08-04T11:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Have lunch', N'none', N'FC_CB_BG01', N'Have lunch at Bản Giốc Restaurant', CAST(N'2022-08-04T11:30:00.000' AS DateTime), CAST(N'2022-08-04T12:15:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Head to Phật tích Trúc Lâm Pagoda', CAST(N'2022-08-04T12:15:00.000' AS DateTime), CAST(N'2022-08-04T12:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit the Phật tích Trúc Lâm Pagoda', N'TS_CB_PTL01', N'none', N'Freely visit the Phật tích Trúc Lâm pagoda and offer incense', CAST(N'2022-08-04T12:30:00.000' AS DateTime), CAST(N'2022-08-04T14:00:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Move to Ngườm Ngao Cave', CAST(N'2022-08-04T14:00:00.000' AS DateTime), CAST(N'2022-08-04T14:20:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit Ngườm Ngao Cave', N'TS_CB_NN01', N'none', N'Traverse Ngườm Ngao Cave', CAST(N'2022-08-04T14:20:00.000' AS DateTime), CAST(N'2022-08-04T15:00:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Board the bus', N'none', N'FC_HN_HV01', N'Go to An Mã Pagoda', CAST(N'2022-08-04T15:00:00.000' AS DateTime), CAST(N'2022-08-04T15:15:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Visit An Mạ Pagoda', N'TS_CB_AM01', N'none', N'Sightseeing at An Mã Pagoda', CAST(N'2022-08-04T15:15:00.000' AS DateTime), CAST(N'2022-08-04T16:30:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Return to the hotel', N'none', N'FC_HN_HV01', N'Return to Thành Loan Hotel', CAST(N'2022-08-04T16:30:00.000' AS DateTime), CAST(N'2022-08-04T17:45:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Have dinner and check out', N'none', N'FC_CB_TL01', N'Have your last meal and check out', CAST(N'2022-08-04T15:45:00.000' AS DateTime), CAST(N'2022-08-04T19:00:00.000' AS DateTime), NULL),
(N'T_HNtoCB01', N'Tour ends', N'none', N'FC_HN_HV01', N'Head back to Hà Nội', CAST(N'2022-08-04T19:00:00.000' AS DateTime), CAST(N'2022-08-04T21:00:00.000' AS DateTime), NULL),
----
(N'T_HNtoMC01', N'Tour starts', N'none', N'FC_HN_TN01', N'Board the bus and make your way to Thung Khe Pass', CAST(N'2022-09-12T07:00:00.000' AS DateTime), CAST(N'2022-09-12T08:30:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Visit Thung Khe Pass', N'TS_MC_TK01', N'none', N'Sightseeing as you climb through the pass', CAST(N'2022-09-12T08:30:00.000' AS DateTime), CAST(N'2022-09-12T10:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Board the bus', N'none', N'FC_HN_TN01', N'Move to Ba Chị Em Restaurant', CAST(N'2022-09-12T10:00:00.000' AS DateTime), CAST(N'2022-09-12T11:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Have lunch at a local restaurant', N'none', N'FC_MC_BCE01', N'Have lunch and a short break at Ba Chị Em Restaurant', CAST(N'2022-09-12T11:00:00.000' AS DateTime), CAST(N'2022-09-12T14:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Board the bus', N'none', N'FC_HN_TN01', N'Go to Dải Yếm Waterfall', CAST(N'2022-09-12T14:00:00.000' AS DateTime), CAST(N'2022-09-12T14:45:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Visit Dải Yếm Waterfall', N'TS_HN_DY01', N'none', N'Sightseeing at Dải Yếm Waterfalls', CAST(N'2022-09-12T14:45:00.000' AS DateTime), CAST(N'2022-09-12T16:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Make way to the hotel', N'none', N'FC_HN_TN01', N'Board the bus and move to 88 Hotel', CAST(N'2022-09-12T16:00:00.000' AS DateTime), CAST(N'2022-09-12T17:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Have dinner and rest', N'none', N'FC_MC_EE01', N'Check in, have dinner and stay the night at 88 Hotel', CAST(N'2022-09-12T17:00:00.000' AS DateTime), CAST(N'2022-09-13T07:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Board the bus', N'none', N'FC_HN_TN01', N'Check out of the hotel and move to the tea hill', CAST(N'2022-09-13T07:00:00.000' AS DateTime), CAST(N'2022-09-13T08:30:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Visit Trái tim Mộc Châu tea hill', N'TS_HN_TMC01', N'none', N'Sightseeing at take pictures at the Trái tim Mộc Châu tea hill', CAST(N'2022-09-13T08:00:00.000' AS DateTime), CAST(N'2022-09-13T11:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Board the bus', N'none', N'FC_HN_TN01', N'Make way to the restaurant for lunch', CAST(N'2022-09-13T11:00:00.000' AS DateTime), CAST(N'2022-09-13T11:45:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Have lunch and rest', N'none', N'FC_MC_HT01', N'Have lunch and a short break at Hợp Thủy Restaurant', CAST(N'2022-09-13T11:45:00.000' AS DateTime), CAST(N'2022-09-13T14:00:00.000' AS DateTime), NULL),
(N'T_HNtoMC01', N'Tour ends', N'none', N'FC_HN_TN01', N'The bus travels back to Hà Nội', CAST(N'2022-09-13T14:00:00.000' AS DateTime), CAST(N'2022-09-13T18:00:00.000' AS DateTime), NULL),
----
(N'T_HNtoTD01', N'Tour starts', N'none', N'FC_HN_NN01', N'Board the bus and make way to the hotel', CAST(N'2022-12-04T07:00:00.000' AS DateTime), CAST(N'2022-12-04T09:30:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Check in the hotel and visit the Stone Church', N'none', N'FC_VY_NA01', N'Check in at Nguyệt Anh Hotel and visit the nearby Stone Church', CAST(N'2022-12-04T09:30:00.000' AS DateTime), CAST(N'2022-12-04T11:00:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Have lunch and rest', N'none', N'FC_VY_NA01', N'Have lunch and a break at Nguyệt Anh Hotel', CAST(N'2022-12-04T11:00:00.000' AS DateTime), CAST(N'2022-12-04T14:00:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Board the bus', N'none', N'FC_HN_NN01', N'Move to Bạc Waterfall', CAST(N'2022-12-04T14:00:00.000' AS DateTime), CAST(N'2022-12-04T14:30:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Visit Bạc Waterfall', N'TS_HN_B01', N'none', N'Sightseeing at Bạc Waterfall', CAST(N'2022-12-04T14:30:00.000' AS DateTime), CAST(N'2022-12-04T17:30:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Board the bus', N'none', N'FC_HN_NN01', N'Return to the hotel', CAST(N'2022-12-04T17:30:00.000' AS DateTime), CAST(N'2022-12-04T18:00:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Have dinner and rest', N'none', N'FC_VY_NA01', N'Have dinner and stay the night at Nguyệt Anh Hotel', CAST(N'2022-12-04T18:00:00.000' AS DateTime), CAST(N'2022-12-05T07:00:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Check out and board the bus', N'none', N'FC_HN_NN01', N'Check out of the hotel and move to Cát Cát Village', CAST(N'2022-12-05T07:00:00.000' AS DateTime), CAST(N'2022-12-05T08:30:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Visit Cát Cát Village', N'TS_HN_CC01', N'none', N'Visit Cát Cát Village', CAST(N'2022-12-05T08:30:00.000' AS DateTime), CAST(N'2022-12-05T11:30:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Board the bus', N'none', N'FC_HN_NN01', N'Go to Phố Núi Restaurant', CAST(N'2022-12-05T11:30:00.000' AS DateTime), CAST(N'2022-12-05T12:00:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Have lunch and rest', N'none', N'FC_VY_PN01', N'Have lunch and a short break at the restaurant', CAST(N'2022-12-05T12:00:00.000' AS DateTime), CAST(N'2022-12-05T13:30:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Board the bus', N'none', N'FC_HN_NN01', N'Make your way to Gió Tam Đảo Cafe', CAST(N'2022-12-05T13:30:00.000' AS DateTime), CAST(N'2022-12-05T13:45:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Enjoy local coffe at Gió Tam Đảo Cafe', N'none', N'FC_VY_GTD01', N'Have coffe and sightseeing at the Gió Tam Đảo Cafe', CAST(N'2022-12-05T13:45:00.000' AS DateTime), CAST(N'2022-12-05T15:45:00.000' AS DateTime), NULL),
(N'T_HNtoTD01', N'Tour ends', N'none', N'FC_HN_NN01', N'Return to Hà Nội', CAST(N'2022-12-05T15:45:00.000' AS DateTime), CAST(N'2022-12-05T18:30:00.000' AS DateTime), NULL)

GO
