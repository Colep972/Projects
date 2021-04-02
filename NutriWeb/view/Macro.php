<?php 
	session_start(); 
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
		<title> Macronutriments - Nutriwebbos </title>
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
		<div class="Macro_container">
			<section class="section"> 
		   
				<h2> Les (macro)nutriments</h2>
				<div>	
				Macronutriments.. Ce nom peut sembler barbare, mais une fois explicité il sera votre plus fidèle allié.
				Un macronutriment est aussi appelé nutriment énergétique, il sert de source d'énergie pour le corps. Il en existe plusieurs sortes.
				</div>	
			</section>
			<section class="section"> 
			<h3> Les principaux macronutriments</h3> 
				<div class="Main_Macro_container">
		
					<div><strong>Les protéines</strong>  </div>
					<div>Maintenant nous allons nous pencher vers le macronutriment qui constitue les muscles, la masse maigre (os,muscles,peau,organes,liquides...), les enzymes et le cytosquelette.
					En cas de jeûn prolongé, l'énergie des cellules proviendra des protéines. Les protéines sont dites essentielles à l'homme car faute de savoir les
					fabriquer elles doivent être apportées par l'alimentation. </div>
					<div><strong>Les glucides</strong>  </div>
					<div>Les glucides vous ne connaissiez pas ce terme avant ? Et pourtant, si vos cellules arrivent à faire leurs tâches c'est grâce à ce macronutriment.
					En effet le rôle des glucides n'est pas moins que de fournir de l'énergie aux cellules de plus ils fournissent en exclusivité le cerveau.</div>
					<div><strong>Les lipides</strong>  </div>
					<div>L'apport en glucide étant discontinue au sein d'une journée, le corps doit trouver son équilibre. Les lipides interviennent ici, la régulation se fait
					grâce aux lipides, leur stock se transforme afin de palier aux manques de glucides. Les lipides sont aussi essentiels, elles jouent un rôle dans 
					beaucoup de fonctions de notre organisme que ce soit dans le transport d'éléments dans le sang ou bien dans la création de mécanismes importants.</div>
					
					</div>
			</section>
			<section class="section"> 
				<h3> Les préjugés </h3>
				<div class="prejudices">
					<div>
						On vient de présenter les principaux macronutriments mais malgré tout je te vois toujours perplexe à propos de l'aide que cela va t'apporter.
						Avant de rentrer dans plus de détails sur l'utilisation des macronutriments dans la nutrition, nous allons démystifier quelques points.
					</div>
					<div>
						On entend souvent parler du fait que les glucides c'est du sucre, les protéines c'est de la viande et les lipides c'est soit du gras donc à éviter ou alors c'est dans
						l'huile et il faut limiter la consommation. Mais tout ceci est faux, la vérité est que chaque aliments contient une proportion plus ou moins importante de
						macronutriments, les fruits ainsi que les féculents auront tendances à avoir un apport important en glucide, les huiles, fromages, poissons gras et viandes auront
						tendances à apporter plus de lipides et les oeufs, les viandes maigres ou grasses et poissons maigres ou gras apporteront plus de protéines mais attention 100 g de viande ce n'est pas 100 g de protéine.
						Il faut donc au maximum avoir une alimentation variée, différentes sources de protéines, lipides et glucides sans jamais brisé l'équilibre entre (oui c'est tout une corvée une alimentation équilibrée).
					</div>
				</div>		
			</section>
			<section class="section"> 
				<h3> Obtenir ses besoins </h3>
				<div class="needs">
					<div>
						Donc le choix des aliments lorsque l'on cherche à combler ses besoins se fait en regardant les apports dans ces différents macronutriments.
						C'est bien beau me diras tu mais comment fait-on pour savoir combien de glucides, lipides et protéines ingérer?
					</div>
					<div>
						Il y a des fourchettes de répartition conseillée pour tes apports nutrionnels :
					</div>
				</div>
				<div class="percentage">
					<div> 40 à 55 % pour les glucides</div>
					<div> 35 à 40 % pour les lipides</div>
					<div>10 à 20 % pour les protéines</div>
					<strong>Bien évidemment malgré tes choix tu dois à la fin obtenir  100 %</strong>.
				</div>

			</section>

			<section class="section"> 
				<h3> Exemple </h3>
				<div class="example">
					<i> 
						Coralie veut remodeler son alimentation, elle veut dans celle-ci un apport important en protéine et moindre en glucide.
						Elle va donc choisir de couvrir le maximum de son besoin
						journalier en protéines, puis 45% avec des glucides et finalement 35% avec des lipides.
						Elle mangera des fromages, du poissons et de la viandes pour un apport en protéines et lipides, mais aussi du soja, des lentilles et autres
						qui couvriront aussi ses besoins en glucides, elle verra que la qualité prévaut sur la quantité mais que ça ne l'empêche pas d'être rassasiée.
					</i> 
				</div>
			</section>
			<section class="section"> 
				<h3> Le calcul </h3>
				<div class="calculation">
					Une fois ceci déterminé il faudra simplement à l'aide de sa dépense énergétique journalière faire le calcul. Une fois ceci fait on obtient des valeurs différentes selon
					le macronutriment, cela correspond à la valeur totale de kcal de ce nutriment énergétique que tu devras manger en une journée. Pour s'en sortir à ce niveau, il faut savoir
					une chose, c'est la valeur calorique de chaques macronutriments. Je te propose de <a class="ici" href="Calcul.php" title="Calcul">calculer</a> tes besoins et de t'expliquer par la suite ton résultat.
				</div>
			</section>
	</div>
	<hr>
	<!-- Pied de Page -->
	<?php include("footer.php"); ?>

	</body>
</html>
