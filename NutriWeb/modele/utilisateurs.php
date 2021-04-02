<?php
function checkAvailability($pseudo,$link,$mail)
{
	if($link)
	{
	$req = "SELECT * FROM membres WHERE pseudo = \"$pseudo\" OR email = \"$mail\" ";
	$res = executeQuery($link,$req);
	return $res;
	}
}


function registerFullUser($pseudo,$hashPwd,$link,$kcal,$dep,$glu,$lip,$prot)
{
	if ($link)
    {
        $req = "INSERT INTO membres VALUES (NULL,\"$pseudo\",\"$hashPwd\",0,\"$kcal\",\"$dep\",\"$glu\",\"$prot\",\"$lip\")";
        executeUpdate($link,$req);
    }
}

function registerUser($pseudo,$hashPwd,$link,$mail)
{
	if ($link)
    {
        $req = "INSERT INTO membres VALUES (NULL,\"$pseudo\",\"$hashPwd\",0,NULL,NULL,NULL,NULL,NULL,\"$mail\")";
        executeUpdate($link,$req);
    }
}

function getUser ($pseudo, $hashPwd, $link, $mail)
{
	if ($link)
	{
		$req = "SELECT * FROM membres WHERE (pseudo = \"$pseudo\" || email= \"$mail\") && mdp = \"$hashPwd\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function getEmail ($link,$mail)
{
	if($link)
	{
		$req = "SELECT pseudo, email FROM membres WHERE email = \"$mail\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function ChangePasswd($link,$pseudo,$mdp)
{
	if($link)
	{
		$req = "UPDATE membres SET mdp = \"$mdp\" WHERE pseudo = \"$pseudo\" ";
		executeUpdate($link,$req);
	}
}
	

function EstAdmin ($link, $pseudo)
{
	if ($link)
	{
		$req = "SELECT admin FROM membres WHERE pseudo = \"$pseudo\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function HasEmail ($link,$pseudo)
{
	if($link)
	{
		$req = "SELECT email FROM membres WHERE pseudo = \"$pseudo\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function AddEmail ($link,$pseudo,$mail)
{
	if($link)
	{
		$req = "UPDATE membres SET email = \"$mail\" WHERE pseudo = \"$pseudo\" ";
		executeUpdate($link,$req);
	}
}

function getId ($link,$pseudo)
{
	if ($link)
	{
		$req = "SELECT id_m FROM membres WHERE pseudo = \"$pseudo\" ";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function AddFood($link,$nom,$k,$g,$l,$p,$c,$spe)
{
	if ($link)
	{
		$req = "INSERT INTO aliments VALUES (NULL,\"$nom\",\"$k\",\"$g\",\"$l\",\"$p\",'100',\"$c\",NULL,\"$spe\")";
		executeUpdate($link,$req);
	}
}
	
function getCat($link)
{
	if($link)
	{
		$req = "SELECT nom, id_c FROM categorie ORDER BY nom";
		$res = executeQuery($link,$req);
		return $res;
	}
}

function addCat($link,$nom)
{
	if ($link)
	{
		$req = "INSERT INTO categorie VALUES (NULL,\"$nom\")";
		executeUpdate($link,$req);
	}
}
?>
