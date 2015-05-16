#include "Cell.h"

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