<?php 
	require_once("../modele/function.php");
	require_once("../modele/donnee.php");
	require_once("../modele/model.php");
	session_start();
	if (isset($_POST['Calcul1']))
	{
		$_SESSION['metabolisme'] = Calc_meta($_POST['Sexe'],$_POST['masse'],$_POST['taille'],$_POST['age']);
		echo '
		<div class="Meta_container">
			<div class="Meta_result"> 
				<div> 
					Voilà ton métabolisme de repos calculé il est de <strong> ' . $_SESSION['metabolisme'] . ' kcal</strong>. 
					C\'est à dire que ton corps utilise cette énergie pour vivre, c\'est le palier minimal à atteindre. 
				</div>
				<div>
					Maintenant parlons de dépense énergétique, il s\'agit du palier d\'équilibre de son poids. Apporter à ton corps plus que ce palier te feras
					prendre du poids, au contraire, lui en apporter moins (mais toujours plus que ton métabolisme de repos) te feras perdre du poids. La dépense
					énergétique journalière correspond à ton métabolisme de repos multiplié par ton niveau d\'activité physique.
					Nous allons maintenant saisir ton niveau d\'activité physique. 
				</div>
			</div>
		</div>';
	}
	if (isset($_POST['NAP']))
	{ 
		$_SESSION['depense'] = Calc_dep($_SESSION['metabolisme'],$_POST['NAP']);
		echo '
		<div class="Meta_container">
			<div class="Meta_result">
				<div> 
					Après tout ce chemin voici une approximation de ta dépense énergétique journalière, qui est de
					<strong> ' . $_SESSION['depense'] .' </strong> kcal.  
				</div>
				<div> 
					Manges plus et tu prendras du poids, moins et tu en perdras mais veille à toujours rester au dessus de ton métabolisme basal (' . $_SESSION['metabolisme'] . ' kcal) 
					pour ta santé. Te voilà doté de précieuses armes dans la compréhension de ton corps et de la nutrition, n\'hésite pas à continuer ta navigation
					pour plus d\'informations.
				</div>
			</div>
		</div>';
	}
	if (isset($_POST['Calcul2']))
	{
		if ($_POST['Glucide']+$_POST['Proteine']+$_POST['Lipide'] == 100)
		{
			$_SESSION['Glucide'] = Calc_glucide($_SESSION['depense'],$_POST['Glucide']);
			$_SESSION['Proteine'] = Calc_proteine($_SESSION['depense'],$_POST['Proteine']);
			$_SESSION['Lipide'] = Calc_lipide($_SESSION['depense'],$_POST['Lipide']);
			echo '
			<div class="Meta_container">
				<div class="Meta_result">
					<div>
						Ton besoin en glucide est de <strong>' . $_SESSION['Glucide'] . ' g</strong> par jour, celui en lipide est de <strong>
						' . $_SESSION['Lipide'] . ' g</strong> par jour et celui en protéine est de <strong> ' . $_SESSION['Proteine'] . ' g</strong>.
						Pour calculer nous avons repris ton besoin énergétique journalier(<strong>' . $_SESSION['depense'] . ' kcal</strong>)et nous l\'avons multilplié par
						le pourcentage de macronutriments que tu as saisi et cela pour chaque macronutriments, de ce calcul nous avons obtenu le grammage nécessaire pour
						chaque macronutriments et nous l\'avons divisé par la valeur calorique d\'un gramme, 4 Kcal pour les glucides et les protéines et 9 kcal pour les lipides.
					</div>
				</div>
			</div>';
			if (isset($_SESSION['pseudo']) AND (isset($_POST['Calcul2'])))
			{
				$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
				SetValue($connexion,$_SESSION['pseudo'],$_SESSION['metabolisme'],$_SESSION['depense'],$_SESSION['Glucide'],$_SESSION['Lipide'],$_SESSION['Proteine']);
			}
		}
		else
		{
			echo '<div class="errors"> La somme des pourcentages de vos macronutrimens doit être de 100%.</div>';
		}
	}
?>
<!doctype html>
<!-- Ceci est un commentaire -->
<html lang="fr">
	<head>
		<meta charset="utf-8" />                    
		<meta name="description" content="Nutriwebbos est une page simple et intuitive pour se lancer dans la prise en main de son corps et de sa nutrition." >
		<meta name="keywords" content="Nutrition, Métabolisme, besoin nutritionnel, nutriweb, aliment, alimentation, Nutriwebbos, macronutriments" >    
		<meta name = "robots" content = "all" >   
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />       
		<title> Calcul - Nutriwebbos </title>
		<link rel="stylesheet" href="../public/css/Nutrition.css"/>
		<link rel="icon" type="image/png" href="../public/images/logo.png" /> 
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
			<section class="section">
				<h2> Calcul Métabolisme de repos et besoin énergétique </h2>
					<div>
						Les calculs au sein de cette page se dérouleront en deux étapes, dans un premier temps il faudra calculer
						ton métabolisme basal et ta dépense énergétique journalière puis enfin tes besoins nutritionnels journalier. 
					</div>
					<?php
					if (!(isset($_POST['Calcul1'])) && !(isset($_POST['Soumettre'])))
					{
						echo'
						<strong>Etape 1.1.</strong>
						<div class="form_container">
						
							<form method="post">
								<fieldset>
									<legend> Données </legend>
									
									<div class="form_item">
									
										<label for="homme"> homme </label>
										<input type="radio" name="Sexe" value="homme" id="homme" required />
										<label for="femme"> femme </label>
										<input type="radio" name="Sexe" value="femme" id="femme" required />
								
										<label for="age"> Age : </label> 
										<input type="number" min="0" name="age" id="age" placeholder="Ex : 16 ans"  required />
										<label for="masse"> Masse : </label>
										<input type="number" min="0" name="masse" id="masse" placeholder="Ex : 58 kg" step="0.1" required />
										<label for="taille"> Taille : </label>
										<input type="number" min="0" name="taille" id="taille" placeholder="Ex : 181 cm" step="0.1" required />
										<input type="submit" name="Calcul1" value="Calculer" />
										<div>Pour en savoir plus c\'est par <a class="ici" href="Meta.php" title="Métabolisme">là</a>.</div>
									</div>
									
								</fieldset>
							</form>
						</div>';
					}
						if (isset($_POST['Calcul1']) && !(isset($_POST['Soumettre'])))
							{
								echo '
								<strong>Etape 1.2.</strong>
								<div class="form_container">
									<form method="post">
										<fieldset>
										
											<div class="form_item">
												<label for ="NAP"> Niveau d\'activité physique : </label>
												<select name = "NAP" id="NAP">
													<option value ="1.2" required>1.2 : sédentaire (travail de bureau, pas d’activité physique de loisir)</option>
													<option value ="1.375" required>1.375 : bas (exercices actifs modérés 1-3 fois/semaine)</option>
													<option value ="1.55" required>1.55 : moyen (sport 3-5 fois/semaine)</option>
													<option value ="1.725" required>1.725 : élevé (sport intensif 6-7 fois/semaine)</option>
													<option value ="1.9" required>1.9 : Très élevé (athlètes avec entraînement intensif ou profession très physique)</option>
												</select>
												<input type="submit" name="Soumettre" value="Soumettre" />
											</div>
										</fieldset>
									</form>
								</div>';
							}
						?>
		</div>
			</section>
			<div class="Macro_container">
				<section class="section">
					<?php 
						if (isset($_POST['Soumettre']))
							{	
								echo '
								Etape 2
									<h2> Calcul Besoin nutritionnel </h2> 
									<div class="form_container">
										<form method="post">
											<fieldset>
												<legend> Besoin Nutritionnel </legend>
												<div class="Macro_item">
													<label for="Glucide"> Glucide : </label>
													<input type="number" min="0" max="100" id="Glucide" name="Glucide" required="required" >
													<label for="Lipide"> Lipide : </label>
													<input type="number" min="0" max="100" id="Lipide" name="Lipide" required="required" >
													<label for="Proteine"> Protéine : </label>
													<input type="number" min="0" max="100" id="Proteine" name="Proteine" required="required" >
													<input type="submit" name="Calcul2" value="Calculer">
													<div>Pour en savoir plus c\'est par <a class="ici" href="Macro.php" title="Macronutriments">là</a>.</div>
												</div>
											</fieldset>
										</form>
									</div>';
							}
					?>   
				</section>
			</div>
			<hr>
			<!-- Pied de Page -->
			<?php include("footer.php"); ?>
	</body>
</html>
  
