CREATE TABLE ITEM_DET (
    Id INT identity, 
    AP_IID INT,
    Rev INT,
    AreaPath VARCHAR(255),
    TeamProject VARCHAR(255),
    IterationPath VARCHAR(255),
    WorkItemType VARCHAR(255),
    State VARCHAR(255),
    AssignedTo_DisplayName VARCHAR(255),
    CreatedDate DATETIME,
    Title VARCHAR(255),
    BoardColumn VARCHAR(255),
    ActivatedDate DATETIME,
    ResolvedDate DATETIME,
    PRIMARY KEY (Id)
);

select * from dbo.ITEM_DET
select * from dbo.ITEM_his
select * from dbo.SPR

CREATE TABLE ITEM_HIS (
    Id INT identity,
    AP_IID INT,
    Rev INT,
    System_ChangedDate_OldValue DATETIME,
    System_ChangedDate_NewValue DATETIME,
    System_BoardColumn_OldValue VARCHAR(255),
    System_BoardColumn_NewValue VARCHAR(255),
    PRIMARY KEY (Id)
);

Create table SPR (
      Id int identity,
      Api_Id varchar(255),
      NAME varchar(255),
      Path varchar(255),
      StartDate datetime,
      FinishDate datetime,
      Primary key (Id)
);