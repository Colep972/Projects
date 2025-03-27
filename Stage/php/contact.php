<?php
    require_once ("model.php");
    require_once ("datacontact.php");
    $bool = false;
    global $dbHost, $dbUser, $dbPwd, $dbName;
    $connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
    $sent = '
                <div id="sent">
                    <div id="container-cross">
                        <button id="close"><img id="cross" src="../data/Cross.svg" alt="close"></button>
                    </div>
                    <img id="sent-dude" src="../data/undraw_letter_re_8m03%201.svg" alt="form-sent">
                    <h1 id="sent-title"> Votre message a bien été envoyé !</h1>
                    <p id="sent-text"> Nous reviendrons vers vous dans un délai de 48h.</p>
                </div>';
    if(isset($_POST['Envoyer']))
    {
        date_default_timezone_set("Europe/Paris");
        $date = date("Y-m-d H:i:s");
        $entete  = 'MIME-Version: 1.0' . "\r\n";
        $entete .= 'Content-type: text/html; charset=utf-8' . "\r\n";
        $entete .= 'From: couleurs-services.com' . "\r\n";
        $message =
            '<h1>Message envoyé depuis la page Contact de couleurs-services.com</h1>
            <p><b> Nom : </b> ' . htmlspecialchars($_POST['name']) . '<br>
            <b> Téléphone : </b> '. htmlspecialchars($_POST['tel']) . '<br>
            <b> Mail : </b>' . htmlspecialchars($_POST['mail']) . '<br>
            <b> Message : </b>' . htmlspecialchars($_POST['message']) . '</p>
            <b> Date : </b>' . $date . '</p>
            ';
        $messageUser =
            '<h1> Réception de votre message de contact </h1>
            <p> Merci d\'avoir rempli notre formulaire de contact </p>';
        $retour = mail('info@couleurs-services.com', 'Envoi depuis page Contact', $message, $entete);
        $toUser = mail(' '. $_POST['mail'] .' ', 'Envoi depuis page Contact', $messageUser, $entete);
        putData($connexion,htmlspecialchars($_POST['name']),htmlspecialchars($_POST['tel']),htmlspecialchars($_POST['mail']),$date);
        $bool = true;
    }
?>
<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Couleurs Services</title>
    <link rel="icon" type="image/png" href="../data/logo_responsiv.png">
    <link rel="stylesheet" href="../css/contact.css">
    <link rel="stylesheet" href="../css/main.css">
    <!-- Google tag (gtag.js) --> <script async src="https://www.googletagmanager.com/gtag/js?id=G-WNH8H5SDGH"></script>
    <script> window.dataLayer = window.dataLayer || []; function gtag(){dataLayer.push(arguments);} gtag('js', new Date()); gtag('config', 'G-WNH8H5SDGH'); </script>
    <script defer src="../js/main.js"></script>
    <script defer src="../js/form.js"></script>
</head>
<body>
    <header id="header">
        <?php include("header.php"); ?>
    </header>
    <button id="hamburger"></button>
    <section id="padding">

    </section>
    <div id="container-message">
        <?php
        if($bool)
        {
            echo $sent;
        }
        ?>
    </div>
    <section id="bg-image">
        <section id="section0">
            <a class="ariane-links" href="/accueil">Couleurs Services > </a>  <b id="section0-bold"> Contact </b>
        </section>
        <section id="section1">
            <h1 id="section1-title"> <span class="highlight"> contactez-nous </span> </h1>
            <div id="section1-container">
                <p id="section1-text">
                    Une question, une suggestion, ou vous voulez simplement discuter ?
                    Complétez notre formulaire de contact ci-dessous ou contactez-nous directement.
                </p>
            </div>
        </section>
    </section>
    <h2 id="section2-up-title"> Contactons-nous </h2>
    <section id="section2">
        <div id="section2-left">
            <form class="form" method="post">
                <div id="section2-left-form">
                    <h2 class="section2-title" id="section2-left-title"> Nous  vous contactons </h2>
                    <label class="must" for="name"> Votre nom </label>
                    <input type="text" name="name" id="name" placeholder="Entrez votre nom" required>
                    <label class="must" for="tel"> Votre numéro de téléphone </label>
                    <input type="tel" name="tel" id="tel" minlength="10" pattern="^\+?\d+$" placeholder="Entrez votre numéro de téléphone" required>
                    <label class="must" for="mail"> Votre adresse mail </label>
                    <input type="email" name="mail" id="mail" placeholder="Entrez votre adresse mail" required>
                    <label for="message"> Votre message </label>
                    <textarea id="message" name="message" placeholder="Entrez votre message ici"></textarea>
                    <i> <sup>*</sup> Champs obligatoires</i>
                    <input class="links-button" id="submit" type="submit" name="Envoyer" value="Envoyer">
                </div>
            </form>
        </div>
        <div id="section2-right">
            <h2 class="section2-title" id="section2-right-title"> Vous nous contactez </h2>
            <div id="section2-right-container">
                <p id="section2-right-rect0" class="section2-right-text"> Du lundi au vendredi de 9h00 à 13h00 </p>
                <p id="section2-right-rect1" class="section2-right-text"> +33 9 82 51 59 74 </p>
                <p id="section2-right-rect2" class="section2-right-text"> info@couleurs-services.com </p>
                <div id="section2-right-rect3" class="section2-right-text">
                    <p> <b> CENTRE REGUS - Couleurs Services </b></p>
                    <p> 1 Esplanade Miriam MAKEBA - CS 40297
                        <br> 69628 Villeurbanne Cedex </p>
                </div>
            </div>
        </div>
    </section>
    <div id="section2-down">
        <img id="section2-interlocutrices" src="../data/interlocutrices.svg" alt="photo-interlocutrices">
        <div id="section2-fl">
            <div id="section2-f">
                <img id="Fatima" src="../data/Profil_FatimaLemmouchia.png" alt="photo-presidente">
                <div class="section2-links">
                    <a class="section2-name" href="mailto:info@couleurs-services.com"> Fatima LEMMOUCHIA </a>
                    <a class="section2-job" href="mailto:info@couleurs-services.com">Présidente et directrice </a>
                </div>
            </div>
            <div id="section2-l">
                <img id="Lucie" src="../data/Profil_LucieBarrel.png" alt="photo-assistante">
                <div class="section2-links">
                    <a class="section2-name"> Lucie BARREL </a>
                    <a class="section2-job">Assistante de gestion </a>
                </div>
            </div>
        </div>
    </div>
    <div id="cloud-container">
        <img id="cloud" src="../data/Nuage_soleil.svg" alt="photo-fond-nuage">
    </div>
    <footer>
        <?php include("footer.php"); ?>
    </footer>
    <button id="top"></button>
</body>
</html>