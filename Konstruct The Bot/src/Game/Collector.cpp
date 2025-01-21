#include "Collector.h"

Collector::Collector(std::string name,Sprite* sprite): m_name(name), m_coSprite(sprite)
{

}

Collector::~Collector()
{

}

void Collector::update()
{

}

bool Collector::canGive()
{
    return false;
}

bool Collector::canTake(ProductEnum finalProduct)
{
    return(finalProduct != NOPROD);
}

Sprite* Collector::getSprite()
{
    return m_coSprite;
}
