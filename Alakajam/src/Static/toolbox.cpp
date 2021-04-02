#include "toolbox.h"

toolbox::toolbox()
{
    //ctor
}

toolbox::~toolbox()
{
    //dtor
}

void toolbox::Write(char* file, int charsize, TTF_Font* font, Uint8 r, Uint8 g, Uint8 b, SDL_Renderer* renderer, const std::string &text, int width, int height, int posx, int posy)
{
    font = TTF_OpenFont(file,charsize);
    if (font == NULL)
    {
        std::cout <<"Problem with the font"<<std::endl;
    }
    else
    {
        TTF_FontLineSkip(font);
        SDL_Color color = {r,g,b};
        SDL_Surface* SurfaceMessage = TTF_RenderText_Blended_Wrapped(font,text.c_str(),color,800);
        SDL_Rect rect;
        rect.h = height;
        rect.w = width;
        rect.x = posx;
        rect.y = posy;
        SDL_Texture* TextMess = SDL_CreateTextureFromSurface(renderer,SurfaceMessage);
        SDL_RenderCopy(renderer,TextMess,NULL,&rect);
        SDL_FreeSurface(SurfaceMessage);
    }
}

void toolbox::Write(char* file, int charsize, TTF_Font* font, Uint8 r, Uint8 g, Uint8 b, SDL_Renderer* renderer, const std::string &text,int object, int width, int height, int posx, int posy)
{
    font = TTF_OpenFont(file,charsize);
    if (font == NULL)
    {
        std::cout <<"Problem with the font"<<std::endl;
    }
    else
    {
        std::stringstream sstm;
        sstm << text << object;
        const std::string &newText = sstm.str();
        SDL_Color color = {r,g,b};
        SDL_Surface* SurfaceMessage = TTF_RenderText_Blended(font,newText.c_str(),color);
        SDL_Rect rect;
        rect.h = height;
        rect.w = width;
        rect.x = posx;
        rect.y = posy;
        SDL_Texture* TextMess = SDL_CreateTextureFromSurface(renderer,SurfaceMessage);
        SDL_RenderCopy(renderer,TextMess,NULL,&rect);
        SDL_FreeSurface(SurfaceMessage);
    }
}

void toolbox::Write(char* file, int charsize, TTF_Font* font, Uint8 r, Uint8 g, Uint8 b, SDL_Renderer* renderer, const std::string &text,const std::string &text2,int object, int width, int height, int posx, int posy)
{
    font = TTF_OpenFont(file,charsize);
    if (font == NULL)
    {
        std::cout <<"Problem with the font"<<std::endl;
    }
    else
    {
        std::stringstream sstm;
        sstm << text << text2 << object ;
        const std::string &newText = sstm.str();
        SDL_Color color = {r,g,b};
        SDL_Surface* SurfaceMessage = TTF_RenderText_Blended(font,newText.c_str(),color);
        SDL_Rect rect;
        rect.h = height;
        rect.w = width;
        rect.x = posx;
        rect.y = posy;
        SDL_Texture* TextMess = SDL_CreateTextureFromSurface(renderer,SurfaceMessage);
        SDL_RenderCopy(renderer,TextMess,NULL,&rect);
        SDL_FreeSurface(SurfaceMessage);
    }
}

void toolbox::Write(char* file, int charsize, TTF_Font* font, Uint8 r, Uint8 g, Uint8 b, SDL_Renderer* renderer, const std::string &text,const std::string &text2, int width, int height, int posx, int posy)
{
    font = TTF_OpenFont(file,charsize);
    if (font == NULL)
    {
        std::cout <<"Problem with the font"<<std::endl;
    }
    else
    {
        std::stringstream sstm;
        sstm << text << text2 ;
        const std::string &newText = sstm.str();
        SDL_Color color = {r,g,b};
        SDL_Surface* SurfaceMessage = TTF_RenderText_Blended(font,newText.c_str(),color);
        SDL_Rect rect;
        rect.h = height;
        rect.w = width;
        rect.x = posx;
        rect.y = posy;
        SDL_Texture* TextMess = SDL_CreateTextureFromSurface(renderer,SurfaceMessage);
        SDL_RenderCopy(renderer,TextMess,NULL,&rect);
        SDL_FreeSurface(SurfaceMessage);
    }
}

void toolbox::Push(std::string file, std::string pseudo)
{
    std::ofstream monflux (file.c_str());
    if (monflux)
    {
        monflux << pseudo;
    }
    else
    {
        std::cout << "Erreur Fichier Ecriture !";
    }
}

std::string toolbox::Pull(std::string file)
{
    std::string pseudo;
    std::ifstream monflux (file.c_str());
    if (monflux)
    {
        monflux >> pseudo;
    }
    else
    {
        std::cout << "Erreur Fichier Lecture !";
    }
    return pseudo;
}

std::string toolbox::PullAll(std::string file)
{
    std::ifstream monflux (file.c_str());
    if (monflux)
    {
        std::string line;
        std::string line2;
        while(getline(monflux,line))
        {
            line2 += line;
        }
        return line2;
    }
    else
    {
        std::cout << "Erreur Fichier Lecture !";
    }
}

std::string toolbox::pullQuest(std::string quest, std::string word)
{
    std::ifstream file(quest.c_str());
    if (file)
    {
        std::string mot;
        std::string line;
        std::string line2;
        while (file >> mot)
        {
            if (mot == word)
            {
                getline(file,line);
                getline(file,line);
                char r = '\n';
                line += r;
                line2 += line;
            }
        }
        return line2;
    }
}

