<?php
    ini_set('display_errors', 1);
    ini_set('display_startup_errors', 1);
    error_reporting(E_ALL);
	session_start();
	require_once("../modele/model.php");
	require_once("../modele/utilisateurs.php");
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	$cat = getCat($connexion);
	if (isset($_POST['Ajouter']))
	{
		AddFood($connexion,$_POST['nom'],$_POST['kcal'],$_POST['glucide'],$_POST['lipide'],$_POST['proteine'],$_POST['categorie'],$_POST['spe']);
		echo ' '.$_POST['nom'].' a bien été ajouté.';
	}
	if (isset($_POST['Valider']))
	{
		addCat($connexion,$_POST['categorie']);
		echo ' '.$_POST['categorie'].' a bien été ajouté.';
	}
	if (isset($_POST['Confirmer']))
	{
		
	}
?>
<!doctype html>
<html lang="fr">
	<head>
		<meta charset="utf-8" />                        
		<meta name = "robots" content = "none" >   
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />       
		<title> Espace Admin - Nutriwebbos </title>
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
		<?php
			if($_SESSION['pseudo'] == 'Colep')
			{
				echo'
					<form method="post">
						<select name="admin" id="admin">
							<option value="gestion"> Gestion </option>
							<option value="membres"> Membres </option>
						</select>
                        <input type="submit" value="Choisir" name="Choisir">
					</form>';
                    if(isset($_POST['Choisir']))
                    {
                        switch($_POST['admin'])
                        {
                            case 'gestion':
                                echo '
                                <form method="post">
                                    <fieldset>
                                        <div class="form_item">
                                            <label for="categorie"> Catégorie </label>
                                            <select id="categorie" name="categorie">';
                                                    while ($c = $cat->fetch()) 
                                                    {
                                                        echo '<option value="' . htmlspecialchars($c['id_c']) . '">' . htmlspecialchars($c['nom']) . '</option>';
                                                    }
                                            echo'
                                            </select>
                                            <label for="nom"> Aliment </label>
                                            <input type="text" id="nom" name="nom" required>
                                            <label for="nom"> Spécificités </label>
                                            <input type="text" id="spe" name="spe">
                                            <label for="kcal"> Kcal </label>	
                                            <input type="number" id="kcal" name="kcal" step="0.01" required>
                                            <label for ="glucide"> Glucide </label>	
                                            <input type="number" id="glucide" name="glucide" step="0.01" required>
                                            <label for="proteine"> Proteine </label>	
                                            <input type="number" id="proteine" name="proteine" step="0.01" required>	
                                            <label for="lipide"> Lipide </label>
                                            <input type="number" id="lipide" name="lipide" step="0.01" required>	
                                            <input type="submit" value="Ajouter" name="Ajouter">
                                        </div>
                                    </fieldset>
                                </form>
                                <form method="post">
									<fieldset>
										<div class="form_item">
											<label for="Ajout"> Ajouter une catégorie : </label>
											<input type="text" id="Ajout" name="categorie" required>
											<input type="submit" value="Valider" name="Valider">
										</div>
									</fieldset>
								</form>
                                ';
                                break;
                            case 'membres':
                                echo 'membres';
                                break;
                            default:
                                echo'not in';
                                break;
                        }
                    }
                    
			}
			else
			{
				echo'
					<div class="index_container">
						<h2> Règles d\'ajout </h2>
							<div>
								Si vous êtes sur cette page vous pouvez vous considérer comme élu, vous êtes vu comme digne de confiance alors n\'oubliez pas,
								un grand pouvoir implique de grande responsabilités.
								Alors voilà les règles à absolument respecter, le nom de l\'aliment ce doit d\'être clair et bien orthographié, il doit être placé dans la catégorie
								qui lui correspond si aucune catégorie ne semble approprié notifiez en moi. Ensuite toutes les valeurs que vous entrez doivent impérativement être
								celle de l\'aliment pour 100 g, ou 100 ml s\'il s\'agit d\'une boisson merci de votre compréhension.
							</div>
					
					<div class="form_container">
						<form method="post">
                                    <fieldset>
                                        <div class="form_item">
                                            <label for="categorie"> Catégorie </label>
                                            <select id="categorie" name="categorie">';
                                                    while ($c = $cat->fetch()) 
                                                    {
                                                        echo '<option value="' . htmlspecialchars($c['id_c']) . '">' . htmlspecialchars($c['nom']) . '</option>';
                                                    }
                                            echo'
                                            </select>
                                            <label for="nom"> Aliment </label>
                                            <input type="text" id="nom" name="nom" required>
                                            <label for="nom"> Spécificités </label>
                                            <input type="text" id="spe" name="spe">
                                            <label for="kcal"> Kcal </label>	
                                            <input type="number" id="kcal" name="kcal" step="0.01" required>
                                            <label for ="glucide"> Glucide </label>	
                                            <input type="number" id="glucide" name="glucide" step="0.01" required>
                                            <label for="proteine"> Proteine </label>	
                                            <input type="number" id="proteine" name="proteine" step="0.01" required>	
                                            <label for="lipide"> Lipide </label>
                                            <input type="number" id="lipide" name="lipide" step="0.01" required>	
                                            <input type="submit" value="Ajouter" name="Ajouter">
                                        </div>
                                    </fieldset>
                                </form>
						
							<div class="form_container">
								<form method="post">
									<fieldset>
										<div class="form_item">
											<label for="Ajout"> Ajouter une catégorie : </label>
											<input type="text" id="Ajout" name="categorie" required>
											<input type="submit" value="Valider" name="Valider">
										</div>
									</fieldset>
								</form>
							</div>
					</div>
				</div>';
		}
		?>
	</body>
