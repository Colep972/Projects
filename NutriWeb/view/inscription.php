<?php
	require_once("../modele/model.php");
	require_once("../modele/utilisateurs.php");
	error_reporting(E_ALL);
	if (isset($_POST['Inscription']))
	{
		$mdp_hache = md5($_POST['mdp']);
		$cmdp_hache = md5($_POST['cmdp']);
		$pseudo = $_POST['pseudo'];
		$connexion = getConnection($dbHost,$dbUser,$dbPwd,$dbName);
		$bon = checkAvailability($pseudo,$connexion,$_POST['mail']);
		$b = $bon->fetch();
		if ($_POST['mdp'] == $_POST['cmdp'])
		{
			if ($b['pseudo'] != $pseudo)
			{
				if ($b['email'] != $_POST['mail'])
				{
					registerUser($pseudo,$mdp_hache,$connexion,$_POST['mail']);
					header('Location: connexion.php');
					exit();
				}
				else
				{
					echo'<div class="errors">Adresse mail déjà enregistrée, veuillez en saisir une autre</div>';
				}
			}
			else 
			{
				echo'<div class="errors">Pseudo déjà existant, veuillez en saisir un autre</div>';
			}
		}
		else 
		{
			echo'<div class="errors">Les mots de passes ne sont pas identiques</div>';
		}
	}
?>
<!doctype html>
<html lang="fr">
<head>
  <meta charset="utf-8">
  <title>Inscription - Nutriwebbos </title>
  <link rel="stylesheet" href="../public/css/Nutrition.css">
  <meta name = "robots" content = "all" >   
  <meta name="description" content="Nutriwebbos est une page simple et intuitive pour se lancer dans la prise en main de son corps et de sa nutrition." >
  <meta name="keywords" content="Nutrition, Métabolisme, besoin nutritionnel, nutriweb, aliment, alimentation, Nutriwebbos, macronutriments" >
  <link rel="icon" type="image/png" href="../public/images/logo.png" /> 
</head>
<body>
	<header>
		<a class="Liens_menu" href="../index.php" title="Acceuil"> <img class="User_logo" src="../public/images/logo.png" alt="Icone d'accueil"> </a>
	</header>
	<div class="User_box">
		<h1 class="User_title"> Inscription Nutriwebbos </h1>
		<div class="User_container">
			<form method="post">
				<div class="User_item">
					<input class="mobil" type="text" name="pseudo" value="<?php if (isset($_POST['Inscription'])){echo$_POST['pseudo'];}?>" id="pseudo" placeholder="Pseudo souhaité" required><br /> 
					<input class="mobil" type="email" id="mail" name="mail" value="<?php if (isset($_POST['Inscription'])){echo$_POST['mail'];}?>" placeholder="Adresse mail"required><br /> 
					<input type="password" name="mdp" id="mdp" placeholder="Mot de Passe" required><br /> 
					<input type="password" name="cmdp" id="cmdp" placeholder="Confirmer mot de passe" required><br /> 
					<a class="connexion" href="connexion.php" title="Inscription"> Déjà inscrit ? </a> <br />
					<input class="User_submit" type="submit" name="Inscription" value="Inscription">
				</div>
			</form>
		</div>
	</div>
	<hr>
		<!-- Pied de Page -->
		<?php include("footer.php"); ?>
</body>
</html>
