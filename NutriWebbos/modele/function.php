<?php
require_once("donnee.php");
function Calc_meta ($Sexe,$masse,$taille,$age)
{
	if ($Sexe == 'homme')
	{
		$met_h = round((1.083*pow($masse,0.48)*pow($taille/100,0.5)*pow($age,-0.13))*(1000/4.1855),1);
		return $met_h;
	}
	else if ($Sexe == 'femme')
	{
		$met_f = round((0.963*pow($masse,0.48)*pow($taille/100,0.5)*pow($age,-0.13))*(1000/4.1855),1);
		return $met_f;
	}
}

function Calc_dep ($meta, $case)
{
	$dep = $meta*$case;
	return $dep;
}

function Calc_glucide ($dep,$glucide)
{
	$Glu = round(($dep*$glucide/100)/4,1);
	return $Glu;
}

function Calc_lipide ($dep,$lipide)
{
	$Lip = round(($dep*$lipide/100)/9,1);
	return $Lip;
}

function Calc_proteine ($dep,$proteine)
{
	$Pro = round(($dep*$proteine/100)/4,1);
	return $Pro;
}

function Proteine ($gramme, $proteine)
{
	$pro = round(($proteine*$gramme)/100,1);
	return $pro;
}

function Glucide ($gramme, $glucide)
{
	$glu = round(($glucide*$gramme)/100,1);
	return $glu;
}

function Lipide ($gramme, $lipide)
{
	$lip = round(($lipide*$gramme)/100,1);
	return $lip;
}

function kcal ($gramme, $kcal)
{
	$k = round(($kcal*$gramme)/100,1);
	return $k;
}

function macro ($link,$macro,$pseudo,$quantite,$id)
{
	$macronutriment = getDataMember($link,$id);
	$m = $macronutriment->fetch();
	$daily = $m['journalier'] + $quantite;
	$return = 0;
	switch($macro)
	{
		case 'Glucide':
			$return = $m['glucides']+(($quantite/2)/4);
			updateGlucides($link,$pseudo,$return,$quantite);
			break;
		case 'Lipide':
			$return = $m['lipides']+(($quantite/2)/9);
			updateLipides($link,$pseudo,$return);
			break;
		case 'Proteine':
			$return = $m['proteines']+(($quantite/2)/4);
			updateProteines($link,$pseudo,$return);
			break;
	}
}

function macro1 ($link,$macro,$macro1,$pseudo,$quantite,$id)
{
	$macronutriment = getDataMember($link,$id);
	$m = $macronutriment->fetch();
	$daily = $m['journalier'] + $quantite;
	$return = 0;
	switch($macro)
	{
		case 'Glucide':
			$return = $m['glucides']+(($quantite/2)/4);
			updateGlucides($link,$pseudo,$return,$quantite,$daily);
			break;
		case 'Lipide':
			$return = $m['lipides']+(($quantite/2)/9);
			updateLipides($link,$pseudo,$return,$quantite,$daily);
			break;
		case 'Proteine':
			$return = $m['proteines']+(($quantite/2)/4);
			updateProteines($link,$pseudo,$return,$quantite,$daily);
			break;
	}
	switch($macro1)
	{
		case 'Glucide':
			$return = $m['glucides']+(($quantite/2)/4);
			updateGlucides($link,$pseudo,$return,$quantite,$daily);
			break;
		case 'Lipide':
			$return = $m['lipides']+(($quantite/2)/9);
			updateLipides($link,$pseudo,$return,$quantite,$daily);
			break;
		case 'Proteine':
			$return = $m['proteines']+(($quantite/2)/4);
			updateProteines($link,$pseudo,$return,$quantite,$daily);
			break;
	}
}

function macro2 ($link,$macro,$macro1,$macro2,$quantite,$pseudo,$id)
{
	$return = 0;
	$macronutriment = getDataMember($link,$id);
	$m = $macronutriment->fetch();
	$daily = $m['journalier'] + $quantite;
	switch($macro)
	{
		case 'Glucide':
			$return = $m['glucides']+(($quantite/2)/4);
			updateGlucides($link,$pseudo,$return,$daily);
			break;
		case 'Lipide':
			$return = $m['lipides']+(($quantite/2)/9);
			updateLipides($link,$pseudo,$return,$daily);
			break;
		case 'Proteine':
			$return = $m['proteines']+(($quantite/2)/4);
			updateProteines($link,$pseudo,$return,$daily);
			break;
	}
	switch($macro1)
	{
		case 'Glucide':
			$return = $m['glucides']+(($quantite/2)/4);
			updateGlucides($link,$pseudo,$return,$daily);
			break;
		case 'Lipide':
			$return = $m['lipides']+(($quantite/2)/9);
			updateLipides($link,$pseudo,$return,$daily);
			break;
		case 'Proteine':
			$return = $m['proteines']+(($quantite/2)/4);
			updateProteines($link,$pseudo,$return,$daily);
			break;
	}
	switch($macro2)
	{
		case 'Glucide':
			$return = $m['glucides']+(($quantite/2)/4);
			updateGlucides($link,$pseudo,$return,$daily);
			break;
		case 'Lipide':
			$return = $m['lipides']+(($quantite/2)/9);
			updateLipides($link,$pseudo,$return,$daily);
			break;
		case 'Proteine':
			$return = $m['proteines']+(($quantite/2)/4);
			updateProteines($link,$pseudo,$return,$daily);
			break;
	}
}

function obj ($link,$obj,$macro,$q,$pseudo,$id)
{

	switch($obj)
	{
		case 'Deficit': 
			//On rajoute nb_cal dans membres
			//On enlève 
			macro($link,$macro,$pseudo,-$q,$id);
			break;
		case 'Surplus':
			macro($link,$macro,$pseudo,$q,$id);
			break;
	}
} 

function obj1 ($link,$obj,$macro,$macro1,$q,$pseudo,$id)
{
	switch($obj)
	{
		case 'Deficit': 
			macro1($link,$macro,$macro1,$pseudo,-$q,$id);
			break;
		case 'Surplus':
			macro1($link,$macro,$macro1,$pseudo,$q,$id);
			break;
	}
} 

function obj2 ($link,$obj,$macro,$macro1,$macro2,$q,$pseudo,$id)
{
	switch($obj)
	{
		case 'Deficit': 
			macro2($link,$macro,$macro1,$macro2,$pseudo,-$q,$id);
			break;
		case 'Surplus':
			macro2($link,$macro,$macro1,$macro2,$pseudo,$q,$id);
			break;
	}
} 
?>
