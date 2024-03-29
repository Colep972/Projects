#include "Scene.h"
#include <iostream>
#include "../Static/toolbox.h"



Scene::Scene(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]): m_renderer(renderer)
{
    for(int i=0; i<NB_IMAGE; i++)
    {
        m_textureArray[i] = textureArray[i];
    }
}

Scene::~Scene()
{
    for(unsigned int i = 0; i < m_tabSprite.size(); i++)
     delete(m_tabSprite[i]);
}

void Scene::render()
{
    SDL_SetRenderDrawColor(m_renderer,0,255,255,255);

    for(unsigned int i = 0; i < m_tabSprite.size(); i++)
        m_tabSprite[i]->render();

    toolbox::Write("data/police.ttf",18,p,245,245,220,m_renderer,"Rules",100,100,350,200);
    toolbox::Write("data/police.ttf",18,p,245,245,220,m_renderer,"Pseudo",100,100,350,300);
}



// ***** MAINMENU ***** //

MainMenu::MainMenu(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]) : Scene(renderer, textureArray), m_isAudioOn(true)
{
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[6], 0, 0, 800, 500));
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[0], 300,100,200,100, m_textureArray[1])); //Bouton Jouer
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[2], 300,400,200,100, m_textureArray[3])); //Bouton Quitter
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[4], 10, 10, 100, 100)); //Bouton AudioOn
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[5], 10, 10, 100, 100)); //Bouton AudioOff
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[17], 300,200,200,100, m_textureArray[18]));
    m_tabSprite.push_back(new Sprite(m_renderer, m_textureArray[17], 300,300,200,100, m_textureArray[18]));

    m_tabSprite[4]->setVisible(false);
}

void MainMenu::update(Input* input)
{
    // "Play" Button is pressed
    if(m_tabSprite[1]->estTouche(input->getX(), input->getY(),input->getRoundDOWN(),input->getRoundUP()))
    {
        input->setSelectedScene(2);
    }

    // "Quit" Button is pressed
    else if(m_tabSprite[2]->estTouche(input->getX(), input->getY(),input->getRoundDOWN(),input->getRoundUP()))
    {
        input->SetTerminer(true);
    }

    // Toggle Music Button
    if(m_tabSprite[3]->estTouche(input->getX(), input->getY(), input->getRoundDOWN(), input->getRoundUP()))
    {
        if(m_isAudioOn)
        {
            Mix_VolumeMusic(0);
            Mix_Volume(-1,0);
            m_tabSprite[3]->setVisible(false);
            m_tabSprite[4]->setVisible();
            m_isAudioOn = false;
        }else{
            Mix_VolumeMusic(30);
            m_tabSprite[3]->setVisible();
            m_tabSprite[4]->setVisible(false);
            m_isAudioOn = true;

        }
    }
    if (m_tabSprite[5]->estTouche(input->getX(), input->getY(), input->getRoundDOWN(), input->getRoundUP()))
    {
        input->setSelectedScene(4);
    }
}
// ***** PAUSEMENU ***** //

PauseMenu::PauseMenu(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]) : Scene(renderer, textureArray)
{

}

void PauseMenu::update(Input* input)
{
    // If Pause Button is Pressed
    if (input->getTouche(SDL_SCANCODE_P))
    {
        input->setSelectedScene(2);
    }

}

void PauseMenu::render()
{
    SDL_SetRenderDrawColor(m_renderer,0,0,0,255);
}


// ***** RULEMENU ***** //
RuleMenu::RuleMenu(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]) : Scene(renderer,textureArray)
{

}

void RuleMenu::update(Input* input)
{
    if (input->getTouche(SDL_SCANCODE_BACKSPACE))
    {
        input->setSelectedScene(0);
    }
}

void RuleMenu::render()
{
    SDL_SetRenderDrawColor(m_renderer,245,245,220,255);
    std::string rules;
    rules = toolbox::PullAll("data/rules.txt");
    toolbox::Write("data/police.ttf",18,p,0,0,0,m_renderer,rules,800,500,0,0);
}

