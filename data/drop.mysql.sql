-- Active: 1732102402366@@127.0.0.1@3306@dashboard
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE mouvement_voiture;

DROP TABLE voiture;

SET FOREIGN_KEY_CHECKS = 1;

SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE mouvement_voiture;

ALTER TABLE mouvement_voiture AUTO_INCREMENT = 0;

TRUNCATE TABLE voiture;
ALTER TABLE voiture AUTO_INCREMENT = 0;

SET FOREIGN_KEY_CHECKS = 1;

SELECT
    id_mouvement,
    vitesse_initial,
    acceleration,
    rapport,
    daty,
    id_voiture
FROM mouvement_voiture;