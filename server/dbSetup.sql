CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

-- utf8mb4 allows for emojis

CREATE TABLE albums(
  id INT AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(100) NOT NULL,
  coverImg VARCHAR(500) NOT NULL,
  category ENUM('animals', 'aesthetics', 'pugs', 'games', 'misc', 'food'),
  archived BOOLEAN NOT NULL DEFAULT false,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

INSERT INTO albums
(title, coverImg, category, creatorId)
VALUES
-- ("Handsome Pugs", "https://plus.unsplash.com/premium_photo-1676479610593-1545d09ab88a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8cHVnc3xlbnwwfHwwfHx8MA%3D%3D", "pugs", "645d75fdfdcb015333f9b0ba");
-- ("Solid Doors", "https://images.unsplash.com/photo-1516134394958-6b824e9566f5?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8amFycyUyMGxpZHxlbnwwfHwwfHx8MA%3D%3D", "misc", "645d75fdfdcb015333f9b0ba");
("Sleepy Time", "https://images.unsplash.com/photo-1541781774459-bb2af2f05b55?q=80&w=2060&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "aesthetics", "634844a08c9d1ba02348913d");

DELETE FROM albums;
DELETE FROM accounts;


SELECT
*
FROM albums
WHERE `creatorId` = "645d75fdfdcb015333f9b0ba";


SELECT
 albums.*,
 accounts.*
FROM albums
JOIN accounts ON albums.creatorId = accounts.id
JOIN frogs
WHERE accounts.name = "Gio";
SELECT
 albums.*,
 accounts.*
FROM albums
JOIN accounts ON albums.creatorId = accounts.id;