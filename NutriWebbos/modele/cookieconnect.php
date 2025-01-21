<?php
session_start();
require_once("../modele/model.php");
require_once("../modele/utilisateurs.php");
require_once("../modele/donnee.php");
$date = date('Y-m-d');

if (!isset($_SESSION['pseudo']) AND isset($_COOKIE['log'],$_COOKIE['psswd']) AND !empty($_COOKIE['log']) AND !empty($_COOKIE['psswd']))
{
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	$gu = getUser($_COOKIE['log'],$_COOKIE['psswd'],$connexion,$_COOKIE['log']);
	$g = $gu->fetch();
	$_SESSION['logged'] = true;
	$_SESSION['pseudo'] = $g['pseudo'];
	Reinitialiser($connexion,$date);
	header('Location:../index.php');
}
