#ifndef PARTPROCESSOR_H
#define PARTPROCESSOR_H
#include "PartEnum.h"
#include "../SDL/Sprite.h"

class PartProcessor
{
    public:
        PartProcessor(PartEnum blueprint, std::string name, Sprite* sprite);
        ~PartProcessor();
        void processPart(PartEnum material);
        void emptyCurrentPart();
        bool canGive();
        bool canTake();
        std::string m_name;
        Sprite* getSprite();

    protected:

    private:
        PartEnum m_blueprint;
        PartEnum m_currentPart;
        Sprite* m_procSprite;

;
};

#endif // PARTPROCESSOR_H
