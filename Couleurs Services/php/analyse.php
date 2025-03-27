<?php
    session_start();
    $_SESSION['autonomie'] = false;
    $_SESSION['famille'] = false;
    $_SESSION['confort'] = false;
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
        <section id="section0">
            <div id="section0-2">
                <div>
                    <h2 id="section0-title"> <span class="highlight"> Étape 1 : Quels sont vos besoins ? </span> </h2>
                </div>
                <div>
                    <p id="section0-text">
                        Vous pouvez sélectionner de 1 à 3 services en cochant la case en haut à gauche.
                    </p>
                </div>
            </div>
            <form method="post" action="../php/pdf.php">
                <div id="section0-box-container">
                    <div class="section0-box" id="section0-autonomie">
                        <label class="custom-checkbox">
                            <input name="autonomie" value="autonomie" class="section0-checkbox" type="checkbox">
                            <span class="checkmark"></span>
                        </label>
                        <div>
                            <img src="../data/Group-215.svg" alt="img-autonomie">
                        </div>
                        <div>
                            <p class="section0-box-title"> autonomie </p>
                        </div>
                        <div>
                            <p class="section0-box-text">
                                Pour des prestations auprès de personnes âgées et de personnes en situation de handicap.
                            </p>
                        </div>
                    </div>
                    <div class="section0-box" id="section0-confort">
                        <label class="custom-checkbox">
                            <input name="confort" value="confort" class="section0-checkbox" type="checkbox">
                            <span class="checkmark"></span>
                        </label>
                        <div>
                            <img src="../data/Group-216.svg" alt="img-confort">
                        </div>
                        <div>
                            <p class="section0-box-title"> confort </p>
                        </div>
                        <div>
                            <p class="section0-box-text">
                                Pour des prestations de travaux ménagers et de bricolage.
                            </p>
                        </div>
                    </div>
                    <div class="section0-box" id="section0-famille">
                        <label class="custom-checkbox">
                            <input name="famille" value="famille" class="section0-checkbox" type="checkbox">
                            <span class="checkmark"></span>
                        </label>
                        <div>
                            <img src="../data/layer1.svg" alt="img-famille">
                        </div>
                        <div>
                            <p class="section0-box-title"> famille </p>
                        </div>
                        <div>
                            <p class="section0-box-text">
                                Pour des prestations de garde d’enfant de plus de 3 ans.
                            </p>
                        </div>
                    </div>
                </div>
                <div id="button-box">
                    <input type="submit" id="section0-button-next" name="analyse1-submit" value="Suivant">
                </div>
            </form>
        </section>
        <div id="navigation">
            <div>
                <a id="colored" href="/analysons-vos-besoins">1</a>
            </div>
            <div>
                2
            </div>
            <div>
                3
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