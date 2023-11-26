#ifndef GAMESCENE_H
#define GAMESCENE_H

#include "../SDL/Scene.h"

#include "../Others/Vector2D.h"

#include "Player.h"
#include "../SDL/Sprite.h"
#include "vector"
#include "EnumObject.h"
#include "time.h"


class GameScene : public Scene
{
    public:
        GameScene(SDL_Renderer* renderer, TextureManager&, MusicManager);

        void render();
        int update(Input* input);

        PartDistributor* isDistributor(Input* input);
        PartProcessor* isProcessor(Input* input);
        WorkBench* isWorkBench(Input* input);
        Collector* isCollector(Input* input);
        int forbbiden(int truc[],int truc1, int taille);


    protected:

    private:
        Vector2D m_lastOrder;

        Player m_player;
        int m_playerIndice;
        //std::vector<Furniture*> m_tabFurniture;

        PartDistributor* m_pd;
        PartProcessor* m_pp[4];
        WorkBench* m_wb;
        Collector* m_c;
        int tmp[3];
        int rand;
        int interdit[3];
        int cmpt;
        int finalrand;
        std::vector<EnumObject*> m_tabEO;

};

#endif // GAMESCENE_H
