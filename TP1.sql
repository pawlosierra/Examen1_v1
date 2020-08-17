CREATE DATABASE Examen1;
GO

USE Examen1;
GO

CREATE TABLE Client(
    ClientId        INT          NOT NULL,
    Nom 	    VARCHAR (50) NOT NULL,
    PRIMARY KEY (ClientId)
);


CREATE TABLE Commandes(
    ComID        INT          	NOT NULL,
    Description  VARCHAR (100) 	NOT NULL,
    Prix         DECIMAL(10,2)  NOT NULL,
    ClientId     INT            NOT NULL,
    PRIMARY KEY  (ComID),
    FOREIGN KEY (ClientId) 
    REFERENCES Client(ClientId)
);


INSERT INTO Client(
	ClientId,
	Nom
)
VALUES(1,'Pierre'),
	(2,'Marie'),
	(3,'Anne'),
	(4,'Jacques')

INSERT INTO Commandes(
	ComID,
	Description,
	Prix,
	ClientId
)
VALUES(1,'clavier',30.00,3),
	(2,'souris',15.00,3),
	(3,'imprimant',250.00,1)






