#ifndef CELL_H
#define CELL_H

#include "GamePiece.h"

class Cell {
private:
	int index;
	GamePiece* owner;
public:
	int getIndex();
};

#endif