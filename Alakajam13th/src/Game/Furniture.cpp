#include "Furniture.h"

Furniture::Furniture(Sprite* sprite, PartEnum blueprint, std::string m_name): m_furnitureSprite(sprite), m_blueprint(blueprint)
{

}

Furniture::Furniture(Sprite* sprite, std::string m_name): m_furnitureSprite(sprite)
{

}

Furniture::~Furniture()
{
    //dtor
}

void Furniture::update()
{

}

Sprite* Furniture::getSprite()
{
    return m_furnitureSprite;
}

