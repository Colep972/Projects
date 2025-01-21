<?php
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

function macro ($macro,$quantite)
{
	switch($macro)
	{
		case 'Glucide':
			return $quantite*4;
			break;
		case 'Lipide':
			return $quantite*9;
			break;
		case 'Proteine':
			return $quantite*4;
			break;
	}
}

function obj ($obj,$nb_cal,$macro,$q)
{
	switch($obj)
	{
		case 'Deficit': 
			$nb_cal-macro($macro,$quantite);
			break;
		case 'Surplus':
			$nb_cal+macro($macro,$q);
			break;
	}
} 
?>
