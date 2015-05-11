#ifndef FIELD_STATE_H
#define FIELD_STATE_H

#include <vector>
#include "Cell.h"
#include "Move.h"

class FieldState {
private: 
	std::vector<Cell> field;
	
	FieldState(std::vector<Cell> field);
public:
	FieldState();

	const std::vector<Cell>& getField();
	
	std::vector<Move*> getAllPossibleMoves();
	
	FieldState* doMove(const Move* move);
	
	//returns 0 if false, otherwise returns 1-based index of winnnig player
	int isGameEnd();
};

#endif