<?php
	session_start();
	require_once("../modele/model.php");
	require_once("../modele/utilisateurs.php");
	$connexion = getConnection($dbHost,$dbUser,$dbPwd,$dbName); 
	if(isset($_POST['Valider']))
	{
		AddEmail($connexion,$_SESSION['pseudo'],$_POST['mail']);
		header('Location: User.php');
		exit();
	}
?>
<!doctype html>
<html lang="fr">
	<head>
		<meta charset="utf-8">
		<meta name = "robots" content = "none" >  
		<link rel="stylesheet" href="../public/css/Nutrition.css"/>
		<link rel="icon" type="image/png" href="../public/images/logo.png" />
	</head>
	<body>
		<header>
			<a class="Liens_menu" href="../index.php" title="Acceuil"> <img class="User_logo" src="../public/images/logo.png" alt="Icone d'accueil"> </a>
		</header>
		<div class="email_box">
			<form method="post">
				<input class="mobil" type="email" name="mail" placeholder="Entrez votre Email" required>
				<input class="email_sub" type="submit" name="Valider" value="Valider">
			</form>
		</div>
	</body>
</html>
