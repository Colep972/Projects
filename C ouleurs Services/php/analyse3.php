<?php
    session_start();
    $_SESSION['tmp'] = false;
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
    <section id="super">
        <section id="section0-2">
            <div>
                <h2 id="section0-title"> <span class="highlight"> Étape 3 : À qui sont destinées ces prestations ? </span> </h2>
            </div>
            <div>
                <p id="section0-text">
                    Vous pouvez sélectionner autant de profils que nécessaire.
                </p>
            </div>
        </section>
        <div id="navigation-container">
            <div id="navigation">
                <div>
                    <a href="/analysons-vos-besoins">1</a>
                </div>
                <div>
                    <a href="analyse2.php">2</a>
                </div>
                <div>
                    <a id="colored" href="analyse3.php">3</a>
                </div>
            </div>
        </div>
        <div id="back">
            <div>
                <a href="analyse2.php"> Revenir à l'étape 2 </a>
            </div>
            <div>
                <a href="analyse2.php"><img src="../data/fleche_gauche.svg" alt="back"></a>
            </div>
        </div>
        <section id="section1">
            <form id="form-analyse" method="post" action="pdf.php">
                <?php if($_SESSION['autonomie'])
                    {
                        echo'
                <details class="list-complementaires">
                    <summary> <h2 class="summary-title">Autonomie</h2> </summary>
                    <div>
                        <div>
                            <label class="custom-checkbox">
                                <input name="analyse3-autonomie-1" value="Pour moi meme" class="section0-checkbox" type="checkbox">
                                Pour moi-même
                                <span class="checkmark"> </span>
                            </label>
                        </div>
                        <div>
                            <label class="custom-checkbox">
                                <input name="analyse3-autonomie-2" value="Pour une personne âgée de + de 65 ans que je connais" class="section0-checkbox" type="checkbox">
                                Pour une personne âgée de + de 65 ans que je connais
                                <span class="checkmark"> </span>
                            </label>
                        </div>
                        <div>
                            <label class="custom-checkbox">
                                <input name="analyse3-autonomie-3" value="Pour une personne dépendante que je connais" class="section0-checkbox" type="checkbox">
                                Pour une personne dépendante que je connais
                                <span class="checkmark"> </span>
                            </label>
                        </div>
                    </div>
                </details>';
                        }
                        if($_SESSION['confort'])
                            {
                                echo'
                <details class="list-complementaires">
                    <summary> <h2 class="summary-title">Confort</h2> </summary>
                    <div>
                        <div>
                            <label class="custom-checkbox">
                                <input name="analyse3-confort-1" value="Pour moi-même" class="section0-checkbox" type="checkbox">
                                Pour moi-même
                                <span class="checkmark"> </span>
                            </label>
                        </div>
                        <div>
                            <label class="custom-checkbox">
                                <input name="analyse3-confort-2" value="Pour quelqu\'un que je connais" class="section0-checkbox" type="checkbox">
                                Pour quelqu\'un que je connais
                                <span class="checkmark"> </span>
                            </label>
                        </div>
                    </div>
                </details>';
                                }
                        if($_SESSION['famille'])
                            {
                                echo '
                <details class="list-complementaires">
                    <summary> <h2 class="summary-title">Famille</h2> </summary>
                    <div>
                        <div>
                            <label class="custom-checkbox">
                                <input name="analyse3-famille-1" value="Pour un enfant de + de 3 ans dont je suis tuteur/tutrice" class="section0-checkbox" type="checkbox">
                                Pour un enfant de + de 3 ans dont je suis tuteur/tutrice
                                <span class="checkmark"> </span>
                            </label>
                        </div>
                    </div>
                </details>';
                                }
                        ?>
                <div id="button-box">
                    <input type="submit" id="section0-button-next" name="analyse3-submit" value="Terminer">
                </div>
            </form>
        </section>
    </section>
    <div id="navigation">
        <div>
            <a href="/analysons-vos-besoins">1</a>
        </div>
        <div>
            <a href="analyse2.php">2</a>
        </div>
        <div>
            <a id="colored" href="analyse3.php">3</a>
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
