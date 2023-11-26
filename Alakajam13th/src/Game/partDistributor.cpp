#include "PartDistributor.h"

PartDistributor::PartDistributor(std::string name, Sprite* sprite): m_name(name), m_distSprite(sprite)
{

}

PartDistributor::~PartDistributor()
{

}

void PartDistributor::update()
{

}

bool PartDistributor::canGive()
{
    return true;
}

bool PartDistributor::canTake()
{
    return false;
}

Sprite* PartDistributor::getSprite()
{
    return m_distSprite;
}
