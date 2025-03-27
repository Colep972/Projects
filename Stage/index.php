<?php
    require_once ("php/model.php");
    require_once ("php/datacontact.php");
    $bool = false;
    global $dbHost, $dbUser, $dbPwd, $dbName;
    $connexion = getConnection($dbHost, $dbUser, $dbPwd, $dbName);
    $sent = '
              <div id="sent">
                        <div id="container-cross">
                            <button id="close"><img id="cross" src="data/Cross.svg" alt="close"></button>
                        </div>
                        <img id="sent-dude" src="data/undraw_letter_re_8m03%201.svg" alt="form-sent">
                        <h1 id="sent-title"> <span class="highlight">Votre message a bien été envoyé !</span></h1>
                        <p id="sent-text"> Nous reviendrons vers vous dans un délai de 48h.</p>
              </div>';
    if(isset($_POST['Envoyer']))
        {
            date_default_timezone_set("Europe/Paris");
            $date = date("Y-m-d H:i:s");
            $entete  = 'MIME-Version: 1.0' . "\r\n";
            $entete .= 'Content-type: text/php; charset=utf-8' . "\r\n";
            $entete .= 'From: couleurs-services.com' . "\r\n";
            $message =
                '<h1>Message envoyé depuis la page Contact de couleurs-services.com</h1>
            <p><b> Nom : </b> ' . htmlspecialchars($_POST['name']) . '<br>
            <b> Téléphone : </b> '. htmlspecialchars($_POST['tel']) . '<br>
            <b> Mail : </b>' . htmlspecialchars($_POST['mail']) . '<br>
            <b> Message : </b>' . htmlspecialchars($_POST['message']) . '</p>
            <b> Date : </b>' . $date . '</p>';
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
        <title> Couleurs Services </title>
        <link rel="icon" type="image/png" href="data/logo_responsiv.png">
        <link rel="stylesheet" href="css/index.css">
        <link rel="stylesheet" href="css/main.css">
        <script defer src="js/main.js"></script>
        <script defer src="js/index.js"></script>
        <script defer src="js/form.js"></script>
        <!-- Google tag (gtag.js) --> <script async src="https://www.googletagmanager.com/gtag/js?id=G-WNH8H5SDGH"></script>
        <script> window.dataLayer = window.dataLayer || []; function gtag(){dataLayer.push(arguments);} gtag('js', new Date()); gtag('config', 'G-WNH8H5SDGH'); </script>
    </head>
    <body>
        <header id="header">
            <?php include("php/header.php"); ?>
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
        <section id="background-img">
            <section id="section0">
                <div id="section0-left">
                    <h1 id="section0-title-left">offrez-vous des services qui illuminent votre vie à domicile</h1>
                    <p id="section0-text-left">
                        Couleurs Services votre prestataire de services à domicile sur-mesure et de proximité, vous proposant une gamme de services personnalisée adaptés à vos besoins et vous accompagnant à chaque étape de votre vie.
                    </p>
                    <a class="links-button" id="section0-link-left" href="/analysons-vos-besoins"> Analysons vos besoins </a>
                </div>
                <div id="section0-right">
                    <iframe id="frame" width="560" height="315" src="https://www.youtube.com/embed/0WN2LRGKvVo?si=YJy5iGVaxLVwKksv" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
                </div>
            </section>
            <section  id="section1">
                <div id="section1-up-container">
                    <div id="section1-left-0">

                    </div>
                    <div id="section1-right-0">
                        <h2 id="section1-title-right-0"> Qui sommes-nous ? </h2>
                        <p class="section1-text-right-0"> Passionnés par l'aide à domicile, nous accompagnons l’humain dans sa vie à domicile. Agréés par la métropole de Lyon, nous sommes un prestataire de services à domicile reconnu pour notre expertise et notre engagement.<br>
                            Notre mission : <b id="section1-bold">illuminer votre vie à domicile</b> <br> et vous offrir un maintien à domicile de qualité.
                        </p>
                            <!--<p class="section1-text-right-0">Nous sommes un prestataire de services à domicile reconnu par la métropole de Lyon.</p>
                            <br>-->
                            <a id="section1-link-button" class="links-button" href="/qui-sommes-nous"> Découvrez-nous </a>
                    </div>
                </div>
                <div id="section1-down-container">
                    <div id="section1-left-1">

                    </div>
                    <div id="section1-right-1">

                    </div>
                </div>
            </section>
            <div id="section2-up">
                <h2 id="section2-title-up"> Nos prestations de services à domicile </h2>
                <p id="section2-text-up"> Couleurs Services vous propose 3 formules de services à domicile personnalisables pour répondre à vos besoins. </p>
            </div>
            <section id="section2">
                <div id="section2-left">
                    <a class="section2-p" id="section2-left-p" href="/offre-autonomie"><h3> Autonomie </h3></a>
                    <div class="section2-relative">
                        <a  class="section2-img-container" id="section2-left-container" href="/offre-autonomie"><img id="section2-left-img" src="data/Icone_Autonomie.svg" alt="Autonomie"></a>
                    </div>
                    <div class="section2-text-container">
                        <a class="section2-text-link" href="/offre-autonomie"><p class="section2-text" id="section2-left-text"> Pour des prestations auprès de personnes âgées et de personnes en situation de handicap. </p>
                            <p class="section2-link" id="section2-left-link"> En savoir plus  ></p></a>
                    </div>
                </div>
                <div id="section2-middle">
                    <a class="section2-p" id="section2-middle-p" href="/offre-confort"><h3> Confort </h3></a>
                    <div class="section2-relative">
                        <a class="section2-img-container" id="section2-middle-container" href="/offre-confort"><img id="section2-middle-img" src="data/Icone_Confort.svg" alt="Confort"></a>
                    </div>
                    <div class="section2-text-container">
                        <a class="section2-text-link" href="/offre-confort"><p class="section2-text" id="section2-middle-text"> Pour des prestations de travaux ménagers et de bricolage </p>
                        <p class="section2-link" id="section2-middle-link"> En savoir plus  ></p></a>
                    </div>
                </div>
                <div id="section2-right">
                    <a class="section2-p" id="section2-right-p" href="/offre-famille"><h3> Famille </h3></a>
                    <div class="section2-relative">
                        <a class="section2-img-container" id="section2-right-container" href="/offre-famille"><img id="section2-right-img" src="data/Icone_Famille.svg" alt="Famille"></a>
                    </div>
                    <div class="section2-text-container">
                        <a class="section2-text-link" href="/offre-famille"><p class="section2-text" id="section2-right-text"> Pour des prestations de garde d’enfant de plus de 3 ans. </p>
                            <p class="section2-link" id="section2-right-link"> En savoir plus  ></p></a>
                    </div>
                </div>
            </section>
        </section>
        <div id="section3-up">
            <h2 id="section3-title-up"> nos offres complémentaires </h2>
            <p id="section3-text-up"> Afin de répondre à l'ensemble de vos besoins, nous collaborons étroitement avec un réseau de partenaires spécialisés. </p>
        </div>
        <section id="section3">
        <div id="section3-left">
            <div id="section3-left-container">
                <img id="section3-left-img" src="data/photo_medico-social.svg" alt="photo-medico-social">
            </div>
            <h2 class="section3-title"> médico-social </h2>
            <ul class="section3-list">
                <li>Infirmiers</li>
                <li>Aides soignants</li>
                <li>Transports adaptés</li>
            </ul>
        </div>
            <div id="section3-middle">
                <div id="section3-middle-container">
                    <img id="section3-middle-img" src="data/photo_bien-etre.svg" alt="photo-bien-être">
                </div>
                <h2 class="section3-title"> bien-être </h2>
                <ul class="section3-list">
                    <li>Sophrologues</li>
                    <li>Coiffeurs</li>
                </ul>
            </div>
            <div id="section3-right">
                <div id="section3-right-container">
                    <img id="section3-right-img" src="data/photo_petits_travaux.svg" alt="photo-travaux">
                </div>
                <h2 class="section3-title"> petits travaux </h2>
                <ul class="section3-list">
                    <li>Jardiniers</li>
                    <li>Bricolage</li>
                </ul>
            </div>
        </section>
        <h2 id="section4-title-up"> nos atouts </h2>
        <section id="section4">
            <div class="section4-atouts">
                <img id="section4-analyse-img" src="data/analyse.svg" alt="photo-analyse">
                <p class="section4-text" id="section4-analyse-text"> Une analyse du besoin au domicile </p>
            </div>
            <div class="section4-atouts">
                <img id="section4-service-img" src="data/service.svg" alt="photo-service">
                <p class="section4-text" id="section4-service-text"> Des services axés sur les 14 besoins de la personne de Virginia Henderson </p>
            </div>
            <div class="section4-atouts">
                <img id="section4-astreinte-img" src="data/astreinte.svg" alt="photo-astreinte">
                <p class="section4-text" id="section4-astreinte-text"> Une astreinte 24/7 </p>
            </div>
            <div class="section4-atouts">
                <img id="section4-prestation-img" src="data/prestation.svg" alt="photo-astreinte">
                <p class="section4-text" id="section4-prestation-text"> Des prestations de services sur-mesure </p>
            </div>
            <div class="section4-atouts">
                <img id="section4-formation-img" src="data/formation.svg" alt="photo-formation">
                <p class="section4-text" id="section4-formation-text"> Un personnel formé en continu </p>
            </div>
        </section>
        <div id="section5-bg">
            <h2 id="section5-title-up"> ils parlent de nous </h2>
            <section id="section5">
                <div id="section5-left">
                    <h4> Maurice M. </h4>
                    <img id="section5-left-img" src="data/stars.svg" alt="photo-notation">
                    <p id="section5-left-text"> Je reçois beaucoup de chaleur en commençant par Fatima Lemmouchia,
                        qui a été exceptionnelle dans tous les sens du terme.
                        Proximité, chaleur et sérénité ; c’est ce qui est important pour moi.
                    </p>
                    <div id="section5-left-container">
                        <p class="section5-small-text"> Bénéficiaire </p>
                        <p class="section5-small-text"> Autonomie </p>
                    </div>
                </div>
                    <div id="section5-right">
                        <h4> Aicha O. </h4>
                        <img id="section5-right-img" src="data/stars.svg" alt="photo-notation">
                        <p id="section5-right-text">
                            En tant qu’Aide Ménagère, j’effectue des missions telles que le ménage, les courses,
                            la cuisine ou encore la vérification de prise de médicaments.
                            Je me sens bien à Couleurs Services. J’aime les moments de formation. Ils me permettent
                            de me sentir entourée et écoutée.
                        </p>
                        <div id="section5-right-container">
                            <p class="section5-small-text"> Aide Ménagère </p>
                        </div>
                    </div>
            </section>
        </div>
        <h2 id="section6-up-title"> Contactons-nous </h2>
        <section id="section6">
            <div id="section6-left">
                <form id="contact-form" class="form" method="post">
                    <div id="section6-left-form">
                        <h2 class="section6-title" id="section6-left-title"> Nous  vous contactons </h2>
                        <label class="must" for="name"> Votre nom </label>
                        <input type="text" name="name" id="name" placeholder="Entrez votre nom" required>
                        <label class="must" for="tel"> Votre numéro de téléphone </label>
                        <input type="tel" name="tel" id="tel" minlength="10" pattern="^\+?\d+$" placeholder="Entrez votre numéro de téléphone" required>
                        <label class="must" for="mail"> Votre adresse mail </label>
                        <input type="email" name="mail" id="mail" placeholder="Entrez votre adresse mail" required>
                        <label for="message"> Votre message </label>
                        <textarea id="message" name="message" placeholder="Entrez votre message ici"></textarea>
                        <i> <sup>*</sup> champs obligatoire</i>
                        <input class="links-button" id="submit" type="submit" name="Envoyer" value="Envoyer">
                    </div>
                </form>
            </div>
            <div id="section6-right">
                <h2 class="section6-title" id="section6-right-title"> Vous nous contactez </h2>
                <div id="section6-right-container">
                    <p id="section6-right-rect0" class="section6-right-text"> Du lundi au vendredi de 9h00 à 13h00 </p>
                    <p id="section6-right-rect1" class="section6-right-text"> +33 9 82 51 59 74 </p>
                    <p id="section6-right-rect2" class="section6-right-text"> info@couleurs-services.com </p>
                    <div id="section6-right-rect3" class="section6-right-text">
                        <p> <b id="section6-bold"> CENTRE REGUS - Couleurs Services </b> </p>
                        <p> 1 Esplanade Miriam MAKEBA - CS 40297
                        <br> 69628 Villeurbanne Cedex </p>
                    </div>
                </div>
            </div>
        </section>
        <div id="section6-down">
            <img id="section6-interlocutrices" src="data/interlocutrices.svg" alt="photo-interlocutrices">
            <div id="section6-fl">
                <div id="section6-f">
                    <img id="Fatima" src="data/Profil_FatimaLemmouchia.png" alt="photo-presidente">
                    <div>
                        <p class="section6-name"> Fatima LEMMOUCHIA <p>
                        <p class="section6-job">Présidente et directrice </p>
                    </div>
                </div>
                <div id="section6-l">
                    <img id="Lucie" src="data/Profil_LucieBarrel.png" alt="photo-assistante">
                    <div>
                        <p class="section6-name"> Lucie BARREL <p>
                        <p class="section6-job">Assistante de gestion </p>
                    </div>
                </div>
            </div>
        </div>
        <div id="cloud-container">
            <img id="cloud" src="data/Nuage_soleil.svg" alt="photo-fond-nuage">
        </div>
        <footer>
            <?php include("php/footer.php"); ?>
        </footer>
        <button id="top"></button>
    </body>
</html>