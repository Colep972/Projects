#include "SDL_Motor.h"


SDL_Motor::SDL_Motor() : m_window("Ouverture", 800, 500, SDL_WINDOW_SHOWN), m_input(), m_renderer() //add flag  | SDL_WINDOW_FULLSCREEN
{
    music = 0;
}

SDL_Motor::~SDL_Motor()
{
    //dtor
}



bool SDL_Motor::init()
{
    if(!m_window.initWindow())
    {
        return false;
    }

    // *** LOADING TEXTURES *** //

    // Buttons
    m_textureArray[0] = chargerTexture("data/buttonPlay.png",m_window.getRenderer());
    m_textureArray[1] = chargerTexture("data/buttonPlayAlt.png",m_window.getRenderer());
    m_textureArray[2] = chargerTexture("data/buttonQuit.png",m_window.getRenderer());
    m_textureArray[3] = chargerTexture("data/buttonQuitAlt.png",m_window.getRenderer());
    m_textureArray[4] = chargerTexture("data/audioOn.png",m_window.getRenderer());
    m_textureArray[5] = chargerTexture("data/audioOff.png",m_window.getRenderer());
    m_textureArray[6] = chargerTexture("data/bgMenu.png",m_window.getRenderer());
    m_textureArray[7] = chargerTexture("data/quest_book.png",m_window.getRenderer());
    m_textureArray[8] = chargerTexture("data/quest_book_open.png",m_window.getRenderer());

    // Fight Images
    m_textureArray[9] = chargerTexture("data/hp.png",m_window.getRenderer());
    m_textureArray[10] = chargerTexture("data/hpEmpty.png",m_window.getRenderer());
    m_textureArray[11] = chargerTexture("data/bg.png",m_window.getRenderer());
    m_textureArray[12] = chargerTexture("data/fightMenu.png",m_window.getRenderer());
    m_textureArray[13] = chargerTexture("data/fightMenu2.png",m_window.getRenderer());
    m_textureArray[14] = chargerTexture("data/fightStrike.png",m_window.getRenderer());
    m_textureArray[15] = chargerTexture("data/fightSurprise.png",m_window.getRenderer());
    m_textureArray[16] = chargerTexture("data/fightDefend.png",m_window.getRenderer());
    m_textureArray[17] = chargerTexture("data/button.png",m_window.getRenderer());
    m_textureArray[18] = chargerTexture("data/button_clk.png",m_window.getRenderer());
    m_textureArray[19] = chargerTexture("data/return.png",m_window.getRenderer());

    // Ships
    m_textureArray[23] = chargerTexture("data/player.png",m_window.getRenderer());
    m_textureArray[24] = chargerTexture("data/playerAlt.png",m_window.getRenderer());
    m_textureArray[25] = chargerTexture("data/cruiser.png",m_window.getRenderer());
    m_textureArray[26] = chargerTexture("data/cruiserAlt.png",m_window.getRenderer());
    m_textureArray[27] = chargerTexture("data/armored.png",m_window.getRenderer());
    m_textureArray[28] = chargerTexture("data/armoredAlt.png",m_window.getRenderer());
    m_textureArray[29] = chargerTexture("data/raider.png",m_window.getRenderer());
    m_textureArray[30] = chargerTexture("data/raiderAlt.png",m_window.getRenderer());

    // Tiles
    m_textureArray[35] = chargerTexture("data/Tiles/tileOutline.png",m_window.getRenderer());
    m_textureArray[36] = chargerTexture("data/Tiles/tileOutline1.png",m_window.getRenderer());
    m_textureArray[37] = chargerTexture("data/Tiles/tileOutline2.png",m_window.getRenderer());
    m_textureArray[38] = chargerTexture("data/Tiles/tile1.png",m_window.getRenderer());
    m_textureArray[39] = chargerTexture("data/Tiles/tile2.png",m_window.getRenderer());
    m_textureArray[40] = chargerTexture("data/Tiles/tile3.png",m_window.getRenderer());
    m_textureArray[41] = chargerTexture("data/Tiles/tile4.png",m_window.getRenderer());
    m_textureArray[42] = chargerTexture("data/Tiles/tileIsland1.png",m_window.getRenderer());

    //Game Over

    //m_textureArray[43] = chargerTexture("data/gameOver.png",m_window.getRenderer());

    // Keep this after any renderer modification
    m_renderer = m_window.getRenderer();

    return true;
}

void SDL_Motor::mainloop()
{
    bool rt = true;
    Scene s(m_renderer,m_textureArray);
    // Framerate variables
    unsigned int frameRate (1000 / 50);
    float debutBoucle(0), finBoucle(0), tempsEcoule(0);

    Bridge bridge = initBridge();

    // Objets Sc�ne
    MainMenu mainMenu(m_renderer, m_textureArray);
    PauseMenu pause(m_renderer, m_textureArray);
    GameTile gameT(m_renderer, m_textureArray, &bridge);
    GameFight gameF(m_renderer, m_textureArray, &bridge);
    RuleMenu Menu (m_renderer, m_textureArray);

    //d�fini le volume initial de la musique
    Mix_VolumeMusic(30);

    int lastSceneEntered = 0;

    Mix_PlayMusic(m_window.getMusic(1), -1 );



    SDL_StartTextInput();

    // Core Loop
    while(!m_input.terminer())
    {

        // Defining timestamp
        debutBoucle = SDL_GetTicks();


        // *** Managing Events ***
        m_input.updateEvenements();

        // Close the window when asked
        if(m_input.getTouche(SDL_SCANCODE_ESCAPE))
        {
            m_input.SetTerminer(true);
        }

        // *** UPDATE ***

            if (m_input.getEvement().type == SDL_TEXTINPUT)
            {
                //Append character
                if (rt)
                {
                    text += m_input.getEvement().text.text;
                    rt = false;
                }
            }
            else
            {
                rt = true;
            }
            if (m_input.getTouche(SDL_SCANCODE_RETURN))
            {
                SDL_StopTextInput();
                toolbox::Push("data/pseudo.txt",text);
            }

        switch(m_input.getSelectedScene())
        {
            case 0:
                mainMenu.update(&m_input);
                break;
            case 1:
                pause.update(&m_input);
                break;
            case 2:
                gameT.update(&m_input);
                break;
            case 3:
                gameF.update(&m_input);
                break;
            case 4 :
                Menu.update(&m_input);
                break;
            default:
                std::cout<<"ON A TOUT PETEEEEEE !!!"<<std::endl;
        }

        //update music
        lastSceneEntered=m_input.getSelectedScene();
        music = lastSceneEntered;


        // *** GRAPHICS ***

        // Clear the Canvas
        SDL_RenderClear(m_renderer);

        switch(m_input.getSelectedScene())
        {
            case 0:
                mainMenu.render();
                toolbox::Write("data/police.ttf",18,p,255,255,255,m_renderer,"Knights of The Beach",200,100,300,0);
                toolbox::Write("data/police.ttf",18,p,255,255,255,m_renderer,text,100,100,310,200);
                break;
            case 1:
                pause.render();
                break;
            case 2:
                gameT.render(&m_input);
                break;
            case 3:
                gameF.render();
                break;
            case 4 :
                Menu.render();
                break;
            default:
                std::cout<<"ON A TOUT PETEEEEEE !!!"<<std::endl;
        }

        // Print the Canvas
        SDL_RenderPresent(m_renderer);



        // *** FRAMERATE ***

        // Calcul du temps �coul�
        finBoucle = SDL_GetTicks();
        tempsEcoule = finBoucle - debutBoucle;
        //std::cout<< tempsEcoule <<std::endl;

        // Si n�cessaire, on met en pause le programme
        if(tempsEcoule < frameRate)
            SDL_Delay(frameRate - tempsEcoule);
    }
}

SDL_Texture* chargerTexture(const std::string &chemin, SDL_Renderer* renderer)
{
    SDL_Surface* surface = IMG_Load(chemin.c_str());
    if (surface == NULL)
    {
        std::cout << "Erreur de chargement de surface " <<std::endl;
        return NULL;
    }
    else
    {
        SDL_Texture* texture = SDL_CreateTextureFromSurface(renderer,surface);
        SDL_FreeSurface(surface);
        return texture;
    }
}

