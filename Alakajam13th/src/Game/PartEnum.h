#ifndef PARTENUM_H
#define PARTENUM_H

enum PartEnum {EMPTY, MATERIAL, COGWHEEL, MOTHERBOARD, ROTOR, SCREWS}; //partie a envoyer au partDistributor si MATERIAL, au workbench sinon
enum ProductEnum {NOPROD, ENGINE, COMPUTER}; //produit final a envoyer au Product_drop

#endif // PARTENUM_H
