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
	if (!empty($_GET))
	{
		foreach($_GET as $k => $v)
		{
				$cats = str_replace('_',' ',$k);
				$cid = getCategorieId($connexion,$cats);
				$i = $cid->fetch();
				if ($i && isset($i['id_c'])) 
				{
			    	$food = ShowFood($connexion, $i['id_c']);
				} 
				else 
				{
				    $food = null; // ou tu mets un message d'erreur si tu veux
				}
		}
			
	}
	foreach($_GET as $k => $v)
	{
		if ($v == 'choisir')
		{
			$gramme = true;
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
			unset($gramme); 
			if (isset($spe))
			{
				$reponse = aff_full_aliments($connexion,$nom,$spe);
			}
			else 
			{
				$reponse = aff_aliments($connexion,$nom);
			}
			$idrecipe = getRecipeId($connexion,$_SESSION['Recipe_name']);
			$idr = $idrecipe->fetch();
			addIngredient($connexion,$idr['id_r'],$nom,$_POST['gramme']);
			$_SESSION['numberIngredients']--;
			
		}
	}
	if(isset($_POST['ValiderRecipe']))
	{
		$_SESSION['Recipe_name'] = $_POST['Recipe_name'];
		addRecipe($connexion,$_POST['Recipe_name'],$idm['id_m']);
		$_SESSION['numberIngredients'] = $_POST['ingredients_number'];
	} 
?>
<!doctype html>
<html lang="fr">
	<head>
	<meta charset="utf-8" />    
	<meta name = "robots" content = "none" >   
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />       
	<title> Recette - Nutriwebbos </title>
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
					if(isset($_SESSION['numberIngredients']) AND $_SESSION['numberIngredients'] > 0)
					{
						while($c = $cat->fetch())
						{
							if($c['nom'] != "Recettes")
							{
								echo'<input class="cat" type="submit" value="'.$c['nom'].'" name="'.$c['nom'].'"> ';
							}
						}
						echo '<br />';
						echo '</div>';
						if (!empty($_GET))
						{
							echo 'Il reste ' . $_SESSION['numberIngredients'] . ' ingrédients à ajouter' ;
							 if ($food) 
							 {
								while ($f = $food->fetch())
								{
									if (empty($f['specificites']))
									{
										echo '<table class="table"> <tr> <td> '.$f['nom'].' </td> <td> <input class="choisir" type="submit" value="choisir" name="'.$f['nom'].'" autofocus> </td> </tr> </table>';
									}
									else
									{
										$al = ''.$f['nom'].' : '.$f['specificites'].' ';
										echo '<table class="table"> <tr> <td> '.$al.' </td> <td> <input class="choisir" type="submit" value="choisir" name="'.$al.'" autofocus> </td> </tr></table>';
									}
								}
							}
						}
					}
					else
					{
						?>
					</form>
						<form method="post">
							<div class="gestion_container">
										<label for="Recipe_name"> Nom de la recette </label>
										<input type="text" name ="Recipe_name" id="Recipe_name">
										<label for="Ingredients"> Nombre d'ingrédients </label>
										<input type="number" name="ingredients_number" min="0" max="100"> 
										<input type="submit" value="Valider" name="ValiderRecipe">
							</div>
						<?php
					}
				?>
				<br />
				</form>
			<form method="post">
			<?php
				if (isset($gramme))
				{
					echo '
					<label for="quantité">  </label> 
					<input type="number" id="gramme" step="0.1" name="gramme" placeholder="Quantité (g ou mL) : " required autofocus>
					 <input type ="submit" value="Valider" name="Valider"> ';
				}
			?>
		</form>
		<hr>
			<!-- Pied de Page -->
			<?php include("footer.php");?>
	</body>
</html>
