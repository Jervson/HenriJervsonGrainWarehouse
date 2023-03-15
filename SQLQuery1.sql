select * from Cargo

set identity_insert Cargo On
INSERT INTO Cargo (id, CarNumber, EnteringMass, LeavingMass)
VALUES (1,'abc123', 150, 140);

select * from Cargo