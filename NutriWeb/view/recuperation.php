<?php
	require_once("../modele/model.php");
	require_once("../modele/utilisateurs.php");
	session_start();
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	if (isset($_POST['Valider']))
	{
		$email = getEmail($connexion,$_POST['email']);
		$mail = $email->fetch();
		if ($mail['email'] == $_POST['email'])
		{
			$_SESSION['mail'] = $_POST['email'];
			$id = md5($mail['pseudo']);
			$_SESSION['pseudo'] = $mail['pseudo'];
			$from = "Nutriwebbos <support@nutriwebbos.fr>";
			$message = "Suite à votre demande ".$mail['pseudo']." pour obtenir un nouveau mot de passe voici un lien pour réinitialiser votre mot de passe cliquez ici : \r\n \r\nhttps://www.nutriwebbos.fr/view/recuperation.php?id=".$id.".
			\r\nAttention si vous n'êtes pas l'auteur de la demande prenez vite contact avec le support du site à l'adresse indiqué en bas de page.\r\n \r\nMerci de votre confiance, nous espérons vous revoir sous peu !";
			$message = wordwrap($message, 150, "\r\n");
			$headers .= 
			'From: '.$from."\r\n".
			'Reply-To: '.$from."\r\n" .
			'X-Mailer: PHP/' . phpversion();
			if(mail($_POST['email'],"Nutriwebbos - Nouveau mot de passe",$message,$headers))
			{
				echo'Le mail a bien été transmis pense bien à regarder tes mails (et peut être tes courriers indésirables).';
			}
		}
		else
		{
			echo '<div class="errors">L\'adresse mail rentrée n\'est pas celle que tu as enregistrée, nous ne pouvons donc savoir si tu es un Nutriwebbos.</div>';
		} 
		
	}
	
	if(isset($_POST['Confirmer']))
	{
		if ($_SESSION['pseudo'] == $_POST['verif'])
		{
			$from = "Nutriwebbos <support@nutriwebbos.fr>";
			$message = " ".$_POST['verif']." votre mot de passe a bien été modifié." ;
			$headers .= 
			'From: '.$from."\r\n".
			'Reply-To: '.$from."\r\n" .
			'X-Mailer: PHP/' . phpversion();
			$hashpsswd = md5($_POST['mdp']);
			ChangePasswd($connexion,$_POST['verif'],$hashpsswd);
			echo ' '.$_POST['verif'].' ton mot de passe a bien été changé tu peux te connecter <a class="ici" href="connexion.php" title="Connexion"> ici </a>.';
			mail($_SESSION['mail'],"Nutriwebbos - Confirmation du changement de mot de passe",$message,$headers);
			session_destroy();
		}
		else
		{
			echo '<div class="errors">Ce pseudo n\'est pas valide !</div>';
		}
	}
?>
<!doctype html>

<html lang="fr">
	<head>
		<link rel="stylesheet" href="../public/css/Nutrition.css"/>
		<meta name = "robots" content = "none" >   
		<title> Récupération - Nutriwebbos </title>
		<link rel="icon" type="image/png" href="../public/images/logo.png" />
	</head>
		<body>
			<header class = "logo_header">
				<a class="Liens_menu" href="../index.php" title="Acceuil"> <img class="User_logo" src="../public/images/logo.png" alt="Icone d\'accueil"> </a>
			</header>
			<h2> Récupération mot de passe</h2>
			<?php
				if (!isset($_GET['id']))
				{
					echo'
						<div class="email_box">
							<form method="post">
								<input class="mobil" type="email" name="email" placeholder="Entrez votre email" required>
								<input class="email_sub" type="submit" name="Valider" value="Valider">
							</form>
						</div>';
				}
				else
				{
					echo '
						<div class="recup_box">
							<div class="recup_items">
								<form method="post">
									<input class="mobil" type"text" name="verif" placeholder="Pseudo" required>
									<input type="password" name="mdp" id="mdp" placeholder="Mot de Passe" required><br /> 
									<input type="password" name="cmdp" id="cmdp" placeholder="Confirmer mot de passe" required><br /> 
									<input class="email_sub" type="submit" name="Confirmer" value="Confimer">
								</form>	
							</div>
						</div>';
				}
			?>
		</body>
</html>
