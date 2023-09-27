CREATE TABLE IF NOT EXISTS [Cards] (
    [rowId]        INTEGER      NOT NULL PRIMARY KEY,
    [id]           VARCHAR(64)  NOT NULL DEFAULT '',
    [name]         VARCHAR(128) NOT NULL DEFAULT '',
    [supertype]    VARCHAR(128) NOT NULL DEFAULT '',
    [setId]        INTEGER      NOT NULL DEFAULT 0,
    [number]       INTEGER      NOT NULL DEFAULT 0,
    [rarity]       VARCHAR(64)  NOT NULL DEFAULT '',
    [imgSmall]     VARCHAR(200) NOT NULL DEFAULT '',
    [imgLarge]     VARCHAR(200) NOT NULL DEFAULT '',
    [tcgUrl]       VARCHAR(200) NOT NULL DEFAULT '',
    [tcgUrlReal]   VARCHAR(200) NOT NULL DEFAULT '',
    [cmUrl]        VARCHAR(200) NOT NULL DEFAULT '',
    [apiJson]      JSON         NOT NULL DEFAULT ''
);

CREATE UNIQUE INDEX IF NOT EXISTS CardId ON Cards(id);
CREATE INDEX IF NOT EXISTS CardName ON Cards(name);


CREATE TABLE IF NOT EXISTS [Sets] (
    [rowId]        INTEGER      NOT NULL PRIMARY KEY,
    [id]           VARCHAR(64)  NOT NULL DEFAULT '',
    [name]         VARCHAR(128) NOT NULL DEFAULT '',
    [series]       VARCHAR(128) NOT NULL DEFAULT '',
    [printedTotal] INTEGER      NOT NULL DEFAULT 0,
    [Total]        INTEGER      NOT NULL DEFAULT 0,
    [releaseDate]  DATE         NOT NULL DEFAULT '0000-00-00',
    [imgSymbol]    VARCHAR(200) NOT NULL DEFAULT '',
    [imgLogo]      VARCHAR(200) NOT NULL DEFAULT ''
);

CREATE UNIQUE INDEX IF NOT EXISTS SetId ON Sets(id);
CREATE INDEX IF NOT EXISTS SetName ON Sets(name);


CREATE TABLE IF NOT EXISTS [Subtypes] (
    [id]           INTEGER      NOT NULL PRIMARY KEY,
    [name]         VARCHAR(64)  NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS [SubtypeMap] (
    [subtypeId]    INTEGER      NOT NULL DEFAULT 0,
    [cardId]       INTEGER      NOT NULL DEFAULT 0
);

CREATE INDEX IF NOT EXISTS MapTypeId ON SubtypeMap(subtypeId);
CREATE INDEX IF NOT EXISTS MapCardId ON SubtypeMap(cardId);

CREATE TABLE IF NOT EXISTS [Rarities] (
    [id]           INTEGER      NOT NULL PRIMARY KEY,
    [name]         VARCHAR(64)  NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS [Prices] (
    [id]           INTEGER       NOT NULL PRIMARY KEY,
    [cardId]       INTEGER       NOT NULL DEFAULT 0,
    [low]          DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    [mid]          DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    [high]         DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    [market]       DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    [directLow]    DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    [date]         DATE          NOT NULL DEFAULT '0000-00-00'
);

CREATE INDEX IF NOT EXISTS PriceCardId ON Prices(cardId);

CREATE TABLE IF NOT EXISTS [Folders] (
  [id]           INTEGER       NOT NULL PRIMARY KEY,
  [parentId]     INTEGER       NOT NULL DEFAULT 0,
  [sortIndex]    INTEGER       NOT NULL DEFAULT 0,
  [name]         VARCHAR(64)   NOT NULL DEFAULT '',
  [folderType]   VARCHAR(64)   NOT NULL DEFAULT '',
  [icon]         VARCHAR(128)  NOT NULL DEFAULT ''
);

CREATE INDEX IF NOT EXISTS FolderSortIndex ON Folders(sortIndex);

CREATE TABLE IF NOT EXISTS [FolderMap] (
    [cardId]     INTEGER       NOT NULL DEFAULT 0,
    [folderId]   INTEGER       NOT NULL DEFAULT 0,
    [cost]       DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    [date]       DATE          NOT NULL DEFAULT '0000-00-00',
    [quantity]   INTEGER       NOT NULL DEFAULT 0,
    [options]    JSON          NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS [Settings] (
    [name]       VARCHAR(64)   NOT NULL PRIMARY KEY,
    [value]      VARCHAR(200)  NOT NULL DEFAULT ''
);

CREATE INDEX IF NOT EXISTS MapFolderId ON FolderMap(folderId);
CREATE INDEX IF NOT EXISTS MapCardId   ON FolderMap(cardId);
