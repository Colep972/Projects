<?php
include_once("../modele/cookieconnect.php");
if (isset($_POST['Connexion']))
{
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	$mdp_hache = md5($_POST['mdp']);
	$gu = getUser($_POST['pseudo'],$mdp_hache,$connexion,$_POST['pseudo']);
	$g = $gu->fetch();
	if (($g['pseudo'] == $_POST['pseudo'] || $g['email'] == $_POST['pseudo']) && $g['mdp'] == $mdp_hache)
	{
		$_SESSION['logged'] = true;
		$_SESSION['pseudo'] = $g['pseudo'];
		Reinitialiser($connexion,$date);
		header('Location:../index.php');
		if(isset($_POST['Remember']))
		{
			setcookie('log',$_POST['pseudo'],time()+3600*24*365,null,null,false,true);
			setcookie('psswd',$mdp_hache,time()+3600*24*365,null,null,false,true);
		}
	}
	else
	{
		echo '<div class="errors"> Utilisateur inexistant </div>';
	}
	
}
?>
<!doctype html>
<html lang="fr">
	<head>
	  <meta charset="utf-8">
	  <title>Connexion - Nutriwebbos </title>
	  <meta name = "robots" content = "all" >   
	  <meta name="description" content="Nutriwebbos est une page simple et intuitive pour se lancer dans la prise en main de son corps et de sa nutrition." >
	  <meta name="keywords" content="Nutrition, Métabolisme, besoin nutritionnel, nutriweb, aliment, alimentation, Nutriwebbos, macronutriments" >
	  <link rel="stylesheet" href="../public/css/Nutrition.css">
	  <link rel="icon" type="image/png" href="../public/images/logo.png" /> 
	</head>

	<body>
		<header>
			<a class="Liens_menu" href="../index.php" title="Acceuil"> <img class="User_logo" src="../public/images/logo.png" alt="Icone d'accueil"> </a>
		</header>
		<div class="User_box">
			<h1 class="User_title"> Authentification Nutriwebbos </h1>
			<div class="User_container">
				<form method="post">
					<div class="User_item">
						<input class="mobil" type="text" name="pseudo" id="pseudo" value="<?php if (isset($_POST['Connexion'])){echo$_POST['pseudo'];}?>" placeholder="Pseudo ou mail" required><br /> 
						<input class="mobil" type="password" name="mdp" id="mdp" required placeholder="Mot de Passe"><br />
						<input type="checkbox" name="Remember" id="Rmbme">
						<label for="Rmbme"> Se souvenir de moi </label><br />
						<input class="User_submit" type="submit" name="Connexion" value="Connexion">
						<h3 class="hr"> Ou </h3>
						<a class="connexion" href="recuperation.php" title="Récupération"> Mot de passe oublié ? </a><br />
						<a class="connexion" href="inscription.php" title="Connexion"> S'inscrire </a>
					</div>
				</form>
				
			</div>
		</div>
		<hr>
		<!-- Pied de Page -->
		<?php include("footer.php"); ?>
	</body>
</html>
