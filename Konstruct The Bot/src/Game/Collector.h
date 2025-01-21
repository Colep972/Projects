#ifndef COLLECTOR_H
#define COLLECTOR_H
#include "PartEnum.h"
#include "Furniture.h"

class Collector
{
    public:
        Collector(std::string name,Sprite* sprite);
        ~Collector();
        void update();
        bool canGive();
        bool canTake(ProductEnum finalProduct);
        std::string m_name;
        Sprite* getSprite();

    private:
        Sprite* m_coSprite;
};

#endif // COLLECTOR_H
