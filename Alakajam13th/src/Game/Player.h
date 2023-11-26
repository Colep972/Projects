#ifndef PLAYER_H
#define PLAYER_H

#include "../SDL/Sprite.h"
#include "../SDL/Scene.h"
#include "../SDL/Input.h"

#include "PartEnum.h"


#include "../Others/Vector2D.h"
#include "WorkBench.h"
#include "PartDistributor.h"
#include "PartProcessor.h"
#include "Collector.h"

class Player
{
    public:
        Player(Vector2D);
        virtual ~Player();
        void playerMove();

        bool hasProduct();
        void setCarryStatus(bool handsFull);
        void setCarryStatusF(bool handsFull);
        void setDestination(Vector2D vec);
        void setRatio(Vector2D ratioScreen);
        void setSprite(Sprite* sprite);
        void collide(Sprite* s);
        PartEnum withdraw(PartDistributor *pd);
        PartEnum withdraw(PartProcessor *pp);
        void deposit(PartProcessor *pp);
        ProductEnum withdraw(WorkBench *wb);
        void deposit(WorkBench *wb);
        void Finaldeposit (Collector *c);


        double m_rotation;
    protected:

    private:
        Vector2D m_pos;
        Vector2D m_posDest;
        Vector2D m_ratio;
        Sprite* m_sprite;

        bool m_handsFull;
        bool m_finalProduct;
        PartEnum m_material;
        ProductEnum m_produit;

        Vector2D straitforward();
};

#endif // PLAYER_H
