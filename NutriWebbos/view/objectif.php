<?php
	include_once("../modele/cookieconnect.php");
	require_once("../modele/function.php");
	$connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
	$id = getId($connexion,$_SESSION['pseudo']);
	$i = $id->fetch();
	$data = getDataMember($connexion,$i['id_m']);
	$d = $data->fetch();
	if(isset($_POST['Valider']))
	{
		switch (count($_POST['Macro'])) 
		{
			case '1':
				obj($connexion,$_POST['Objectifs'],$_POST['Macro'][0],$_POST['calorie'],$_SESSION['pseudo'],$i['id_m']);
				break;
			case '2':
				 obj1($connexion,$_POST['Objectifs'],$_POST['Macro'][0],$_POST['Macro'][1],$_POST['calorie'],$_SESSION['pseudo'],$i['id_m']);
				break;
			case '3':
				obj2($connexion,$_POST['Objectifs'],$d['journalier'],$_POST['Macro'][0],$_POST['Macro'][1],$_POST['Macro'][2],$_POST['Macro'][1],$_POST['calorie'],$_SESSION['pseudo'],$i['id_m']);
				break;
			default:
				break;
		}
		//updateGoal($link,$id,$obj)
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
			<h2> Objectifs </h2>
			<div>
				Si tu es ici c'est que tu te rends compte qu'avoir des valeurs c'est bien mais encore faut t'il s'avoir s'en servir intelligement. Tout d'abord je vais
				te donner ici les grandes lignes pour définir ton objectif. Malgré tout, les choses qui vont être abordées peuvent différer de ton besoin, mais
				tout conseil est bon à prendre cher Nutriwebbos. Je vais diviser les objectifs en 3 catégories.
			</div>
			<h3> Le Maintien </h3>
			<div>
				Celui ci parle de lui même il s'agit de manger le nombre de calorie dont on a besoin pour une journée afin de maintenir ton poids actuel, à plus ou moins 100 kcal, nul besoin de te prendre
				la tête. Mais attention les calories ne font pas tout, ce conseil te servira quelque soit ton objectif, il est très important
				de respecter le quota de calories que l'on se fixe mais il faut aussi choisir ses aliments minutieusement. Il y a une différence entre 800 kcal ingéré 
				via un burger et 800 kcal ingéré via un repas composé de différents aliments avec différents grammage, en premier lieu il y aura la qualité des macronutriments
				qui va varier, et surtout la satiété apportée par le repas sera différente, tout cela à son importance. Je ne parle pas de stopper les burgers et autres
				mais de ne pas compter sur eux pour tenir ton objectif.  
			</div>
			<h3> Le Déficit/Surplus </h3>
			<div>
				Déficit et surplus sont les faces d'une mêmes pièce, en premier lieu ce que j'appelle un déficit ou un surplus consiste simplement en un changement dans son apport calorique en plus ou en moins.
				Cette différence sera définie en fonction de ton objectif néanmoins une fourchette de 250 à 500 kcal est conseillée pour débuter. Et là une question te viens directement en tête, comment tu vas pouvoir
				correctement réguler des calories, il va falloir jouer avec tes <a class="ici" href="Macro.php" title="Macronutriments"> macronutriments </a>. Dans les grandes lignes, tu vas tout d'abord choisir à combien va 
				s'élever ton changement, puis tu choisiras le ou les macronutriments qui varieront et le site te donneras un nouvel objectif.
			</div>
			<h3> Le Surplus </h3>
			<div>
				Le surplus consiste en la même mécanique que le déficit, tu choisis ta fourchette mais cette fois ces calories seront à ajouter à ton besoin énergétique
				journalier dans le but d'une prise de poids. Puis tu indiques quels macronutriments tu vas augmenter la quantité et même chose le site te donneras ton nouvel objectif.
			</div>
			<h3> Quelques interrogations </h3>
			<div>
				Tu te demandes peut être que faire maintenant ton objectif définit, quel(s) macronutriment(s) baisser ou augmenter ? Tout va vraiment dépendre de là où tu veux 
				aller avec ton alimentation. Mais je peux te conseiller en premier lieu de veiller à ne pas toucher à tes protéines à moins que ça soit pour les augmenter
				et au contraire de chercher à puiser dans tes glucides et lipides, ceci sont des conseils génériques fais au mieux selon tes ressentis, tu te connais
				meiux que quiconque. Tu pars ici sur un marathon alors écoute toi et écoute ton corps nul besoin de te gaver ou de t'affamer pour atteindre tes objectifs.
			</div>
			<h3> Calcul </h3>
			<div class="form_container">
				<form method="post">
					<fieldset>
						<div class="obj_item">
							<h3> Objectifs </h3>
							<input type="radio" name="Objectifs" id="Déficit" value="Deficit">
							<label for="Déficit"> Déficit </label><br />
							<input type="radio" name="Objectifs" id="Surplus" value="Surplus">
							<label for="Surplus"> Surplus </label><br />
							<input type="number" name="calorie" placeholder="Calories" required>
							<h3> Choix </h3>
							<input type="checkbox" id="Proteines" name="Macro[]" value="Proteine">
							<label for="Proteines"> Protéines </label><br />
							<input type="checkbox" id="Glucides" name="Macro[]" value="Glucide">
							<label for="Glucides"> Glucides </label><br />
							<input type="checkbox" id="Lipides" name="Macro[]" value="Lipide">
							<label for="Lipides"> Lipides </label><br />
							<input class="User_submit" type="submit" name="Valider" value="Valider">
						</div>
					</fieldset>
				</form>
			</div>
		</div>
		<hr>
			<!-- Pied de Page -->
			<?php include("footer.php");?>
	</body>
</html>
