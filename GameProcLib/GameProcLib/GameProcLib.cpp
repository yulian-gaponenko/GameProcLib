#include <algorithm>
#include "GameProcLib.h"

//--------------------------------------
//--------------------------------------

Resource::Resource(int resourceName): resourceName(resourceName) {
}

int Resource::getResourceName() const {
	return resourceName;
}

//--------------------------------------
//--------------------------------------

GamePiece::GamePiece(int resourceName) : Resource(resourceName) {

}

//--------------------------------------
//--------------------------------------

Move::Move(int player, std::shared_ptr<GamePiece> doer) : player(player), doer(doer) {

}

int Move::getPlayer() const {
	return player;
}

int Move::getNewStateEvaluation() const {
	return newStateEvaluation;
}

void Move::setNewStateEvaluation(int evalValue) {
	this->newStateEvaluation = evalValue;
}

bool Move::operator < (const Move& other) const {
	return this->getNewStateEvaluation() > other.getNewStateEvaluation();
}

//--------------------------------------
//--------------------------------------

Cell::Cell(int index): index(index) {
}

int Cell::getIndex() const {
	return index;
}

std::shared_ptr<GamePiece> Cell::getOwner() const{
	return owner;
}
void Cell::setOwner(std::shared_ptr<GamePiece> owner) {
	this->owner = owner;
}

//--------------------------------------
//--------------------------------------

GameProcessor::GameProcessor(std::shared_ptr<FieldState> initalState, int minimaxTreeDepth)
	: currentState(initalState), minimaxTreeDepth(minimaxTreeDepth){

}


int GameProcessor::evaluateTreeNode(std::shared_ptr<FieldState> state, bool max, int a, int b, int depth) const{
	if (depth == 0) {
		return state->evaluate();
	}

	int bestRating = max ? INT_MIN : INT_MAX;

	std::vector<std::shared_ptr<Move>> possibleMoves = state->getAllPossibleMoves();
	int evalRating;
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		std::shared_ptr<FieldState> newState = state->doMove(possibleMoves[i]);
		evalRating = evaluateTreeNode(newState, !max, a, b, depth - 1);
		bestRating = max ? std::max(bestRating, evalRating) : std::min(bestRating, evalRating);
		if (max)
			a = std::max(a, evalRating);
		else 
			b = std::min(b, evalRating);
		if (b <= a)
			break;
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
		evalRating = evaluateTreeNode(newState, true, INT_MIN, INT_MAX, minimaxTreeDepth);
		possibleMoves[i]->setNewStateEvaluation(evalRating);
		currentState->undoMove(possibleMoves[i]);
	}
	std::sort(possibleMoves.begin(), possibleMoves.end());
	return possibleMoves;
}

//--------------------------------------
//--------------------------------------
