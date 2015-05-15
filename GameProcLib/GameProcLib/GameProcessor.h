#ifndef GAME_PROCESSOR_H
#define GAME_PROCESSOR_H

#include "FieldState.h"
#include "Evaluator.h"
#include "Move.h"

class GameProcessor {
private:
	FieldState* currentState;
	Evaluator* evaluator;
	int minimaxTreeDepth;

	std::pair<int, const Move*> evaluateTreeNode(FieldState* state, bool max, int depth);
public:
	GameProcessor(FieldState* initalState, Evaluator* evaluator, int minimaxTreeDepth);
	void doMove(const Move* move);
	const Move* getBestMove();
};

#endif