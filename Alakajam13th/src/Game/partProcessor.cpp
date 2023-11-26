#include "partProcessor.h"

PartProcessor::PartProcessor(PartEnum blueprint, std::string name, Sprite* sprite): m_procSprite(sprite), m_name(name)
{
    m_blueprint = blueprint;
}

void PartProcessor::processPart(PartEnum material)
{
    std::cout <<"CLING CLONG ... "<< material <<"is transformed into a "<< m_blueprint <<" !"<< std::endl;
    m_currentPart = m_blueprint;
}

void PartProcessor::emptyCurrentPart()
{
    m_currentPart = EMPTY;
}

bool PartProcessor::canGive()
{
    return(m_currentPart != EMPTY);
}

bool PartProcessor::canTake()
{
    return(m_currentPart == EMPTY);
}

Sprite* PartProcessor::getSprite()
{
    return m_procSprite;
}
