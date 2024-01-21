/*
Navicat SQL Server Data Transfer

Source Server         : dev-sins
Source Server Version : 140000
Source Host           : 91.135.244.25:1433
Source Database       : NIS_SERVICE_TEST2
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 140000
File Encoding         : 65001

Date: 2018-10-23 09:57:24
*/


-- ----------------------------
-- Table structure for [dbo].[LawProcess]
-- ----------------------------
DROP TABLE [dbo].[LawProcess]
GO
CREATE TABLE [dbo].[LawProcess] (
[OrderDateTime] datetime2(7) NOT NULL ,
[OrderNo] nvarchar(MAX) NULL ,
[InputDateTime] datetime2(7) NULL ,
[Court] nvarchar(MAX) NULL ,
[Judge] nvarchar(MAX) NULL ,
[Description] nvarchar(250) NULL ,
[CreatedDate] datetime NULL ,
[CustomerRequestId] int NOT NULL ,
[Id] int NOT NULL ,
[Final] bit NOT NULL DEFAULT ((0)) ,
[Amount] float(53) NULL 
)


GO

-- ----------------------------
-- Records of LawProcess
-- ----------------------------
INSERT INTO [dbo].[LawProcess] VALUES (N'2018-10-22 10:23:44.0000000', N'002', N'2018-10-22 10:23:51.0000000', N'ali', N'hesen', N'no comment', null, N'42322', N'1', N'0', null);
GO
INSERT INTO [dbo].[LawProcess] VALUES (N'1-01-01 00:00:00.0000000', N'002', N'1-01-01 00:00:00.0000000', N'aliyev', N'hesen', N'no comment', null, N'42322', N'2', N'0', null);
GO
INSERT INTO [dbo].[LawProcess] VALUES (N'2018-10-23 08:42:37.0000000', N'00234234', N'2018-10-31 08:55:00.0000000', N'Ali Kont', N'Ni hakim', N'<p>comment here</p>', null, N'42325', N'4', N'0', null);
GO
INSERT INTO [dbo].[LawProcess] VALUES (N'2018-10-22 12:55:00.0000000', N'0234234', null, N'Konst', N'Mamed', N'<p>mamedilo</p>', null, N'42325', N'5', N'0', null);
GO
INSERT INTO [dbo].[LawProcess] VALUES (N'2018-10-22 13:00:00.0000000', N'44324', null, N'ali', N'memur', null, null, N'42325', N'6', N'1', null);
GO
INSERT INTO [dbo].[LawProcess] VALUES (N'2018-10-22 08:55:00.0000000', N'7', N'2018-10-23 08:40:00.0000000', N'1 saylı Bakı İnzibati-İqtisadi Məhkəməsi', N'Aida Hüseyn', N'<p>test</p>', null, N'42325', N'7', N'0', N'4234');
GO

-- ----------------------------
-- Indexes structure for table LawProcess
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[LawProcess]
-- ----------------------------
ALTER TABLE [dbo].[LawProcess] ADD PRIMARY KEY ([Id])
GO
