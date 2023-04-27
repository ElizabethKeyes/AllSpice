CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT "Primary Key",
  title VARCHAR(50) NOT NULL,
  instructions VARCHAR(500) NOT NULL,
  img VARCHAR(255) NOT NULL,
  category VARCHAR(255) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT "Primary Key",
  name VARCHAR(50),
  quantity VARCHAR(50),
  recipeId INT NOT NULL,

  FOREIGN KEY (recipeId) REFERENCES recipes (id) ON DELETE CASCADE
)default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS favorites(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT "Primary Key",
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,

  FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes (id) ON DELETE CASCADE
)default charset utf8mb4 COMMENT '';

