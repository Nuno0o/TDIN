DROP TABLE IF EXISTS User;
CREATE TABLE User (
    username TEXT PRIMARY KEY,
    password TEXT,
    currency INTEGER
);

DROP TABLE IF EXISTS Diginote;
CREATE TABLE Diginote (
    id INTEGER PRIMARY KEY,
    owner TEXT,
    FOREIGN KEY (owner) REFERENCES User(username)
);