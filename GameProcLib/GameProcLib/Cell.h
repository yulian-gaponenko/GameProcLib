#ifndef CELL_H
#define CELL_H

#include <memory>
#include "GamePiece.h"

class Cell {
private:
	int index;
	std::shared_ptr<GamePiece> owner;
public:
	Cell(int index);
	inline int getIndex() const;

	std::shared_ptr<GamePiece> getOwner() const;
	void setOwner(std::shared_ptr<GamePiece> owner);
};

#endif