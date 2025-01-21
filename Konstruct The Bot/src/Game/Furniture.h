#ifndef FURNITURE_H
#define FURNITURE_H
#include "PartEnum.h"
#include "../SDL/Sprite.h"

class Furniture
{
    public:
        Furniture(Sprite* sprite, PartEnum blueprint, std::string m_name);
        Furniture(Sprite* sprite, std::string m_name);
        virtual ~Furniture();
        virtual void update();
        virtual Sprite* getSprite();
        std::string m_name;

    protected:
        Sprite* m_furnitureSprite;
        PartEnum m_blueprint;

    private:
};

#endif // FURNITURE_H
