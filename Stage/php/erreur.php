<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Couleurs Service</title>
    <link rel="icon" type="image/png" href="../data/logo_responsiv.png">
    <link rel="stylesheet" href="../css/erreur.css">
    <link rel="stylesheet" href="../css/main.css">
    <!-- Google tag (gtag.js) --> <script async src="https://www.googletagmanager.com/gtag/js?id=G-WNH8H5SDGH"></script>
    <script> window.dataLayer = window.dataLayer || []; function gtag(){dataLayer.push(arguments);} gtag('js', new Date()); gtag('config', 'G-WNH8H5SDGH'); </script>
    <script defer src="../js/main.js"></script>
</head>
<body>
    <header id="header">
        <?php include("header.php"); ?>
    </header>
    <button id="hamburger"></button>
    <section id="padding">

    </section>
    <section id="bg-image">
        <section id="section1">
            <div>
                <p id="section1-ariane"> Erreur 404 </p>
            </div>
            <h1 id="section1-title"> <span class="highlight"> cette page n'existe pas </span> </h1>
            <div id="section1-container">
                <p id="section1-text">
                    La page que vous cherchez n’existe pas à été déplacée ou supprimée.<br>
                    Pas de panique, vous pouvez retourner à la page d’accueil en cliquant sur le bouton
                    ci-dessous.
                </p>
            </div>
            <div>
                <a id="section1-link" href="../index.php"> Retour à la page d'accueil </a>
            </div>
        </section>
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