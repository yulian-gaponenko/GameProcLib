#include "Move.h"

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