<?php
	session_start();
?>
<!doctype html>
<html>
	<head>
		<meta charset="utf-8" />                    
		<meta name="description" content="Nutriwebbos est une page simple et intuitive pour se lancer dans la prise en main de son corps et de sa nutrition." >
		<meta name="keywords" content="Nutrition, Métabolisme, besoin nutritionnel, nutriweb, aliment, alimentation, Nutriwebbos, macronutriments" >    
		<meta name = "robots" content = "all" >    
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />     
		<title> Métabolisme - Nutriwebbos </title>
		<link rel="stylesheet" href="../public/css/Nutrition.css"/>
		<link rel="icon" type="image/png" href="../public/images/logo.png" />        
	</head>
	<body>
		<header>
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
		<div class="Meta_container">
			<section class="section">  
				<h2> Métabolisme Basal et Dépense énergétique </h2>	
				<div>
					<div>
						Métabolisme de repos et dépense énergétique sont étroitements liés, au point qu'ils sont parfois confondus. Pourtant bien que l'un complète l'autre il y a une différence majeure entre :
					</div>
					<div>
						- Le métabolisme de repos est l'énergie minimale dont le corps a besoin afin de maintenir ses fonctions vitales, ce sont des dépenses d'énergies irréductibles
						pour l'entretient d'un organisme éveillé, à jeûn, au repos complet (allongé) et à thermoneutralité.
					</div>
					<div>
						- La dépense énergétique journalière correspond à la somme des dépenses qui ont lieu dans la journée, soit le métabolisme basal et l'activité physique de façon principale.
					</div>
				</div>
			</section>
				<section class="section">
					<h2> Calcul du Métabolisme de Repos </h2>	
						<div>
							Nous allons ici calculer ton métabolisme de repos à l'aide d'une formule faisant intervenir ton poids, ta taille, ton âge et ton sexe.
							A l'aide de la formule de <strong> Black et al. </strong> qui est la référence actuelle.
						</div>
						<div class="Calcul">
							<div> Pour tes calculs ça se passe <a class="ici" href="Calcul.php" title="Calcul"> ici </a>
						</div>
				</section class="section">
				<section>
					<h2> Tes objectifs </h2>
					<div> 
						Tu te demandes maintenant quoi faire de ces chiffres et bien tout dépendra de tes objectifs, si tu veux <strong>conserver ta masse actuelle</strong> alors mange
						dans la zone autour de ta dépense énergétique journalière. Dans le cadre d'un <strong>gain</strong> de masse il faudra mettre en place un surplus calorique. 
						Au contraire si tu es dans un objectif de <strong>perte</strong> il faudra mettre en place un léger déficit calorique. Tout ceci se règle selon tes objectifs évidemment mais il s'agit d'un marathon
						et non d'un sprint, ta meilleure arme sera donc ta patience.
					</div>
				</section>
		</div>
		<hr>
		<!-- Pied de Page -->
		<?php include("footer.php"); ?>
	</body>
</html>
