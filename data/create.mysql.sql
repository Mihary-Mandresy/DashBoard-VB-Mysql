-- Active: 1732102402366@@127.0.0.1@3306@dashboard
-- CREATE DATABASE dashboard;
-- USE dashboard

CREATE TABLE voiture(
   id_voiture INT AUTO_INCREMENT,
   matricule VARCHAR(50)  NOT NULL,
   acceleration DECIMAL(15,2)   NOT NULL,
   freinage DECIMAL(15,2)   NOT NULL,
   reservoire INT NOT NULL,
   consommation DECIMAL(15,2)   NOT NULL,
   max_tableau DECIMAL(15,2)   NOT NULL,
   PRIMARY KEY(id_voiture)
);

CREATE TABLE mouvement_voiture(
   id_mouvement INT AUTO_INCREMENT,
   vitesse_initial DECIMAL(15,2)   NOT NULL,
   acceleration DECIMAL(15,2)   NOT NULL,
   rapport INT NOT NULL,
   duration DECIMAL(15,2)   NOT NULL,
   daty DATETIME NOT NULL,
   id_voiture INT NOT NULL,
   PRIMARY KEY(id_mouvement),
   FOREIGN KEY(id_voiture) REFERENCES voiture(id_voiture)
);
