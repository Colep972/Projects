#ifndef WORKBENCH_H
#define WORKBENCH_H
#include "PartEnum.h"
#include "Furniture.h"



class WorkBench
{
    public:
        WorkBench(std::string name,Sprite* sprite);
        virtual ~WorkBench();
        void update();
        ProductEnum craftProduct();
        void Take(PartEnum material);
        std::string m_name;
        Sprite* getSprite();



    protected:

    private:
        PartEnum m_part1;
        PartEnum m_part2;
        PartEnum m_part3;
        Sprite* m_workSprite;

};

#endif // WORKBENCH_H
