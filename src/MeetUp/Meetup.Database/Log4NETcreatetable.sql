CREATE TABLE [dbo].[Log4Net] (
[ID] [int] IDENTITY (1, 1) NOT NULL ,
[Date] [datetime] NOT NULL ,
[Thread] [varchar] (255) NOT NULL ,
[Level] [varchar] (10) NOT NULL ,
[Logger] [varchar] (1000) NOT NULL ,
[Message] [varchar] (4000) NOT NULL ,
[Exception] [varchar] (4000) NOT NULL
) ON [PRIMARY]