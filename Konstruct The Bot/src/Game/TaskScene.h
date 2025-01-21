#ifndef TASKSCENE_H
#define TASKSCENE_H

#include "../SDL/Scene.h"

class TaskScene : public Scene
{
    public:
        TaskScene(SDL_Renderer* renderer, TextureManager& m_textureMap, MusicManager);
        int update(Input* input);
        float interpolation(float a, float b, float i);
        void spriteMove(Sprite* s, float destX, float destY);
        void spriteMoveLoop(Sprite* s, float destX, float destY);
    protected:

    private:
};

#endif // TASKSCENE_H
