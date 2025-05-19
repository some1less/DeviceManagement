
CREATE TABLE DeviceType (
    Id             INT           NOT NULL IDENTITY(1,1),
    Name           VARCHAR(100)  NOT NULL UNIQUE,
    CONSTRAINT PK_DeviceType PRIMARY KEY (Id)
);

CREATE TABLE Position (
    Id             INT           NOT NULL IDENTITY(1,1),
    Name           VARCHAR(100)  NOT NULL UNIQUE,
    MinExpYears    INT           NOT NULL,
    CONSTRAINT PK_Position PRIMARY KEY (Id)
);

CREATE TABLE Person (
    Id             INT           NOT NULL IDENTITY(1,1),
    PassportNumber VARCHAR(30)   NOT NULL UNIQUE,
    FirstName      VARCHAR(100)  NOT NULL,
    MiddleName     VARCHAR(100)  NULL,
    LastName       VARCHAR(100)  NOT NULL,
    PhoneNumber    VARCHAR(20)   NOT NULL UNIQUE,
    Email          VARCHAR(150)  NOT NULL UNIQUE,
    CONSTRAINT PK_Person PRIMARY KEY (Id)
);


CREATE TABLE Device (
    Id                     INT           NOT NULL IDENTITY(1,1),
    Name                   VARCHAR(150)  NOT NULL,
    IsEnabled              BIT           NOT NULL  DEFAULT 1,
    AdditionalProperties   VARCHAR(8000) NOT NULL  DEFAULT '',
    DeviceTypeId           INT           NULL,
    CONSTRAINT PK_Device PRIMARY KEY (Id),
    CONSTRAINT FK_Device_DeviceType
        FOREIGN KEY (DeviceTypeId) REFERENCES DeviceType(Id)
);

CREATE TABLE Employee (
    Id         INT             NOT NULL IDENTITY(1,1),
    Salary     DECIMAL(18,2)   NOT NULL,
    PositionId INT             NOT NULL,
    PersonId   INT             NOT NULL,
    HireDate   DATETIME2       NOT NULL  DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Employee PRIMARY KEY (Id),
    CONSTRAINT FK_Employee_Position
        FOREIGN KEY (PositionId) REFERENCES Position(Id),
    CONSTRAINT FK_Employee_Person
        FOREIGN KEY (PersonId)   REFERENCES Person(Id)
);


CREATE TABLE DeviceEmployee (
    Id          INT         NOT NULL IDENTITY(1,1),
    DeviceId    INT         NOT NULL,
    EmployeeId  INT         NOT NULL,
    IssueDate   DATETIME2   NOT NULL  DEFAULT SYSUTCDATETIME(),
    ReturnDate  DATETIME2   NULL,
    CONSTRAINT PK_DeviceEmployee PRIMARY KEY (Id),
    CONSTRAINT FK_DeviceEmployee_Device
        FOREIGN KEY (DeviceId)   REFERENCES Device(Id),
    CONSTRAINT FK_DeviceEmployee_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
);
