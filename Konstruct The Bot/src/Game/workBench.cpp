#include "WorkBench.h"

WorkBench::WorkBench(std::string name, Sprite* sprite): m_name(name), m_workSprite(sprite)
{
    m_part1 = EMPTY;
    m_part2 = EMPTY;
    m_part3 = EMPTY;
}

WorkBench::~WorkBench()
{
    //dtor
}

void WorkBench::Take(PartEnum material)
{
    if (m_part1 == EMPTY)
    {
        m_part1 = material;
    }
    else if (m_part2 == EMPTY)
    {
        m_part2 = material;
    }
    else if (m_part3 == EMPTY)
    {
        m_part3 = material;
    }

}


ProductEnum WorkBench::craftProduct()
{
    if(m_part1 != MOTHERBOARD || m_part2 != MOTHERBOARD || m_part3 != MOTHERBOARD)
    {
        if(m_part1 != m_part2 && m_part2 != m_part3 && m_part3 != m_part1)
        {
            std::cout << "Toutes les parties pour l'ENGINE sont là !" << std::endl;
            return ENGINE;
        }
        else
        {
            std::cout << "La recette n'est pas valide ! Echec de l'assemblage" << std::endl;
            m_part1 = EMPTY;
            m_part2 = EMPTY;
            m_part3 = EMPTY;
            return NOPROD;
        }
    }

    if(m_part1 != ROTOR || m_part2 != ROTOR || m_part3 != ROTOR)
    {
        if(m_part1 != m_part2 && m_part2 != m_part3 && m_part3 != m_part1)
        {
            std::cout << "Toutes les parties pour le COMPUTER sont là !" << std::endl;
            return COMPUTER;
        }
        else
        {
            std::cout << "La recette n'est pas valide ! Echec de l'assemblage" << std::endl;
            m_part1 = EMPTY;
            m_part2 = EMPTY;
            m_part3 = EMPTY;
            return NOPROD;
        }
    }
    std::cout << "La recette n'est pas valide ! Echec de l'assemblage" << std::endl;
    return NOPROD;
}

Sprite* WorkBench::getSprite()
{
    return m_workSprite;
}
