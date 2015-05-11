#ifndef MOVE_H
#define MOVE_H

#include <vector>
#include "Cell.h"
#include "GamePiece.h"

class Move {
private:
	int player;
	GamePiece* doer;
public:
	int getPlayer();
	std::vector<Cell> doMove(const std::vector<Cell>& field);
};

#endif