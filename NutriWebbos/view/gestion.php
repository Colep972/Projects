<?php
	error_reporting(E_ALL);
	session_start();
	$date = date('Y-m-d H:i:s');
	require_once("../modele/donnee.php");
	require_once("../modele/model.php");
	require_once("../modele/utilisateurs.php");
	require_once("../modele/function.php");
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	$cat = getCat($connexion);
	$id = getId($connexion,$_SESSION['pseudo']);
	$idm = $id->fetch();
	$recipe = hasRecipe($connexion,$idm['id_m']);
	$r = $recipe->fetch();
	if (!empty($_GET))
	{
		foreach($_GET as $k => $v)
		{
			$cats = str_replace('_',' ',$k);
			$cid = getCategorieId($connexion,$cats);
			$i = $cid->fetch();
			if($i)
			{
				$food = ShowFood($connexion,$i['id_c']);
			}
			else
			{
				$food = ShowFood($connexion,"Recettes");
			}
			$recipes = showRecipe($connexion,$idm['id_m']);
		}
	}
	foreach($_GET as $k => $v)
	{
		if ($v == 'Choisir')
		{
			$gramme = true;
		}
		elseif ($v == 'Saisir')
		{
			$gramme = true;
			$recette = true;
			$idrecipe = getRecipeId($connexion,$k);
		}
		$aliment = str_replace('_',' ',$k);
		$chaine = ':';
		if (strpos($aliment,$chaine))
		{
			$aliments = explode (':',$aliment);
			$nom = trim($aliments[0]);
			$spe = trim($aliments[1]);
		}
		else
		{
			$nom = $aliment;
		}
			
		if (isset($_POST['Valider']))
		{ 
			if(!isset($recette))
			{
				if (isset($spe))
				{
					$reponse = aff_full_aliments($connexion,$nom,$spe);
				}
				else 
				{
					$reponse = aff_aliments($connexion,$nom);
				}
				$rep = $reponse->fetch();
				$newk = kcal($_POST['gramme'],$rep['kcal']);
				$newp = Proteine($_POST['gramme'],$rep['proteines']);
				$newg = Glucide($_POST['gramme'],$rep['glucides']);
				$newl = Lipide($_POST['gramme'],$rep['lipides']);
				if(isset($spe))
				{
					insert_full_aliments($connexion,$idm['id_m'],$nom,$newk,$newg,$newl,$newp,$_POST['gramme'],$date,$spe);
					echo ' '.$nom.' : '.$spe.' a bien été ajouté.';
				}
				else
				{
					insert_aliments($connexion,$idm['id_m'],$nom,$newk,$newg,$newl,$newp,$_POST['gramme'],$date);
					echo ' '.$nom.' a bien été ajouté.';
				} 
			}
			else
			{
				$proteines = 0;
				$lipides = 0;
				$glucides = 0;
				$kcal = 0;
				while($idr = $idrecipe->fetch())
				{
					$ingredient = getIngredients($connexion,$idr['id_r']);
					while($ing = $ingredient->fetch())
					{
						$nourriture = getFood($connexion,$ing['nom']);
						$n = $nourriture->fetch();
						$proteines += round(($n['proteines'] * $_POST['gramme'])/100,1);
						$lipides += round(($n['lipides'] * $_POST['gramme'])/100,1);
						$glucides += round(($n['glucides'] * $_POST['gramme'])/100,1);
						$kcal += round(($n['kcal'] * $_POST['gramme'])/100,1);
					}
				}
				insert_aliments ($connexion,$idm['id_m'],$nom,$kcal,$glucides,$lipides,$proteines,$_POST['gramme'],$date);
				echo 'La recette '.$nom.' a bien été ajouté.';
			}
			
		}
	}
	
?>
<!doctype html>
<html lang="fr">
	<head>
	<meta charset="utf-8" />    
	<meta name = "robots" content = "none" >   
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />       
	<title> Gestion - Nutriwebbos </title>
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
		<form>
			<div class="gestion_container">
				<?php
				
					while($c = $cat->fetch())
					{
						if($c['nom'] != "Recettes" OR !empty($r))
						{
							echo'<input class="cat" type="submit" value="'.$c['nom'].'" name="'.$c['nom'].'"> ';
						}
					}
					echo '<br />';
					echo '</div>';
					&
				?>
				<br />
		</form>
		<form method="post">
			<?php
				if (isset($gramme))
				{
					echo '<label for="quantité">  </label> <input type="number" id="gramme" step="0.1" name="gramme" placeholder="Quantité (g ou mL) :" required autofocus> <input type ="submit" value="Valider" name="Valider"> ';
				}
			?>
		</form>
		<hr>
			<!-- Pied de Page -->
			<?php include("footer.php");?>
	</body>
</html>
