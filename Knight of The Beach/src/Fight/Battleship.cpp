#include "Battleship.h"
#include "stdlib.h"
#include "time.h"
#include "math.h"

Battleship::Battleship()
{
    //cstr
}
Battleship::Battleship(TYPE_BOAT b, bool player)
{
    srand(time(NULL));
    if (player)
    {
        switch(b)
        {
            case 3:
                m_armor = rand()%6+10;
                m_life = rand()%11+50;
                m_damage = rand()%5+6;
                m_stamina = 15;
                m_LIFEMAX = m_life;
                m_exp = 0;
                m_level = 1;
                break;
                std::cout << "Bateau cree : " << std::endl;
                std::cout << "Armure : " << m_armor << std::endl;
                std::cout << "Dommage : " << m_damage << std::endl;
                std::cout << "Vie : " << m_life << std::endl;
        }
    }
    else
    {
        switch(b)
        {
            case 0:
                m_level = (rand()%8+1);
                m_armor = (rand()%3+10)*m_level;
                m_life = (rand()%3+7)*m_level;
                m_damage = (rand()%3+2)*m_level;
                break;
            case 1:
                m_level = (rand()%8+1);
                m_armor = (rand()%3+10)*m_level;
                m_life = (rand()%3+7)*m_level;
                m_damage = (rand()%3+2)*m_level;
                break;
            case 2:
                m_level = (rand()%8+1);
                m_armor = (rand()%3+10)*m_level;
                m_life = (rand()%3+7)*m_level;
                m_damage = (rand()%3+2)*m_level;
                break;
        }
    }
    m_compteur = 0;
    m_Normalize = false;
    m_nbLevel = 10;
    m_palier[m_nbLevel];
    m_palier[0] = 150;
    for (int i = 1 ; i < m_nbLevel ; ++i)
    {
        m_palier[i] = round(m_palier[i-1]*((1+(double)i/10)));
    }
    m_armor_cmpt = 0.5;
}

// Getter

int Battleship::getArmor()
{
    return m_armor;
}

int Battleship::getDamage()
{
    return m_damage;
}

int Battleship::getStamina()
{
    return m_stamina;
}

double Battleship::getLife()
{
    return m_life;
}

// Setter

void Battleship::SetArmor(int a)
{
    m_armor = a;
}

void Battleship::SetLife(int l)
{
    m_life = l;
}

void Battleship::SetDamage(int d)
{
    m_life = d;
}

// Player

void Battleship::level_Up()
{
    for (int i = 0; i < m_nbLevel; ++i)
    {
        if (m_level == i)
        {
            if (m_exp > m_palier[i])
            {
                m_level++;
            }
        }
    }
}

void Battleship::Heal(int p)
{
    int Case = healthAnalyze(m_life,m_LIFEMAX);
    if (p == 1)
    {
        switch(Case)
        {
            case 1 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.05);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 2 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.1);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 3 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.15);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 4 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.2);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 5 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.25);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 6 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.3);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 7 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.35);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 8 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.4);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 9 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.45);
                std::cout << "A : " <<m_life<<std::endl;
                break;
        }
    }
    else
    {
        switch(Case)
        {
            case 1 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.05);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 2 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.1);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 3 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.15);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 4 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.2);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 5 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.25);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 6 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.3);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 7 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.35);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 8 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.4);
                std::cout << "A : " <<m_life<<std::endl;
                break;
            case 9 :
                std::cout << "B : " <<m_life<<std::endl;
                m_life += round(m_LIFEMAX*.45);
                std::cout << "A : " <<m_life<<std::endl;
                break;
        }
    }
}

int Battleship::healthAnalyze (int life, double Max)
{
    if (Max - life < Max*.1)
    {
        return 1;
    }
    else if (Max - life < Max*.2)
    {
        return 2;
    }
    else if (Max - life < Max*.3)
    {
        return 3;
    }
    else if (Max - life < Max*.4)
    {
        return 4;
    }
    else if (Max - life < Max*.5)
    {
        return 5;
    }
    else if (Max - life < Max*.6)
    {
        return 6;
    }
    else if (Max - life < Max*.7)
    {
        return 7;
    }
    else if (Max - life < Max*.8)
    {
        return 8;
    }
    else if (Max - life < Max*.9)
    {
        return 9;
    }
    return -1;
}

void Battleship::stamina()
{
    m_stamina += 1;
}

void Battleship::Strike(Battleship& a)
{
    double tmp2;
    if (a.m_armor <= 50)
    {
        tmp2 = 1-(a.m_armor / 100);
        std::cout << "Cas 1 : " << " Armure " << a.m_armor << " tmp " << tmp2 << " dommage " << m_damage << std::endl;
    }
    else
    {
        tmp2 = a.m_armor / 100;
        std::cout << "Cas 2 : " << " Armure " << a.m_armor << " tmp " << tmp2 << " dommage " << m_damage << std::endl;
    }
    a.m_life -= round(m_damage*tmp2);
}

bool Battleship::Vivant()
{
    if (m_life <= 0)
    {
        m_life = 0;
        return false;
    }
    return true;
}

void Battleship::DamageBoost(int bo)
{
    m_damage+=bo;
}

void Battleship::Defend()
{
    m_armor = round(m_armor*(1 + m_armor_cmpt));
    m_armor_cmpt = m_armor_cmpt/2;
    std::cout << "Armure = " <<m_armor << std::endl;
}

void Battleship::Surprise()
{
    srand(time(NULL));
    int action = rand()%2;
    if(action == 1)
    {
        int chaos = rand()%2;
        if (chaos == 1)
        {
            switch(m_damage)
            {
                case 0:
                        DamageBoost(2);
                        std::cout << "Damage = " << m_damage << std::endl;
                case 1:
                        break;
                case 2:
                        DamageBoost(-1);
                        std::cout <<"Damage -1"<<std::endl;
                        std::cout << "Damage = " << m_damage << std::endl;
                        break;
                default:
                        DamageBoost(-2);
                        std::cout <<"Damage -2"<<std::endl;
                        std::cout << "Damage = " << m_damage << std::endl;
                        break;
            }
        }
        else
        {
            DamageBoost(2);
            std::cout << "Damage +2" <<std::endl;
            std::cout << "Damage = " << m_damage << std::endl;
        }

    }
    else
    {
        int chaos = rand()%2;
        if(chaos == 1)
        {
            Heal(chaos);
        }
        else
        {
            Heal(chaos);
        }
    }
}

void Battleship::LowerStats(bool normalize)
{
    if (normalize)
    {
        std::cout << "Before LS : Armor = " << m_armor << "Life = " << m_life << "Damage = " << m_damage << std::endl;
        m_armor = round(m_armor*1.15);
        m_life = round(m_life*1.15);
        m_damage = round(m_damage*1.15);
        std::cout << "After LS : Armor = " << m_armor << "Life = " << m_life << "Damage = " << m_damage << std::endl;
    }
    else
    {
        std::cout << "Before LS : Armor = " << m_armor << "Life = " << m_life << "Damage = " << m_damage << std::endl;
        m_armor = round(m_armor*0.85);
        m_life = round(m_life*0.85);
        m_damage = round(m_damage*0.85);
        std::cout << "After LS : Armor = " << m_armor << "Life = " << m_life << "Damage = " << m_damage << std::endl;
    }

}

//Ennemy

void Battleship::Canon(Battleship &a)
{
    Strike(a);
}

void Battleship::Canon_Vache(Battleship &a)
{

}

void Battleship::Abordage(Battleship &a)
{
    //a.LowerStats();
}

std::string Battleship::EnnemyActions(Battleship &a)
{
    srand(time(NULL));
    //const std::string &attack = "";
    int alea = rand()%99+1;
    if ((alea >= 1) && (alea<= 55))
    {
        Canon(a);
        const std::string &attack1 = "Canon";
        return attack1;
    }
    else if ((alea >= 56) && (alea <= 90))
    {
        Canon_Vache(a);
        const std::string &attack2 = "Canon a vache";
        return attack2;
    }
    else
    {
        Abordage(a);
        const std::string &attack3 = "Abordage";
        return attack3;
    }
}


void Battleship::Cost(int state)
{
    if (m_stamina > 2)
    {
        switch (state)
            {
                case 0:
                    m_compteur ++;
                    m_stamina -= 3;
                    break;
                case 1:
                    m_compteur ++;
                    m_stamina -= 2;
                    break;
                case 2:
                    m_compteur ++;
                    m_stamina += 1;
                    break;
            }
    }
    else
    {
        switch (state)
            {
                case 0:
                    m_compteur ++;
                    break;
                case 1:
                    m_compteur ++;
                    break;
                case 2:
                    m_compteur ++;
                    break;
            }
    }
    if(!m_Normalize && m_stamina < 2)
    {
        tmp = m_compteur;
        LowerStats(false);
        std::cout << "Not Normalize" << std::endl;
        m_Normalize = true;
    }
    if(m_Normalize)
    {
        std::cout << "tmp = " << tmp << std::endl;
        std::cout << "compteur :" << m_compteur << std::endl;
        if(m_compteur == tmp+3)
        {
            std::cout << "NORMALIZE" << std::endl;
            LowerStats(true);
            m_Normalize = false;
        }
    }
}



