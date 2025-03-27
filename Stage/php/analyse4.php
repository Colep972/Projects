<?php
session_start();
$sent = '
                <div id="sent">
                    <div id="container-cross">
                        <button id="close"><img id="cross" src="../data/Cross.svg" alt="close"></button>
                    </div>
                    <img id="sent-dude" src="../data/undraw_letter_re_8m03%201.svg" alt="form-sent">
                    <h1 id="sent-title"> Votre message a bien été envoyé !</h1>
                    <p id="sent-text"> Nous reviendrons vers vous dans un délai de 48h.</p>
                </div>';
?>
<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Couleurs Services </title>
    <link rel="icon" type="image/png" href="../data/logo_responsiv.png">
    <link rel="stylesheet" href="../css/analyse.css">
    <link rel="stylesheet" href="../css/main.css">
    <script defer src="../js/main.js"></script>
    <script defer src="../js/form.js"></script>
    <!-- Google tag (gtag.js) --> <script async src="https://www.googletagmanager.com/gtag/js?id=G-WNH8H5SDGH"></script>
    <script> window.dataLayer = window.dataLayer || []; function gtag(){dataLayer.push(arguments);} gtag('js', new Date()); gtag('config', 'G-WNH8H5SDGH'); </script>
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
        if(($_SESSION['tmp']))
        {
            echo $sent;
        }
        ?>
    </div>
    <section id="super-analyse4">
        <section id="section0-2">
            <div>
                <h2 id="section0-title"> <span class="highlight"> Étape 4 : Quelles sont vos coordonnées ? </span> </h2>
            </div>
            <div>
                <p id="section0-text">
                    Nous vous contacterons dans les 48 heures ouvrées.
                </p>
            </div>
        </section>
        <section id="section1-4">
            <div>
                <form id="form-contact-analyse" method="post" action="pdf.php">
                    <div id="section1-form">
                        <label class="must" for="name"> Votre nom </label>
                        <input type="text" name="name" id="name" placeholder="Entrez votre nom" required>
                        <label class="must" for="tel"> Votre numéro de téléphone </label>
                        <input type="tel" name="tel" id="tel" minlength="10" pattern="^\+?\d+$" placeholder="Entrez votre numéro de téléphone" required>
                        <label class="must" for="mail"> Votre adresse mail </label>
                        <input type="email" name="mail" id="mail" placeholder="Entrez votre adresse mail" required>
                        <label for="message"> Votre message </label>
                        <textarea id="message" name="message" placeholder="Entrez votre message ici"></textarea>
                    </div>
                    <div>
                        <label class="custom-checkbox">
                            <input name="autonomie" value="autonomie" class="section0-checkbox" type="checkbox" required>
                            <span class="checkmark"> </span>
                        </label>
                        <p id="checkbox-text">Je consens à ce que mes données personnelles soient utilisées à des fins de traitement de données. <a id="checkbox-link" href="charte.php">Consultez notre charte sur le respect de la vie privée.*</a></p>
                    </div>
                    <i> <sup>*</sup> Champs obligatoires</i>
                    <input id="section0-button-next" type="submit" name="analyse4-submit" value="Envoyer">
                </form>
            </div>
        </section>
        <div id="back">
            <div>
                <a href="analyse3.php"> Revenir au questionnaire </a>
            </div>
            <div>
                <a href="analyse3.php"><img src="../data/fleche_gauche.svg" alt="back"></a>
            </div>
        </div>
    </section>
    <div id="cloud-container">
        <img id="cloud" src="../data/Nuage_soleil.svg" alt="photo-fond-nuage">
    </div>
    <footer>
        <?php include("footer.php"); ?>
    </footer>
    <button id="top"></button>
</body>
</html>
