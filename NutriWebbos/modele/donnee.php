<?php
function SetValue($link,$pseudo,$basal,$journ,$glu,$lip,$prot)
{
	if ($link)
	{
		$req = "UPDATE membres SET basal = \"$basal\", journalier = \"$journ\", glucides = \"$glu\", proteines = \"$prot\", lipides = \"$lip\" WHERE pseudo = \"$pseudo\" ";
		executeUpdate($link,$req);
	}
}

function aff_membres ($link,$pseudo)
{
	if ($link)
	{
		$req = "SELECT * FROM membres WHERE pseudo = \"$pseudo\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function insert_full_aliments ($link,$id,$nom,$kcal,$glucides,$lipides,$proteines,$gramme,$date,$spe)
{
	if ($link)
	{
		$req = "INSERT INTO journal VALUES (NULL,\"$id\",\"$date\",\"$nom\",\"$kcal\",\"$glucides\",\"$lipides\",\"$proteines\",\"$gramme\",\"$spe\") ";
		if ($res = executeUpdate ($link,$req) == NULL)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}

function insert_aliments ($link,$id,$nom,$kcal,$glucides,$lipides,$proteines,$gramme,$date)
{
	if ($link)
	{
		$req = "INSERT INTO journal VALUES (NULL,\"$id\", \"$date\",\"$nom\",\"$kcal\",\"$glucides\",\"$lipides\",\"$proteines\",\"$gramme\",NULL) ";
		if ($res = executeUpdate ($link,$req) == NULL)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}

function aff_full_aliments ($link,$aliment,$spe)
{
	if ($link)
	{
		$req = "SELECT * from aliments WHERE nom = \"$aliment\" AND specificites = \"$spe\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function aff_aliments ($link,$aliment)
{
	if ($link)
	{
		$req = "SELECT * from aliments WHERE nom = \"$aliment\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function aff_journal ($link,$id)
{
	if ($link)
	{
		$req = "SELECT * from journal WHERE fk_idm = \"$id\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}


function nbutilisateurs ($link)
{
	if($link)
	{
		$req = "SELECT COUNT(*) AS nbutilisateur FROM membres";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function Somme_kcal ($link,$id)
{
	if($link)
	{
		$req = "SELECT ROUND(SUM(kcal),1) AS SELECTION FROM journal WHERE fk_idm = \"$id\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function Somme_proteine ($link,$id)
{
	if($link)
	{
		$req = "SELECT ROUND(SUM(proteines),1) AS SELECTION FROM journal WHERE fk_idm = \"$id\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function Somme_lipide ($link,$id)
{
	if($link)
	{
		$req = "SELECT ROUND(SUM(lipides),1) AS SELECTION FROM journal WHERE fk_idm = \"$id\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function Somme_glucide ($link,$id)
{
	if($link)
	{
		$req = "SELECT ROUND(SUM(glucides),1) AS SELECTION FROM journal WHERE fk_idm = \"$id\" ";
		$res = executeQuery ($link,$req);
		return $res;
	}
}

function Supprimer ($link,$date)
{
	if($link)
	{
		$req = "DELETE FROM journal WHERE ajout = \"$date\" ";
		executeUpdate($link,$req);
	}
}

function Reinitialiser ($link,$date)
{
	if($link)
	{
		$req = "DELETE FROM journal WHERE ajout < \"$date\" ";
		executeUpdate($link,$req);
	}
}

function getAlimId ($link,$nom)
{
	if($link)
	{
		$req = "SELECT id_al FROM journal WHERE nom = \"$nom\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function getCategorieId ($link,$nom)
{
	if($link)
	{
		$req = "SELECT id_c FROM categories WHERE nom=\"$nom\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function ShowFood($link,$id)
{
	if($link)
	{
		$req = "SELECT nom, specificites FROM aliments WHERE fk_idc = \"$id\" ORDER BY nom ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function addRecipe($link,$name,$idm)
{
	if($link)
	{
		$req = "INSERT INTO recette VALUES (NULL,\"$name\",\"$idm\")";
		if ($res = executeUpdate ($link,$req) == NULL)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}

function getRecipeId($link,$nom)
{
	if($link)
	{
		$req = "SELECT id_r FROM recette WHERE nom = \"$nom\"";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function addIngredient($link,$idr,$name,$gramme)
{
	if($link)
	{
		$req = "INSERT INTO ingredients VALUES (NULL,\"$name\",\"$idr\",\"$gramme\")";
		if ($res = executeUpdate ($link,$req) == NULL)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}

function hasRecipe($link,$idm)
{
	if($link)
	{
		$req = "SELECT nom FROM recette WHERE fk_idm = \"$idm\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function showRecipe($link,$idm)
{
	if($link)
	{
		$req = "SELECT nom FROM recette WHERE fk_idm = \"$idm\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function getIngredients($link,$idr)
{
	if($link)
	{
		$req = "SELECT * FROM ingredients WHERE fk_idr = \"$idr\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function getFood($link,$nom)
{
	if($link)
	{
		$req = "SELECT * FROM aliments WHERE nom = \"$nom\"";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function getDataMember($link,$id)
{
	if($link)
	{
		$req = "SELECT * FROM membres WHERE id_m = \"$id\"";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function updateProteines($link,$pseudo,$new,$goal,$daily)
{
	$req = "UPDATE membres SET proteines = \"$new\", goal = \"$goal\", journalier = \"$daily\" WHERE pseudo = \"$pseudo\" ";
	executeUpdate($link,$req);
}

function updateGlucides($link,$pseudo,$new,$goal,$daily)
{
	$req = "UPDATE membres SET glucides = \"$new\", goal = \"$goal\", journalier = \"$daily\" WHERE pseudo = \"$pseudo\" ";
	executeUpdate($link,$req);
}

function updateLipides($link,$pseudo,$new,$goal,$daily)
{
	$req = "UPDATE membres SET lipides = \"$new\", goal = \"$goal\", journalier = \"$daily\" WHERE pseudo = \"$pseudo\" ";
	executeUpdate($link,$req);
}

?>
