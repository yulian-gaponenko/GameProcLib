#include <algorithm>
#include "GameProcessor.h"

GameProcessor::GameProcessor(std::shared_ptr<FieldState> initalState, int minimaxTreeDepth)
	: currentState(initalState), minimaxTreeDepth(minimaxTreeDepth){

}


int GameProcessor::evaluateTreeNode(std::shared_ptr<FieldState> state, bool max, int depth) const{
	if (depth == 0) {
		return state->evaluate();
	}

	int bestRating = max ? INT_MIN : INT_MAX;

	std::vector<std::shared_ptr<Move>> possibleMoves = state->getAllPossibleMoves();
	int evalRating;
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		std::shared_ptr<FieldState> newState = state->doMove(possibleMoves[i]);
		evalRating = evaluateTreeNode(newState, !max, depth - 1);
		bestRating = max ? std::min(bestRating, evalRating) : std::max(bestRating, evalRating);
	}

	return bestRating;
}

void GameProcessor::doMove(std::shared_ptr<const Move> move) {
	currentState = currentState->doMove(move);
}

std::vector<std::shared_ptr<Move>> GameProcessor::evaluatePossibleMoves() const{
	std::vector<std::shared_ptr<Move>> possibleMoves = currentState->getAllPossibleMoves();

	int evalRating;
	std::shared_ptr<FieldState> newState;
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		newState = currentState->doMove(possibleMoves[i]);
		evalRating = evaluateTreeNode(newState, true, minimaxTreeDepth);
		possibleMoves[i]->setNewStateEvaluation(evalRating);
		currentState->undoMove(possibleMoves[i]);
	}
	std::sort(possibleMoves.begin(), possibleMoves.end());
	return possibleMoves;
}

