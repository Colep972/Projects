#include "Player.h"
#include <cassert>
#include "math.h"


#define PI 3.14159265
#define SPEED 10

Player::Player(Vector2D pos): m_pos(pos), m_posDest(pos), m_handsFull(false), m_sprite(nullptr), m_ratio(1, 1), m_finalProduct(false), m_produit(NOPROD), m_rotation(0)
{

}

Player::~Player()
{
    //dtor
}


void Player::setSprite(Sprite* sprite)
{
    m_sprite = sprite;
    sprite->setPosition(m_pos.x, m_pos.y);
}

void Player::playerMove()
{
    Vector2D pos = Vector2D(m_sprite->getSDL_Rect()->x, m_sprite->getSDL_Rect()->y)/m_ratio;
    Vector2D offset = Vector2D(m_sprite->getSDL_Rect()->w, m_sprite->getSDL_Rect()->h)/(m_ratio*2);

    //add offset to correct trajectory
    pos = pos + offset;

    Vector2D direction = Vector2D(m_posDest.x-pos.x, m_posDest.y-pos.y) ;
    Vector2D normal = Vector2D::Normal(direction);

    float mag = Vector2D::Magnitude(direction);
    Vector2D nextPos(0,0);

    // Incrémente la position de SPEED par frame
    if(mag>SPEED)
    {
        nextPos = pos + normal*SPEED - offset;

        //calculer l'angle
        m_rotation = (atan(direction.y/direction.x)* 180 / PI) - 90;
        if(direction.x < 0)
        {
            m_rotation = 180+m_rotation;
        }

    }
    else
    {
        nextPos = m_posDest - offset;
    }

    m_sprite->setPosition(nextPos.x, nextPos.y);
}

Vector2D Player::straitforward()
{
    Vector2D pos = Vector2D(m_sprite->getSDL_Rect()->x, m_sprite->getSDL_Rect()->y)/m_ratio;
    Vector2D offset = Vector2D(m_sprite->getSDL_Rect()->w, m_sprite->getSDL_Rect()->h)/(m_ratio*2);

    //add offset to correct trajectory
    pos = pos + offset;

    Vector2D direction = Vector2D(m_posDest.x-pos.x, m_posDest.y-pos.y) ;
    Vector2D normal = Vector2D::Normal(direction);

    float mag = Vector2D::Magnitude(direction);
    //Vector2D nextPos(0,0);

    // Incrémente la position de SPEED par frame
    if(mag>SPEED)
    {
        return pos + normal*SPEED - offset;
    }
    else
    {
        return m_posDest - offset;
    }
}

float interpolation(float a, float b, float coef)
{
    assert(coef >= 0 && coef <= 1);
    return coef*a + (1-coef)*b;
}

void Player::collide(Sprite* s)
{

}

bool Player::hasProduct()
{
    return m_handsFull;
}

void Player::setCarryStatus(bool handsFull)
{
    m_handsFull = handsFull;
}

void Player::setCarryStatusF(bool handsFull)
{
    m_finalProduct = handsFull;
}

void Player::setDestination(Vector2D vec)
{
    m_posDest = vec;
}

void Player::setRatio(Vector2D ratioScreen)
{
    m_ratio = ratioScreen;
}

PartEnum Player::withdraw(PartDistributor *pd)
{
    if (pd->canGive())
    {
        if (!hasProduct())
        {
            m_material = MATERIAL;
            setCarryStatus(true);
            return MATERIAL;
        }
    }
    return EMPTY;
}

void Player::deposit(PartProcessor *pp)
{
    if (pp->canGive())
    {
        if (hasProduct())
        {
            setCarryStatus(false);
            std::cout << "Revenir dans 5 secondes " << std::endl;
        }
    }
}

PartEnum Player::withdraw(PartProcessor *pp)
{
    if (pp->canGive())
    {
        if (!hasProduct())
        {
            setCarryStatus(true);
            pp->processPart(m_material);
            return m_material;
        }
    }
    return EMPTY;
}


void Player::deposit(WorkBench *wb)
{
        if (hasProduct())
        {
            wb->Take(m_material);
            setCarryStatus(false);
        }
}


ProductEnum Player::withdraw(WorkBench *wb)
{
        if (!hasProduct())
        {
            setCarryStatus(true);
            setCarryStatusF(m_finalProduct);
            m_produit = wb->craftProduct();
            return m_produit;
        }
        return NOPROD;
}

void Player::Finaldeposit(Collector *c)
{
    if (m_finalProduct && m_handsFull)
    {
        if(c->canTake(m_produit))
        {
            std::cout<< "Bravo !" << std::endl;
        }
    }
}
