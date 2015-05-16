#ifndef MOVE_H
#define MOVE_H

#include <memory>
#include <vector>
#include "Cell.h"
#include "GamePiece.h"

class Move {
private:
	int player;
	int newStateEvaluation;
	std::shared_ptr<GamePiece> doer;
public:
	Move(int player, std::shared_ptr<GamePiece> doer);
	int getPlayer() const;
	int getNewStateEvaluation() const;
	void setNewStateEvaluation(int evalValue);

	inline bool operator < (const Move& other) const;
};

#endif