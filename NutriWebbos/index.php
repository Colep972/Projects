<?php
	session_start();
?>
<!doctype html>
<html lang="fr">
	<head>
         <meta charset="utf-8" >   
         <meta name="description" content="Nutriwebbos est une page simple et intuitive pour se lancer dans la prise en main de son corps et de sa nutrition." >
         <meta name="keywords" content="Nutrition, Métabolisme, besoin nutritionnel, nutriweb, aliment, alimentation, Nutriwebbos, macronutriments" >    
         <meta name = "robots" content = "all" >
         <meta name="viewport" content="width=device-width, initial-scale=1" />          
         <title> Accueil - Nutriwebbos </title>
         <link rel="stylesheet" href="public/css/Nutrition.css"/>
         <link rel="icon" type="image/png" href="public/images/logo.png" />
   </head>
   
	<body>
		<header class="header">
			<h1> Bienvenue sur Nutriwebbos </h1>
				<hr>
				<!-- Menu -->
					<?php
						if (isset($_SESSION['logged']))
						{
							include ("view/menuUser.php");
						}
						else
						{
							include("view/menu.php");
						} 
					?>
		</header>	
		<hr>
		<div class="index_container">
			<section class="section">    
				<h2> Objectif </h2>
  
				Bienvenue à toi cher NutriWebbos sur un site pleinement dédié à la nutrition. Sous une multitude d'aspect que tu auras plaisir à découvrir.
				Ce site s'adresse à tous : Néophyte comme avancé. Au fur et à mesure de ta naviguation dans le site tu pourras en apprendre plus sur les mécanismes du corps et plus spécifiquement de ton corps.
				Tu pourras aussi directement calculer ton métabolisme de repos, ta dépense énergétique journalière et tes besoins nutritionnels.
				
			</section>
			<hr>
	
			<section class="section"> 
				<h2> La Nutrition </h2>
					<div class="Definitions">
					Nous ne sommes plus à l'école mais quelques définitions peuvent toujours te guider :
						<div>La nutrition est la science traitant de la chimie et des transformations des aliments pour assurer les fonctions de l'organisme.  </div>
						<div>Un aliment est une matière d'origine agricole ou industrielle dont la consommation sert à couvrir les besoins nutritionnels permettant de rester en bonne santé.  </div>
						<div>Un nutriment est un élément simple et absorbable de l'alimentation qui permet de couvrir les besoins nutritionnels.  </div>
						<div>Les besoins en un nutriment donné correspondent à la quantité nécessaire pour maintenir des fonctions physiologiques
						et un état de santé normal et faire face à certaines périodes de la vie. </div>
						<div>La balance énergétique décrit l'état d'équilibre entre les apports et les dépenses énergétiques d'un individu.
						Lorsque celle-ci est équilibrée le poids reste stable et à l'inverse lorsqu'elle est déséquilibrée le poids varie.
						Le poids augmente lorsque les apports sont supérieurs aux dépenses et le poids diminue lorsque les apports sont inférieurs aux dépenses. </div>
						Ces définitions pourront t'être utiles dans la suite de ta navigation. 
					</div>
			</section> 
		</div>
  
		<hr>
		<!-- Pied de Page -->
		<?php include("view/footer.php"); ?>
		
	</body>
	
</html>
