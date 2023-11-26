#include "TaskScene.h"

TaskScene::TaskScene(SDL_Renderer* renderer, TextureManager& m_textureMap, MusicManager musicMap) : Scene(renderer, &musicMap)
{
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Plaque_metal"],0,0,1920,1080));
    m_tabSprite.push_back(new Sprite(m_renderer,m_textureMap["Player"],10,10,100,100));


}

int TaskScene::update(Input* input)
{
    if(input->getPressedKeys() == SDL_SCANCODE_SPACE)
    {
        spriteMove(m_tabSprite[1],200,200);

    }else

    if(input->getPressedKeys() == SDL_SCANCODE_1)
    {
        spriteMove(m_tabSprite[1],500,200);

    }else

    if(input->getPressedKeys() == SDL_SCANCODE_2)
    {
        spriteMove(m_tabSprite[1],10,10);

    }

    if(input->getPressedKeys() == SDL_SCANCODE_3)
    {
        int a = input->getX();
        int b = input->getY();
        std::cout << "input->getX() : " <<a<< std::endl;
        std::cout << "input->getY() : " <<b<< std::endl;
        spriteMove(m_tabSprite[1],input->getX(),input->getY());
    }

    return 3;
}

float TaskScene::interpolation(float a,float b,float i)
{
    return (1-i)*a + b*i;
}

void TaskScene::spriteMove(Sprite* s, float destX, float destY)
{
    float i=0.0;
    int posX;
    int posY;
    int startX=s->getSDL_Rect()->x;
    int startY=s->getSDL_Rect()->y;

    while(startX != destX && startY != destY && i <= 1)
    {
        posX = interpolation(startX,destX,i);
        posY = interpolation(startY,destY,i);
        s->setPosition(posX,posY);
        s->render();
        i+=0.001;
        //std::cout << "X : " <<posX<< std::endl;
        //std::cout << "Y : " <<posY<< std::endl;
    }
}


