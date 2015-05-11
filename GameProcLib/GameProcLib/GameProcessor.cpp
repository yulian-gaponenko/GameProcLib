#include "GameProcessor.h"

GameProcessor::GameProcessor(FieldState* initalState, Evaluator* evaluator, int minimaxTreeDepth)
	: currentState(initalState), evaluator(evaluator), minimaxTreeDepth(minimaxTreeDepth){

}

const Move* GameProcessor::getBestMove() {
	int bestRating = INT_MIN;
	const Move* bestMove;

	std::vector<Move*> possibleMoves = currentState->getAllPossibleMoves();
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		int currentNodeRating = evaluateTreeNode(possibleMoves[i], true, minimaxTreeDepth);
		if (bestRating < currentNodeRating) {
			bestRating = currentNodeRating;
			bestMove = possibleMoves[i];
		}
	}

	return bestMove;
}

int GameProcessor::evaluateTreeNode(const Move* move, bool max, int depth) {
	move->doMove(currentState);
}