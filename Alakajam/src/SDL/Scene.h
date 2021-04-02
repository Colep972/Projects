#ifndef SCENE_H
#define SCENE_H

#define MAX_SPRITE 100

#include <SDL.h>
#include <SDL_mixer.h>
#include <SDL_ttf.h>

#include <vector>

#include "Sprite.h"
#include "Input.h"




class Scene
{
    public:
        Scene(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]);
        ~Scene();

        void render();
        SDL_Renderer* m_renderer;

    protected:

        SDL_Texture* m_textureArray[NB_IMAGE];

        std::vector<Sprite*> m_tabSprite;
        TTF_Font* p;

    private:


};

class MainMenu : public Scene
{
    public:
        MainMenu (SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]);

        void update(Input* input);
    private:
        bool m_isAudioOn;
        //GameFight Gf;
};

class PauseMenu : public Scene
{
    public:
        PauseMenu(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]);

        void update(Input* input);
        void render();
};

class RuleMenu : public Scene
{
    public:

        RuleMenu(SDL_Renderer* renderer, SDL_Texture* textureArray[NB_IMAGE]);

        void update(Input* input);
        void render();
};

#endif // SCENE_H
