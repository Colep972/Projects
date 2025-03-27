<?php
session_start()
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
                    <h2 id="section0-title"> <span class="highlight"> Étape 2 : De quelles prestations avez-vous besoin ? </span> </h2>
                </div>
                <div>
                    <p id="section0-text">
                        Vous pouvez sélectionner de autant de prestations que désirées.
                    </p>
                </div>
            </section>
            <div id="navigation-container">
                <div id="navigation">
                    <div>
                        <a href="/analysons-vos-besoins">1</a>
                    </div>
                    <div>
                        <a id="colored" href="analyse2.php">2</a>
                    </div>
                    <div>
                        3
                    </div>
                </div>
            </div>
            <div id="back">
                <div>
                    <a href="/analysons-vos-besoins"> Revenir à l'étape 1 </a>
                </div>
                <div>
                    <a href="/analysons-vos-besoins"><img src="../data/fleche_gauche.svg" alt="back"></a>
                </div>
            </div>
            <section id="section1">
                <form method="post" action="pdf.php" id="form-analyse">
                    <?php if($_SESSION['autonomie'])
                    {
                        echo
                        '
                        <details id="list-autonomie">
                            <summary> <h2 class="summary-title">Autonomie</h2> </summary>
                            <div class="list-box">
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-1" value="aide au repas" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/repas.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Aide au repas </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-2" value="aide a la toilette" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/toilette.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Aide à la toilette </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-3" value="aide au transfert" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/transfert.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Aide au transfert </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-4" value="aide aux courses" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/courses.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Aide aux courses </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-5" value="accompagnement exterieurs" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/Accompagnement.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Accompagnement extérieurs </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-6" value="entretien du cadre de vie" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/Entretien.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Entretien du cadre de vie </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-7" value="assistance administrative" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/Assistance.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Assistance administrative </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-8" value="compagnie et soutien social" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/Compagnie.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Compagnie et soutien social </p>
                                    </div>
                                </div>
                                <div>
                                    <label class="custom-checkbox">
                                        <input name="analyse2-autonomie-9" value="aide a la prise de medicaments" class="section0-checkbox" type="checkbox">
                                        <span class="checkmark"></span>
                                    </label>
                                    <div>
                                        <img src="../data/médicament.svg" alt="img-autonomie">
                                    </div>
                                    <div>
                                        <p> Aide à la prise de médicaments </p>
                                    </div>
                                </div>
                            </div>
                        </details>';

                        }
                        if($_SESSION['confort'])
                            {
                                echo'
                    <details id="list-confort">
                        <summary> <h2 class="summary-title">Confort</h2> </summary>
                        <div class="list-box">
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-1" value="entretien du cadre de vie" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/Entretien_confort.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Entretien du cadre de vie </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-2" value="repassage" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/Repassage.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Repassage </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-3" value="préparation des repas" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/préparation.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Préparation des repas </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-4" value="petits travaux de jardinage" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/jardinage.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Petits travaux de jardinage </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-5" value="aide aux courses" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/courses_confort.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Aide aux courses </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-6" value="assistance administrative" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/administrative.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Assistance administrative </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-7" value="gestion des budgets" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/budget.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Gestion des budgets </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-confort-8" value="gardiennage" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/gardiennage.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Gardiennage </p>
                                </div>
                            </div>
                        </div>
                    </details>';
                    }
                        if($_SESSION['famille'])
                            {
                                echo'
                    <details id="list-famille">
                        <summary> <h2 class="summary-title">Famille</h2> </summary>
                        <div class="list-box">
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-1" value="preparation de repas" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/repas_famille.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Préparation de repas </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-2" value="aide a la prise des 4 repas" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/aide_repas.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Aide à la prise des 4 repas </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-3" value="aide a la toilette" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/toilette_famille.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Aide à la toilette </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-4" value="animation d\'activite" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/activité.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Animation d\'activités </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-5" value="aide au sommeil" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/sommeil.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Aide au sommeil </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-6" value="garde de nuit" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/nuit.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Garde de nuit </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-7" value="accompagnement extra et peri scolaire" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/péri_scolaire.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Accompagnement extra et péri-scolaire </p>
                                </div>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-famille-8" value="entretien du cadre de vie" class="section0-checkbox" type="checkbox">
                                    <span class="checkmark"></span>
                                </label>
                                <div>
                                    <img src="../data/cadre.svg" alt="img-autonomie">
                                </div>
                                <div>
                                    <p> Entretien du cadre de vie </p>
                                </div>
                            </div>
                        </div>
                    </details>';

                    }
                        ?>
                    <details class="list-complementaires">
                        <summary> <h2 class="summary-title">Offres Complémentaires</h2> </summary>
                        <div>
                            <?php if(($_SESSION['autonomie']) || ($_SESSION['famille']))
                                {
                                    echo'
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-complementaire-1" value="medico-social" class="section0-checkbox" type="checkbox">
                                    Médico-social
                                    <span class="checkmark"> </span>
                                </label>
                            </div>';
                            }
                            ?>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-complementaire-2" value="bien etre" class="section0-checkbox" type="checkbox">
                                    Bien être
                                    <span class="checkmark"> </span>
                                </label>
                            </div>
                            <div>
                                <label class="custom-checkbox">
                                    <input name="analyse2-complementaire-3" value="petits travaux" class="section0-checkbox" type="checkbox">
                                    Petits travaux
                                    <span class="checkmark"> </span>
                                </label>
                            </div>
                        </div>
                    </details>
                    <div id="button-box">
                        <input type="submit" id="section0-button-next" name="analyse2-submit" value="Suivant">
                    </div>
                </form>
            </section>
        </section>
        <div id="navigation">
            <div>
                <a href="/analysons-vos-besoins">1</a>
            </div>
            <div>
                <a id="colored" href="analyse2.php">2</a>
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

