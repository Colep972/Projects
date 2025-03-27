<!--<html lang="fr">
    <script defer src="../js/pdf.js"></script>
</html>-->

<?php
session_start();

if (!isset($_SESSION['autonomie-texte'])) $_SESSION['autonomie-texte'] = "";
if (!isset($_SESSION['confort-texte'])) $_SESSION['confort-texte'] = "";
if (!isset($_SESSION['famille-texte'])) $_SESSION['famille-texte'] = "";
if (!isset($_SESSION['complementaire-texte'])) $_SESSION['complementaire-texte'] = "";
$coordonnee = "";


if (isset($_POST['analyse1-submit']))
{
    $_SESSION['autonomie'] = isset($_POST['autonomie']);
    $_SESSION['confort'] = isset($_POST['confort']);
    $_SESSION['famille'] = isset($_POST['famille']);
    header("Location:analyse2.php");
}

if (isset($_POST['analyse2-submit']))
{
    if ($_SESSION['autonomie'])
    {
        for ($i = 1; $i <= 9; $i++)
        {
            if (isset($_POST['analyse2-autonomie-' . $i]))
            {
                $_SESSION['autonomie-texte'] .= $_POST['analyse2-autonomie-' . $i] . ",";
            }
        }
    }

    if ($_SESSION['confort'])
    {
        for ($i = 1; $i <= 8; $i++)
        {
            if (isset($_POST['analyse2-confort-' . $i]))
            {
                $_SESSION['confort-texte'] .= $_POST['analyse2-confort-' . $i] . ",";
            }
        }
    }

    if ($_SESSION['famille'])
    {
        for ($i = 1; $i <= 8; $i++)
        {
            if (isset($_POST['analyse2-famille-' . $i]))
            {
                $_SESSION['famille-texte'].= $_POST['analyse2-famille-' . $i] . ",";
            }
        }
    }

    for ($i = 1; $i <= 3; $i++)
    {
        if (isset($_POST['analyse2-complementaire-' . $i]))
        {
            $_SESSION['complementaire-texte'] .= $_POST['analyse2-complementaire-' . $i] . ",";
        }
    }

    header("Location:analyse3.php");
}

if (isset($_POST['analyse3-submit']))
{
    if ($_SESSION['autonomie']) {
        for ($i = 1; $i <= 3; $i++) {
            if (isset($_POST['analyse3-autonomie-' . $i]))
            {
                $_SESSION['autonomie-texte'] .= $_POST['analyse3-autonomie-' . $i] . ",";
            }
        }
    }

    if ($_SESSION['confort']) {
        for ($i = 1; $i <= 3; $i++)
        {
            if (isset($_POST['analyse3-confort-' . $i]))
            {
                $_SESSION['confort-texte'] .= $_POST['analyse3-confort-' . $i] . ",";
            }
        }
    }

    if ($_SESSION['famille'])
    {
        if (isset($_POST['analyse3-famille-1']))
        {
            $_SESSION['famille-texte'] .= $_POST['analyse3-famille-1']. ",";
        }
    }

    header("Location:analyse4.php");
}

if(isset($_POST['analyse4-submit']))
{
    $coordonnee .= "nom : " . $_POST['name'] . ",";
    $coordonnee .= "telephone : " . $_POST['tel'] . ",";
    $coordonnee .= "mail : " . $_POST['mail'] . ",";
    if (isset($_POST['message']))
    {
        $coordonnee .= "message : " . $_POST['message'] . "\n";
    }
    $entete  = 'MIME-Version: 1.0' . "\r\n";
    $entete .= 'Content-type: text/html; charset=utf-8' . "\r\n";
    $entete .= 'From: couleurs-services.com' . "\r\n";
    $to = "info@couleurs-services.com";
    $subject = "récapitulatif";
    $message = '<h1> Récapitulatif</h1><br>
                <h2> Coordonnée </h2>
                <p> '.$coordonnee.'</p>
                <h2> Autonomie </h2>
                <p> '.$_SESSION['autonomie-texte'].'</p>
                <h2> Confort </h2>
                <p> '.$_SESSION['confort-texte'].'</p>
                <h2> Famille </h2>
                <p> '.$_SESSION['famille-texte'].'</p>
                <h2> Complémentaire </h2>
                <p> '.$_SESSION['complementaire-texte'].'</p>';
    if (mail($to, $subject, $message, $entete))
    {
        echo "Email envoyé avec succès.";
    }
    else
    {
        echo "Échec de l'envoi de l'email.";
    }
    if(!isset($_SESSION['tmp']))
    {
        $_SESSION['tmp'] = false;
    }
    else
    {
        $_SESSION['tmp'] = true;
    }
    header('location: analyse4.php');
}
