<?php 
$dbHost = '';
$dbUser = '';
$dbPwd = '';
$dbName = '';

function getConnection($dbHost, $dbUser, $dbPwd, $dbName)
{
    try 
    {
        $bdd = new PDO("mysql:host=$dbHost;dbname=$dbName", $dbUser,$dbPwd); // on créer la connexion
        $bdd->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $bdd->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
        // Connexion module PDO
    } 
    catch (PDOException $e) 
    {
        // en cas d'erreur
        $erreur = $e->getMessage();
        echo "vous n'êtes pas connecté" . $e->getMessage();
    }
    return $bdd;
}

function executeQuery($link, $query)
{
    try 
    {
		$tmp = $link->prepare($query);

		$tmp->execute();

		return $tmp;
	}
    catch (PDOException $e) 
    {
        $erreur = $e->getMessage();
        echo"Erreur execute ! " . $e->getMessage();
    }
}

function executeUpdate($link, $query)
{
    try 
    {
		$tmp = $link->prepare($query);

		$tmp->execute();

		return $tmp;
	}

     
    catch (PDOException $e) 
    {
        $erreur = $e->getMessage();
        echo"Erreur update ! " . $e->getMessage();
    }
}
?>
