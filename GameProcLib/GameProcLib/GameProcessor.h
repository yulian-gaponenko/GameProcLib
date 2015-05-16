#ifndef GAME_PROCESSOR_H
#define GAME_PROCESSOR_H

#include "FieldState.h"
#include "Move.h"

class GameProcessor {
private:
	std::shared_ptr<FieldState> currentState;
	int minimaxTreeDepth;

	int evaluateTreeNode(std::shared_ptr<FieldState> state, bool max, int depth) const;
public:
	GameProcessor(std::shared_ptr<FieldState>initalState, int minimaxTreeDepth);
	void doMove(std::shared_ptr<const Move> move);
	std::vector<std::shared_ptr<Move>> evaluatePossibleMoves() const;
};

#endif