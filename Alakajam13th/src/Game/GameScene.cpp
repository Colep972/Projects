#include "GameScene.h"


GameScene::GameScene(SDL_Renderer* renderer, TextureManager& m_textureMap, MusicManager musicMap) : Scene(renderer, &musicMap), m_player(Vector2D(200, 500))
{
    srand(time(NULL));
    m_musicMap.playMusic("hell_for_eternity",-1);
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Plaque_metal"],0,0,1920,1080));

    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Workbench"],0,880,400,200));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Part_distributor1"],0,0,200,200));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Station1"],800,450,150,200));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Station1"],1000,450,150,200));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Station1"],1200,450,150,200));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Station1"],1400,450,150,200));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Product_drop"],1820,830,100,250));

    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Player"],200,500,130,130)); //should be last?
    m_playerIndice = 8;

    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Material"],0,0,50,50));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Screw"],1400,450,50,50));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Rotor"],1200,450,50,50));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Motherboard"],1000,450,50,50));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Cogwheel"],800,450,50,50));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Engine"],0,880,50,50));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Computer"],100,880,50,50));


    m_player.setSprite(m_tabSprite[m_playerIndice]);

    m_wb = new WorkBench("Workbench",m_tabSprite[1]);
    m_pd = new PartDistributor("Part_distributor",m_tabSprite[2]);
    m_pp[0] = new PartProcessor(COGWHEEL,"Station 1",m_tabSprite[6]);
    m_pp[1] = new PartProcessor(MOTHERBOARD,"Station 1",m_tabSprite[5]);
    m_pp[2] = new PartProcessor(ROTOR,"Station 1",m_tabSprite[4]);
    m_pp[3] = new PartProcessor(SCREWS,"Station 1",m_tabSprite[3]);
    m_c = new Collector("Product_drop",m_tabSprite[7]);

    m_tabEO.push_back(new EnumObject(m_tabSprite[9]));
    m_tabEO.push_back(new EnumObject(m_tabSprite[10]));
    m_tabEO.push_back(new EnumObject(m_tabSprite[11]));
    m_tabEO.push_back(new EnumObject(m_tabSprite[12]));
    m_tabEO.push_back(new EnumObject(m_tabSprite[13]));

    for (int i = 9; i< 16; i++)
    {
        m_tabSprite[i]->setVisible(false);
    }


    tmp[0] = 10;
    tmp[1] = 11;
    tmp[2] = 12;
    tmp[3] = 13;

    interdit[0] = 0;
    interdit[1] = 1;
    interdit[2] = 2;
    interdit[3] = 3;

    finalrand = 0;

    cmpt = 0;

    //m_tabFurniture.push_back(new Furniture(m_tabSprite[6],"Product_drop"));
}

int GameScene::update(Input* input)
{
    if(input->getPressedKeys() == SDL_SCANCODE_P)
    {
        return 1;
    }


    m_player.setRatio(Vector2D(m_ratioX, m_ratioY));


    // donne l'ordre de déplacement
    if(input->getMouseButton(SDL_BUTTON_LEFT))
    {
        m_lastOrder = Vector2D(input->getX()/m_ratioX, input->getY()/m_ratioY);
        m_player.setDestination(m_lastOrder);
    }
    isProcessor(input);
    isWorkBench(input);
    isDistributor(input);
    isCollector(input);

    m_player.playerMove();
    m_player.collide(m_tabSprite[2]);
    return 2; // this is the window
}


void GameScene::render()
{
    SDL_SetRenderDrawColor(m_renderer,0,255,255,255);
    for(unsigned int i = 0; i < m_tabSprite.size(); i++)
    {
        if(i == m_playerIndice)
        {
            m_tabSprite[m_playerIndice]->render(m_player.m_rotation);
        }else
        {
            m_tabSprite[i]->render();
        }

    }
}



PartDistributor* GameScene::isDistributor(Input* input)
{
    if (m_pd->getSprite()->isClicked(input))
    {
        std::cout << m_pd << " : " << m_pd->m_name << std::endl;
        m_tabSprite[9]->setVisible(true);
        return m_pd;
    }
    return nullptr;
}

int GameScene::forbbiden(int truc[], int truc1, int taille)
{
    for (int i = 0; i < taille; i++)
    {
        if (truc[i] != truc1)
        {
            return truc[i];
        }
        else if (truc[i] != truc1--)
        {
            return truc1--;
        }
        else
        {
            return 12;
        }

    }
}

PartProcessor* GameScene::isProcessor(Input* input)
{
    for (int i = 0; i < 4; i++)
    {
        if (i%2 ==0 )
        {
            finalrand = 14;
        }
        else
        {
            finalrand = 15;
        }

        if (m_pp[i]->getSprite()->isClicked(input))
        {
            m_tabSprite[i+10]->setVisible(true);
            rand = i+10;
            m_tabSprite[9]->setVisible(false);
            std::cout << m_pp[i] << " : " << m_pp[i]->m_name << std::endl;
            return m_pp[i];
        }
    }
    return nullptr;
}

WorkBench* GameScene::isWorkBench(Input* input)
{
    if (m_wb->getSprite()->isClicked(input))
    {
        m_tabSprite[rand]->setVisible(false);

        m_tabSprite[finalrand]->setVisible(true);
        std::cout << m_wb << " : " << m_wb->m_name << std::endl;
        return m_wb;
    }
    return nullptr;
}

Collector* GameScene::isCollector(Input* input)
{
    if (m_c->getSprite()->isClicked(input))
    {

        m_tabSprite[finalrand]->setVisible(false);
        std::cout << m_c << " : " << m_c->m_name << std::endl;
        return m_c;
    }
    return nullptr;
}

