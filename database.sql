CREATE DATABASE IF NOT EXISTS gamestation;
USE gamestation;

CREATE TABLE IF NOT EXISTS Usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
);

INSERT INTO Usuarios (nombre_usuario, password)
VALUES
('admin', 'Admin123'),
('usuario1', 'Usuario123');