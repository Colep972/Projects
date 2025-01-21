#ifndef BATTLESHIP_H
#define BATTLESHIP_H

#include "../Tile/Boat.h"


class Battleship
{
    public:
        Battleship();
        Battleship(TYPE_BOAT b, bool player);

        // getteur

        int getArmor();
        double getLife();
        int getDamage();
        int getStamina();

        //setteur

        void SetArmor (int a);
        void SetLife (int l);
        void SetDamage (int d);

        // player

        void DamageBoost(int bo);
        void Heal(int p);
        int healthAnalyze(int life, double Max);
        void Defend();
        void Strike(Battleship& a);
        bool Vivant();
        void stamina();
        void LowerStats(bool normalize);
        void DamageBoost(Battleship b ,int bo);
        void Surprise();
        void level_Up();

        //Ennmy

        std::string EnnemyActions(Battleship& a);
        void Canon(Battleship& a);
        void Canon_Vache(Battleship& a);
        void Abordage(Battleship& a);
        void Cost(int state);

    protected:
        double m_armor;
        int m_damage;
        int m_life;
        int m_stamina;
        double m_LIFEMAX;
        bool m_Normalize;
        int m_compteur;
        int tmp;
        int m_exp;
        int m_level;
        int m_nbLevel;
        int m_palier[];
        double m_armor_cmpt;

    private:

};

#endif // BATTLESHIP_H
