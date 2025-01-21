<?php 
	include_once("../modele/cookieconnect.php");
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	$admin= EstAdmin($connexion,$_SESSION['pseudo']);
	$email = HasEmail($connexion,$_SESSION['pseudo']);
	$mail = $email->fetch();
	$a = $admin->fetch();
	$membres = aff_membres ($connexion,$_SESSION['pseudo']);
	$id = getId($connexion,$_SESSION['pseudo']);
	$i = $id->fetch();
	$reponse = aff_journal($connexion,$i['id_m']);
	$kcal = Somme_kcal($connexion,$i['id_m']);
	$k = $kcal->fetch();
	$proteine = Somme_proteine($connexion,$i['id_m']);
	$p = $proteine->fetch();
	$lipide = Somme_lipide($connexion,$i['id_m']);
	$l = $lipide->fetch();
	$glucide = Somme_glucide($connexion,$i['id_m']);
	$g = $glucide -> fetch();
	if (!empty($_POST))
	{
		foreach ($_POST as $k => $v)
		{
			$Date = str_replace('_',' ',$k);
			Supprimer($connexion,$Date);
			header('Location:User.php');
		}
	}
	if ($a['admin'] == '1')
	{
		echo '<a href="admin.php" title="admin"> Admin </a><br />';
	}
	if (empty($mail['email']))
	{
		echo 'Tu n\'as pas encore rentré ton adresse mail, tu peux le faire <a href="email.php" title="Ajout Email"> maintenant </a>.';
	}
?>
<!doctype html>
<html lang="fr">
	<head>
		<meta charset="utf-8" />                      
		<meta name = "robots" content = "none" >   
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />       
		<title> Espace Perso - Nutriwebbos </title>
		<link rel="stylesheet" href="../public/css/Nutrition.css"/>
		<link rel="icon" type="image/png" href="../public/images/logo.png" /> 
		<script src="Nutrition.js"> </script>
	</head>
	<body>
		<header>
		<!-- Menu -->
		<?php
			if (isset($_SESSION['logged']))
			{
				include ("menuUser.php");
				
			}
			else
			{
				include("menu.php");
			} 
		?>
		</header>
		<div class="index_container">
				<div>
					Bienvenue dans ton espace personnel, ici tu pourras gérer ton alimentation au quotidien.
				</div>
				<?php
						while ($membre = $membres->fetch())
					{
						if (!(empty($k['SELECTION'])))
						{
							echo'
								<div>
									Pour visualiser et choisir tes aliments ça se passe <a class="ici" href="gestion.php" title="gestion"> ici </a>.
								</div>
								<div>
									Pour définir tes objectifs ça va se passer <a class="ici" href="objectif.php" title="gestion"> là </a>.
								</div>';
							echo'
								<div class="Compteur">
									<div>Besoin : ' . $k['SELECTION'] .'/' . $membre['journalier'] . ' kcal</div>
									<div>Glucides : ' . $g['SELECTION'] .'/' . $membre['glucides'] . ' g </div>
									<div>Lipides : ' . $l['SELECTION'] .'/' . $membre['lipides'] . ' g </div>
									<div>Protéines : ' . $p['SELECTION'] .'/' . $membre['proteines'] . ' g </div>
								</div>';
						}
						else if (empty($membre['journalier']))
						{
							echo 'Tu n\'as pas encore calculé ton besoin énergétique et tes besoins nutritionnels, tu peux le faire <a class="ici" href="calcul.php" title="Calcul"> ici </a>';
						}
						else
						{
							echo'
								<div>
									Pour visualiser et choisir tes aliments ça se passe <a class="ici" href="gestion.php" title="gestion"> ici </a>.
								</div>
								<div>
									Pour définir tes objectifs ça va se passer <a class="ici" href="objectif.php" title="gestion"> là </a>.
								</div>';
							echo'
								<div class="Compteur">
									<div>Besoin : 0/' . $membre['journalier'] . ' kcal</div>
									<div>Glucides : 0/' . $membre['glucides'] . ' g </div>
									<div>Lipides : 0/' . $membre['lipides'] . ' g </div>
									<div>Protéines : 0/' . $membre['proteines'] . ' g </div>
								</div>';
						}
					}
					?>
					<br />
					<div>
						<table class="Journal">
							<thead>
								<tr>
									<th> Aliment </th>
									<th> Apport Energétique (kcal) </th>
									<th> Glucides (g) </th>
									<th> Lipides (g) </th>
									<th> Protéines (g) </th>
									<th> Quantité (g) </th>
								</tr>
							</thead>
							<?php
							while ($donnees = $reponse->fetch())
							{
								if ($donnees['specificites'] != NULL)
								{
									echo'
									<div>
										<tr>
											<td data-label="Aliment"> ' . $donnees['nom'] . ' : ' . $donnees['specificites'] . ' </td>
											<td data-label="Apport Energétique"> ' . $donnees['kcal'] . '  </td>
											<td data-label="Glucides"> ' . $donnees['glucides'] . ' </td>
											<td data-label="Lipides"> ' . $donnees['lipides'] . ' </td>
											<td data-label="Protéines"> ' . $donnees['proteines'] . ' </td>
											<td data-label="Quantité"> ' . $donnees['gramme'] . ' </td>
											<form method="post">
												<td><input type="submit" value ="Supprimer" name="'.$donnees['ajout'].'"/></td>
											</form>
										</tr>
									</div>';
								}
								else
								{
									echo'
									<div>
										<tr>
											<td data-label="Aliment"> ' . $donnees['nom'] . ' </td>
											<td data-label="Apport Energétique"> ' . $donnees['kcal'] . '  </td>
											<td data-label="Glucides"> ' . $donnees['glucides'] . ' </td>
											<td data-label="Lipides"> ' . $donnees['lipides'] . ' </td>
											<td data-label="Protéines"> ' . $donnees['proteines'] . ' </td>
											<td data-label="Quantité"> ' . $donnees['gramme'] . ' </td>
											<form method="post">
												<td><input type="submit" value ="Supprimer" name="'.$donnees['ajout'].'"/></td>
											</form>
										</tr>
									</div>';
								}
							}
							
							?>
							</table>
						</div>
		</div>
		<hr>
			<!-- Pied de Page -->
			<?php include("footer.php");?>
	</body>
</html>
