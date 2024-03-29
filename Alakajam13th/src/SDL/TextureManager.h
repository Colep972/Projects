#ifndef TEXTUREMANAGER_H
#define TEXTUREMANAGER_H

#define DATA_PATH "data/"

#include <SDL_image.h>

#include <string>
#include <map>

class TextureManager
{
    public:
        TextureManager(SDL_Renderer* renderer);
        ~TextureManager();

        SDL_Texture* operator[](const std::string);

    protected:

    private:
        SDL_Renderer* m_renderer;
        std::map<std::string, SDL_Texture*> m_textureMap;
        void loadTexture(const std::string &path);
};

#endif // TEXTUREMANAGER_H
