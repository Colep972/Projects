<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title> Couleurs Services </title>
    <link rel="icon" type="image/png" href="../data/logo_responsiv.png">
    <link rel="stylesheet" href="../css/prestations.css">
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
    <section id="section0">
            <h1 id="section0-title"> <span class="highlight"> Nos services à domicile </span> </h1>
        <div id="section0-up">
            <p id="section0-up-text-0">
                Chez Couleurs Services,

                nous vous proposons une gamme d'offres modulables afin que vous puissiez choisir la solution qui correspond le mieux à votre situation et à vos attentes.<br><br>

                Que vous recherchiez une aide ménagère, un accompagnement pour vos courses, un soutien à la garde d'enfants ou tout autre services à domicile, nous avons la formule idéale pour vous.
            </p>
        </div>
        <div id="section0-down">
            <div id="section0-left">
                <a class="section0-p" id="section0-left-p" href="/offre-autonomie"><p class="section0-title"> Autonomie </p></a>
                <div class="section0-relative">
                    <a class="section0-img-container" id="section0-left-container" href="/offre-autonomie"><img id="section0-left-img" src="../data/Icone_Autonomie.svg" alt="Autonomie"></a>
                </div>
            </div>
            <div id="section0-middle">
                <a class="section0-p" id="section0-middle-p" href="/offre-confort"><p class="section0-title"> Confort </p></a>
                <div class="section0-relative">
                    <a class="section0-img-container" id="section0-middle-container" href="/offre-confort"><img id="section0-middle-img" src="../data/Icone_Confort.svg" alt="Confort"></a>
                </div>
            </div>
            <div id="section0-right">
                <a class="section0-p" id="section0-right-p" href="/offre-famille"><p class="section0-title"> Famille </p></a>
                <div class="section0-relative">
                    <a class="section0-img-container" id="section0-right-container" href="/offre-famille"><img id="section0-right-img" src="../data/Icone_Famille.svg" alt="Famille"></a>
                </div>
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