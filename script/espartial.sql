-- pop：0 - 删除；1 - 更新；2 - 插入
-- -----------------------------------------------------------
-- 存储过程：增、删、改->用户基本表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_USER_BASE`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_USER_BASE`(
IN `pUserID` VARCHAR(32),
IN `pPassword` VARCHAR(128),
IN `pSession` VARCHAR(240),
IN `pAlias` NVARCHAR(16),
IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pUserID` IS NOT NULL) THEN
		 DELETE FROM `ER_USER_BASE` WHERE USER_IDX=`pUserID`;
		END IF;
	ELSE 
	 IF(`pUserID` IS NOT NULL) THEN
		IF(pop=1) THEN
		 UPDATE `ER_USER_BASE` SET 
			PASSWORD = `pPassword`,
			SESSION = `pSession`,
			ALIAS = `pAlias`
		WHERE USER_IDX = `pUserID`;
		ELSE
		 INSERT INTO `ER_USER_BASE`(`USER_IDX`,`PASSWORD`,`SESSION`,`ALIAS`) VALUE(`pUserID`,`pPassword`,`pSession`,`pAlias`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：增、删、改->队伍基本表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_TEAM_DETAIL`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_TEAM_DETAIL`(
		IN `pTeamID` VARCHAR(32),
        IN `pTeamName` NVARCHAR(32),
        IN `pTeamHolderID` VARCHAR(32),
        IN `pTeamMealTime` DATETIME,
        IN `pTeamAfford` SMALLINT,
        IN `pTeamResID` VARCHAR(32),
        IN `pTeamMaxPerson` INT,
        IN `pTeamAliveStatus` INT,
        IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pTeamID` IS NOT NULL) THEN
        START TRANSACTION;
            DELETE FROM `ER_TEAM_BASE` WHERE TEAM_IDX=`pTeamID`;
            DELETE FROM `ER_TEAM_ADVANCED` WHERE TEAM_IDX=`pTeamID`;
         COMMIT;
		END IF;
	ELSE 
	  IF(`pTeamID` IS NOT NULL) THEN
		IF(pop=1) THEN
     START TRANSACTION;   
            UPDATE `ER_TEAM_BASE` SET 
                TEAM_NAME = `pTeamName`,
                TEAM_HOLDER_IDX = `pTeamHolderID`,
                TEAM_MEAL_TIME = `pTeamMealTime`,
                TEAM_AFFORD = `pTeamAfford`,
                TEAM_RESTAURANT_IDX = `pTeamResID`
            WHERE TEAM_IDX = `pTeamID`;
            UPDATE `ER_TEAM_ADVANCED` SET
                  TEAM_MAX_PERSONS = `pTeamMaxPerson`,
                  TEAM_ALIVE_STATUS = `pTeamAliveStatus`
            WHERE TEAM_IDX = `pTeamID`;
        COMMIT;
		ELSE
        START TRANSACTION;
            INSERT INTO `ER_TEAM_BASE`(`TEAM_IDX`,`TEAM_NAME`,`TEAM_HOLDER_IDX`,`TEAM_MEAL_TIME`,`TEAM_AFFORD`,`TEAM_RESTAURANT_IDX`,`TEAM_MAX_PEOPLE`,`TEAM_CREATE_TIME`) VALUE
                (`pTeamID`,`pTeamName`,`pTeamHolderID`,`pTeamMealTime`,`pTeamAfford`,`pTeamResID`,`pTeamMaxPerson`,NOW());
            INSERT INTO `ER_TEAM_ADVANCED`(`TEAM_IDX`,`TEAM_MAX_PERSONS`,`TEAM_ALIVE_STATUS`) VALUE(`pTeamID`,`pTeamMaxPerson`,`pTeamAliveStatus`);
         COMMIT;
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->好友列表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_FRIENDS`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_FRIENDS`(
		IN `pFriendForUserID` VARCHAR(32),
        IN `pFriendID` VARCHAR(32),
        IN `pFriendStatus` SMALLINT,
        IN `pCommuLog` TEXT, 
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pFriendForUserID` IS NOT NULL && `pFriendID` IS NOT NULL) THEN
		 DELETE FROM `ER_FRIENDS` WHERE USER_IDX=`pFriendForUserID` AND FRIEND_IDX=`pFriendID`;
		ELSE IF(`pFriendForUserID` IS NOT NULL) THEN
		 DELETE FROM `ER_FRIENDS` WHERE USER_IDX = `pFriendForUserID`;
		END IF;
    END IF;
	ELSE 
	 IF(`pFriendForUserID` IS NOT NULL && `pFriendID` IS NOT NULL) THEN
		IF(pop=1) THEN
		 UPDATE `ER_FRIENDS` SET
			FRIEND_STUTAS = `pFriendStatus`,
			COMMUNICATION_EVENTS = `pCommuLog`
		WHERE USER_IDX = `pFriendForUserID` AND FRIEND_IDX=`pFriendID`;
		ELSE
		 INSERT INTO `ER_FRIENDS`(`USER_IDX`,`FRIEND_IDX`,`FRIEND_STUTAS`,`COMMUNICATION_EVENTS`) VALUE
			(`pFriendForUserID`,`pFriendID`,`pFriendStatus`,`pCommuLog`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->队伍高级表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_TEAM_ADVANCED`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_TEAM_ADVANCED`(
		IN `pTeamID` VARCHAR(32) ,
        IN `pTeamMaxPerson` INT,
        IN `pTeamAliveStatus` SMALLINT, 
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pTeamID` IS NOT NULL) THEN
		 DELETE FROM `ER_TEAM_ADVANCED` WHERE TEAM_IDX=`pTeamID`;
		END IF;
	ELSE 
		IF(`pTeamID` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_TEAM_ADVANCED` SET
			FRIEND_STUTAS = `pTeamMaxPerson`,
			TEAM_ALIVE_STATUS = `pTeamAliveStatus`
		WHERE TEAM_IDX = `pTeamID`;
		ELSE
		 INSERT INTO `ER_TEAM_ADVANCED`(`TEAM_IDX`,`TEAM_MAX_PERSONS`,`TEAM_ALIVE_STATUS`) VALUE
			(`pTeamID`,`pTeamMaxPerson`,`pTeamAliveStatus`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->队伍操作表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_TEAM_OPERATIONS`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_TEAM_OPERATIONS`(
        IN `pTeamID` VARCHAR(32),
        IN `pTeamOpearationType` SMALLINT,
        IN `pTeamUserID` VARCHAR(32),
        IN `pTeamOpearationTime` DATETIME,
        IN `pTeamOpearationNote` TEXT, 
        IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pTeamID` IS NOT NULL) THEN
		 DELETE FROM `ER_TEAM_OPERATIONS` WHERE TEAM_IDX=`pTeamID`;
		END IF;
	ELSE 
		IF(`pTeamID` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_TEAM_OPERATIONS` SET 
			TEAM_OPERATION_TYPE = `pTeamOpearationType`,
			USER_IDX = `pTeamUserID`,
			TEAM_OPERATION_TIME = `pTeamOpearationTime`,
			TEAM_NOTE=`pTeamOpearationNote`
		WHERE TEAM_IDX = `pTeamID`;
		ELSE
		 INSERT INTO `ER_TEAM_OPERATIONS`(`TEAM_IDX`,`TEAM_OPERATION_TYPE`,`USER_IDX`,`TEAM_OPERATION_TIME`,`TEAM_NOTE`) VALUE
			(`pTeamID`,`pTeamOpearationType`,`pTeamUserID`,`pTeamOpearationTime`,`pTeamOpearationNote`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：增、删、改->用户位置表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_USER_LOCATION`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_USER_LOCATION`(
		IN `pUserID` VARCHAR(32),
        IN `pUserAddress` NVARCHAR(48),
        IN `pUserPort` INT,
        IN `pNetType` SMALLINT,
        IN `pLantitude` FLOAT ,
        IN `pLogitude` FLOAT ,
        IN `pCity` INT,
        IN `pOnlineStatus` SMALLINT,
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(pUserID IS NOT NULL) THEN
		 DELETE FROM `ER_USER_LOCATION` WHERE USER_IDX=`pUserID`;
		END IF;
	ELSE 
		IF(pUserID IS NOT NULL) THEN
		 IF(`pop`=1) THEN
		 UPDATE `ER_USER_LOCATION` SET 
			USER_IPADDRESS = `pUserAddress`,
			USER_PORT = `pUserPort`,
			USER_NETWORK_TYPE = `pNetType`,
			USER_LATITUDE = `pLantitude`,
			USER_LONGITUDE = `pLogitude`,
			USER_CITY_CODE = `pCity`,
			USER_ONLINE_STATUS = `pOnlineStatus`
		WHERE USER_IDX = pUserID;
		ELSE
		 INSERT INTO `ER_USER_LOCATION`(`USER_IDX`,`USER_IPADDRESS`,`USER_PORT`,`USER_NETWORK_TYPE`,`USER_LATITUDE`,`USER_LONGITUDE`,`USER_CITY_CODE`,`USER_ONLINE_STATUS`) VALUE
			(`pUserID`,`pUserAddress`,`pUserPort`,`pNetType`,`pLantitude`,`pLogitude`,`pCity`,`pOnlineStatus`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 用户积分表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_USER_POINTS`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_USER_POINTS`(
		IN `pUserID` VARCHAR(32),
    IN `pUserPoints` INT, 
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(pUserID IS NOT NULL) THEN
		 DELETE FROM `ER_USER_POINTS` WHERE USER_IDX=pUserID;
		END IF;
	ELSE 
		IF(pUserID IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_USER_POINTS` SET
			USER_ACTIVE_POINTS = `pUserPoints`
		WHERE USER_IDX = pUserID;
		ELSE
		 INSERT INTO `ER_USER_POINTS`(`USER_IDX`,`USER_ACTIVE_POINTS`) VALUE
			(pUserID,`pUserPoints`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 用户配置表，以JSON结构保存，用以用户更换设备后也能获取配置
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_USER_CONFIG_REMOTE`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_USER_CONFIG_REMOTE`(
		IN `pUserID` VARCHAR(32),
    IN `pUserConfig` TEXT, 
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(pUserID IS NOT NULL) THEN
		 DELETE FROM `ER_USER_CONFIG_REMOTE` WHERE USER_IDX=pUserID;
		END IF;
	ELSE 
		IF(pUserID IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_USER_CONFIG_REMOTE` SET
			USER_CONFIG_ALL = `pUserConfig`
		WHERE USER_IDX = pUserID;
		ELSE
		 INSERT INTO `ER_USER_CONFIG_REMOTE`(`USER_IDX`,`USER_CONFIG_ALL`) VALUE
			(pUserID,`pUserConfig`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：增、删、改->菜谱类型表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_COOK_BASE_TYPE`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_COOK_BASE_TYPE`(
		IN `pCookType` SMALLINT,
        IN `pCookName` NVARCHAR(48),
        IN `pCookParentType` SMALLINT, 
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pCookType` IS NOT NULL) THEN
		 DELETE FROM `ER_COOK_BASE_TYPE` WHERE COOK_TYPE=`pCookType`;
		END IF;
	ELSE 
		IF(`pCookType` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_COOK_BASE_TYPE` SET
			COOK_LOCALIZATION_NAME = `pCookName`,
			COOK_PARENT_TYPE = `pCookParentType`
		WHERE COOK_TYPE = `pCookType`;
		ELSE
		 INSERT INTO `ER_COOK_BASE_TYPE`(`COOK_TYPE`,`COOK_LOCALIZATION_NAME`,`COOK_PARENT_TYPE`) VALUE
			(`pCookType`,`pCookName`,`pCookParentType`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->菜名表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_COOK_DETAIL`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_COOK_DETAIL`(
       IN `pCookTopParentType` SMALLINT,
       IN `pCookSubParentType` SMALLINT,
       IN `pCookCode` VARCHAR(10),
       IN `pCookName` NVARCHAR(48),
       IN `pCookDescription` TEXT,
       IN `pCookMaterial` VARCHAR(256),
       IN `pCookAttribute` TEXT,
       IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pCookCode` IS NOT NULL) THEN
		 DELETE FROM `ER_COOK_DETAIL` WHERE COOK_CODE=`pCookCode`;
		END IF;
	ELSE 
		IF(`pCookCode` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_COOK_DETAIL` SET 
			COOK_TOPPARENT_TYPE = `pCookTopParentType`,
			COOK_SUBPARENT_TYPE = `pCookSubParentType`,
			COOK_LOCALIZATION_NAME = `pCookName`,
			COOK_LOCALIZATION_DESCRIPTION = `pCookDescription`,
      COOK_MAJOR_MATERIALS = `pCookMaterial`,
			COOK_ATTRIBUTES = `pCookAttribute`
		WHERE COOK_CODE = `pCookCode`;
		ELSE
		 INSERT INTO `ER_COOK_DETAIL`(`COOK_TOPPARENT_TYPE`,`COOK_SUBPARENT_TYPE`,`COOK_CODE`,`COOK_LOCALIZATION_NAME`,`COOK_LOCALIZATION_DESCRIPTION`,`COOK_MAJOR_MATERIALS`,`COOK_ATTRIBUTES`) VALUE
			(`pCookTopParentType`,`pCookSubParentType`,`pCookCode`,`pCookName`,`pCookDescription`,`pCookMaterial`,`pCookAttribute`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：增、删、改->原料类型表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_COOK_RAWMATERIAL_TYPE`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_COOK_RAWMATERIAL_TYPE`(
            IN `pTypeCode` SMALLINT UNSIGNED,
            IN `pTypeName` NVARCHAR(48),
            IN `pCookParentType` SMALLINT, 
            IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pTypeCode` IS NOT NULL) THEN
		 DELETE FROM `ER_COOK_RAWMATERIAL_TYPE` WHERE TYPE_CODE=`pTypeCode`;
		END IF;
	ELSE 
		IF(`pTypeCode` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_COOK_RAWMATERIAL_TYPE` SET
			TYPE_LOCALIZATION_NAME = `pTypeName`,
			COOK_PARENT_TYPE = `pCookParentType`
		WHERE TYPE_CODE = `pTypeCode`;
		ELSE
		 INSERT INTO `ER_COOK_RAWMATERIAL_TYPE`(`TYPE_CODE`,`TYPE_LOCALIZATION_NAME`,`COOK_PARENT_TYPE`) VALUE
			(`pTypeCode`,`pTypeName`,`pCookParentType`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：插入/更新/删除->主要原料表
-- -----------------------------------------------------------
DELIMITER $$
USE `EasyRestaurant`$$
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_MAJOR_MATERIAL`$$

CREATE PROCEDURE `EasyRestaurant`.`IUD_MAJOR_MATERIAL`(
        IN `pMaterialCode` VARCHAR(32),
        IN `pMaterialTypeCode` VARCHAR(20),
        IN `pMaterialName` NVARCHAR(24),
        IN `pMaterialDescription` TEXT,
        IN `pop` INT)
BEGIN
    IF(pop=0) THEN
		IF(`pMaterialCode` IS NOT NULL) THEN
		 DELETE FROM `ER_COOK_RAWMATERIAL` WHERE RAWMATERIAL_CODE=`pMaterialCode`;
		END IF;
	ELSE 
		IF(`pMaterialCode` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_COOK_RAWMATERIAL` SET 
			MAJOR_TYPE_CODE = `pMaterialTypeCode`,
			RAWMATERIAL_LOCALIZATION_NAME = `pMaterialName`,
			RAWMATERIAL_LOCALIZATION_DESCRIPTION = `pMaterialDescription`
		WHERE RAWMATERIAL_CODE = `pMaterialCode`;
		ELSE
		 INSERT INTO `ER_COOK_RAWMATERIAL`(`MAJOR_TYPE_CODE`,`RAWMATERIAL_LOCALIZATION_NAME`,`RAWMATERIAL_LOCALIZATION_DESCRIPTION`) VALUE
			(`pMaterialTypeCode`,`pMaterialName`,`pMaterialDescription`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->菜肴评论表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_COOK_COMMENT`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_COOK_COMMENT`(
		IN `pCookCode` VARCHAR(10),
        IN `pResID` VARCHAR(32) ,
        IN `pCommentContent` TEXT ,
        IN `pCommentUserID` NVARCHAR(32),
        IN `pCommentTime` DATETIME,
        IN `pEvalutionRate` SMALLINT ,
        IN `pCommentUseful` INT,
        IN `pCommentUnuseful` INT,
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pCookCode` IS NOT NULL) THEN
		 DELETE FROM `ER_COOK_COMMENT` WHERE COOK_CODE=`pCookCode`;
		END IF;
	ELSE 
		IF(`pCookCode` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_COOK_COMMENT` SET 
			RESTAURANT_IDX = `pResID`,
			COMMENT_CONTENT = `pCommentContent`,
			COMMENT_USER_IDX = `pCommentUserID`,
			COMMENT_DATETIME = `pCommentTime`,
			EVA_RATE = `pEvalutionRate`,
			COMMENT_USEFUL = `pCommentUseful`,
			COMMENT_UNUSEFUL = `pCommentUnuseful`
		WHERE COOK_CODE = `pCookCode`;
		ELSE
		 INSERT INTO `ER_COOK_COMMENT`(`COOK_CODE`,`RESTAURANT_IDX`,`COMMENT_CONTENT`,`COMMENT_USER_IDX`,`COMMENT_DATETIME`,`EVA_RATE`,`COMMENT_USEFUL`,`COMMENT_UNUSEFUL`) VALUE
			(`pCookCode`,`pResID`,`pCommentContent`,`pCommentUserID`,`pCommentTime`,`pEvalutionRate`,`pCommentUseful`,`pCommentUnuseful`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->用户菜名表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_USER_COOKBOOK`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_USER_COOKBOOK`(
		IN `pUserID` NVARCHAR(32),
        IN `pCookPriority` TEXT, 
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(pUserID IS NOT NULL) THEN
		 DELETE FROM `ER_USER_COOKBOOK` WHERE USER_IDX=pUserID;
		END IF;
	ELSE 
		IF(pUserID IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_USER_COOKBOOK` SET
			USER_IDX = pUserID,
			COOK_PRIORITY = `pCookPriority`
		WHERE USER_IDX = pUserID;
		ELSE
		 INSERT INTO `ER_USER_COOKBOOK`(`USER_IDX`,`COOK_PRIORITY`) VALUE
			(pUserID,`pCookPriority`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->餐厅基本表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_RESTAURANT_BASE`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_RESTAURANT_BASE`(
		IN `pResID` VARCHAR(32),
    IN `pResName` NVARCHAR(48),
    IN `pResDescription` TEXT,
    IN `pResCity` INT,
    IN `pResCountry` INT,
    IN `pResLatitude` FLOAT,
    IN `pResLongitude` FLOAT,
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pResID` IS NOT NULL) THEN
		 DELETE FROM `ER_RESTAURANT_BASE` WHERE RESTAURANT_IDX=`pResID`;
    END IF;
	ELSE 
		IF(`pResID` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_RESTAURANT_BASE` SET 
			RESTAURANT_NAME = `pResName`,
			RESTAURANT_DESCRIPTION = `pResDescription`,
			RESTAURANT_CITY_CODE = `pResCity`,
			RESTAURANT_COUNTRY_CODE = `pResCountry`,
			RESTAURANT_LANTITUDE = `pResLatitude`,
			RESTAURANT_LONGITUDE = `pResLongitude`
		WHERE RESTAURANT_IDX = `pResID`;
		ELSE
		 INSERT INTO `ER_RESTAURANT_BASE`(`RESTAURANT_IDX`,`RESTAURANT_NAME`,`RESTAURANT_DESCRIPTION`,`RESTAURANT_CITY_CODE`,`RESTAURANT_COUNTRY_CODE`,`RESTAURANT_LANTITUDE`,`RESTAURANT_LONGITUDE`) VALUE
			(`pResID`,`pResName`,`pResDescription`,`pResCity`,`pResCountry`,`pResLatitude`,`pResLongitude`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->餐厅评价表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_RESTAURANT_COMMENT`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_RESTAURANT_COMMENT`(
       IN `pResID` VARCHAR(32),
       IN `pCommentContent` TEXT(140),
       IN `pCommentUserID` VARCHAR(32),
       IN `pCommentTime` DATETIME,
       IN `pCommentUseful` INT,
       IN `pCommentUnuseful` INT,
		IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pResID` IS NOT NULL) THEN
		 DELETE FROM `ER_RESTAURANT_COMMENT` WHERE RESTAURANT_IDX=`pResID`;
		END IF;
	ELSE 
		IF(`pResID` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_RESTAURANT_COMMENT` SET 
			COMMENT_CONTENT = `pCommentContent`,
			COMMENT_USER_IDX = `pCommentUserID`,
			COMMENT_TIME = `pCommentTime`,
			COMMENT_USEFUL = `pCommentUseful`,
			COMMENT_UNUSEFUL = `pCommentUnuseful`
		WHERE RESTAURANT_IDX = `pResID`;
		ELSE
		 INSERT INTO `ER_RESTAURANT_COMMENT`(`RESTAURANT_IDX`,`COMMENT_CONTENT`,`COMMENT_USER_IDX`,`COMMENT_TIME`,`COMMENT_USEFUL`,`COMMENT_UNUSEFUL`) VALUE
			(`pResID`,`pCommentContent`,`pCommentUserID`,`pCommentTime`,`pCommentUseful`,`pCommentUnuseful`);
		END IF;
	 END IF;
	END IF;
END $$
-- -----------------------------------------------------------
-- 存储过程：增、删、改->反馈建议表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_ADVICES_FORUS`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_ADVICES_FORUS`(
     IN `pAdviceContent` TEXT ,
     IN `pAdviceUserID` VARCHAR(32),
     IN `pAdviceType` SMALLINT UNSIGNED,
     IN `pAdviceTime` DATETIME,
		 IN `pop` INT)
BEGIN
	IF(pop=0) THEN
		IF(`pAdviceUserID` IS NOT NULL) THEN
		 DELETE FROM `ER_ADVICES` WHERE ADVICE_USER_IDX=`pAdviceUserID`;
		END IF;
	ELSE 
		IF(`pAdviceUserID` IS NOT NULL) THEN
		 IF(pop=1) THEN
		 UPDATE `ER_ADVICES` SET 
			ADVICE_CONTENT = `pAdviceContent`,
			ADVICE_TYPE = `pAdviceType`,
			ADVICE_DATETIME = `pAdviceTime`
		WHERE ADVICE_USER_IDX = `pAdviceUserID`;
		ELSE
		 INSERT INTO `ER_ADVICES`(`ADVICE_CONTENT`,`ADVICE_USER_IDX`,`ADVICE_TYPE`,`ADVICE_DATETIME`) VALUE
			(`pAdviceContent`,`pAdviceUserID`,`pAdviceType`,`pAdviceTime`);
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：增、删、改->餐厅菜谱表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IUD_RESTAURANT_COOK`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE PROCEDURE `EasyRestaurant`.`IUD_RESTAURANT_COOK`(
     IN `pResID` VARCHAR(32),
		 IN `pCookCode` VARCHAR(10),
     IN `pEvalutionRate` SMALLINT ,
     IN `pBookNumber` INT,
     IN `pReferPrice` FLOAT,
     IN `pPriceBy` NVARCHAR(32),
     IN `pPriceTime` DATETIME,
		 IN `pop` INT)
BEGIN
   DECLARE `@deleteSql` VARCHAR(2000);
   DECLARE `@updateSql` VARCHAR(2000); 
   DECLARE `@insertSql` VARCHAR(2000); 
   DECLARE `@TeamName` VARCHAR(32);
   
   SET @rate= `pEvalutionRate`;
   SET @num= `pBookNumber`;
   SET @pri= `pReferPrice`;
   SET @pby= `pPriceBy`;
   SET @ptime= `pPriceTime`;
   SET @ccode=`pCookCode`;
   SET `@TeamName` = (SELECT RESTAURANT_TABLENAME FROM `ER_RESTAURANT_TABLENAMES` WHERE RESTAURANT_IDX = `pResID`);
   
	IF(pop=0) THEN
		IF(`pCookCode` IS NOT NULL) THEN
     SET @deleteSql = CONCAT('DELETE FROM ',`pTeamName`,' WHERE COOK_CODE=?');
     SET @idx = `pCookCode`;
     PREPARE del FROM @deleteSql;
     EXECUTE del USING @idx;
     DEALLOCATE PREPARE del;
		END IF;
	ELSE 
		IF(`pCookCode` IS NOT NULL) THEN
		 IF(pop=1) THEN
     SET @updateSql = CONCAT('
		 UPDATE ',`pTeamName`,' SET 
			EVA_RATE = ?,
			BOOK_NUMBER = ?,
			REFER_PRICE = ?,
			PRICE_UPDATE_BY = ?,
			PRICE_UPDATE_TIME = ?
		WHERE COOK_CODE = ?');
        
      PREPARE upd FROM @updateSql;
      EXECUTE upd USING @rate,@num,@pri,@pby,@ptime,@ccode;
      DEALLOCATE PREPARE upd;
		ELSE
        SET @insertSql = CONCAT('INSERT INTO ',
        `pTeamName`,
        ' (COOK_CODE,EVA_RATE,BOOK_NUMBER,REFER_PRICE,PRICE_UPDATE_BY,PRICE_UPDATE_TIME) VALUE(?,?,?,?,?,?)');
		    PREPARE ins FROM @insertSql;
        EXECUTE ins USING @ccode,@rate,@num,@pri,@pby,@ptime;
        DEALLOCATE PREPARE ins;
		END IF;
	 END IF;
	END IF;
END $$

-- -----------------------------------------------------------
-- 存储过程：获取用户
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_USER`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_USER`(
		IN `pUserID` VARCHAR(32),
		IN `pPassword` VARCHAR(128)
		)
BEGIN
	SELECT A.`SESSION`,A.`ALIAS`,B.USER_ACTIVE_POINTS FROM `ER_USER_BASE` AS A LEFT JOIN `ER_USER_POINTS` AS B ON A.USER_IDX = B.USER_IDX
   WHERE A.USER_IDX = `pUserID` AND A.`PASSWORD` = pPassword;
END$$

-- -----------------------------------------------------------
-- 存储过程：更新用户会话
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`UPD_USER_SESSION`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`UPD_USER_SESSION`(
		IN `pUserID` VARCHAR(32),
    IN `pSession` VARCHAR(240)
		)
BEGIN
	UPDATE `ER_USER_BASE` SET `SESSION` = `pSession` WHERE `USER_IDX` = `pUserID`;
END$$

-- -----------------------------------------------------------
-- 存储过程：用户名是否存在
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IS_USER_EXISTS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`IS_USER_EXISTS`(
		IN `pUserID` VARCHAR(32)
		)
BEGIN
	SELECT CASE EXISTS(SELECT `USER_IDX` FROM `ER_USER_BASE` WHERE USER_IDX = `pUserID`) WHEN 1 THEN 'TRUE' ELSE 'FALSE' END AS `EXISTS`;
END$$

-- -----------------------------------------------------------
-- 存储过程：用户菜谱信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_USER_COOKS_PRIORITY`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_USER_COOKS_PRIORITY`(
		IN `pUserID` VARCHAR(32)
		)
BEGIN
	SELECT `COOK_PRIORITY` FROM `ER_USER_COOKBOOK` WHERE USER_IDX = `pUserID`;
END$$

-- -----------------------------------------------------------
-- 存储过程：验证会话
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IS_USER_SESSION`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`IS_USER_SESSION`(
		IN `pUserID` VARCHAR(32),
		IN `pSession` VARCHAR(240)
		)
BEGIN
	SELECT `USER_IDX` FROM `ER_USER_BASE` WHERE USER_IDX = `pUserID` AND `SESSION`=`pSession`;
END$$

-- -----------------------------------------------------------
-- 存储过程：查找会话
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`IS_SESSION_EXISTS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`IS_SESSION_EXISTS`(
		IN `pSession` VARCHAR(240)
		)
BEGIN
	SELECT `SESSION` FROM `ER_USER_BASE` WHERE `SESSION`=`pSession`;
END$$

-- -----------------------------------------------------------
-- 存储过程：获取指定用户好友列表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_USER_FRIENDS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_USER_FRIENDS`(
		IN `pUserID` VARCHAR(32)
		)
BEGIN
	SELECT `FRIEND_IDX`,`FRIEND_STUTAS` FROM `ER_FRIENDS` WHERE `USER_IDX`=`pUserID`;
END$$

-- -----------------------------------------------------------
-- 存储过程：模糊查询用户
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_VAGUESEARCH_USER`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_VAGUESEARCH_USER`(
		IN `pAlias` VARCHAR(32)
		)
BEGIN
	SELECT `USER_IDX`,`ALIAS` FROM `ER_USER_BASE` WHERE `ALIAS` LIKE CONCAT('%',`pAlias`,'%');
END$$

-- -----------------------------------------------------------
-- 存储过程：模糊查询队伍
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_VAGUESEARCH_TEAM`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_VAGUESEARCH_TEAM`(
		IN `pTeamName` VARCHAR(32)
		)
BEGIN
	SELECT * FROM `ER_TEAM_BASE` A LEFT JOIN `ER_TEAM_ADVANCED` B ON A.`TEAM_IDX`=B.`TEAM_IDX` WHERE A.`TEAM_NAME` LIKE CONCAT('%',`pTeamName`,'%');
END$$

-- -----------------------------------------------------------
-- 存储过程：队伍操作信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_TEAM_OPERATION`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_TEAM_OPERATION`(
		IN `pTeamID` VARCHAR(32),
    IN `pUserID` VARCHAR(32)
		)
BEGIN
    IF(`pTeamID` IS NOT NULL && `pUserID` IS NULL) THEN
        SELECT * FROM `ER_TEAM_OPERATIONS` WHERE `TEAM_IDX` = `pTeamID`;
    ELSE IF(`pTeamID` IS NULL && `pUserID` IS NOT NULL) THEN
        SELECT * FROM `ER_TEAM_OPERATIONS` WHERE `USER_IDX` = `pUserID`;
    ELSE
        SELECT * FROM `ER_TEAM_OPERATIONS` WHERE `USER_IDX` = `pUserID` AND `TEAM_IDX` = `pTeamID`;
    END IF;
    END IF;
END$$

-- -----------------------------------------------------------
-- 存储过程：餐厅评价信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_RESTAURANT_COMMENTS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_RESTAURANT_COMMENTS`(
		IN `pResID` VARCHAR(32),
    IN `pUserID` VARCHAR(32)
		)
BEGIN
    IF(`pResID` IS NOT NULL && `pUserID` IS NULL) THEN
        SELECT * FROM `ER_RESTAURANT_COMMENT` WHERE `RESTAURANT_IDX` = `pResID`;
    ELSE IF(`pResID` IS NULL && `pUserID` IS NOT NULL) THEN
        SELECT * FROM `ER_RESTAURANT_COMMENT` WHERE `COMMENT_USER_IDX` = `pUserID`;
    ELSE
        SELECT * FROM `ER_RESTAURANT_COMMENT` WHERE `COMMENT_USER_IDX` = `pUserID` AND `RESTAURANT_IDX` = `pResID`;
    END IF;
    END IF;
END$$

-- -----------------------------------------------------------
-- 存储过程：菜肴评价信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_COOK_COMMENTS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_COOK_COMMENTS`(
		IN `pCookCode` VARCHAR(32)
		)
BEGIN
    SELECT * FROM `ER_COOK_COMMENT` WHERE `COOK_CODE` = `pCookCode`;
END$$

-- -----------------------------------------------------------
-- 存储过程：获取餐厅信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_RESTAURANT_COOKS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_RESTAURANT_COOKS`(
		IN `pResID` VARCHAR(32)
		)
BEGIN
    SET @tableName = (SELECT `RESTAURANT_TABLENAME` FROM `ER_RESTAURANT_TABLENAMES` WHERE `RESTAURANT_IDX` = `pResID`);
    IF(@tableName IS NOT NULL && @tableName!='') THEN
        SET @selSql = CONCAT('SELECT * FROM ',@tableName);
        PREPARE SEL FROM @selSql;
        EXECUTE SEL;
    END IF;
END$$

-- -----------------------------------------------------------
-- 存储过程：获取菜肴信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_COOKS_DETAIL`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_COOKS_DETAIL`(
		IN `pCookCode` VARCHAR(32)
		)
BEGIN
    IF(`pCookCode`  IS NOT NULL) THEN
        SELECT * FROM `ER_COOK_DETAIL` WHERE `COOK_CODE` = `pCookCode`;
    ELSE 
        SELECT * FROM `ER_COOK_DETAIL` LIMIT 0,200;
    END IF;
END$$

-- -----------------------------------------------------------
-- 存储过程：获取原料信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_RAW_MATERIALS`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_RAW_MATERIALS`(
		IN `pMaterialCode` VARCHAR(32)
		)
BEGIN
    IF(`pMaterialCode`  IS NOT NULL) THEN
        SELECT * FROM `ER_COOK_RAWMATERIAL` WHERE `RAWMATERIAL_CODE` = `pMaterialCode`;
    ELSE
        SELECT * FROM `ER_COOK_RAWMATERIAL`;
    END IF;
END$$

-- -----------------------------------------------------------
-- 存储过程：获取原料类型信息
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`GET_RAW_MATERIALS_TYPES`;

DELIMITER $$
CREATE PROCEDURE `EasyRestaurant`.`GET_RAW_MATERIALS_TYPES`(
		IN `pTypeCode` VARCHAR(32)
		)
BEGIN
    IF(`pTypeCode`  IS NOT NULL) THEN
        SELECT * FROM `ER_COOK_RAWMATERIAL_TYPE` WHERE `TYPE_CODE` = `pTypeCode`;
    ELSE
        SELECT * FROM `ER_COOK_RAWMATERIAL_TYPE`;
    END IF;
END$$
