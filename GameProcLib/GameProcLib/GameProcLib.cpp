#include <algorithm>
#include "GameProcLib.h"

//--------------------------------------
//--------------------------------------

Resource::Resource(int resourceName, int player): resourceName(resourceName), player(player) {

}

int Resource::getPlayer() const {
	return player;
}

int Resource::getResourceName() const {
	return resourceName;
}

//--------------------------------------
//--------------------------------------

GamePiece::GamePiece(int resourceName, int player) : Resource(resourceName, player) {

}

//--------------------------------------
//--------------------------------------

Move::Move(int player, GamePiece::ptr doer) : player(player), doer(doer) {
	newStateEvaluation = 0;
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

int Cell::getIndex() const {
	return index;
}

void Cell::setIndex(int index) {
	this->index = index;
}

GamePiece::ptr Cell::getOwner() const{
	return owner;
}
void Cell::setOwner(GamePiece::ptr owner) {
	this->owner = owner;
}

//--------------------------------------
//--------------------------------------

GameProcessor::GameProcessor(FieldState::ptr initalState, int minimaxTreeDepth)
	: currentState(initalState), minimaxTreeDepth(minimaxTreeDepth){

}


int GameProcessor::evaluateTreeNode(FieldState::ptr state, bool max, int a, int b, int depth) const{
	if (depth == 0 || state->isGameEnd()) {
		return state->evaluate();
	}

	int bestRating = max ? INT_MIN : INT_MAX;

	std::vector<Move::ptr> possibleMoves = state->getAllPossibleMoves();
	int evalRating;
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		FieldState::ptr newState = state->doMove(possibleMoves[i]);
		evalRating = evaluateTreeNode(newState, !max, a, b, depth - 1);
		newState->undoMove(possibleMoves[i]);
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

void GameProcessor::doMove(Move::ptr move) {
	currentState = currentState->doMove(move);
}

std::vector<Move::ptr> GameProcessor::evaluatePossibleMoves() const {
	currentState->notifyEvaluationStarted();

	std::vector<Move::ptr> possibleMoves = currentState->getAllPossibleMoves();
	int evalRating;
	FieldState::ptr newState;
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		newState = currentState->doMove(possibleMoves[i]);
		evalRating = evaluateTreeNode(newState, false, INT_MIN, INT_MAX, minimaxTreeDepth);
		possibleMoves[i]->setNewStateEvaluation(evalRating);
		currentState->undoMove(possibleMoves[i]);
	}
	
	currentState->notifyEvaluationEnded();

	std::sort(possibleMoves.begin(), possibleMoves.end());
	return possibleMoves;
}

bool operator < (Move::ptr m1, Move::ptr m2) {
	return m1->getNewStateEvaluation() > m2->getNewStateEvaluation();
}

//--------------------------------------
//--------------------------------------
