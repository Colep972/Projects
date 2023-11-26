#ifndef PARTDISTRIBUTOR_H
#define PARTDISTRIBUTOR_H
#include "PartEnum.h"
#include "Furniture.h"

class PartDistributor
{
    public:
        PartDistributor(std::string name, Sprite* sprite);
        ~PartDistributor();
        bool canGive();
        bool canTake();
        void update();
        Sprite* getSprite();
        std::string m_name;

    private:

        Sprite* m_distSprite;
};

#endif // PARTDISTRIBUTOR_H
