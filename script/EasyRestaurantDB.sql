SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `EasyRestaurant` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `EasyRestaurant` ;

-- -----------------------------------------------------
-- 存储过程： 创建各基本表
-- -----------------------------------------------------
USE `EasyRestaurant`;
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`CREATE_TABLES`;

DELIMITER $$
USE `EasyRestaurant`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `CREATE_TABLES`()
BEGIN
-- -----------------------------------------------------------
-- 用户基本表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_USER_BASE`(
        `IDX` INT NOT NULL AUTO_INCREMENT,
        `USER_IDX` VARCHAR(32) NOT NULL UNIQUE,
        `PASSWORD` VARCHAR(128) NOT NULL DEFAULT '',
        `SESSION` VARCHAR(240) NULL UNIQUE DEFAULT NULL,
        `ALIAS` NVARCHAR(16) NULL UNIQUE DEFAULT NULL COMMENT '昵称',
        CONSTRAINT PK_IDX PRIMARY KEY(`IDX`),
        CONSTRAINT IDX_USER_IDX UNIQUE INDEX (`USER_IDX`),
		    CONSTRAINT IDX_SESSION UNIQUE INDEX (`SESSION`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 测试用
SET AUTOCOMMIT=0;
INSERT INTO `ER_USER_BASE`(`USER_IDX`,`PASSWORD`,`SESSION`,`ALIAS`) VALUES('admin','','00000000000000000000000000000000','');
COMMIT;
-- -----------------------------------------------------------

-- -----------------------------------------------------------
-- 队伍基本表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_TEAM_BASE`(
        `IDX` INT NOT NULL AUTO_INCREMENT,
        `TEAM_IDX` VARCHAR(32) NOT NULL UNIQUE,
        `TEAM_NAME` NVARCHAR(32) NOT NULL DEFAULT '',
        `TEAM_HOLDER_IDX` VARCHAR(32) NOT NULL COMMENT '创建者标示符',
        `TEAM_MEAL_TIME` DATETIME NOT NULL COMMENT '用餐时间',
        `TEAM_AFFORD` TINYINT UNSIGNED NOT NULL COMMENT '消费方式',
        `TEAM_RESTAURANT_IDX` VARCHAR(32) NOT NULL COMMENT '餐厅标示符',
        `TEAM_MAX_PEOPLE` INT NOT NULL DEFAULT 8 COMMENT '参加最大人数',
        `TEAM_CREATE_TIME` DATETIME NOT NULL COMMENT '队伍创建时间',
        CONSTRAINT PK_IDX PRIMARY KEY(`IDX`),
        CONSTRAINT IDX_TEAMID UNIQUE INDEX(`TEAM_IDX`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 好友列表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_FRIENDS`(
        `USER_IDX` VARCHAR(32) NOT NULL,
        `FRIEND_IDX` VARCHAR(32) NOT NULL,
        `FRIEND_STUTAS` TINYINT UNSIGNED NOT NULL COMMENT '好友状态：申请中、好友、死党',
        `COMMUNICATION_EVENTS` TEXT NULL COMMENT '交流日志：以JSON结构保存',
        CONSTRAINT IDX_USERID UNIQUE INDEX(`USER_IDX`),
        FOREIGN KEY(`USER_IDX`) REFERENCES `ER_USER_BASE`(`USER_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 队伍高级表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_TEAM_ADVANCED`(
        `TEAM_IDX` VARCHAR(32) NOT NULL UNIQUE,
        `TEAM_MAX_PERSONS` INT NOT NULL DEFAULT 8,
        `TEAM_ALIVE_STATUS` TINYINT UNSIGNED NOT NULL DEFAULT 0 COMMENT '队伍状态：关闭，激活',
        CONSTRAINT PK_TEAMID PRIMARY KEY(`TEAM_IDX`),
        CONSTRAINT IDX_TEAMID UNIQUE INDEX(`TEAM_IDX`),
        FOREIGN KEY(`TEAM_IDX`) REFERENCES `ER_TEAM_BASE`(`TEAM_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 队伍操作表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_TEAM_OPERATIONS`(
        `TEAM_IDX` VARCHAR(32) PRIMARY KEY NOT NULL,
        `TEAM_OPERATION_TYPE` TINYINT UNSIGNED NOT NULL DEFAULT 0 COMMENT '操作类型：申请、加入、拒绝',
        `USER_IDX` VARCHAR(32) NOT NULL,
        `TEAM_OPERATION_TIME` DATETIME NOT NULL COMMENT '操作开始时间',
        `TEAM_NOTE` TEXT NULL COMMENT '操作备注信息',
        INDEX(`TEAM_IDX`),
        FOREIGN KEY(`TEAM_IDX`) REFERENCES `ER_TEAM_BASE`(`TEAM_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 用户位置表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_USER_LOCATION`(
        `USER_IDX` VARCHAR(32) PRIMARY KEY NOT NULL UNIQUE,
        `USER_IPADDRESS` VARCHAR(48) NULL DEFAULT '',
        `USER_PORT` INT NULL DEFAULT 35302,
        `USER_NETWORK_TYPE` TINYINT NOT NULL DEFAULT 0 COMMENT '当前使用网络类型：WIFI,2G,3G,4G',
        `USER_LATITUDE` FLOAT NULL,
        `USER_LONGITUDE` FLOAT NULL,
        `USER_CITY_CODE` INT NULL,
        `USER_ONLINE_STATUS` TINYINT NOT NULL DEFAULT 0 COMMENT '用户在线状态：登出、拒绝骚扰、约饭中',
        INDEX(`USER_IDX`),
        FOREIGN KEY(`USER_IDX`) REFERENCES `ER_USER_BASE`(`USER_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 用户积分表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_USER_POINTS`(
        `USER_IDX` VARCHAR(32) PRIMARY KEY NOT NULL UNIQUE,
        `USER_ACTIVE_POINTS` INT NULL DEFAULT 100,
        INDEX(`USER_IDX`),
        FOREIGN KEY(`USER_IDX`) REFERENCES `ER_USER_BASE`(`USER_IDX`) ON DELETE CASCADE
        )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 用户配置表，以JSON结构保存，用以用户更换设备后也能获取配置
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_USER_CONFIG_REMOTE`(
        `USER_IDX` VARCHAR(32) PRIMARY KEY NOT NULL,
        `USER_CONFIG_ALL` TEXT NULL COMMENT '用户配置信息：JSON结构保存',
        INDEX(`USER_IDX`),
        FOREIGN KEY(`USER_IDX`) REFERENCES `ER_USER_BASE`(`USER_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 菜肴类型表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_COOK_BASE_TYPE`(
        `COOK_TYPE` SMALLINT PRIMARY KEY NOT NULL UNIQUE,
        `COOK_LOCALIZATION_NAME` NVARCHAR(48) NOT NULL COMMENT '本地化菜谱名称',
        `COOK_PARENT_TYPE` SMALLINT NOT NULL DEFAULT -1)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SET AUTOCOMMIT=0;
INSERT INTO `ER_COOK_BASE_TYPE`(`COOK_TYPE`,`COOK_LOCALIZATION_NAME`,`COOK_PARENT_TYPE`) VALUES
(0,'中餐',-1),
(1,'西餐',-1),
(2,'小吃',-1),
(3,'酒',-1),
(4,'饮料',-1),
(5,'烧烤',-1),
(6,'火锅',-1),
(7,'干锅',-1),
(8,'炒菜',0),
(9,'炖菜',0),
(10,'中餐 - 汤',0),
(11,'烧菜',0),
(12,'炖菜',0),
(14,'面食',-1),
(15,'美式',1),
(16,'法式',1),
(17,'意式',1),
(18,'日式',1),
(13,'泰式',1);
COMMIT;

-- -----------------------------------------------------------
-- 菜名表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_COOK_DETAIL`(
        `IDX` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        `COOK_TOPPARENT_TYPE` SMALLINT NOT NULL,
        `COOK_SUBPARENT_TYPE` SMALLINT NOT NULL,
        `COOK_CODE` VARCHAR(10) NOT NULL,
        `COOK_LOCALIZATION_NAME` NVARCHAR(48) NOT NULL COMMENT '本地化菜谱名称',
        `COOK_LOCALIZATION_DESCRIPTION` TEXT NULL COMMENT '本地化菜谱描述',
        `COOK_MAJOR_MATERIALS` VARCHAR(256) NULL COMMENT '主要原料',
        `COOK_ATTRIBUTES` TEXT NULL COMMENT '菜肴属性：辣度，麻度，烫度；JSON结构保存',
        CONSTRAINT IDX_COOKCODE UNIQUE INDEX(`COOK_CODE`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 原料类型表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_COOK_RAWMATERIAL_TYPE`(
        `TYPE_CODE` SMALLINT UNSIGNED PRIMARY KEY NOT NULL UNIQUE,
        `TYPE_LOCALIZATION_NAME` NVARCHAR(48) NOT NULL COMMENT '本地化原料类型名称',
        `COOK_PARENT_TYPE` SMALLINT NOT NULL DEFAULT -1)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SET AUTOCOMMIT=0;
INSERT INTO `ER_COOK_RAWMATERIAL_TYPE`(`TYPE_CODE`,`TYPE_LOCALIZATION_NAME`,`COOK_PARENT_TYPE`) VALUES
(0,'肉类',-1),
(1,'蔬菜',-1),
(2,'配料',-1),
(3,'主食',-1),
(4,'水产',-1),
(5,'海鲜',4),
(6,'河鲜',4),
(7,'禽类',0),
(8,'畜类',0),
(9,'鱼类',0),
(10,'其他肉类',0),
(11,'面食',3),
(12,'米饭',0),
(14,'水果',-1),
(15,'香料',2),
(16,'辅料',2);
COMMIT;

-- -----------------------------------------------------------
-- 主要原料表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_COOK_RAWMATERIAL`(
        `RAWMATERIAL_CODE` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        `MAJOR_TYPE_CODE` VARCHAR(20) NOT NULL COMMENT '可能原料属于多个类别：肉类->鱼类&水产->海鲜',
        `RAWMATERIAL_LOCALIZATION_NAME` NVARCHAR(24) NOT NULL,
        `RAWMATERIAL_LOCALIZATION_DESCRIPTION` TEXT NULL COMMENT '本地化原料描述')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 菜肴评论表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_COOK_COMMENT`(
        `COOK_CODE` VARCHAR(10) PRIMARY KEY NOT NULL,
        `RESTAURANT_IDX` VARCHAR(32) NULL,
        `COMMENT_CONTENT` TEXT NOT NULL,
        `COMMENT_USER_IDX` VARCHAR(32) NOT NULL,
        `COMMENT_DATETIME` DATETIME NOT NULL,
        `EVA_RATE` SMALLINT NULL DEFAULT 0 COMMENT '总体评分，当指定餐厅时以餐厅该菜肴评分为准',
        `COMMENT_USEFUL` INT NULL DEFAULT 0,
        `COMMENT_UNUSEFUL` INT NULL DEFAULT 0,
        INDEX(`COOK_CODE`),
        FOREIGN KEY(`COOK_CODE`) REFERENCES `ER_COOK_DETAIL`(`COOK_CODE`) ON DELETE CASCADE
        )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 用户菜名表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_USER_COOKBOOK`(
        `USER_IDX` VARCHAR(32) PRIMARY KEY NOT NULL,
        `COOK_PRIORITY` TEXT NULL COMMENT '用户菜肴权重信息：权重；JSON结构保存',
        INDEX(`USER_IDX`),
        FOREIGN KEY(`USER_IDX`) REFERENCES `ER_USER_BASE`(`USER_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 餐厅基本表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_RESTAURANT_BASE`(
        `RESTAURANT_IDX` VARCHAR(32) NOT NULL PRIMARY KEY,
        `RESTAURANT_NAME` NVARCHAR(48) NOT NULL,
        `RESTAURANT_DESCRIPTION` TEXT NULL,
        `RESTAURANT_CITY_CODE` INT NULL,
        `RESTAURANT_COUNTRY_CODE` INT NULL,
        `RESTAURANT_LANTITUDE` FLOAT NULL,
        `RESTAURANT_LONGITUDE` FLOAT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 餐厅评价表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_RESTAURANT_COMMENT`(
        `RESTAURANT_IDX` VARCHAR(32) NOT NULL PRIMARY KEY,
        `COMMENT_CONTENT` TEXT(140) NOT NULL,
        `COMMENT_USER_IDX` VARCHAR(32) NOT NULL,
        `COMMENT_TIME` DATETIME NOT NULL,
        `COMMENT_USEFUL` INT NULL DEFAULT 0,
        `COMMENT_UNUSEFUL` INT NULL DEFAULT 0,
        INDEX(`RESTAURANT_IDX`),
        FOREIGN KEY(`RESTAURANT_IDX`) REFERENCES `ER_RESTAURANT_BASE`(`RESTAURANT_IDX`) ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 餐厅表名表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_RESTAURANT_TABLENAMES`(
        `RESTAURANT_IDX` VARCHAR(32) NOT NULL PRIMARY KEY,
        `RESTAURANT_TABLENAME` VARCHAR(60),
        INDEX(`RESTAURANT_IDX`),
        FOREIGN KEY(`RESTAURANT_IDX`) REFERENCES `ER_RESTAURANT_BASE`(`RESTAURANT_IDX`) ON DELETE CASCADE
        )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- -----------------------------------------------------------
-- 反馈建议表
-- -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ER_ADVICES`(
        `IDX` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        `ADVICE_CONTENT` TEXT NOT NULL,
        `ADVICE_USER_IDX` VARCHAR(32),
        `ADVICE_TYPE` SMALLINT UNSIGNED NOT NULL DEFAULT 0 COMMENT '建议类型：程序问题 - 0、界面改进 - 1、功能改进 - 2、功能增加 - 3',
        `ADVICE_DATETIME` DATETIME NOT NULL
        )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;
END$$

-- -----------------------------------------------------------
-- 创建表存储过程结束
-- -----------------------------------------------------------

-- -----------------------------------------------------------
-- 调用存储过程创建表
-- -----------------------------------------------------------
DELIMITER ;
USE `EasyRestaurant`;
CALL `EasyRestaurant`.`CREATE_TABLES`();

-- -----------------------------------------------------------
-- 存储过程：动态创建餐厅菜谱表，插入餐厅表名表：表名与餐厅ID
-- -----------------------------------------------------------
DELIMITER $$
USE `EasyRestaurant`$$
DROP PROCEDURE IF EXISTS `EasyRestaurant`.`CREATE_RESTAURANT_TABLE`$$

CREATE PROCEDURE `EasyRestaurant`.`CREATE_RESTAURANT_TABLE`(IN tableName VARCHAR(24),IN tableId VARCHAR(32))
BEGIN
    DECLARE `@create_sql` NVARCHAR(480);
    DECLARE final_tableName NVARCHAR(34);
    DECLARE isTableExists BOOL;
    SET final_tableName=CONCAT('ER_RES_',tableName);
    SET isTableExists=(SELECT ISTABLEEXISTS(final_tableName));
    IF(!isTableExists) THEN
    SET @create_sql=CONCAT('CREATE TABLE ',final_tableName,'(
        `COOK_CODE` VARCHAR(10) PRIMARY KEY NOT NULL UNIQUE,
        `EVA_RATE` SMALLINT NULL DEFAULT 0 COMMENT \'评分\',
        `BOOK_NUMBER` INT NULL DEFAULT 0 COMMENT \'多少人点过\',
        `REFER_PRICE` FLOAT NULL COMMENT \'参考价格\',
        `PRICE_UPDATE_BY` VARCHAR(32) NULL COMMENT \'价格更新者\',
        `PRICE_UPDATE_TIME` DATETIME NULL COMMENT \'价格更新时间\'
    ) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;
');
    PREPARE STMT FROM @create_sql;
    EXECUTE STMT;
        SET isTableExists= (SELECT ISTABLEEXISTS('ER_RESTAURANT_TABLENAMES')) && (SELECT ISTABLEEXISTS(final_tableName));
            IF(isTableExists) THEN
                INSERT INTO `ER_RESTAURANT_TABLENAMES`(`RESTAURANT_IDX`,`RESTAURANT_TABLENAME`) VALUE(tableId,final_tableName);
            END IF;
    END IF;
END$$

-- -----------------------------------------------------------
-- 函数：检测是否存在指定表
-- -----------------------------------------------------------
DELIMITER $$
USE `EasyRestaurant`$$
DROP FUNCTION IF EXISTS `ISTABLEEXISTS` $$

CREATE FUNCTION `EasyRestaurant`.`ISTABLEEXISTS`(tableName VARCHAR(60))
RETURNS BOOL
BEGIN
    DECLARE ret_Exists BOOL DEFAULT FALSE;
    IF EXISTS 
    (SELECT `TABLE_NAME` 
        FROM `INFORMATION_SCHEMA`.`TABLES` 
        WHERE `TABLE_SCHEMA`='EasyRestaurant' AND `TABLE_NAME`=tableName
    )
    THEN
    SET ret_Exists=TRUE;
    END IF;
    RETURN (ret_Exists);
END$$ 