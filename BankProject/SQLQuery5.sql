USE [BANK]
Go

CREATE TABLE Accounts (
    AccountNumber INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    City NVARCHAR(50),
    State NVARCHAR(50),
    DateOfOpening DATE,
    Amount DECIMAL(18, 2),
    AccountType NVARCHAR(20),
    Status NVARCHAR(20)
);
Go

CREATE PROCEDURE GenerateAccount
AS
BEGIN
SELECT CASE WHEN MAX (AccountNumber) IS NULL THEN 1
ELSE Max(AccountNumber) +1 END accno From Accounts;
END;
GO

CREATE PROCEDURE CreateAccount
    @AccountNumber INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @City NVARCHAR(50),
    @State NVARCHAR(50),
    @DateOfOpening DATE,
    @Amount DECIMAL(18, 2),
    @AccountType NVARCHAR(20),
    @Status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Accounts (AccountNumber, FirstName, LastName, City, State, DateOfOpening, Amount, AccountType, Status)
    VALUES (@AccountNumber, @FirstName, @LastName, @City, @State, @DateOfOpening, @Amount, @AccountType, @Status);
END;
Go

EXEC CreateAccount 
    @AccountNumber = 101,
    @FirstName = 'John',
    @LastName = 'Doe',
    @City = 'New York',
    @State = 'NY',
    @DateOfOpening = '2020-01-15',
    @Amount = 1500.00,
    @AccountType = 'Savings',
    @Status = 'Active';

Go

CREATE PROCEDURE DeleteBankAccount
    @AccountNumber INT
AS
BEGIN
    DELETE FROM Accounts
    WHERE AccountNumber = @AccountNumber;
END;
Go

CREATE PROCEDURE GetBankAccount
    @AccountNumber INT
AS
BEGIN
    SELECT * FROM Accounts
    WHERE AccountNumber = @AccountNumber;
END;
Go


CREATE PROCEDURE GetAllBankAccounts
AS
BEGIN
    SELECT * FROM Accounts
END;
Go

Select * from Accounts;
Go

CREATE PROCEDURE UpdateAccount
		@AccountNumber INT,
		@Amount DECIMAL(18, 2)
AS
BEGIN
     SET NOCOUNT ON;
	 UPDATE Accounts
    SET 
          Amount = @Amount
    WHERE AccountNumber = @AccountNumber;

END;
Go
	

CREATE PROCEDURE WithdrawAmount
		@AccountNumber INT,
		@withdrawAmount DECIMAL(18, 2)	
As
Begin
		declare @currentbalance DECIMAL(18, 2)
		select @currentbalance= Amount from Accounts where AccountNumber= @AccountNumber

		if @currentbalance > @withdrawAmount
		begin 
		UPDATE Accounts SET Amount= Amount - @withdrawAmount  where AccountNumber = @AccountNumber
		end
END
Go

	