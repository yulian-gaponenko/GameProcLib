#ifndef FIELD_STATE_H
#define FIELD_STATE_H

#include <vector>
#include <memory>
#include "Cell.h"
#include "Move.h"

class FieldState {
private: 
	std::vector<Cell> field;

public:
	FieldState();
	virtual std::vector<std::shared_ptr<Move>> getAllPossibleMoves() const = 0;
	virtual std::shared_ptr<FieldState> doMove(std::shared_ptr<const Move> move) = 0;
	virtual std::shared_ptr<FieldState> undoMove(std::shared_ptr<const Move> move) = 0;
	virtual int evaluate() = 0;
	
	//returns 0 if false, otherwise returns 1-based index of winnnig player
	virtual int isGameEnd() const = 0;
};

#endif