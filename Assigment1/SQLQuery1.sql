USE [FUMiniHotelManagement]
GO

SELECT [BookingReservationID]
      ,[RoomID]
      ,[StartDate]
      ,[EndDate]
      ,[ActualPrice]
  FROM [dbo].[BookingDetail]

GO

SELECT [BookingReservationID]
      ,[BookingDate]
      ,[TotalPrice]
      ,[CustomerID]
      ,[BookingStatus]
  FROM [dbo].[BookingReservation]

GO

SELECT [CustomerID]
      ,[CustomerFullName]
      ,[Telephone]
      ,[EmailAddress]
      ,[CustomerBirthday]
      ,[CustomerStatus]
      ,[Password]
  FROM [dbo].[Customer]

GO

SELECT [RoomID]
      ,[RoomNumber]
      ,[RoomDetailDescription]
      ,[RoomMaxCapacity]
      ,[RoomTypeID]
      ,[RoomStatus]
      ,[RoomPricePerDay]
  FROM [dbo].[RoomInformation]

GO
SELECT [RoomTypeID]
      ,[RoomTypeName]
      ,[TypeDescription]
      ,[TypeNote]
  FROM [dbo].[RoomType]

GO
