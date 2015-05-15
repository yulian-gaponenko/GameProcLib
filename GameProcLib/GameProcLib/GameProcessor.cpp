#include <algorithm>
#include "GameProcessor.h"
#include "GameProcessorUtils.h"

GameProcessor::GameProcessor(FieldState* initalState, Evaluator* evaluator, int minimaxTreeDepth)
	: currentState(initalState), evaluator(evaluator), minimaxTreeDepth(minimaxTreeDepth){

}


//evalRating , bestMove from the state
std::pair<int, const Move*> GameProcessor::evaluateTreeNode(FieldState* state, bool max, int depth) {
	if (depth == 0) {
		return std::pair<int, const Move*>(evaluator->evaluate(state), NULL);
	}

	Move* bestMove = NULL;
	int bestRating = max ? INT_MIN : INT_MAX;

	std::vector<Move*> possibleMoves = state->getAllPossibleMoves();
	int evalRating;
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		FieldState* newState = state->doMove(possibleMoves[i]);
		evalRating = evaluateTreeNode(newState, !max, depth - 1).first;

		if (GameProcessorUtils::compareMinimax(max, evalRating, bestRating) > 0) {
			bestRating = evalRating;
			bestMove = possibleMoves[i];
		}
	}

	return std::make_pair(bestRating, bestMove);
}

void GameProcessor::doMove(const Move* move) {
	currentState = currentState->doMove(move);
}

const Move* GameProcessor::getBestMove() {
	return evaluateTreeNode(currentState, true, minimaxTreeDepth).second;
}

