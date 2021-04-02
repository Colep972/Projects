/*CREATE DATABASE infos CHARACTER SET 'utf8';*/
CREATE TABLE IF NOT EXISTS membres (
	id_m SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nom varchar(50) NOT NULL,
    prenom varchar(50) NOT NULL,
    mdp varchar(25) NOT NULL,
    mail varchar(50) NOT NULL,
    inscription date NOT NULL,
    naissance date NOT NULL,
    kcal INT UNSIGNED NOT NULL,
    glucides INT UNSIGNED NOT NULL,
    lipides INT UNSIGNED NOT NULL,
    proteines INT UNSIGNED NOT NULL,
    PRIMARY KEY (id_m) 
)
ENGINE=INNODB;


CREATE TABLE IF NOT EXISTS aliments (
	id_al SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nom varchar(70) NOT NULL,
    kcal FLOAT UNSIGNED NOT NULL,
    glucides FLOAT UNSIGNED NOT NULL,
    lipides FLOAT UNSIGNED NOT NULL,
    proteines FLOAT UNSIGNED NOT NULL,
    gramme FLOAT unsigned NOT NULL,
    PRIMARY KEY (id_al) 
)
ENGINE=INNODB;


